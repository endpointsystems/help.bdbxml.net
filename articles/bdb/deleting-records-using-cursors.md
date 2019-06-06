# Deleting Records Using Cursors
To delete a record using a cursor, simply position the cursor to the record you want to delete and call `Delete`. 

```
public void DeleteRecordUsingCursor(string keyval)
{
    Stopwatch watch = new Stopwatch();
    watch.Start();
    bool b = true;
    cursor.MoveFirst();
    while (b)
    {
    if (Encoding.UTF8.GetString(cursor.Current.Key.Data) == keyval)
    {
        Console.WriteLine($"key found: '{keyval}'. Deleting...");          
        cursor.Delete();          
        Console.WriteLine($"database record '{keyval}' deleted.");          
        break;
    }

    b = cursor.MoveNext();
    }
    watch.Stop();
    Console.WriteLine($"delete task completed in {watch.ElapsedMilliseconds} milliseconds.");
    Console.WriteLine("---");
}
```
