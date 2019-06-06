// ReSharper disable UnusedParameter.Global
// ReSharper disable UnusedParameter.Local
// ReSharper disable CommentTypo
using System;
using System.Xml;
using System.Xml.Linq;

namespace Figaro    
{
    /// <summary>
    /// Encapsulates the value of a node in an Xml document. The type of the value may be any one of the
    /// enumerated types in the <see cref="XmlValueType"/> enumeration.
    /// </summary>
    /// <remarks>
    /// The <see cref="XmlValue"/> class implements a set of methods that test if
    /// the <see cref="XmlValue"/> is of a named type. The <see cref="XmlValue"/>
    /// class also implements a set of methods that return the <see cref="XmlValue"/> as a value of a specified type. If the
    /// <see cref="XmlValue"/> is of type variable and no query context is provided when calling the test or cast methods, or no
    /// binding can be found for the variable, an exception is thrown.
    /// <para>
    /// In addition to type conversion, the <see cref="XmlValue"/> class also provides DOM-like navigation functions. Using
    /// these, you can retrieve features from the underlying document, such as the parent, an attribute, or a
    /// child of the current node.
    /// </para>
    /// <para>
    ///     For information about XML and .NET datatypes, see "Mapping XML Data Types to CLR Types"  
    ///     in the section "Type Support in the System.Xml Classes" of the MSDN documentation.
    /// </para>
    /// </remarks>
    /// <seealso cref="XmlDocument"/>
    /// <seealso cref="XmlData"/>
    /// <seealso href="https://msdn.microsoft.com/en-us/library/2bcctyt8.aspx">XML Documents and Data</seealso>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/xa669bew.aspx">Mapping XML Data Types to CLR Types</seealso>
    /// <seealso href="https://msdn.microsoft.com/en-us/library/fbe7e4cy(v=vs.110).aspxx">Type Support in the System.Xml Classes</seealso>
    /// <seealso href="https://msdn.microsoft.com/en-us/library/hh156542(v=vs.110).aspx">.NET Framework Development Guide</seealso>
    /// <threadsafety static="false" instance="false"/>
    public sealed class XmlValue : IDisposable
    {
        /// <summary>
        /// Convert an <see cref="XmlValue"/> to an <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="val">The <see cref="XmlValue"/> to convert.</param>
        /// <returns>An <see cref="XmlReader"/> instance, or <c>null</c> upon failure.</returns>
        /// <exception cref="InvalidCastException">If a <c>null</c> value or value that is not a <c>string</c> or <c>node</c> XML datatype.</exception>
        public static explicit operator XmlReader(XmlValue val)
    {
        return null;
    }
        /// <summary>
        /// Convert an <see cref="XmlValue"/> to an <see cref="XDocument"/>.
        /// </summary>
        /// <param name="val">The <see cref="XmlValue"/> to convert.</param>
        /// <returns>An <see cref="XDocument"/> instance, or <c>null</c> upon failure.</returns>
        /// <exception cref="InvalidCastException">If a <c>null</c> value or value that is not a <c>string</c> or <c>node</c> XML datatype.</exception>
        public static explicit operator XDocument(XmlValue val)
    {
        return null;
    }


        /// <summary>
        /// Initializes a new instance of the XmlValue class of the URI and type name provided, 
        /// converting the string value to the specified type.
        /// </summary>
        /// <param name="typeUri">the URI defining the type.</param>
        /// <param name="typeName">the name of the type.</param>
        /// <param name="data">the <see cref="XmlValue"/> object value.</param>
        public XmlValue(string typeUri, string typeName, string data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the XmlValue class using the specified <see cref="decimal"/> value.
        /// </summary>
        /// <param name="value">the <see cref="decimal"/> value.</param>
        public XmlValue(decimal value)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the XmlValue class using the specified <see cref="short"/> value.
        /// </summary>
        /// <param name="value">the <see cref="short"/> value.</param>
        public XmlValue(short value)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the XmlValue class using the specified <see cref="int"/> value.
        /// </summary>
        /// <param name="value">the <see cref="int"/> value.</param>
        public XmlValue(int value)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the XmlValue class using the specified <see cref="long"/> value.
        /// </summary>
        /// <param name="value">the <see cref="long"/> value.</param>
        public XmlValue(long value)
        {            
        }


        /// <summary>
        /// Initializes a new instance of the XmlValue class with
        /// a <see cref="System.String"/> value.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> value for the object.</param>
        public XmlValue(string value) { }
        /// <summary>
        /// Initializes a new instance of the XmlValue class with a <see cref="bool"/> value.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value for the object.</param>
        public XmlValue(bool value) { }
        /// <summary>
        /// Initializes a new instance of the XmlValue class with a <see cref="System.Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="System.Double"/> value for the object.</param>
        public XmlValue(double value) { }

        /// <summary>
        /// Initializes a new instance of the XmlValue class with
        /// an <see cref="XmlDocument"/> object.
        /// </summary>
        /// <param name="value">The <see cref="XmlDocument"/> value for the object.</param>
        public XmlValue(XmlDocument value) { }
        /// <summary>
        /// Initializes a new instance of the XmlValue class using the specified data and data type.
        /// <see cref="System.String"/> representation of that value.
        /// </summary>
        /// <param name="valueType">The specific value type to initialize with.</param>
        /// <param name="data">The <see cref="System.String"/> representation of the value type.</param>
        public XmlValue(XmlValueType valueType, string data) { }

        /// <summary>
        /// Initializes a new instance of the XmlValue class using the specified <see cref="XmlData"/> and data type.
        /// </summary>
        /// <param name="valueType">The specific value type to initialize with.</param>
        /// <param name="data">The <see cref="XmlData"/> representation
        /// of the value type.</param>
        public XmlValue(XmlValueType valueType, XmlData data) {}
        /// <summary>
        /// Determines if two <see cref="XmlValue"/> objects represent
        /// the same value.
        /// </summary>
        /// <param name="value">The <see cref="XmlValue"/> to compare.</param>
        /// <returns>
        /// Returns <c>true</c> if the two <see cref="XmlValue"/>
        /// objects represent the same value.
        /// </returns>
        public bool Equals(XmlValue value)
        {
            return true;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="XmlValue"/> object contains
        /// the requested type.
        /// </summary>
        /// <param name="valueType">The <see cref="XmlValueType"/> to check against.</param>
        /// <returns>
        /// Returns <c>true</c> if the <see cref="XmlValue"/>
        /// is the requested type.
        /// </returns>
        public bool IsType(XmlValueType valueType)
        {
            return true;
        }

        /// <summary>
        /// Gets a string handle of a node representing an
        /// <see cref="XmlValue"/>,
        /// which can be used to construct a new object representing the same node, at a later time,
        /// using <see cref="Figaro.Container.GetNode(System.String,Figaro.RetrievalModes)"/>.
        /// The handle returned encodes its document ID; however, it does not include its container. Matching a
        /// node handle to container is the responsibility of the caller.
        /// </summary>
        /// <value>The node handle.</value>
        /// <remarks>
        /// Node handles are guaranteed to remain stable in the absence of modifications to a document. If
        /// a document is modified, a handle may cease to exist, or may belong to a different node.
        /// <para>
        /// If the node type is not <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </para>
        /// </remarks>
        /// <seealso href="Container.GetNode"/>
        /// <seealso cref="System.InvalidOperationException"/>
        public string NodeHandle { get { return string.Empty; } }

        /// <summary>
        /// Gets the name of the node contained in this <see cref="XmlValue"/>.
        /// If the node type is not <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an
        /// invalid value is thrown.
        /// </summary>
        /// <value>Gets the name of the Xml node.</value>
        public string NodeName { get { return string.Empty; } }

        /// <summary>
        /// Gets the node's value.If the node type is not
        /// <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the value of the node as a <see cref="System.String"/>.</value>
        public string NodeValue { get { return string.Empty; } }

        /// <summary>
        /// Gets the URI used for the node's namespace. If the node type is not
        /// <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the URI used for the node's namespace.</value>
        public string NamespaceContainerSettings { get { return string.Empty; } }

        /// <summary>
        /// Gets the prefix set for the node's namespace. If the node type is not
        /// <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the prefix set for the node's namespace.</value>
        public string Prefix { get { return string.Empty; } }

        /// <summary>
        /// Gets the node's local name. For example, if a node has the namespace prefix, "prefix,"
        /// and its qualified name is prefix:name, then 'name' is the local name. If the node type is not
        /// <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the node's local name.</value>
        public string LocalName { get { return string.Empty; } }

        /// <summary>
        /// Gets the <see cref="XmlValueType"/> value for the node's type. If the node type is not
        /// <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>
        /// Gets the <see cref="XmlValueType"/> value for the node's type.
        /// </value>
        public XmlValueType NodeType { get { return XmlValueType.UntypedAtomic; } }

        /// <summary>
        /// Gets the current node's parent. If the node has no parent, a <see langword="null"/> node is returned. If the node
        /// type is not <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the current node's parent, if one exists.</value>
        public XmlValue ParentNode { get { return new XmlValue(false); } }

        /// <summary>
        /// Gets the current node's first child node. If the node has no children, a <see langword="null"/> node is returned.
        /// If the node type is not <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the current node's first child node.</value>
        public XmlValue FirstChild { get { return new XmlValue(false); } }

        /// <summary>
        /// Gets the current node's last child node. If the node has no children, a <see langword="null"/> node is returned.
        /// If the node type is not <see cref="XmlValueType.Node"/>,
        /// an <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the current node's last child node.</value>
        public XmlValue LastChild { get { return new XmlValue(false); } }

        /// <summary>
        /// Gets the sibling node immediately preceding this node in the document. If the current node
        /// had no siblings preceding it in the document, a <c>null</c> node is returned.If the node type is
        /// not <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the current node's previous sibling node.</value>
        /// *
        public XmlValue PreviousSibling { get { return new XmlValue(false); } }

        /// <summary>
        /// Gets the sibling node immediately following this node in the document. If the current
        /// node had no siblings following it in the document, a <c>null</c> node is returned. If the
        /// node type is not <see cref="XmlValueType.Node"/>,
        /// an <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>Gets the current node's next sibling node.</value>
        public XmlValue NextSibling { get { return new XmlValue(false); } }

        /// <summary>
        /// Gets returns the document element node that contains
        /// this attribute node, if the current node is an attribute node. If the node type is not
        /// <see cref="XmlValueType.Node"/>, an
        /// <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>
        /// Gets the containing document element node, if <see cref="XmlValue"/> is
        /// of type <see cref="XmlValueType.Node"/>.
        /// </value>
        public XmlValue OwnerElement { get { return new XmlValue(false); } }

        /// <summary>
        /// Gets an <see cref="XmlResults"/> that contains all of the attributes appearing on
        /// this node.If the node type is not <see cref="XmlValueType.Node"/>,
        /// an <see cref="System.InvalidOperationException"/> for an invalid value is thrown.
        /// </summary>
        /// <value>
        /// Gets a <see cref="XmlResults"/> containing all of the attributes appearing in
        /// this node.
        /// </value>
        public XmlResults Attributes { get { return null; } }

        /// <summary>
        /// Gets the URI associated with the type of the <see cref="XmlValue"/>.
        /// </summary>
        /// <value>Gets the URI associated with this type.</value>
        public string TypeUri { get { return string.Empty; } }

        /// <summary>
        /// Gets the string name of the type of the <see cref="XmlValue"/>.
        /// </summary>
        /// <value>Gets the string name of this type.</value>
        public string TypeName { get { return string.Empty; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="XmlValue"/> is a number.
        /// </summary>
        /// <value>
        /// Gets the boolean condition indicating this object type is a number.
        /// </value>
        public bool IsNumber { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="XmlValue"/> is a string.
        /// </summary>
        /// <value>
        /// Gets the <see cref="bool"/> value determining whether
        /// <see cref="XmlValue"/> is a string.
        /// </value>
        public bool IsString { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="XmlValue"/> is a boolean.
        /// </summary>
        /// <value>
        /// Gets the <see cref="bool"/> value determining whether
        /// <see cref="XmlValue"/> is a boolean.
        /// </value>
        public bool IsBoolean { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether the <see cref="XmlValue"/> contains binary data.
        /// </summary>
        /// <value>
        /// Gets the <see cref="bool"/> value determining whether
        /// <see cref="XmlValue"/> contains binary data.
        /// </value>
        public bool IsBinary { get { return false; } }

        /// <summary>
        /// Gets a value indicating whether the value is a <see cref="XmlNode"/>.
        /// </summary>
        /// <value>
        /// Gets the <see cref="bool"/> value determining whether
        /// <see cref="XmlValue"/> is an Xml node.
        /// </value>
        public bool IsNode { get { return false; }}

        /// <summary>
        /// Gets a value indicating whether the value is a number.
        /// </summary>
        /// <value>
        /// Gets the <see cref="System.Double"/> value of <see cref="XmlValue"/>.
        /// </value>
        public double AsNumber { get { return 0; } }

        /// <summary>
        /// Gets the <see cref="System.String"/> value of <see cref="XmlValue"/>.
        /// </summary>
        /// <value>
        /// Gets the <see cref="System.String"/> value of <see cref="XmlValue"/>.
        /// </value>
        public string AsString { get { return string.Empty; } }

        /// <summary>
        /// Gets a value indicating whether the value is a boolean.
        /// </summary>
        /// <value>
        /// Gets the <see cref="bool"/> value of <see cref="XmlValue"/>.
        /// </value>
        public bool AsBoolean { get { return false; } }

        /// <summary>
        /// Gets the <see cref="XmlData"/> value of <see cref="XmlValue"/> as a binary datatype.
        /// </summary>
        /// <value>
        /// Gets the <see cref="XmlData"/> value of <see cref="XmlValue"/> as a binary datatype.
        /// </value>
        public XmlData AsBinary { get { return null; } }

        /// <summary>
        /// Gets the <see cref="XmlDocument"/> value of <see cref="XmlValue"/>.
        /// </summary>
        /// <value>
        /// Gets the <see cref="XmlDocument"/> value of <see cref="XmlValue"/>.
        /// </value>
        public XmlDocument AsDocument { get { return null; } }

        /// <summary>
        /// Gets the <see cref="System.String"/> representation of the underlying value.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <remarks>
        /// <note type="caution">
        /// This override returns the <see cref="string"/> value for <see cref="XmlValueType.Binary"/>,
        /// <see cref="XmlValueType.Boolean"/>, and number values. If the underlying
        /// value is <c>null</c>, a <c>null</c> value is returned. All other values return their respective
        /// <see cref="XmlValueType"/>.
        /// </note>
        /// </remarks>
        /// <value>The underlying <see cref="System.String"/> value, if it can be reached.</value>
        public override string ToString()
        {
            return string.Empty;
        }


        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            return;
        }

        #endregion
    }
}
