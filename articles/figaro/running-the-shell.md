---
uid: running-the-shell.md
---

# Running the Shell


The [dbxml shell command](xref:the-dbxml-shell.md) is located in the Figaro NuGet packages. To run the shell, simply type `dbxml` at the command prompt for your operating system. Assuming that you have the dbxml shell in your operating system's command line path, you'll then be greeted by the `dbxml>` prompt.


```
C:\ dbxml
dbxml>
```

In the examples that follow, you'll see the `dbxml>` prompt followed by the command that should be entered. Most commands are simple one line commands. However, some are more complicated XQuery examples that will span multiple lines. Each example will show both the command to enter and the resulting output. When the output is too long, ellipsis (...) will be used to abbreviate the intermediate results.


When using Figaro you will find that document content is stored in a container. This is the first basic concept in Figaro XML database development:

* Containers hold collections of XML documents.
* The documents within a container may or may not share the same schema.

To begin exploring Figaro, create a container. Our first example models a simple phone book database. The container's name will be `phone.dbxml`.


```
dbxml> createContainer phone.dbxml      

Creating node storage container with nodes indexed
```

The command and output in this case was very simple. It was meant to merely confirm command execution. Note that a file named `phone.dbxml` was created in your working directory. This is the new node storage container. Containers hold the XML data, indices, document metadata, and any other useful information, and are managed by Figaro. Never edit a container directly - always allow the Figaro library to manage it for you. The `.dbxml` extension helps to identify the database on disk, but is simply a naming convention that is not strictly required.
>[!NOTE]
>In addition to creating the container, the `dbxml` shell also automatically opened it and made it ready for us to use.

This phone book example's data model uses XML entries of the following format:

``` XML
    <phonebook>
    <name>
        <first>Tom</first>
        <last>Jones</last>
    </name>   
    <phone type="home">420-203-2032</phone>
</phonebook>
```

Now add a few phone book entries to the container in the following manner:


```
dbxml> putDocument phone1 '<phonebook>
    <name>
        <first>Tom</first>
        <last>Jones</last>
    </name>   
    <phone type="home">420-203-2032</phone>
</phonebook>' s

Document added, name = phone1

dbxml> putDocument phone2 '<phonebook>
    <name>
        <first>Lisa</first>
        <last>Smith</last>
    </name>   
    <phone type="home">420-992-4801</phone>
    <phone type="cell">390-812-4292</phone>
</phonebook>' s

Document added, name = phone2
```
>[!NOTE]
>The XML document content is wrapped in single quote characters and the command is terminated by an `s` character. This indicates that we are adding a new document using a string. The single quote characters are used for any command parameter that either contains spaces or needs to span multiple lines.

Now the container has a few phone book entries. The following few examples demonstrate some basic XQuery queries based solely on XPath statements. Subsequent sections will demonstrate more complex XQuery statements.

>[!NOTE]
>XPath is a central part of the XQuery specification. It serves much the same function as the `SELECT` statement does in SQL. It is essentially used to identify a subset of data within the data set.
To retrieve all the last names stored in the container:


```
dbxml> query 'collection("phone.dbxml")/phonebook/name/last/string()'

2 objects returned for eager expression 'collection("phone.dbxml")/phonebook/name/last/string()'

dbxml> print
Jones
Smith
```

To find Lisa's home phone number:


```
dbxml> query '
collection("phone.dbxml")/phonebook[name/first = "Lisa"]/phone[@type = "home"]/string()'

1 objects returned for eager expression 'collection("phone.dbxml")/phonebook[name/first = "Lisa"]/phone[@type = "home"]/string()'

dbxml> print
420-992-4801
```

To find all phone numbers in the 420 area code:


```
dbxml> query 'collection("phone.dbxml")/phonebook/phone[starts-with(., "420")]/string()'

2 objects returned for eager expression 'collection("phone.dbxml")/phonebook/phone[starts-with(., "420")]/string()'

dbxml> print
420-203-2032
420-992-4801
```

These queries simply retrieve subsets of data, just like a basic `SELECT` statement would in a relational database. Each query consists of two parts. The first part of the query identifies the set of documents to be examined (equivalent to a projection). This is done with an XQuery navigation function such as `collection()`. In this example, `collection("phone.dbxml")` specifies the container against which we want to apply our query. The second part is an XPath statement (equivalent to a selection). The first example's XPath statement was `/phonebook/name/last/string()` which, based on our document structure, will retrieve all last names and present them as a string.


Understanding XPath is the first step toward understanding XQuery.
>[!NOTE]
>You can perform a query against multiple containers using the union operator ("|") with the `collection()` function. For example, to query against containers `c1.dbxml ` and `c2.dbxml`, you would use the following expression: 

```
(collection("c1.dbxml") | collection("c2.dbxml"))/name/string()
```

## See Also

#### Other Resources

* [Introducing Figaro](xref:intro.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)