#if NET35
namespace System.Diagnostics.CodeAnalysis
{
	///<summary>
	///Initializes a new instance of the System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute class.	
	///</summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
	public class ExcludeFromCodeCoverageAttribute : Attribute
	{
		///<summary>
		///Gets or sets the justification for excluding the member from code coverage.
		///</summary>
		public string Justification { get; set; }
	}
}
#endif
