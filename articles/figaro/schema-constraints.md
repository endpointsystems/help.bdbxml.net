---
uid: schema-constraints.md
---

# Schema Constraints

XML documents can optionally be validated against a schema to enforce document similarity. Most databases support schema constraints, but Figaro has the unique ability to store collections of data with schemas that vary from document to document if desired. This is an added level of functionality not commonly found in XML databases.


Recall our phone book example. The documents for that example had the following structure:

``` XML
<part number="999">
  <description>Description of 999</description>
  <category>9</category>
</part>
```

Three things are required to validate this document. First, a schema is required. 

``` XML
<?xml version="1.0" encoding="UTF-8"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="phonebook">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="name" minOccurs="1" maxOccurs="1">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="first" type="xs:string"/>
              <xs:element name="last" type="xs:string"/>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name="phone" minOccurs="0" maxOccurs="unbounded">
          <xs:complexType>
            <xs:simpleContent>
              <xs:extension base="xs:string">
                <xs:attribute name="type" type="xs:string"/>
              </xs:extension>
            </xs:simpleContent>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>
```

Suppose this schema is available from a web-server at:


``` XML
http://schemas.endpointsystems.net/sampleSchema.md
```

Second, we need to create a container with validation enabled.


```
dbxml> createContainer validate.dbxml d validate
Creating document storage container, with validation
```

Third, we need to attach the schema to a document and insert it into the container.


```
dbxml> putDocument phone1 '
<phonebook xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
xsi:noNamespaceSchemaLocation=
"http://schemas.endpointsystems.net/sampleSchema.md">
<name>
<first>Tom</first>
<last>Jones</last>
</name>
<phone type="home">420-203-2032</phone>
</phonebook>' s

Document added, name = phone1
```

That document was successfully added because it conforms to the schema. Now, try to add an invalid document.


```
dbxml> putDocument phone2 '
<phonebook xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
xsi:noNamespaceSchemaLocation=
"http://schemas.endpointsystems.net/sampleSchema.md">
<name>
<first>Tom</first>
<last>Jones</last>
</name>
<phone type="home">420-203-2032</phone>
<cell-phone>430-201-2033</cell-phone>
</phonebook>' s

stdin:67: putDocument failed, Error: XML Indexer:  Parse error in document at
line, 10, char 17. Parser message: Unknown element 'cell-phone']]>
```

Since the schema doesn't define the cell-phone element and we have schema validation enabled, Figaro won't allow the document to be added to the container.


XML schemas provide a powerful tool for constraining the structure and content of XML documents.


## See Also


#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)
