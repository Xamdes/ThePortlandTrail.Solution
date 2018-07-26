using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ThePortlandTrail.Models;
using System;

namespace ThePortlandTrail
{
    public class Random
    {
        private int _id;
        private string _resultText;
        public Random(int id = 0, string resultText = "")
        {
            _id = id;
            _resultText = resultText;
        }
        public string GetResultText()
        {
            return _resultText;
        }
        public string publicEvents(int Id, int Fix = 100, int Rest = 100, int Food = 100)
        { 
            if(Fix >= 20)
            {

            }
            else if(Rest >= 20)
            {

            }
            else if(Food >= 20)
            {

            }
            else
            {
            }
            string resultTextModifier = "";
            return resultTextModifier;
        }
    }
}