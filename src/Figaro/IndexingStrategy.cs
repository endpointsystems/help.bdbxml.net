// ReSharper disable UnusedParameter.Local

namespace Figaro    //namespace Figaro    
{
    /// <summary>
    /// A utility class geared toward aiding in the creation of indexes for
    /// <see cref="Container"/> objects.
    /// </summary>
    /// <details>
    /// Proper use of indices can significantly reduce the time required to execute a particular
    /// XQuery expression. Indices are specified in four parts: path type, node type, key type,
    /// and uniqueness. Each part of the index contains one or more static values used in the index
    /// expected by Figaro.
    /// </details>
    /// <threadsafety static="true" instance="true"/>
    /// <seealso cref="Container"/>
    public class IndexingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the IndexingStrategy class.
        /// </summary>
        /// <param name="index">The <see cref="System.String"/> version of the index.</param>
        /// <remarks>
        /// This constructor will attempt to parse the incoming <c>index</c> into <see cref="IndexingStrategy"/> properties;
        /// if unsuccessful, an <see cref="System.ArgumentException"/> will be thrown.
        /// </remarks>
        public IndexingStrategy(string index) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexingStrategy"/> class.
        /// </summary>
        public IndexingStrategy() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexingStrategy"/> class.
        /// </summary>
        /// <param name="unique">if <c>true</c>, indicates that the values in the index are unique.</param>
        /// <param name="pathType">sets the <seealso cref="IndexPathType"/> property of the index.</param>
        /// <param name="nodeType">indicates whether the node is an element, attribute, or metadata.</param>
        /// <param name="keyType">sets the types of query supported by the index.</param>
        /// <param name="indexValueSyntax">sets the intended datatype of the item being indexed.</param>
        public IndexingStrategy(
            bool unique, IndexPathType pathType,
            IndexNodeType nodeType, IndexKeyType keyType,
            XmlDatatype indexValueSyntax)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the indexed value must be unique within the container. For example,
        /// you can index an attribute and declare that index to be unique. This means the value
        /// indexed for the attribute must be unique within the container.
        /// </summary>
        /// <value>The boolean value of whether the indexed value must be unique.</value>
        /// <remarks>
        /// By default, indexed values are not unique; you must explicitly declare uniqueness for
        /// your indexing strategy in order for it to be enforced.
        /// </remarks>
        public bool Unique { get; set; }

        /// <summary>
        /// Gets or sets the strategy for the path to the indexed nodes.
        /// </summary>
        /// <value>The index type.</value>
        /// <remarks>
        /// If you think of an XML document as a tree of nodes, then there are two types of path elements
        /// in the tree. One type is just a node, such as an element or attribute within the document. The
        /// other type is any location in a path where two nodes meet. The path type, then, identifies the
        /// path element type that you want indexed. Path type <c>node</c> indicates that you want to index
        /// a single node in the path. Path type <c>edge</c> indicates that you want to index the portion of the path where
        /// two nodes meet.
        /// <para>
        /// Of the two of these, the BDB XML query processor prefers <c>edge</c>-type indices because they
        /// are more specific than a <c>node</c>-type index. This means that the query processor will
        /// use an <c>edge</c>-type index over a <c>node</c>-type if both indices provide similar information.
        /// </para>
        /// </remarks>
        /// <seealso cref="IndexPathType"/>
        public IndexPathType PathType { get; set; }

        /// <summary>
        /// Gets or sets the type of node being indexed.
        /// </summary>
        /// <value>The node type of the index.</value>
        /// <remarks>
        /// BDB XML can index three types of nodes: element, attribute, or metadata. Metadata nodes are,
        /// of course, indices set for a document's metadata content.
        /// <para>
        /// Metadata nodes are found only in a document's metadata content. This indices improve
        /// the performance of querying for documents based on metadata information. If you are
        /// declaring a metadata node, you cannot use a path type
        /// of <see cref="IndexPathType.EdgePath"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="IndexNodeType"/>
        public IndexNodeType NodeType { get; set; }

        /// <summary>
        /// Gets or sets what sort of test the index supports.
        /// </summary>
        /// <value>The key type supported by the index.</value>
        /// <remarks>
        /// See the definition of <see cref="IndexKeyType"/> for details
        /// regarding the key types.
        /// </remarks>
        /// <seealso cref="IndexKeyType"/>
        public IndexKeyType KeyType { get; set; }

        /// <summary>
        /// Gets or sets the data type of the index value.
        /// values are compared.
        /// </summary>
        /// <value>The index syntax.</value>
        /// <remarks>
        /// There are a large number of syntax types available to you, such
        /// as <see cref="XmlDatatype.DayTimeDuration"/>,
        /// <see cref="XmlDatatype.Boolean"/>, or
        /// <see cref="XmlDatatype.Date"/>.
        /// </remarks>
        /// <seealso cref="XmlDatatype"/>
        public XmlDatatype IndexValueSyntax { get; set; }

        /// <summary>
        /// Returns the index strategy.
        /// </summary>
        /// <returns>
        /// The index strategy in the expected <c>unique-{path type}-{node type}-{key type}-{syntax}</c> syntax.
        /// </returns>
        public override string ToString()
        {
            return string.Empty;
        }

        /// <summary>
        /// Checks the given string as to whether or not it is a valid indexing strategy.
        /// </summary>
        /// <param name="strategy">The index strategy.</param>
        /// <returns><c>true</c> if the string is a valid indexing strategy; otherwise, <c>false</c> is returned.</returns>
        public static bool IsValidIndexingStrategy(string strategy)
        {
            return false;
        }
    }
}