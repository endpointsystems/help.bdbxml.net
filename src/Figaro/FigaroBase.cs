// ReSharper disable UnusedParameter.Local
// ReSharper disable CommentTypo


namespace Figaro
{
/*
#pragma warning disable 1591
    public class FigaroBase
#pragma warning restore 1591
    {
#pragma warning disable 1591
    }
*/

    /// <summary>
    /// General product information for the Figaro XML Database product installed.
    /// </summary>
    public struct FigaroProductInfo
    {
        /// <summary>
        /// Gets the installed edition of the Figaro product.
        /// </summary>
        public static FigaroProductEdition ProductEdition { get; private set; }
    }

}
