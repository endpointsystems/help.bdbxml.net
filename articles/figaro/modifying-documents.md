---
uid: modifying-documents.md
---

# Modifying Documents

The best way to modify a document in a container is to use [XQuery Update](http://xqilla.sourceforge.net/XQueryUpdate) statements. Update statements allow you to insert, delete, replace and rename information in XML. In this section, we provide a very brief overview of this aspect of the XQuery language.

## Modifying Documents

First, let's create a container and then a couple of documents that we can use for our Update queries:

```
dbxml> createContainer modify.dbxml
Creating node storage container with nodes indexed

dbxml> putDocument "mod1.xml" '<mod1>
    <nodeOne>Sample text</nodeOne>
    <nodeTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </nodeTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>' s
Document added, name = mod1.xml

dbxml> putDocument "mod2.xml" '<mod2>
    <nodeA>A sample text</nodeA>
    <nodeB>
        <nodeBA>B A text</nodeBA>
        <nodeBB>B B text</nodeBB>
        <nodeBC>B C text</nodeBC>
    </nodeB>
</mod2>' s
Document added, name = mod2.xml
```

Now let's insert some content into `mod1.xml`. There's a few basic rules that are good to keep in mind at this point:

* Update queries never return a result; they just modify the document and then quit.
* The queries that you use to select a document for updating must not themselves be an update query.
* Update queries can only work on one document at a time, although they can be used in FLWOR expressions and so operate on multiple documents and containers as the expression iterates.


Here's how you insert a node, specifying it as a simple text argument on the query:


```
dbxml> query 'insert nodes
       <newNode>Some new text</newNode>
       after
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeOne'
0 objects returned for eager expression 'insert nodes
       <newNode>Some new text</newNode>
       after
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeOne'
```

Notice that no data returned as a result of the query. This is required by the XQuery Update specification.


Also, notice the keyword `after` in the query. This causes the new data to be inserted after the selected node. Other keywords are available: `before`, `into`, `as first into` and `as last into`.


The results of this query is:


```
dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'

dbxml> print
<mod1>
    <nodeOne>Sample text</nodeOne><newNode>Some new text</newNode>
    <nodeTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </nodeTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

It is also possible to identify new content using a selection query, instead of providing it as a string. For example:


```
dbxml> query 'insert nodes
       doc("dbxml:/modify.dbxml/mod2.xml")/mod2/nodeB
       before
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeThree'
0 objects returned for eager expression 'insert nodes
       doc("dbxml:/modify.dbxml/mod2.xml")/mod2/nodeB
       before
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeThree'
```

As a result of this query, we now have:


```
dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'


dbxml> print
<mod1>
    <nodeOne>Sample text</nodeOne><newNode>Some new text</newNode>
    <nodeTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </nodeTwo>
    <nodeB>
        <nodeBA>B A text</nodeBA>
        <nodeBB>B B text</nodeBB>
        <nodeBC>B C text</nodeBC>
    </nodeB><nodeThree>Node three text</nodeThree>
</mod1>
```

You can delete a node, and when you do the selected node and all its children are also deleted:


```
dbxml> query 'delete nodes doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeB'
0 objects returned for eager expression 'delete nodes doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeB'

dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'


dbxml> print
<mod1>
    <nodeOne>Sample text</nodeOne><newNode>Some new text</newNode>
    <nodeTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </nodeTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

You can also rename a node:


```
dbxml> query 'rename node doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeTwo as "renamedTwo"'
0 objects returned for eager expression 'rename node
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeTwo
       as "renamedTwo"'

dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'

dbxml> print
<mod1>
    <nodeOne>Sample text</nodeOne><newNode>Some new text</newNode>
    <renamedTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </renamedTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

Finally, you can replace document content. Here you can either replace an entire node, or just the node's content. Also, just as is the case with content insertion, you can either specify the new content as a string, or use a selection query to obtain the content from some other document.

To replace a node:

```
dbxml> query 'replace node doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeOne
       with doc("dbxml:/modify.dbxml/mod2.xml")/mod2/nodeB'

0 objects returned for eager expression 'replace node doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeOne
       with doc("dbxml:/modify.dbxml/mod2.xml")/mod2/nodeB'

dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'

dbxml> print
<mod1>
    <nodeB>
        <nodeBA>B A text</nodeBA>
        <nodeBB>B B text</nodeBB>
        <nodeBC>B C text</nodeBC>
    </nodeB><newNode>Some new text</newNode>
    <renamedTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </renamedTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

To replace a node's contents, use replace value of node instead of replace node:

```
dbxml> query 'replace value of node
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeB
       with
       "replacement text"'
0 objects returned for eager expression 'replace value of node
       doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeB
       with
       "replacement text"'

dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'

dbxml> print
<mod1>
    <nodeB>replacement text</nodeB><newNode>Some new text</newNode>
    <renamedTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </renamedTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

It is possible to create an update function that groups multiple update statements together in a single query. When this is done, there are series of rules that govern the order in which the statements are applied to the targeted document, and the conditions under which an error will automatically be raised.


For example, notice that in the following updating function, both the rename and replace operations operate on the same target. However, the rename appears before the replace operation. If these operations were applied strictly in the order that they are supplied, then the replace should fail because the node that it operates on has been replaced. However, this is not the case. The reason why is that XQuery Update always applies replace operations to a document before it applies rename operations.


```
dbxml> query 'declare updating function 
    local:myUpdate($target as element(),
                   $repVal as xs:string,
                   $rep as xs:string)
{
    rename node $target as $rep,
    replace value of node $target with $repVal
};

local:myUpdate(
    doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeB,
    "nodeZ content",
    "nodeZ")'
0 objects returned for eager expression 'declare updating function 
     local:myUpdate($target as element(),
                    $repVal as xs:string,
                    $rep as xs:string)
{
    rename node $target as $rep,
    replace value of node $target with $repVal
};

local:myUpdate(doc("dbxml:/modify.dbxml/mod1.xml")/mod1/nodeB, "nodeZ content", "nodeZ")'


dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'


dbxml> print
<mod1>
    <nodeZ>nodeZ content</nodeZ><newNode>Some new text</newNode>
    <renamedTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </renamedTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

Finally, while it is not possible to perform an update and return a result in the same operation, it is possible to create a function that creates a copy of a document, modifies the copy, and then returns that copy to you. Note that this copy is not stored permanently in the container; the update operations are simply applied to the result of copy operation. All other rules applying to Update operations still apply.


These sort of functions are called _transform functions_. For example:


```
dbxml> query 'copy $c := doc("dbxml:/modify.dbxml/mod1.xml")/mod1      
modify (rename node $c/nodeZ as "nodeB",
        replace value of node $c/nodeZ with "replacement text")
return $c'
1 objects returned for eager expression 'copy $c := 
doc("dbxml:/modify.dbxml/mod1.xml")/mod1
modify (rename node $c/nodeZ as "nodeB",
        replace value of node $c/nodeZ with "replacement text")
return $c'


dbxml> print
<mod1>
    <nodeB>replacement text</nodeB><newNode>Some new text</newNode>
    <renamedTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </renamedTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

However, if we query for `mod1.xml`, we see that it has not been modified by the copy and modify operation:

```
dbxml> query 'collection("modify.dbxml")/mod1'
1 objects returned for eager expression 'collection("modify.dbxml")/mod1'


dbxml> print
<mod1>
    <nodeZ>nodeZ content</nodeZ><newNode>Some new text</newNode>
    <renamedTwo>
        <nodeTwoOne>Two One text</nodeTwoOne>
        <nodeTwoTwo>Two Two text</nodeTwoTwo>
        <nodeTwoThree>Two Three text</nodeTwoThree>
    </renamedTwo>
    <nodeThree>Node three text</nodeThree>
</mod1>
```

### References
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)
* [XQuery Update](http://xqilla.sourceforge.net/XQueryUpdate)
