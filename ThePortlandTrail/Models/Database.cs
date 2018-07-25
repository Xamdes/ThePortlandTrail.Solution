
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using ThePortlandTrail;


namespace ThePortlandTrail.Models
{
  public class DB
  {
    private static MySqlConnection _conn;
    private static MySqlCommand _cmd;
    private static string _connectionString = DBConfiguration.GetConnection();
    private static List<MySqlParameter> _parameters = new List<MySqlParameter>(){};
    private static int _lastId;
    public delegate void Del(MySqlDataReader rdr, List<Object> objects);


    public static MySqlConnection GetConnection()
    {
      return _conn;
    }

    public static void OpenConnection()
    {
      _conn = new MySqlConnection(_connectionString);
      _conn.Open();
      _cmd = _conn.CreateCommand() as MySqlCommand;
    }

    public static void CloseConnection()
    {
      _conn.Close();
      if(_conn!=null)
      {
        _conn.Dispose();
      }
      _cmd = null;
    }

    public static void ResetCommand()
    {
      _cmd = _conn.CreateCommand() as MySqlCommand;
    }

    public static void AddParameter(string name, Object parameterValue)
    {
      _parameters.Add(new MySqlParameter(name, parameterValue));
    }

    public static void AddParameter(MySqlParameter para)
    {
      _parameters.Add(para);
    }

    private static void SetParameters()
    {
      foreach(MySqlParameter para in _parameters)
      {
        _cmd.Parameters.Add(para);
      }
    }

    public static void SetCommand(string commandText)
    {
      _cmd.CommandText = commandText;
    }

    public static void RunSqlCommand()
    {
      SetParameters();
      _cmd.ExecuteNonQuery();
      _lastId = (int)_cmd.LastInsertedId;
    }

    public static MySqlDataReader ReadSqlCommand()
    {
      return (_cmd.ExecuteReader() as MySqlDataReader);
    }

    public static void ReadTable(string command, Del callback, List<Object> objects)
    {
      OpenConnection();
      SetCommand(command);
      SetParameters();
      MySqlDataReader rdr = ReadSqlCommand();
      while(rdr.Read())
      {
        callback(rdr,objects);
      }
      CloseConnection();
    }

    public static void Run(string command)
    {
      OpenConnection();
      SetCommand(command);
      RunSqlCommand();
      CloseConnection();
    }

    public static void Edit(string tableName,int id, string what,  Object editValue)
    {
      OpenConnection();
      SetCommand(@"UPDATE "+tableName+" SET "+what+" = @updateValue WHERE id = @searchId;");
      AddParameter("@searchId",id);
      AddParameter("@updateValue",editValue);
      RunSqlCommand();
      CloseConnection();
    }

    public static void ClearTable(string tableName, bool saveUniqueIds = true)
    {
      OpenConnection();
      if(saveUniqueIds)
      {
        SetCommand(@"DELETE FROM "+tableName+";");
      }
      else
      {
        SetCommand(@"TRUNCATE TABLE "+tableName+";");
      }
      RunSqlCommand();
      CloseConnection();
    }

    public static void DeleteById(string tableName, int deleteId)
    {
      OpenConnection();
      SetCommand(@"DELETE FROM "+tableName+" WHERE id=@id");
      AddParameter("@id",deleteId);
      RunSqlCommand();
      CloseConnection();
    }

    public static void SaveToTable(string tableName,string columns,List<Object> values)
    {
      OpenConnection();
      List<string> tempValues = columns.Split(',').ToList();
      List<string> valueNames = new List<string>(){};
      foreach(string s in tempValues)
      {
        string tempString = "@"+s.ToUpper();
        valueNames.Add(tempString);
      }
      string valueList = string.Join(",",valueNames);
      SetCommand(@"INSERT INTO "+tableName+" ("+columns+") VALUES ("+valueList+");");
      for(int i = 0; i<values.Count();i++)
      {
        AddParameter(valueNames[i], values[i]);
      }
      RunSqlCommand();
      CloseConnection();
    }

    public static int LastInsertId()
    {
      return _lastId;
    }

    public static int LastTableId(string tableName, string sid = "id")
    {
      int id = -1;
      OpenConnection();
      SetCommand(@"SELECT Max("+sid+") FROM "+tableName+";");
      MySqlDataReader rdr = ReadSqlCommand();
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
      }
      CloseConnection();
      return id;
    }
  }
}
