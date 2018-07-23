using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ThePortlandTrail;
using System;

namespace ThePortlandTrail.Models
{
    public class Player
    {   
        private int _id;
        private int _food;
        private int _fix;
        private int _rest;
        private string _name;

        private static Player livePlayer = null;

        public Player(string name, int food = 100, int fix = 100, int rest = 100, int id = 0){
            _name = name;
            _id = id;
            _food = food;
            _fix = fix;
            _rest = rest;
        }

        public string GetName(){
            return _name;
        }

        public int GetId(){
            return _id;
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

        public override bool Equals (System.Object otherPlayer) {
            if (!(otherPlayer is Player)) {
                return false;
            } else {
                Player newPlayer = (Player) otherPlayer;
                bool idEquality = this.GetId () == newPlayer.GetId ();
                bool nameEquality = this.GetName () == newPlayer.GetName ();
                return (idEquality && nameEquality);
            }
        }

        public override int GetHashCode () {
            return this.GetId ().GetHashCode ();
        }

        public void Save(){
            livePlayer = this;
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"INSERT INTO players (name, food, fix, rest) VALUES (@Name, @Food, @Fix, @Rest);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@Name", _name));
            cmd.Parameters.Add(new MySqlParameter("@Food", _food));
            cmd.Parameters.Add(new MySqlParameter("@Fix", _fix));
            cmd.Parameters.Add(new MySqlParameter("@Rest", _rest));

            cmd.ExecuteNonQuery ();
            _id = (int) cmd.LastInsertedId;
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public static Player Find (int id) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM players WHERE id = (@SearchId);", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", id));

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            int playerId = 0;
            int playerFood = 0;
            int playerFix = 0;
            int playerRest = 0;
            string playerName = "";

            while (rdr.Read ()) {
                playerId = rdr.GetInt32 (0);
                playerFood = rdr.GetInt32 (1);
                playerFix = rdr.GetInt32 (2);
                playerRest = rdr.GetInt32 (3);
                playerName = rdr.GetString (4);
            }
            Player newPlayer = new Player (playerName, playerFood, playerFix, playerRest, playerId);
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return newPlayer;
        }

        public static Player GetPlayer(){
            return livePlayer;
        }

        public static List<Player> GetAll () {
            List<Player> allPlayers = new List<Player> { };
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM players ORDER BY name;", conn);

            var rdr = cmd.ExecuteReader () as MySqlDataReader;
            while (rdr.Read ()) {
                int playerId = rdr.GetInt32 (0);
                int playerFood = rdr.GetInt32 (1);
                int playerFix = rdr.GetInt32 (2);
                int playerRest = rdr.GetInt32 (3);
                string playerName = rdr.GetString (4);
                Player newPlayer = new Player (playerName, playerFood, playerFix, playerRest, playerId);
                allPlayers.Add (newPlayer);
            }
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
            return allPlayers;
        }

        public void UpdatePlayerName (string newPlayerName) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand(@"UPDATE players SET name = @NewPlayerName WHERE id = @SearchId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", _id));
            cmd.Parameters.Add (new MySqlParameter ("@NewPlayerName", newPlayerName));

            cmd.ExecuteNonQuery ();
            _name = newPlayerName;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public void UpdatePlayerFood (int newPlayerFood) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand(@"UPDATE players SET food = @NewPlayerFood WHERE id = @SearchId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", _id));
            cmd.Parameters.Add (new MySqlParameter ("@NewPlayerFood", newPlayerFood));

            cmd.ExecuteNonQuery ();
            _food = newPlayerFood;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public void UpdatePlayerFix (int newPlayerFix) {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = new MySqlCommand(@"UPDATE players SET fix = @NewPlayerFix WHERE id = @SearchId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@SearchId", _id));
            cmd.Parameters.Add (new MySqlParameter ("@NewPlayerFix", newPlayerFix));

            cmd.ExecuteNonQuery ();
            _fix = newPlayerFix;

            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }

        public void UpdatePlayerRest(int newPlayerRest)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(@"UPDATE players SET fix = @NewPlayerRest WHERE id = @SearchId;", conn);

            cmd.Parameters.Add(new MySqlParameter("@SearchId", _id));
            cmd.Parameters.Add(new MySqlParameter("@NewPlayerRest", newPlayerRest));

            cmd.ExecuteNonQuery();
            _rest = newPlayerRest;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void GiveFood(){
            if(_food >= 100 || _fix >= 100)
            {
                _rest -= 5;
            } else{
                _food += 10;
                _rest -= 5;
                _fix += 5;
            }
        }

        public void GiveFix(){
            if(_fix >= 100){
                _fix -= 10;
            } else{
                _fix += 15;
                _rest -= 10;
                _food -= 5;
            }
        }

        public void GiveRest(){
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
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(@"DELETE FROM players WHERE id = @PlayerId;", conn);

            cmd.Parameters.Add (new MySqlParameter ("@PlayerId", _id));

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll () {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();

            MySqlCommand cmd = new MySqlCommand(@"DELETE FROM players;", conn);

            cmd.ExecuteNonQuery ();
            conn.Close ();
            if (conn != null) {
                conn.Dispose ();
            }
        }   
    }    
}