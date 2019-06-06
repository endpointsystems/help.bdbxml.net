# Database Records
DB records contain two parts — a key and some data. Both the key and its corresponding data are encapsulated in Dbt class objects. Therefore, to access a DB record, you need two such objects, one for the key and one for the data. 

Dbt objects provide a void * data member that you use to point to your data, and another member that identifies the data length. They can therefore be used to store anything from simple primitive data to complex class objects so long as the information you want to store resides in a single contiguous block of memory. 

This chapter describes Dbt usage. It also introduces storing and retrieving key/value pairs from a database. 

## Using Database Records

Each database record is comprised of two Dbt objects — one for the key and another for the data. 

```
      float money = 122.45f;
      string description = "grocery bill";
      DatabaseEntry key = new DatabaseEntry(BitConverter.GetBytes(money)); //new DatabaseEntry(money, sizeof(float));
      DatabaseEntry value = new DatabaseEntry(Encoding.UTF8.GetBytes(description));

      Console.WriteLine($"key: {BitConverter.ToSingle(key.Data,0)}, value: {Encoding.UTF8.GetString(value.Data)}");
```
