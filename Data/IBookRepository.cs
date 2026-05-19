using System;
using System.Collections.Concurrent;


public interface IBookRepository
{
    List<Unit> GetAll(); 

    Unit? GetById(int id);

    void Add(Unit book);

    void Update(Unit book); 

    void Delete(int id); 

    void ToggleReadStatus(int id); 

}




