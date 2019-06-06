namespace Figaro    
{
    /// <summary>
    /// A basic utility object for indexing.
    /// </summary>
    public struct XmlIndex
    {
        /// <summary>
        /// Gets or sets the namespace of the node to be indexed. The default namespace is selected by passing
        /// an empty string for the namespace.
        /// </summary>
        /// <value>Gets or sets the namespace of the node to be indexed.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the element or attribute node to be indexed.
        /// </summary>
        /// <value>The name of the element or attribute node to be indexed.</value>
		public string NodeName{get; set; }

        /// <summary>
        /// Gets or sets a comma-separated list of strings that represent one or more indexing strategies for the index.
        /// </summary>
        /// <value>
        /// A comma-separated list of strings representing one or more indexing strategies for the index.
        /// </value>
        /// <remarks>
        /// The strings must contain the following information in the following order:
        /// <code lang="Xml">
        /// unique-{path type}-{node type}-{key type}-{syntax}
        /// </code>
        /// See [Using Indices](xref:using-indices.md) for more information.
        /// </remarks>
        public string Index { get; set; }
    }
}
