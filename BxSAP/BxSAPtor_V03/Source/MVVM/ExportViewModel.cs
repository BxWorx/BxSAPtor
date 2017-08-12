using System;
using System.ComponentModel.Composition;

namespace BxSAPtor.MVVM
	{
		[MetadataAttribute]
		[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
		public class ExportViewModel : ExportAttribute
		{
				public string Name { get; private set; }

				public ExportViewModel(string name, bool isStatic) : base("ViewModel")
				{
						Name = name;
				}
		}

		//public interface IViewModelMetadata
		//{
		//		string Name { get; }
		//}
}
