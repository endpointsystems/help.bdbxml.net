---
uid: examining-document-query-results.md
---

# Examining Document Query Results

When you perform a query against Figaro, you receive a results set in the form of an @Figaro.XmlResults object. To examine the results, you iterate over this result set, retrieving each element of the set as an @Figaro.XmlValue object.

Once you have an individual result element, you can obtain the data encapsulated in the @Figaro.XmlValue object in a number of ways. For example, you can obtain the information as a string object using @Figaro.XmlValue.ToString. Alternatively, you could obtain the data as an @Figaro.XmlDocument object using @Figaro.XmlValue.AsDocument.

It is also possible to use DOM-like navigation on the @Figaro.XmlValue object since that class offers navigational methods such as @Figaro.XmlValue.FirstChild, @Figaro.XmlValue.NextSibling, @Figaro.XmlValue.Attributes, and so forth. For details on these and other @Figaro.XmlValue attributes, see the [API Reference documentation.](/api/index.html)


