# Implementing Key Extractors

You must provide every secondary database with a class that creates keys from primary records. You identify this class when you associate your secondary database to your primary. 
You can create keys using whatever data you want. Typically you will base your key on some information found in a record's data, but you can also use information found in the primary record's key. How you build your keys is entirely dependent upon the nature of the index that you want to maintain. 

You implement a key extractor by writing a function that extracts the necessary information from a primary record's key or data. This function must conform to a specific prototype, and it must be provided as a callback to the associate() method. 

For example, suppose your primary database records contain data that uses the following structure: 

```
[Serializable]
public class Vendor
{
    public string VendorName;
    public string Street;
    public string City;
    public string State;
    public string Zip;
    public string PhoneNumber;
    public string SalesRep;
    public string SalesRepPhone;
}

```
Further suppose that you want to be able to query your primary database based on the name of a sales representative. Then you would write a function that looks like this: 

TODO: FINISH
