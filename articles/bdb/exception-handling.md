# Exception Handling

Before continuing, it is useful to spend a few moments on exception handling in DB with the C++ API. 

By default, most DB methods throw `DatabaseException` in the event of a serious error. 
You can obtain the DB error number for a `DatabaseException` by using `DatabaseException.ErrorCode`. You can also obtain the informational message associated with that error number using `DatabaseException.Message`. 

## Error Returns
In addition to exceptions, the DB interfaces always return a value of 0 on success. If the operation does not succeed for any reason, the return value will be non-zero. 
If a system error occurred (for example, DB ran out of disk space, or permission to access a file was denied, or an illegal argument was specified to one of the interfaces), DB returns an errno value. All of the possible values of errno are greater than 0. 
If the operation did not fail due to a system error, but was not successful either, DB returns a special error value. For example, if you tried to retrieve data from the database and the record for which you are searching does not exist, DB would return `DB_NOTFOUND`, a special error value that means the requested key does not appear in the database. All of the possible special error values are less than 0. 