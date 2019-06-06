---
uid: consistency.md
---

# Consistency

Your containers will never see a partially completed transaction. This is true even if your application fails while there are in-progress transactions. If the application or system fails, then either all of the container changes appear when the application next runs, or none of them appear.


In other words, whatever consistency requirements your application has will never be violated by Figaro. If, for example, your application requires every record to include an employee ID, and your code faithfully adds that ID to its container records, then Figaro will never violate that consistency requirement. The ID will remain in the container records until such a time as your application chooses to delete it.



## See Also

[Why Transactions?](xref:why-transactions.md)
[Introduction](xref:introduction.md)