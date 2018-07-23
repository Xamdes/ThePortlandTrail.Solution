
namespace ThePortlandTrail
{
  public static class DBConfiguration
    {
      private static string _connectionString = "server=localhost;user id=root;password=root;port=8889;database=crust;Convert Zero Datetime=True;Allow User Variables=true;";

      private static string _testConnectionString = "server=localhost;user id=root;password=root;port=8889;database=crust;Convert Zero Datetime=True;Allow User Variables=true;";

      public static string GetConnection()
      {
        return _connectionString;
      }

      public static void SetConnection(string s)
      {
        _connectionString = s;
      }

      public static string UsingTestConnection()
      {
        return _testConnectionString;
      }

      public static string UsingDefaultConnection()
      {
        return _connectionString;
      }
    }
}
