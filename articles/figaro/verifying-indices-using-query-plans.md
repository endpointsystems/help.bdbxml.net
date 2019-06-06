---
uid: verifying-indices-using-query-plans.md
---

# Verifying Indices Using Query Plans


When designing your indexing strategy, you should create indices to improve the performance of your most frequently occurring queries. Without indices, Figaro must walk every document in the container in order to satisfy the query. For containers that contain large numbers of documents, or very large documents, or both, this can be a time-consuming process.


However, when you set the appropriate index(es) for your container, the same query that otherwise takes minutes to complete can now complete in a time potentially measured in milliseconds. So setting the appropriate indices for your container is a key ingredient to improving your application's performance.


That said, the question then becomes, how do you know that a given index is actually being used by a given query? That is, how do you do this without loading the container with enough data that it is noticeably faster to complete a query with an index set than it is to complete the query without the index?


The way to do this is to examine Figaro's query plan for the query to see if it intends to use an index for the query. And the best and easiest way to examine a query plan is by using the **dbxml** command line utility.


The query plan is literally Figaro's plan for how it will satisfy a query. When you use @Figaro.XmlManager.Prepare, one of the things you are doing is regenerating a query plan so that Figaro does not have to continually re-create it every time you run the query.


Printed out, the query plan looks like an XML document that describes the steps the query processor will take to fulfill a specific query.


For example, suppose your container holds documents that look like the following:


``` XML
<a>
  <docId id="aaUivth" />
  <b>
    <c>node1</c>
    <d>node2</d>
  </b>
</a>
```

Also, suppose you will frequently want to retrieve the document based on the value set for the id parameter on the docId node. That is, you will frequently perform queries that look like this:


``` XML
collection("myContainer.dbxml")/a/docId[@id='bar']
```

In this case, if you print out the query plan (we describe how to do this below), you will see something like this:


``` XML
<XQuery>
  <QueryPlanToAST>
    <NodePredicateFilterQP uri="" name="#tmp5">
      <StepQP axis="child" name="docId" nodeType="element">
        <StepQP axis="child" name="a" nodeType="element">
          <SequentialScanQP container="myContainer.dbxml" nodeType="document"/>
        </StepQP>
      </StepQP>
      <ValueFilterQP comparison="eq" general="true">
        <StepQP axis="attribute" name="id" nodeType="attribute">
          <VariableQP name="#tmp5"/>
        </StepQP>
        <Sequence>
          <AnyAtomicTypeConstructor value="bar" typeuri="http://www.w3.org/2001/XMLSchema" typename="string"/>
        </Sequence>
      </ValueFilterQP>
    </NodePredicateFilterQP>
  </QueryPlanToAST>
</XQuery>
```

While a complete description of the query plan is outside the scope of this manual, notice that there is no element specified in the query plan that includes an index attribute. This attribute can appear on different element nodes, depending on the nature of the query and the actual index that the query wants to use. For example, queries that use indexes which example the value of a node might specify a `ValueQP` node.


``` XML
<ValueQP container="myContainer.dbxml" index="node-attribute-equality-string" operation="eq" child="id" value="bar"/>
```

Other indexes that simply test for the presence of a node would specify the index on a `PresenceQP` element:


``` XML
<PresenceQP container="parts.dbxml" index="node-element-presence-none" operation="eq" child="parent-part"/>
```


## See Also

[Using Indices](xref:using-indices.md)