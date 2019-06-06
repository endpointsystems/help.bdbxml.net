---
uid: adding-data.md
---

# Adding Data

In this example, the container will manage a few thousand node documents modeling an imaginary parts database. Begin by using the following command to create a container called `parts.dbxml`:


```
dbxml> createContainer parts.dbxml

Creating node storage container with nodes indexed
```

A successful response indicates that the container was created on disk, opened, and made the default container within the current context of the shell. Next populate the container with 100000 XML nodes that have the following basic structure:

``` XML
<part number="999">
  <description>Description of 999</description>
  <category>9</category>
</part>
```

Some of the nodes will provide additional complexity to the database and have the following structure:

``` XML
<part number="990">
  <description>Description of 990</description>
  <category>0</category>
  <parent-part>0</parent-part>
</part>
```

Use the following `putDocument` command to insert the sample data into the new parts container.
>[!NOTE]
>Depending on the speed of your machine, you may want to reduce the total number of documents you add to your container for performance reasons. We use a moderately sized document set here so that we are better able to observe timing results later in this chapter. If you are using slow hardware, you should be able to observe the same results using a smaller document size.

```
dbxml> putDocument "" '
for $i in (0 to 99999) 
return 
  <part number="{$i}">
    <description>Description of {$i}</description>
    <category>{$i mod 10}</category>
    {
      if (($i mod 10) = 0) 
      then <parent-part>{$i mod 3}</parent-part> 
      else ""
    }
  </part>' q
```

As the query executes, one line will be printed for each document inserted into the database.



## See Also


#### Other Resources
* [Figaro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Introducing Figaro](xref:intro.md)