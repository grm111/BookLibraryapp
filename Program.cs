// Books List started  

// Pathing
using System.Buffers;
using System.Collections.Immutable;
using System.IO.Pipelines;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

String Appdatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); 
string subdirfold = "LibraryData";
string datafolderpath = Path.Combine(Appdatapath, subdirfold);
string BooksPath = Path.Combine(datafolderpath, "Books.txt");
if (File.Exists(BooksPath))
{
   Console.WriteLine("Data Folder Exists");
}

else
{
  Directory.CreateDirectory(datafolderpath);
  File.Create(BooksPath).Close();
}

string[] LoadList = File.ReadAllLines(BooksPath); 
List<Unit> ListBooks = new List<Unit>();
List<string> lines = new List<string>();
foreach (Unit Book in ListBooks)
{
  lines.Add(Book.ToSaveString());
}


// Creating List Loop 
foreach (string line in LoadList)
  {
  string[] Unit_info = line.Split('|');
  Unit Book = new Unit(
  Unit_info[0],
  Unit_info[1],
  bool.Parse(Unit_info[2])
  );
  ListBooks.Add(Book);
  }
//
int ListindexCount = ListBooks.Count; 
int ListCount = ListindexCount - 1; 


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
   Directory.GetCurrentDirectory(); 
   foreach (Unit item in ListBooks)
   {
    Console.WriteLine(item.ToString());
   }
} 
else if (input == "2")
  { bool Menu2 = true; 
  while (Menu2)
  { 
    Console.WriteLine("Enter a number to mark a book as read or not.");
    string markentry = Console.ReadLine();
    bool IsNumber = int.TryParse(markentry, out int numberinput);
    if (string.IsNullOrWhiteSpace(markentry))
      {
        Menu2 = false; 
      }
    else if (!IsNumber)
      {
        Console.WriteLine("Please select a number for the entry you'd like to mark.");
      }  
    else if (numberinput <= 0 || numberinput > ListBooks.Count)
      {
        Console.WriteLine("Please input a number corresponding to an entry.");
      }
    else
      {
        int SelectedEntry = numberinput - 1;
        ListBooks[SelectedEntry].Toggle();
        Console.WriteLine(ListBooks[SelectedEntry]);
        lines.Clear(); 
        foreach (Unit Book in ListBooks)
          {
              lines.Add(Book.ToSaveString());
          }
          File.WriteAllLines(BooksPath, lines); 
      }
      Console.WriteLine("Press Enter to exit");
  } 
  }
    
  
else if (input == "3") 
  { 
    bool Menu3 = true; 
    while(Menu3)
    { 
    Console.WriteLine("Add a Book.");
    Console.WriteLine("Type in your book title");
    string Book = (Console.ReadLine());
    Console.WriteLine("Type in the Author");
    string Author = (Console.ReadLine());
    Unit Entry = new Unit(Book, Author, false );
    Console.WriteLine(Entry.ToString());
    ListBooks.Add(Entry);
    lines.Clear();
    foreach(Unit book in ListBooks)
      {
        lines.Add(book.ToSaveString());
      }
    File.WriteAllLines(BooksPath, lines);
    Console.WriteLine("Press any key to make another entry or z to exit");
    ConsoleKeyInfo keyentry = Console.ReadKey();
    if (keyentry.Key == ConsoleKey.Z)
    {break;}
    }
  }
else if (input == "4")
  { 
    bool Menu4 = true;
    while(Menu4)
    {
    Console.WriteLine("Remove A Book");
    Console.WriteLine("Press Enter to Exit");
    string RemoveInput = Console.ReadLine();
    bool removecheck = int.TryParse(RemoveInput, out int RemoveNumber);
    if (string.IsNullOrWhiteSpace(RemoveInput))
      {
        break; 
      }
    else if (RemoveNumber <= 0 || RemoveNumber > ListBooks.Count)
      {
        Console.WriteLine("Please select a number within range"); 
      }
    else
      {
        int pick = RemoveNumber - 1; 
        ListBooks.RemoveAt(pick);
        lines.Clear();
        foreach (Unit Book in ListBooks)
        {
          lines.Add(Book.ToSaveString());
        }
        File.WriteAllLines(BooksPath, lines);
      }
    } 

  }

  else if (input == "5")
  { bool Menu5 = true;   
  for (int i = 0; i < ListBooks.Count; i++ )
   
    {
      int number = i + 1;
      Console.WriteLine($"{number}.){ListBooks[i]}");
    }
  Console.WriteLine("Select a number to update.");
  string UserUpdate = Console.ReadLine();
  bool updatecheck = int.TryParse(UserUpdate, out int updtnum);
  if (String.IsNullOrWhiteSpace(UserUpdate))
  {
      break; 
    }
    else if (updtnum <= 0 || updtnum > ListBooks.Count)
    {
      Console.WriteLine("Please Select a listed Entry.");
    }
    else
    {
      int pick = updtnum - 1; 
      Unit selectedbook = ListBooks[pick];
      bool editmenu = true;
      while(editmenu)
      {
        Console.WriteLine("Select a Field to Edit"); 
        Console.WriteLine(selectedbook);
        Console.WriteLine("1.Title");
        Console.WriteLine("2.Author"); 
        Console.WriteLine("Press Escape to go back"); 
        ConsoleKeyInfo titlauth = Console.ReadKey();
        string validselection = Convert.ToString(titlauth.KeyChar); 
        bool optioncheck = int.TryParse(validselection, out int fieldselection);
        if (titlauth.Key == ConsoleKey.Escape)
        {
          break;
        }
        else if (fieldselection == 1)
        {
          Console.WriteLine("Go ahead and rename");
          selectedbook.TitleEntry = Console.ReadLine(); 
          lines.Clear();
        foreach (Unit Book in ListBooks)
        {
          lines.Add(Book.ToSaveString());
        }
        File.WriteAllLines(BooksPath, lines);
        }
        else if (fieldselection == 2)
        {
          Console.WriteLine("Update the Author");
          selectedbook.AuthorEntry = Console.ReadLine(); 
          lines.Clear();
        foreach (Unit Book in ListBooks)
        {
          lines.Add(Book.ToSaveString());
        }
        File.WriteAllLines(BooksPath, lines);
        }

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

// Book Entries object for Title, Author, and read or not status. 
public class Unit
{
  public string TitleEntry {get; set;}
  public string AuthorEntry {get; set;}
  public bool ReadNotEntry {get; set;}

    // Override ToString() 
    public override string ToString()
    {
        return $"Title: {TitleEntry} Author: {AuthorEntry} {(ReadNotEntry ? "✅" : "")} "; 
    }
  
  // Toggling the boolean
    public void Toggle()
  {
    ReadNotEntry = !ReadNotEntry; 
  }
  public Unit(string booktitle, string authorname, bool readornot)
  {
    TitleEntry = booktitle;
    AuthorEntry = authorname;
    ReadNotEntry = readornot;
  }

  public string ToSaveString()
  {
    return  $"{TitleEntry}|{AuthorEntry}|{ReadNotEntry}"; 
  }
}
