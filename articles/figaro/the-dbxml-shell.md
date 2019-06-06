---
uid: the-dbxml-shell.md
---

# The dbxml Shell

The `dbxml` shell is a .NET console application that allows users to run the most common administrative and maintenance tasks, such as creating containers and querying content. This version is based on the original Oracle [dbxml](http://docs.oracle.com/cd/E17276_01/html/api_reference/XML_CXX/dbxml.html) console, but rewritten in C# to use the Figaro library and extended with several new commands designed to help facilitate .NET development. Use the [GitHub](https://github.com/bdbxml/figaro-dbxml-shell) repository to download the source code, request a feature, or report an issue.


The `dbxml` utility provides an interactive shell that you can use to manipulate containers, documents and indices, and to perform XQuery queries against those containers.

>[!NOTE]
>You can build scripts for `dbxml` by passing them in with the `-s` option.

`dbxml` uses an optional Berkeley DB home (environment) directory, which defaults to the current directory. An attempt is made to join an existing environment; if that fails, a private environment is created in the specified location. dbxml has a concept of a `default` open container; that is, the container upon which container operations such as adding and deleting indices are performed. The default container is set by use of the  `createContainer` and  `openContainer` commands. An in-memory container can be created using the command,  `createContainer ""`. This is useful for using dbxml without file system side effects.

For a list of the commands available in the shell, use the  `help` command. For help on a specific command, pass the command's name to the `help` command. For example:

```
dbxml> help createContainer

        dbxml [-c] [-h homeDirectory]  [-P password] [-s script] [-t] [-V] [-v]  [-x] [-z] [-?]
      
```

## Parameters

`-c`

Create a new environment in the directory specified by the `-h` option. This option should only be used for debugging, since it does not allow you to specify important environment configuration options.

`-h <homeDirectory>`

Specify a home directory for the database environment; by default, the current working directory is used.

`-P <password>`

Specify an environment password. Although BDB utilities overwrite password strings as soon as possible, be aware there may be a window of vulnerability on systems where unprivileged users can see command-line arguments or where utilities are not able to overwrite the memory containing the command-line arguments.

`-s <script>`


Execute the dbxml commands contained in the `script` file upon shell startup. The commands must be specified one to a line in the script file. If any of the commands contained in the script file fail, the shell will not start.

For example, the following is the contents of a script that creates a container, loads several files into it, performs a query, and then prints the results:

```
dbxml> createContainer myContainer.dbxml 
dbxml> putDocument a {<a><b name=&quot;doc1&quot;>doc1 n1</b><c>doc1 n2</c></a>} 
dbxml> putDocument Avocado D:\dev\data\xmlData\nsData\Avocado.xml f 
dbxml> putDocument a {<a><b name=&quot;doc3&quot;>doc3 n1</b><c>doc3 n2</c></a>} 
dbxml> query collection(&quot;myContainer.dbxml&quot;)/a/b 
dbxml> print
```

>[!NOTE]
>When using the `putDocument` command, be sure to use forward slashes ('/') in your paths, and not the backslash ('\'), or you may get a streaming error.

If you are using dbxml to manipulate containers that are managed by an existing database environment, you must specify the path to that existing database environment. dbxml cannot be used to create environment files that can be shared with other applications. It will either create a private environment, or join an existing, shareable environment created by another application.

`-t`

Transaction mode. Transactions are automatically used by the console for every operation that accepts a [XmlTransaction](xref:Figaro.XmlTransaction) object.

`-V`

Print software version.

`-v`

Verbose option. Specifying this option twice will increase the verbosity output.

`-x`

Secure mode - disallow access to the local file system and the Internet.

`-z <size>`

If an environment is created, set the cache size to `<size>` Mb (default: 64)

`-?`

Print the help message.

## Environment Variables

`DB_HOME`


If the `-h` option is not specified and the environment variable `DB_HOME` is set, it is used as the path of the database home.


## dbxml Interactive Shell Commands

The `dbxml` shell includes the following commands:

* [Comment (`#`)](#comment)
* [addAlias](#addalias)
* [addIndex](#addindex)
* [close](#close)
* [commit](#commit)
* [compactContainer](#compactcontainer)
* [contextQuery](#contextquery)
* [cquery](#cquery)
* [createContainer](#createcontainer)
* [delIndex](#delindex)
* [echo](#echo)
* [getDocuments](#getdocuments)
* [getMetadata](#getmetadata)
* [help](#help)
* [info](#info)
* [listIndexes](#listindexes)
* [lookupEdgeIndex](#lookupedgeindex)
* [lookupIndex](#lookupindex)
* [lookupStats](#lookupstats)
* [openContainer](#opencontainer)
* [preload](#preload)
* [prepare](#prepare)
* [print](#print)
* [putDocument](#putdocument)
* [putDocuments](#putdocuments)
* [query](#query)
* [queryPlan](#queryplan)
* [quit](#quit)
* [reindexContainer](#reindexcontainer)
* [removeAlias](#removealias)
* [removeContainer](#removecontainer)
* [removeDocument](#removedocument)
* [run](#run)
* [setAutoIndexing](#setautoindexing)
* [setBaseUri](#setbaseuri)
* [setIgnore](#setignore)
* [setLazy](#setlazy)
* [setMetaData](#setmetadata)
* [setNamespace](#setnamespace)
* [setProjection](#setprojection)
* [setQueryTimeout](#setquerytimeout)
* [setVariable](#setvariable)
* [setVerbose](#setverbose)
* [sync](#sync)
* [time](#time)
* [transaction](#transaction)
* [upgradeContainer](#upgradecontainer)

### Comment 

Comment. Does nothing.

Example:
```
dbxml> #this is a comment
```


### addAlias

Add an alias to the default container. You can refer to this alias, instead of the container's name, when referencing the container in queries.


Usage: 
```
addAlias <alias>
```

Parameter | Description
--------- | -----------
alias | The alias to use for the container.

Use [openContainer](#opencontainer) for opening the default container.


Example:
```
dbxml> opencontainer clients.dbxml
dbxml> addalias clients
Added alias: clients
```

[to top](#)

### addIndex

Add an index to the container.


If the `namespaceUri` and `name` are not set, then this command adds to the default index.


Usage: 

```
addIndex [<nodeNamespaceUri> <nodeName>] <indexDescription>
```

Parameter | Description
--------- | -----------
nodeNamespaceUri | Optional. The namespace URI of the indexed node or attribute. You can specify using the default namespace by entering `""` for this value.
nodeName | Optional. The name of the node or attribute to be indexed.
indexDescription | The index, in `[unique]-{path type}-{node type}-{key type}-{syntax type}` format. See [Using Indices](using-indices.md) for more information.

```
dbxml> addIndex "" Key unique-node-element-equality-string
Adding index type: unique-node-element-equality-string to node: {}:Key
```

This command uses [Figaro.Container.AddIndex](xref:Figaro.Container.AddIndex(Figaro.XmlTransaction,System.String,System.String,Figaro.IndexingStrategy,Figaro.UpdateContext)) and [Figaro.Container.AddDefaultIndex](xref:Figaro.Container.AddDefaultIndex(System.String,Figaro.UpdateContext)).


[to top](#)


### close

Closes a container opened by the `dbxml` shell.


Usage: 

```
close <containerName>
```

Parameter | Description
--------- | -----------
containerName | Optional. The container to close. If a container isn't specified, all open containers are closed.


Example:
```
dbxml> close testdb.dbxml
Container testdb.dbxml closed.
You have 2 containers open.

dbxml>
```

[to top](#)

### commit

Commits the current transaction in a transactional `dbxml` shell environment, and starts a new one.


Usage: 

```
commit
```

[to top](#)


### compactContainer

Compacts a container to shrink its size.


Usage: 
```
openContainer <containerName>
```

Parameter | Description
--------- | -----------
containerName | The container to compact.

```
dbxml> compactContainer testdb.dbxml
Container compacted: testdb.dbxml
```

[to top](#)


### contextQuery

Execute the query expression using the last results as the context item.


Usage: 
```
contextQuery <queryExpression>
```

Parameter | Description
--------- | -----------
queryExpression | The query to run against the current results.

Example:
```
dbxml> getDocuments
4 documents found
dbxml> contextQuery "let $doc:= . where $doc/node1=7 return $doc"
1 objects returned for expression  'let $doc:= . where $doc/node1=7 return $doc'
```

This command uses @Figaro.XmlManager.Prepare to create a @Figaro.XQueryExpression, then loops through the @Figaro.XmlValue objects in the results, calling @Figaro.XQueryExpression.Execute.


[to top](#)

### cquery

Execute the query expression in the context of the default (current) container.


Usage: 
```
cquery <queryExpression>
```

Parameter | Description
--------- | -----------
queryExpression | The query to run against the container. This is useful for scenarios where you don't want to explicitly use the `collection()` function in your query.


Example:
```
dbxml> opencontainer shelltest.dbxml
dbxml> setnamespace "" "http://schemas.endpoint-systems.net/samples/figaro/v1/"
Binding -> http://schemas.endpoint-systems.net/samples/figaro/v1/
dbxml> cquery "for $f in /StoredTableData/Table return $f"
48 objects returned for eager expression 'for $f in /StoredTableData/Table return $f'
```

[to top](#)



### createContainer

Creates a new container, which then becomes the new container.


Usage: 

```
createContainer <contaiNername> [n|in|d|id] [[no]validate]`
```		

Parameter | Description
--------- | -----------
containerName | The name of the container to create.
n | Creates a node storage container.
in | Creates a node storage container with node indexes.
d | Creates a document storage container.
id | Creates a document storage container with node indexes.
[no]validate | Create the container with (or without) Xml validation support.

If another container is open before this command is run, the container will be closed.

The default is to create a node storage container, with node indexes.

A container name of "" creates an in-memory container.

Example:
```
createContainer edi834.dbxml d novalidate
```

[to top](#)


### delIndex

Deletes an index from the default container.

Usage: 
```
delIndex [<nodeNamespaceUri> <nodeName>] <indexDescription>
```

Parameter | Description
--------- | -----------
nodeNamespaceUri | The namespace of the deleted index's node.
nodeName | The name of the deleted index's node.
indexDescription | The indexing strategy, in `unique-node-metadata-equality-string` format.

If the `namespaceUri` and `nodeName` are not set, then this command deletes from the default index.

Example:
```
dbxml> delindex http://schemas.endpointsystems.net/samples/figaro/v1/ id unique-node-element-equality-string
Deleting index type: unique-node-element-equality-string from node: {http://schemas.endpointsystems.net/samples/figaro/v1/}:id
dbxml>
```

This command uses the [Figaro.Container.DeleteIndex](xref:Figaro.Container.DeleteIndex(System.String,System.String,System.String,Figaro.UpdateContext)) and [Figaro.XmlIndexSpecification.DeleteDefaultIndex](xref:Figaro.Container.DeleteDefaultIndex(System.String,Figaro.UpdateContext)) methods.

>[!NOTE]
>You cannot delete the default node index in a container.


[to top](#)



### echo

This command echoes the (optional) text, followed by a newline.


Usage: 
```
echo [text]
```
		
Example:
```
dbxml> echo "hi there"
hi there
dbxml>
```

[to top](#)



### getDocuments

Gets document(s) by name from the default container


Usage: 
```
getDocuments [<docName>]
```
		
Parameter | Description
--------- | -----------
docName | Optional. The document to look up in the default container.


If `docName` is set, it is looked up in the default container. If no arguments are used, all documents in the container are looked up, and placed in the results.

The resulting document names and/or content can be displayed using the `print` command.

Example:
```
dbxml> getdocuments instance1
1 documents found
```

[to top](#)


### getMetaData

Get metadata item from the named content.


Usage: 
```
getMetaData <docName> [<metaDataUri> <metaDataName>]
```
		
Get a metadata item or a list of named metadata items from the named document. This method resets the default results to the returned value. This command, when used to return a specific item, is equivalent to the query expression:

Parameter | Description
--------- | -----------
docName | The name of the document to retrieve metadata for.
metaDataUri | (Optional) The URI of the referenced metadata.
metaDataName | The name of the referenced metadata to look up.

Example:
```
for $i in doc('containerName/docName') return dbxml:metadata('metaDataUri:metaDataName', $i)

dbxml> getmetadata instance1
Metadata for document: instance1 http://www.sleepycat.com/2002/dbxml:name
dbxml>
```

[to top](#)



### help

Print help information. Use `help <commandName>` for extended help.


[to top](#)



### info

Get info on the default container.


Usage: 
```
info [preload]
```


Parameter | Description
--------- | -----------
preload | If specified, return information on any preloaded containers.

Example:
```
dbxml> info
Version: Oracle: Berkeley DB XML 2.4.16: (October 21, 2008)
Berkeley DB 4.6.21: (September 27, 2007)
Default container name: shelltest.dbxml
Type of default container: NodeContainer
Index Nodes: on Shell and XmlManager
state: Not transactional
Verbose: on Query context
state: LiveValues,Eager
```

[to top](#)



### listIndexes

List all indexes in the default container.


Usage: 
```
listIndexes
```
This command calls [Figaro.Container.GetIndexSpecification](xref:Figaro.Container.GetIndexSpecification) and iterates over the indexes in the [Figaro.XmlIndexSpecification](xref:Figaro.XmlIndexSpecification).

Example:
```
dbxml> listindexes
Index: unique-node-metadata-equality-string for node {http://www.sleepycat.com/2002/dbxml}:name
1 indexes found.
dbxml>
```

[to top](#)



### lookupEdgeIndex

Performs an edge lookup in the default container


Usage: 
```
lookupEdgeIndex <indexDescription> <namespaceUri> <nodeName> <parentNamespaceUri> <parentNodeName> [[<operation>] <value>]
```

Parameter | Description
--------- | -----------
indexDescription | The indexing strategy, in `unique-node-metadata-equality-string` format.
namespaceUri | The URI of the referenced node.
nodeName | The name of the referenced node.
parentNamespaceUri | The URI of the parent of the referenced node.
parentNodeName | The name of the parent of the referenced node.
operation | Optional. Valid operations are '>', '<', '>=', '<=', or the default value of '='.
value | Optional. The name of the parent of the referenced node.


[to top](#)



### lookupIndex

Performs an index lookup in the default container


Usage: 
```
lookupIndex <indexDescription> <namespaceUri> <nodeName> [[<operation>] <value>]
```

Parameter | Description
--------- | -----------
indexDescription | The indexing strategy, in `unique-node-metadata-equality-string` format.
namespaceUri | The URI of the referenced node.
nodeName | The name of the referenced node.
parentNamespaceUri | The URI of the parent of the referenced node.
parentNodeName | The name of the parent of the referenced node.
operation | Optional. Valid operations are '>', '<', '>=', '<=', or the default value of '='.
value | Optional. The name of the parent of the referenced node.

Available indexes can be found using the [listIndexes](#listindexes) command.

Example:
```
dbxml> listindexes
Index: unique-node-metadata-equality-string for node {http://www.sleepycat.com/2002/dbxml}:name
1 indexes found.
dbxml> lookupIndex node-metadata-equality-string http://www.sleepycat.com/2002/dbxml name avocado
1 objects returned for eager index lookup 'node-metadata-equality-string'
dbxml>
```

[to top](#)



### lookupStats

Look up statistics on the default container.

Usage:
```
lookupStats <inDexdescription> <namespaceUri> <nodeName> [<parentNamespaceUri> <parentNodeName> <value>]
```

Parameter | Description
--------- | -----------
indexDescription | The indexing strategy, in `unique-node-metadata-equality-string` format.
namespaceUri | The URI of the referenced node.
nodeName | he name of the referenced node.
parentNamespaceUri | Optional. The URI of the parent of the referenced node.
parentNodeName | Optional. The name of the parent of the referenced node.
value | Optional. The name of the parent of the referenced node.

<!-- operation | Valid operations are Valid operations are '>', '<', '>=', '<=', or the default value of '='. -->

The optional parent URI and name are used for edge indexes.

Available indexes can be found using the [listIndexes](#listindexes) command.


```
dbxml> lookupStats node-metadata-equality-string http://www.sleepycat.com/2002/dbxml name
Number of  Indexed Keys: 1 Number of Unique Keys: 1 Sum Key Value Size: 12
dbxml>
```

[to top](#)



### openContainer

Open a container, and use it as the default container.

Usage: 
```
openContainer <container> [[no]validate]
```

Parameter | Description
--------- | -----------
container | The container to open.
[no]validate | Open the container with (or without) XML validation support.

Example:
```
dbxml> openContainer clients.dbxml validate
```

### preload

Preloads (opens) a container.


Usage: 
```
preload <container>
```

Parameter | Description
--------- | -----------
container | The container to preload.
		

This command calls the @Figaro.XmlManager.OpenContainer method to open the container and store the resulting object in a vector. This holds the container open for the lifetime of the program. There is no corresponding `unload` or `close` command.

Example:
```
dbxml> preload test2.dbxml
dbxml>
```

[to top](#)


### prepare

Prepare the given query expression as the default pre-parsed query.

Usage: 
```
prepare <queryExpression> [[no]validate]
```

Parameter | Description
--------- | -----------
queryExpression | The query expression to prepare.
[no]validate | Query the container with (or without) XML validation support.

Example:
```
dbxml> prepare "for $veg in collection('shelltest.dbxml')/vegetables:item return $veg"
Prepared expression 'for $veg in collection('shelltest.dbxml')/vegetables:item return $veg'
```

[to top](#)



### print

Prints most recent results, optionally to a file


Usage: 
```
print | printNames [n Number] [pathToFile]
```
		
If `printNames` is used, the results are turned into document names and printed if possible. If the results cannot be converted, the command will fail. If the optional argument `n` is specified followed by a number, then only the specified number of results are printed. If the optional `pathToFile` parameter is specified, the output is written to the named file rather than `stdout`.

### putDocument

Insert a document into the default container.


Usage: 
```
putDocument <nameprefix> <string> [f|s|q]
```

Insert a document one of three ways:
* By string content (the default, specify `s`)
* By filename. String is a file name, specify `f`
* By XQuery. String is an XQuery expression, specify `q`

### putDocuments

Put a collection of documents found in the specified directory, with the optional file filter, into the default container.

>[!NOTE]
>This command incorporates the files names, without the file extensions, from the file system when inserting into the container. If a file already exists with the same name in the container, an exception will be thrown and processing will stop.

Usage: 
```
putDocuments <directory> [filter]
```

Parameter | Description
--------- | -----------
directory | The directory containing the XML files you wish to put into the default container.
filter | The file filter. The default value is `*.xml`.

In this example we're inserting XML files with the default file filter into the default container:

```
dbxml>putdocuments C:\dev\db\xmldata\simpledata\
298 documents inserted into groceries.dbxml container.
dbxml>
```


### query

Execute the given query expression, or the default pre-parsed query.


Usage: 
```
query [queryExpression]
```

Parameter | Description
--------- | -----------
queryExpression | Optional query expression to execute. If you previously prepared a query, you do not have to enter an expression.
In this example we are executing the query we prepared in the [prepare](#prepare) command.

Example (using prepared command):
```
dbxml> query
100 objects returned for eager expression 'for $veg in collection('shelltest.dbxml')/vegetables:item return $veg'
dbxml>
```

[to top](#)


### queryPlan

Prints the query plan for the specified query expression.


Usage: 
```
queryPlan <queryExpression> [pathToFile]
```


Parameter | Description
--------- | -----------
queryExpression | The query expression to evaluate the query plan for.
pathToFile | The optional file path to save the query plan to.

Example:
```
dbxml> queryPlan "for $veg in collection('shelltest.dbxml')/vegetables:item return $veg"

<XQuery> <Return> <ForTuple uri=&quot;&quot; name=&quot;veg&quot;> <ContextTuple/> <QueryPlanToAST> <StepQP axis=&quot;child&quot; prefix=&quot;vegetables&quot; uri=&quot;http://groceryItem.dbxml/vegetables&quot; name=&quot;item&quot; nodeType=&quot;element&quot;> <SequentialScanQP container=&quot;shelltest.dbxml&quot; nodeType=&quot;document&quot;/> </StepQP> </QueryPlanToAST> </ForTuple> <QueryPlanToAST> <VariableQP name=&quot;veg&quot;/> </QueryPlanToAST> </Return> </XQuery>

dbxml>
```

[to top](#)



### quit

Quits the dbxml shell.


Usage: 
```
quit
```		

[to top](#)



### reindexContainer

Re-index a container, optionally changing index type.

Usage: 
```
reindexContainer <containerName> <d|n>
```		
Parameter | Description
--------- | -----------
containerName | The container name.
[d/n] | Optional. Change the indexing type from nodes ('n') to documents ('d'), or vice versa, if required.

This command can take a long time on large containers.

Containers must be closed, and should be backed up before executing this command.

Example:
```
dbxml> reindexcontainer shelltest.dbxml
Container reindexed: shelltest.dbxml
dbxml>
```

[to top](#)


### removeAlias

Remove an alias from the default container.

Usage: 
```
removeAlias <alias>
```

Parameter | Description
--------- | -----------
alias | The alias associated with the default container.

Example:
```
dbxml> opencontainer shelltest.dbxml
dbxml> addalias 'shelltest'
Alias 'shelltest' added to container 'shelltest.dbxml'.
dbxml> removealias 'shelltest'
Removed alias 'shelltest' from container 'shelltest.dbxml'.

dbxml>
```

[to top](#)



### removeContainer

Removes (deletes) the named container.


Usage: 
```
removeContainer <contaiNername>
```
		
Removes (deletes) the named container. The container must not be open, or the command will fail. If the container is the current container, the current results and container are released in order to perform the operation.

Parameter | Description
--------- | -----------
container | The container to remove.

Example:
```
dbxml> removeContainer test2.dbxml
Removing container: test2.dbxml
Container removed
dbxml>
```

[to top](#)


### removeDocument

Remove (delete) a document from the default container.


Usage: 
```
removeDocument <Docname>
```

Parameter | Description
--------- | ------------
docName | The name of the document to remove.
	
Uses the [Figaro.Container.DeleteDocument](xref:Figaro.Container.DeleteDocument(Figaro.XmlDocument,Figaro.UpdateContext)) method.

>[!NOTE]
>Document names are used in both node and document container types.

Example:
```
dbxml> removedocument Artichoke
Document deleted, name = Artichoke

dbxml>
```

[to top](#)



### run

Runs the given file as a script.


Usage: 
```
run <scriptFile>
```

Parameter | Description
--------- | -----------
scriptFile | The `dbxml` file to execute.


[to top](#)



### setAutoIndexing

Set auto-indexing state of the default container.


Usage: 
```
setAutoIndexing <on|off>
```		

Sets the auto-indexing state of the specified value. The `info` command returns the current state of auto-indexing.

[to top](#)

### setBaseUri

Get/set the base URI in the default context.

Usage: 

```
setBaseUri [<uri>]

```

Parameter | Description
--------- | -----------
uri | The base URI. Must be in the form "scheme:path". If not specified, the command returns the current base URI.
		

This command calls the @Figaro.QueryContext.BaseUri property.

Example:
```
dbxml> setbaseuri http://groceryItem.dbxml/fruits
Base URI = 'http://groceryItem.dbxml/fruits'
dbxml> setBaseUri
Current base URI: 'http://groceryItem.dbxml/fruits'

dbxml>
```

[to top](#)



### setIgnore

Tell the shell to ignore script errors.


Usage: 
```
setIgnore <on|off>
```
		
When set on, errors from commands in dbxml shell scripts will be ignored. When off, they will cause termination of the script. Default value is `off`.


[to top](#)


### setLazy

Sets lazy evaluation on or off in the default context.


Usage: 
```
setLazy <on|off>
```
		
This command calls the [Figaro.QueryContext.EvaulationType](xref:Figaro.QueryContext.EvaluationType) property.


[to top](#)

### setMetaData

Set a metadata item on the named document.

Usage: 
```
setMetaData <docName> <metaDataUri> <metaDataName> <metaDataType> <metaDataValue>
```

Parameter | Description
--------- | -----------
docName | The name of the document to add metadata to.
metaDataUri | The URI of the metadata.
metaDataName | The name of the metadata.
metaDataValue | The value of the metadata.

Example:
```
dbxml> setMetadata apples "http://fruits.net/" fruit Apple
MetaData item 'http://fruits.net/:fruit' added to document apples

dbxml> getmetadata apples
Metadata for document: apples
http://www.sleepycat.com/2002/dbxml:name
http://fruits.net/:fruit

dbxml>
```

[to top](#)

### setNamespace

Create a `prefix->namespace_binding` in the default context.

Usage: 
```
setNamespace <prefix> <namespace>
```

Parameter | Description
--------- | -----------
prefix | The namespace prefix used by the XML documents in the container.
namespace | The namespace associated with the specified prefix.

Example:
```
dbxml> setnamespace fruits http://groceryItem.dbxml/fruits
Binding fruits -> http://groceryItem.dbxml/fruits

dbxml>
```

[to top](#)



### setProjection

Enables or disables the use of the document projection optimization.

Usage: 
```
setProjection <on|off>
```

>[!NOTE]
>Document projection uses static analysis of the query to materialize only those portions of the document relevant to the query, which can significantly enhance performance of queries against documents from `Wholedoc` containers and documents not in a container.


It should not be used if arbitrary navigation of the resulting nodes is to be performed, as not all nodes in the original document will be present and unexpected results could be returned.
[to top](#)


### setQueryTimeout

Set a query timeout in seconds in the default context.


Usage: 
```
setQueryTimeout <seconds>
```

Parameter | Description
--------- | -----------
seconds | The query timeout, specified in seconds.

Example:
```
dbxml> setQueryTimeout 10
Setting query timeout to 10 seconds
dbxml>
```

[to top](#)



### setVariable

Sets an untyped variable in the current context.


Usage: 
```
setVariable <varName> <value> [(<value> ...)]
```

Parameter | Description
--------- | -----------
varName | The variable name.
value | The variable value.

Example:
```
dbxml> setvariable fruit apple
Setting $fruit = apple

dbxml>
```

[to top](#)


###setVerbose

Set verbosity output settings for the shell.

Usage: 
```
setVerbose <level> <category>
		
```

Parameter | Description
--------- | -----------
level | <ul style="list-style:none;"><li> `0x01` -- LEVEL_DEBUG -- program execution tracing@Figaro.UpdateContext `0x02` -- LEVEL_INFO -- informational messages@Figaro.UpdateContext `0x04` -- LEVEL_WARNING -- recoverable warnings@Figaro.UpdateContext `0x08` -- LEVEL_ERROR -- unrecoverable errors@Figaro.UpdateContext `-1` -- LEVEL_ALL --everything
category | <ul style="list-style:none;"><li> `0x01` -- CATEGORY_INDEXER -- messages from the indexer@Figaro.UpdateContext `0x02` -- CATEGORY_QUERY -- messages from the query processor@Figaro.UpdateContext `0x04` -- CATEGORY_OPTIMIZER -- messages from the query optimizer@Figaro.UpdateContext `0x08` -- CATEGORY_DICTIONARY -- messages from the name dictionary@Figaro.UpdateContext `0x10` -- CATEGORY_CONTAINER -- messages from container management@Figaro.UpdateContext `0x20` -- CATEGORY_NODESTORE -- messages from node storage management@Figaro.UpdateContext `0x40` -- CATEGORY_MANAGER -- messages from the manager@Figaro.UpdateContext `-1` -- CATEGORY_ALL -- everything

Level is used for [Figaro.LogConfiguration.SetLogLevel](xref:Figaro.LogConfiguration.SetLogLevel(Figaro.LogConfigurationLevel,System.Boolean)).

Category is used for [Figaro.LogConfiguration.SetCategory](xref:Figaro.LogConfiguration.SetCategory(Figaro.LogConfigurationCategory,System.Boolean)).

Using `0 0` for the parameters turns verbosity off. Using `-1 -1` turns on maximum verbosity.

The values are masks from the library, and can be combined. For example, to turn on INDEXER and optimizer messages, use a category of 0x03.


Example:

```
#set verbose debug & warning levels on the indexer and container categories
dbxml> setverbose 0x01|0x04 0x01|0x10
dbxml>
```

[to top](#)


###sync

Sync the current container to disk.

Usage: 
```
sync
```	

This command syncs the current container to disk, using [Figaro.Container.Sync](xref:Figaro.Container.Sync).

[to top](#)



### time

Wrap a shell command in a wall-clock timer.

Usage: 
```
time <command>
```


Parameter | Description
--------- | -----------
command | The `dbxml` shell command to run.

>[!NOTE]
>The `echo`, `help`, and `setIgnore` commands do not respond to the `time` command. 

If the verbose switch is enabled for the shell, the time command output is always generated.

This command wraps a timer around the specified command and times its execution. The result is sent to the console window (`stdout`).

Example:
```
dbxml> time cquery "for $i in /fruits:item return $i"
96 objects returned for eager expression 'for $i in /fruits:item return $i'
Time in seconds for command 'cquery': 0.013
dbxml>
```

[to top](#)



### transaction

Create a transaction for all subsequent operations to use.


Usage: 
```
transaction
```
		
Any transaction already in force is committed.

This command uses [Figaro.XmlManager.CreateTransaction](xref:Figaro.XmlManager.CreateTransaction(Figaro.TransactionType)).

The following example is used after starting the dbxml shell with the `-t` option and after opening the default container:

```
dbxml> transaction
Transaction started
dbxml> putdocument apple1 d:\dev\db\xmlData\nsData\apples.xml f
Document added, name = apple1
dbxml> commit
Transaction committed
dbxml>
```

[to top](#)



### upgradeContainer

Upgrade a container to the current container format.


Usage: 
```
upgradeContainer <contaiNername>
```
		
Parameter | Description
--------- | -----------
containerName | The name of the container to upgrade.

This command can take a long time on large containers. Containers should be backed up before running this command.


[to top](#)

## See Also

### Other Resources
[Utilities](utilities.md)
