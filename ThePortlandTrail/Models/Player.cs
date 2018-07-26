using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using ThePortlandTrail;

namespace ThePortlandTrail.Models
{
  public class Player
  {
    private static string _tableName = "players";
    private int _id;
    private int _food;
    private int _fix;
    private int _rest;
    private string _name;


    private static Player livePlayer = null;

    public Player(string name, int food = 100, int fix = 100, int rest = 100, int id = 0)
    {
      _name = name;
      _id = id;
      _food = food;
      _fix = fix;
      _rest = rest;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public void Save()
    {
      livePlayer = this;
      DB.OpenConnection();
      string columns = "name,food,fix,rest";
      List<Object> values = new List<Object>(){_name,_food,_fix,_rest};
      DB.SaveToTable(_tableName,columns,values);
      _id = DB.LastInsertId();
      DB.CloseConnection();
    }

    public static Player Find (int id)
    {
      List<Object> objects = new List<Object>(){};
      string command = @"SELECT * FROM players WHERE id = (@SearchId);";
      DB.AddParameter("@SearchId", id);
      DB.ReadTable(command,DelegateFind,objects);
      return (Player) objects[0];
    }

    private static void DelegateFind(MySqlDataReader rdr,List<Object> objects)
    {
      int playerId = rdr.GetInt32 (0);
      int playerFood = rdr.GetInt32 (1);
      int playerFix = rdr.GetInt32 (2);
      int playerRest = rdr.GetInt32 (3);
      string playerName = rdr.GetString (4);
      Player newPlayer = new Player (playerName, playerFood, playerFix, playerRest, playerId);
      objects.Add(newPlayer);
    }

    public static Player GetPlayer()
    {
      return livePlayer;
    }

    public static List<Player> GetAll ()
    {
      List<Object> objects = new List<Object>(){};
      string command = @"SELECT * FROM players ORDER BY name;";
      DB.ReadTable(command,DelegateGetAll,objects);
      return objects.Cast<Player>().ToList();
    }

    public static void DelegateGetAll(MySqlDataReader rdr,List<Object> objects)
    {
      int playerId = rdr.GetInt32 (0);
      int playerFood = rdr.GetInt32 (1);
      int playerFix = rdr.GetInt32 (2);
      int playerRest = rdr.GetInt32 (3);
      string playerName = rdr.GetString (4);
      Player newPlayer = new Player (playerName, playerFood, playerFix, playerRest, playerId);
      objects.Add (newPlayer);
    }

    public void UpdatePlayerName (string newPlayerName)
    {
      DB.Edit(_tableName,_id,"name",newPlayerName);
    }

    public void UpdatePlayerFood (int newPlayerFood)
    {
      DB.Edit(_tableName,_id,"food",newPlayerFood);
    }

    public void UpdatePlayerFix (int newPlayerFix)
    {
      DB.Edit(_tableName,_id,"fix",newPlayerFix);
    }

    public void UpdatePlayerRest (int newPlayerRest)
    {
      DB.Edit(_tableName,_id,"rest",newPlayerRest);
    }

    public int GetFood()
    {
      return _food;
    }

    public int GetFix()
    {
      return _fix;
    }

    public int GetRest()
    {
      return _rest;
    }

    public void GiveFood()
    {
      if(_food >= 100 || _fix >= 100)
      {
        _rest -= 5;
      } else{
        _food += 10;
        _rest -= 5;
        _fix += 5;
      }
    }

    public void GiveFix()
    {
      if(_fix >= 100){
        _fix -= 10;
      } else{
        _fix += 15;
        _rest -= 10;
        _food -= 5;
      }
    }

    public void GiveRest()
    {
      if(_rest >= 100){
        _food -= 10;
        _fix -= 20;
      }
      else{
        _rest += 15;
        _food -= 10;
        _fix -= 5;
      }
    }

    public void PassTime()
    {
      _food -= 15;
      _fix -= 15;
      _rest -= 15;
    }

    public void Delete()
    {
      DB.DeleteById(_tableName,_id);
    }

    public static void Delete(int id)
    {
      DB.DeleteById(_tableName,id);
    }

    public static void DeleteAll ()
    {
      DB.ClearTable(_tableName);
    }

    public override bool Equals (System.Object otherPlayer)
    {
      if (!(otherPlayer is Player)) {
        return false;
      } else {
        Player newPlayer = (Player) otherPlayer;
        bool idEquality = this.GetId () == newPlayer.GetId ();
        bool nameEquality = this.GetName () == newPlayer.GetName ();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode ()
    {
      return this.GetId ().GetHashCode ();
    }
  }
}
