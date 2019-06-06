---
uid: the-query-context.md
---

# The Query Context

Context is a term that is heavily used in both Figaro and XQuery. While overlap exists in how the term is used between the two, it is important to understand that differences exist between what Figaro means by context and what the XQuery language means by it.

In XQuery, the context defines aspects of the query that aid in query navigation. For example, the XQuery context defines things like the namespace(s) and variables used by the query, the query's focus (which changes over the course of executing the query), and the functions and collations used by the query. Most thorough descriptions of XQuery will describe these things in detail.

In Figaro, however, the context is a physical object (@Figaro.QueryContext) that is used for very limited things (compared to what is meant by the XQuery context). You can use @Figaro.QueryContext to control only part of the XQuery context. You also use @Figaro.QueryContext to control Figaro's behavior toward the query in ways that have no corresponding concept for XQuery contexts.


Specifically, you use @Figaro.QueryContext to:
* Define the namespaces to be used by the query.
* Define any variables that might be needed for the query, although, these are not the same as the variables used by XQuery FLWOR expressions (see [Defining Variables](xref:defining-variables.md)).
* Define whether the query is processed "eagerly" or "lazily" (see [Defining the Evaluation Type](xref:defining-the-evaluation-type.md)).

Note that Figaro also uses the @Figaro.QueryContext to identify the query's focus as you iterate over a result set. See [Performing Queries](xref:performing-queries.md) for more information.


#### See Also
* @Figaro.QueryContext
* [Defining Variables](xref:defining-variables.md)
* [Defining the Evaluation Type](xref:defining-the-evaluation-type.md)
* [Performing Queries](xref:performing-queries.md)