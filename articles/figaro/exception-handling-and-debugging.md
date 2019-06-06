---
uid: exception-handling-and-debugging.md
---

# Exception Handling and Debugging

The internal Berkeley DB XML system throws a combination of Berkeley XML DB, Berkeley DB and `std::exception` objects whenever error conditions occur. Because the Figaro library acts as an intermediary between Figaro and the .NET Framework, these exceptions are wrapped in custom .NET exception classes before being re-thrown. In some cases, Figaro will perform simple validation tests in advance and throw a .NET Framework exception; for example, most method arguments undergo a basic validity check before method call in order to prevent problems with the Berkeley DB XML subsystem. For more information about these conditions, consult with the API documentation.


In some cases, the exceptions thrown by your Figaro application may not contain enough information to allow you to debug the source of an error. In this case, you can cause Figaro to issue more information using the messaging and error streams.



## In This Section

This section contains the following debugging topics:


* [Debug Output Streams](xref:debug-output-streams.md)
* [Debug Output Files](xref:debug-output-files.md)
* [Debug Output Events](xref:debug-output-events.md)



## See Also
* @Figaro.LogConfiguration
* @Figaro.FigaroEnv