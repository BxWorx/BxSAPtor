using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BxSAPtor_V03.Source.MVVM
	{
		class Class1
			{
			}


	/// <summary>
	/// A design- vs run-time wrapper catalog which provides prioritizing and filtering based on the DesignTimeExportAttribute
	/// </summary>
	public class DRPartCatalog : ComposablePartCatalog
		{
		private ComposablePartCatalog _catalog;
		private bool _designTime = false;
		public bool DesignTime
			{
			get
				{
				return _designTime;
				}
			}

		/// <summary> 
		/// Creates a new DRPartCatalog around an existing catalog. 
		/// The catalog that this class decorates is provides in the constructor 
		/// paramater "catalog". The "DesignTime" property is set to false.
		/// <summary> 
		public DRPartCatalog(ComposablePartCatalog catalog)
			{
			_catalog = catalog;
			}

		/// <summary> 
		/// Creates a new DRPartCatalog around an existing catalog. 
		/// The catalog that this class decorates is provides in the constructor 
		/// paramater "catalog". The "designTime" parameter controls sets the "DesignTime"
		/// property which is used to control the import satisfaction
		/// <summary> 
		public DRPartCatalog(ComposablePartCatalog catalog, bool designTime)
			{
			_designTime = designTime;
			_catalog = catalog;
			}

		public override System.Linq.IQueryable<ComposablePartDefinition>
			{
				 get { return _catalog.Parts; }
			}

/// <summary>
/// Returns the exports in the catalog that match a given definition of an import.
/// This method is called every time MEF tries to satisfy an import.
///</summary>
public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition importDef)
	{
	// If ImportMany is defined and we are at design-time the use the standard bahavior and return
	// all matching exports.
	if (importDef.Cardinality == ImportCardinality.ZeroOrMore && DesignTime)
		{
		return base.GetExports(importDef);
		}

	//otherwise we have to do our own logic
	IList<Tuple<ComposablePartDefinition, ExportDefinition>> result
			= new List<Tuple<ComposablePartDefinition, ExportDefinition>>();

	// Walk through all parts in that catalog...
	foreach (ComposablePartDefinition partDef in Parts)
		{
		// ... and for each part, examine if any export definition matches the
		// requested import definition.
		foreach (ExportDefinition exportDef in partDef.ExportDefinitions)
			{
			if (importDef.IsConstraintSatisfiedBy(exportDef))
				{
				//ok the import definition is satisfied
				Tuple<ComposablePartDefinition, ExportDefinition> matchingExport = null;
				matchingExport = new Tuple<ComposablePartDefinition, ExportDefinition>(partDef, exportDef);
				object designTimeMetadata;
				exportDef.Metadata.TryGetValue("DesignTime", out designTimeMetadata);
				//if DesignTimeAttribute is set then ToBool returns the assigend value
				//ohterwise it returns false 
				bool hasDesignTimeAttribute = ToBool(designTimeMetadata);

				//If ImportMany is defined and we are at run-time then filter out
				//design-time exports
				if (importDef.Cardinality == ImportCardinality.ZeroOrMore)
					{
					if (DesignTime || !hasDesignTimeAttribute)
						result.Add(matchingExport);
					}
				//If Import or Import(AllowDefault=true) then prioritize design-time exports
				//at design-time
				else
					{
					if (DesignTime)
						{
						if (result.Count == 0) //also allow run-time exports at design-time
							result.Add(matchingExport);
						else if (hasDesignTimeAttribute) //but prioritize design time data at design time
							{
							result.Clear();
							result.Add(matchingExport);
							}
						}
					else
						{
						if (!hasDesignTimeAttribute) //only allow run-time exports at run-time
							result.Add(matchingExport);
						}
					}
				}
			}
		}
	return result;
	}

/// <summary>
/// Converts an untyped value into a bool. If the object is null
/// or cannot be converted to an bool, returns false.
/// </summary>
protected static bool ToBool(object value)
	{
	if (value == null)
		{
		return false;
		}

	bool result = false;
	bool.TryParse(value.ToString(), out result);
	return result;
	}
 }



	}



