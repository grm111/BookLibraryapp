

public class Unit
{
  public string TitleEntry {get; set;}
  public string AuthorEntry {get; set;}
  public bool ReadNotEntry {get; set;}
  public int Id_locate {get; set;}

    // Override ToString() 
    public Unit(string booktitle, string authorname, bool readornot)
    {
        TitleEntry = booktitle;
        AuthorEntry = authorname; 
        ReadNotEntry = readornot; 
    }

    public override string ToString()
    {
        return $"{Id_locate}.) Title: {TitleEntry} Author: {AuthorEntry} {(ReadNotEntry ? "✅" : "")}";
    }
  
  // Toggling the boolean
    public void Toggle()
  {
    ReadNotEntry = !ReadNotEntry; 
  }
  public Unit(int id, string booktitle, string authorname, bool readornot)
  {
    Id_locate = id; 
    TitleEntry = booktitle;
    AuthorEntry = authorname;
    ReadNotEntry = readornot;
  }

  public string ToSaveString()
  {
    return  $"{Id_locate}|{TitleEntry}|{AuthorEntry}|{ReadNotEntry}"; 
  }
}

