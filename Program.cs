// Books List started  

// Pathing
using System.Buffers;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

  
string Appdatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); 
string subdirfold = "LibraryData";
string datafolderpath = Path.Combine(Appdatapath, subdirfold);
string BooksPath = Path.Combine(datafolderpath, "Library.db");


IBookRepository repository = new SqliteBookRepository(BooksPath);


if (File.Exists(BooksPath))
{
   Console.WriteLine("Data Folder Exists");
}

else
{
  Directory.CreateDirectory(datafolderpath);
  File.Create(BooksPath).Close();
}


// Menu starts up 

bool running = true;
while (running)
{
    Console.WriteLine("-----Book Library-----");
    Console.WriteLine("1. View Book List");
    Console.WriteLine("2. Mark as Read");
    Console.WriteLine("3. Add a Book");
    Console.WriteLine("4. Remove a Book");
    Console.WriteLine("5. Update an Entry");
    Console.WriteLine("6. Exit");


string input = Console.ReadLine();

if (input == "1")
{
   Console.WriteLine("Book List");
   List<Unit> books = repository.GetAll(); 
   foreach (Unit book in books)
   {
    Console.WriteLine(book);
   }
} 
else if (input == "2")
  { bool Menu2 = true; 
  while (Menu2)
  { 
    List<Unit> books = repository.GetAll();

    foreach (Unit book in books)
      {
        Console.WriteLine(book);
      }

      Console.WriteLine("Enter a book ID to mark read/unread. Press Enter to exit.");
      string markentry = Console.ReadLine(); 

      bool IsNumber = int.TryParse(markentry, out int numberinput);

      if(string.IsNullOrWhiteSpace(markentry))
      {break;}

      else if(!IsNumber)
      {
        Console.WriteLine("Please Select a number.");
      }
      else
      {
        repository.ToggleReadStatus(numberinput);
      }
  } 
  }
    
  
else if (input == "3") 
  { 
    bool Menu3 = true; 
    while(Menu3)
    { 
      Console.WriteLine("Please begin typing to add a book");
      string Book = Console.ReadLine(); 

      Console.WriteLine("Type in the Author"); 
      string Author = Console.ReadLine(); 

      Unit Entry = new Unit(Book, Author, false); 

      repository.Add(Entry); 

      Console.WriteLine(Entry.ToString()); 

      Console.WriteLine("Press any key to make another entry or z to exit");
      ConsoleKeyInfo keyentry = Console.ReadKey(); 

      if(keyentry.Key == ConsoleKey.Z)
      {
        break; 
      } 
    }
  }
else if (input == "4")
  { 
    bool Menu4 = true;
    while(Menu4)
    {
      Console.WriteLine("Select an Entry to remove.");
      Console.WriteLine("Press Enter to Exit");
      List<Unit> books = repository.GetAll();
      foreach(Unit book in books)
      {
        Console.WriteLine(book);
      }

      string DelEntry = Console.ReadLine(); 

      bool removal = int.TryParse(DelEntry, out int getridof); 
      if(string.IsNullOrWhiteSpace(DelEntry))
      {
        break;
      }
      else if(!removal)
      {
        Console.WriteLine("Please Select a Valid Entry");
      }
      else
      {
        repository.Delete(getridof);
      }

      
    } 

  }

  else if (input == "5")
  { bool Menu5 = true;  
  while(Menu5)
  {
    List<Unit> books = repository.GetAll(); 
    Console.WriteLine("Type in a Number to Select an Entry");
    Console.WriteLine("Press Enter or Space to Exit");

    string updateinput = Console.ReadLine(); 
    bool inputcheck = int.TryParse(updateinput, out int updatenumber);

    if(String.IsNullOrWhiteSpace(updateinput))
    {break;}
    Unit? selection = repository.GetById(updatenumber); 

    if (selection == null)
      {
        Console.WriteLine("No Entries match this ID"); 
        break; 
      }
    Console.WriteLine($"{selection}"); 
    Console.WriteLine("Press 1 for Title and 2 for Author");
    string titleauthor = Console.ReadLine(); 
    bool titlecheck = int.TryParse(titleauthor, out int choice);
    if (choice == 1)
      {
        selection.TitleEntry = Console.ReadLine();
        repository.Update(selection);
      }
    if (choice == 2)
      {
        selection.AuthorEntry = Console.ReadLine(); 
        repository.Update(selection); 
      }
  

  
    }
  }

  
else if (input == "6")
  {
    running = false;
  }
else
  {
    Console.WriteLine("Invalid Choice");
  }
  Console.WriteLine();
}

