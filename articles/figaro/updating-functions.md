---
uid: updating-functions.md
---

# Updating Functions

You can create a function that performs an update, so long as it is declared to be an updating function. In addition, this function must not have a return value, and the argument passed to the function cannot be an update query.


For example, the following query creates a function that renames any element node passed to it, to the value passed in the second argument. The function is then called for `b1` in document `mydoc.xml`, which is stored in container `con.dbxml`:


``` XML
local:renameNode($elem as element(),
$rep as xs:string)
{
rename node $elem as $rep
};

local:renameNode(doc("dbxml:/con.dbxml/mydoc.xml")/a/b1, "aab1")
```

If the prior query is called on a document such as this:


``` XML
<a>
  <b1>first child</b1>
  <b2>second child</b2>
  <b4>inserted child</b4>
  <b3>third child</b3>
</a>
```

then that document becomes:


``` XML
<a>
  <aab1>first child</aab1>
  <b2>second child</b2>
  <b3>third child</b3>
</a>
```


## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
