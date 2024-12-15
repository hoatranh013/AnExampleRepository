// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using Npgsql;
using System;
using System.Data;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=your_username;Password=your_password;Database=your_database";
        string getQuery = "SELECT id, version FROM version";
        string insertQuery = "INSERT INTO version (id, version) VALUES (@value1, @value2)";
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var getReadVersion = 0;
            using (var getPreviousVersion = new NpgsqlCommand(getQuery, connection))
            {
                var readDatas = getPreviousVersion.ExecuteReader();
                while (readDatas.Read())
                {
                    if (int.Parse(readDatas.GetValue(1).ToString()) > getReadVersion)
                    {
                        getReadVersion = int.Parse(readDatas.GetValue(1).ToString());
                    }
                }
            }
            getReadVersion = getReadVersion + 1;
            using (var insertCommand = new NpgsqlCommand(insertQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@value1", Guid.NewGuid());
                insertCommand.Parameters.AddWithValue("@value2", getReadVersion);
                insertCommand.ExecuteNonQuery();
            }
        }
    }
}