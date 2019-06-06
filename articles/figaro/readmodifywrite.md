---
uid: readmodifywrite.md
---

# Read/Modify/Write


If you are retrieving a document from the container for the purpose of modifying or deleting it, you should declare a read-modify-write cycle at the time that you read the document. Doing so causes Figaro to obtain write locks (instead of a read locks) at the time of the read. This helps to prevent deadlocks by preventing another transaction from acquiring a read lock on the same record while the read-modify-write cycle is in progress.

Note that declaring a read-modify-write cycle may actually increase the amount of blocking that your application sees, because readers immediately obtain write locks and write locks cannot be shared. For this reason, you should use read-modify-write cycles only if you are seeing a large amount of deadlocking occurring in your application.

In order to declare a read/modify/write cycle when you perform a read operation, pass the @Figaro.QueryOptions.ReadModifyWrite flag to the @Figaro.XQueryExpression.Execute or @Figaro.XmlManager.Query  method.


For example:

``` C#
using System;
using System.Diagnostics;
using System.IO;
using Figaro;

namespace Figaro.Documentation.Examples
{
    internal class ReadModifyWrite
    {

        private const EnvOpenOptions envOpenOptions =
            EnvOpenOptions.Create |
            EnvOpenOptions.InitLock |
            EnvOpenOptions.InitLog |
            EnvOpenOptions.InitMemoryBufferPool |
            EnvOpenOptions.Thread | 
            EnvOpenOptions.InitTransaction;

        private const string baseUri = @"C:\dev\db\samples\ReadModifyWrite";
        private const string testdb = "ReadModifyWrite.dbxml";
        private const string testdata = @"C:\dev\db\xmlData\nsData\";

        private static void Main()
        {
            BuildDir(baseUri, true);
            FigaroEnv environment = new FigaroEnv();
            try
            {
                environment.Open(baseUri, envOpenOptions);
                using (XmlManager mgr = new XmlManager(environment))
                {
                    ContainerSettings containerSettings =
                        ContainerSettings.Create |
                        ContainerSettings.Transactional;

                    using (Container myContainer =
                        mgr.CreateContainer(testdb, containerSettings,
                                            XmlContainerType.WholeDocContainer))
                    {
                        XmlTransaction transaction =
                            mgr.CreateTransaction(TransactionType.None);
                        UpdateContext updateContext = 
                            mgr.CreateUpdateContext();
                        try
                        {
                            myContainer.AddAlias("testdb");

                            //1. fill the container
                            foreach (string file in Directory.GetFiles(testdata))
                            {
                                myContainer.PutDocument(transaction, file, updateContext);
                            }
                            transaction.Commit();

                            //2. perform a query, and delete the results
                            QueryContext queryContext = mgr.CreateQueryContext(EvaluationType.Eager);
                            queryContext.SetNamespace("fruits", "http://groceryItem.dbxml/fruits");
                            string query = "collection('testdb')/fruits:item";
                            // Perform the query. Declare a read/modify/write cycle
                            XmlResults results = mgr.Query(query, 
                                queryContext, 
                                QueryOptions.ReadModifyWrite);

                            updateContext = mgr.CreateUpdateContext();
                            transaction = mgr.CreateTransaction();
                            while (results.HasNext())
                            {
                                XmlDocument figaroXmlDocument = results.NextDocument();
                                myContainer.DeleteDocument(transaction,figaroXmlDocument, updateContext);
                            }
                            transaction.Commit();
                        }
                        finally
                        {
                            myContainer.Close();
                        }
                    }
                }
            }
            catch (FigaroException fe)
            {
                //handle the exception
            }
        }
```

