using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Dynamic;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Threading.Tasks.Dataflow;
using Microsoft.Data.Sqlite;



public class SqliteBookRepository : IBookRepository
{
    private readonly string _booksPath;

    public SqliteBookRepository(string BooksPath)
    {
        _booksPath = BooksPath;
        Console.WriteLine(_booksPath);
        Console.ReadLine();

        string? folderpath = Path.GetDirectoryName(_booksPath);
        if (!string.IsNullOrWhiteSpace(folderpath))
        {
            Directory.CreateDirectory(folderpath);
        }
                using (var connection = new SqliteConnection($"Data Source={_booksPath}"))
                {
                connection.Open();

                var command = connection.CreateCommand(); 
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Books (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Author TEXT, Read INTEGER); ";
                command.ExecuteNonQuery(); 
                }

    }
   public List<Unit> GetAll()
    {
        List<Unit> CurrentList = new List<Unit>(); 
        using (var connection = new SqliteConnection($"Data Source={_booksPath}"))
        {
            connection.Open();
            var command = new SqliteCommand("SELECT Id, Title, Author, Read FROM Books", connection);
            using (var reader = command.ExecuteReader()) 
                { while (reader.Read())
                    {
                        int Id = reader.GetInt32(0); 
                        string Title = reader.GetString(1); 
                        string Author = reader.GetString(2);
                        int Read = reader.GetInt32(3); 
                        bool convert = Read == 1 ? true : false; 
                        Unit Book = new Unit(Id, Title, Author, convert); 
                        CurrentList.Add(Book); 
                    
                    }
                 
                }
        }
        return CurrentList; 
    }
    public Unit? GetById(int id)
    {
        List<Unit> books = GetAll();
        foreach (Unit book in books)
        {
            if (book.Id_locate == id)
            {
                return book; 
            }
        }

        return null;


    }
    public void Add(Unit book)
    {
        using (var connection = new SqliteConnection($"Data Source={_booksPath}"))
        {
        connection.Open();  
        var insertCommand = new SqliteCommand("INSERT INTO Books (Title, Author, Read) VALUES (@Title, @Author, @Read)", connection);
        insertCommand.Parameters.AddWithValue("@Title", book.TitleEntry);
        insertCommand.Parameters.AddWithValue("@Author", book.AuthorEntry);
        insertCommand.Parameters.AddWithValue("@Read", book.ReadNotEntry ? 1 : 0);
        insertCommand.ExecuteNonQuery(); 
        } 
        

    }
    public void Update(Unit book)
    {
        List<Unit> books = GetAll();
        using (var connection = new SqliteConnection($"Data Source={_booksPath}"))
        {
            connection.Open();
            var insertCommand = new SqliteCommand("UPDATE Books SET Title = @Title, Author = @Author WHERE Id = @Id", connection);
            insertCommand.Parameters.AddWithValue("@Title", book.TitleEntry);
            insertCommand.Parameters.AddWithValue("@Author", book.AuthorEntry);
            insertCommand.Parameters.AddWithValue("@Id", book.Id_locate);
            insertCommand.ExecuteNonQuery(); 
        } 

       


    }
    public void Delete(int id)
    {
        using (var connection = new SqliteConnection($"Data Source={_booksPath}"))
        {
            connection.Open(); 
            var insertCommand = new SqliteCommand("DELETE FROM Books WHERE Id = @Id", connection);
            insertCommand.Parameters.AddWithValue("@Id", id );
            insertCommand.ExecuteNonQuery(); 
        }

    }
    public void ToggleReadStatus(int id)
    {
        using (var connection = new SqliteConnection($"Data Source={_booksPath}"))
        {
            connection.Open();
            var insertCommand = new SqliteCommand("UPDATE Books SET Read = CASE WHEN Read = 1 THEN 0 WHEN Read = 0 THEN 1 END WHERE Id = @Id;", connection); 
            insertCommand.Parameters.AddWithValue("@Id", id); 
            insertCommand.ExecuteNonQuery(); 
        }

    }
     
}