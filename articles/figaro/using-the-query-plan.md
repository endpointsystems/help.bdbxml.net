---
uid: using-the-query-plan.md
---
# Using the Query Plan Optimizer Log Output

Whenever starting an application containing the Berkeley DB XML library, you will see this legend in the application output. 

```
	 Legend for the Query Plan log output

	 RQP  - Raw Query Plan before any optimizations
	 POQP - Partially optimized Query Plan
	 OQP  - Optimized Query Plan after optimizations

	 path - Paths

	 P    - Presence index look up
	 V    - Value index look up
	 R    - Range index look up
	 Pd   - Presence document index look up
	 Vd   - Value document index look up
	 Rd   - Range document index look up
	 SS   - Sequential scan
	 U    - Universal set
	 E    - Empty set

	 COL  - Collection function
	 DOC  - Document function
	 CN   - Context node
	 VAR  - Variable
	 AST  - Non query plan operation

	 VF   - Value filter
	 PF   - Predicate filter
	 NPF  - Node predicate filter
	 NNPF - Negative node predicate filter
	 NuPF - Numeric predicate filter
	 RNPF - Reverse numeric predicate filter
	 LF   - Level filter

	 DP   - Optimization decision point
	 DPE  - Decision point end
	 BUF  - Buffer
	 BR   - Buffer reference
	 CH   - Choice

	 n    - Intersection
	 u    - Union
	 e    - Except

	 step - Conventional navigation step

	 d    - Descendant join
	 ds   - Descendant or self join
	 c    - Child join
	 ca   - Attribute or child join
	 a    - Attribute join
	 p    - Parent join
	 pa   - Parent of attribute join
	 pc   - Parent of child join
	 an   - Ancestor join
	 ans  - Ancestor or self join
```

The purpose behind this legend is to help understand the logic used by the database engine to query data out of the database. Observe the following sample output snippet:

```
err: Optimizer  - COL -> SS(document(*))
err: Optimizer  - RQP(1): DPE
err: Optimizer  - OQP(1): SS(document(*))
err: Optimizer  - Original query plan: SS(document(*))
err: Optimizer  - QueryPlanToAST alternative chosen (from 1)
err: Optimizer  - SS(document(*)) : keys=0, overhead=0k, forKeys=0k
err: Optimizer  - Finished parse, time taken = 297ms
```

Using the legend, we can see that the optimizer performs the following steps:
- the current query plan is a collection function consisting of a sequential scan for documents
- a Raw Query Plan (before any optimizations) with a decision point end
- the Optimized Query Plan (after optimizations) is still the sequential scan
- the query plan was chosen
- a keyless sequential scan for documents was performed
- the parsing operation was performed taking 297 milliseconds of time.


Using this query plan output will help you determine the efficiency level of your queries and make adjustments to your queries as well as your indexing strategies for boosting performance.