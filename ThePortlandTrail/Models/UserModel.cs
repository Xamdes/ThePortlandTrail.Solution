using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ThePortlandTrail;
using System;

namespace ThePortlandTrail.Models
{
    public class User
    {
        private int _hygiene;
        private int _mood;
        private int _rest;
        private int _hunger;
        private int _fix;
        private int _thirst;
        private int _time;
        private string _name;

        public User(int hygiene = 100, int mood = 100, int rest = 100, int hunger = 100, int fix = 100, int thirst = 100, int time = 100, string name = "Curt")
        {
            _hygiene = hygiene;
            _mood = mood;
            _rest = rest;
            _hunger = hunger;
            _fix = fix;
            _thirst = thirst;
            _time = time;
            _name = name;
        }

        public int GetHygiene()
        {
            return _hygiene;
        }
        public int GetMood()
        {
            return _mood;
        }
        public int GetRest()
        {
            return _rest;
        }
        public int GetHunger()
        {
            return _hunger;
        }
        public int GetFix()
        {
            return _fix;
        }
        public int GetThirst()
        {
            return _thirst;
        }
    }
}