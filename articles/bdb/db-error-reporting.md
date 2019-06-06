# Database Error Reporting
To simplify error reporting and handling, the database objects offer the @Database.ErrorFeedback delegate, which provides a message prefix from the @Database.ErrorPrefix property, as well as the message being passed. There is also the @Database.MsgFile property for writing output to a log file.

```
namespace BerkeleyDB.Demo
{
  class Program
  {
    static void Main(string[] args)
    {
      var btree = BTreeDatabase.Open("my_btree.db",new BTreeDatabaseConfig{Creation = CreatePolicy.IF_NEEDED,ReadOnly = false, Truncate = false});
      btree.Msgfile = @"C:\temp\btree-msg.log";
      //set the prefix to the database name so we know which database made it
      btree.ErrorPrefix = "my_btree";
      btree.ErrorFeedback = (prefix, message) => { Console.WriteLine($"{prefix}: {message}"); };
      btree.Feedback = (opcode, percent) => { Console.WriteLine($"{opcode.ToString()} at {percent}%"); };
      
      // ...
    }
  }
}
```
