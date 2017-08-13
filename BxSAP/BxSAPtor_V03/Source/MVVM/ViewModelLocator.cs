using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Dynamic;
using System.Linq;
using BxSAPtor_V03.Source.MVVM;
using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor.MVVM
	{
		[TypeDescriptionProvider(typeof(ModelViewMapDescriptionProvider))]
		public class VMLocator : DynamicObject, ITypedList
			{
				#region Declarations

					private static Dictionary<string, object> _dictionary	= new Dictionary<string, object>();

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region Properties

					[ImportMany("ViewModel", AllowRecomposition = true)]
					private IEnumerable<Lazy<object, IViewModelMetadata>>	ViewModels	{ get; set; }
					public	int																						Count				{ get { return _dictionary.Count; } }

				#endregion


					#region DynamicObject

					public override bool TryGetMember(GetMemberBinder binder, out object result)
					{
							string name = binder.Name;
							if (!_dictionary.TryGetValue(name, out result))
									try
									{
											if (ViewModels == null)
											{
													AggregateCatalog	agcatalog	= new AggregateCatalog();
													agcatalog.Catalogs.Add(	new AssemblyCatalog(typeof(VMLocator).Assembly)			);
													agcatalog.Catalogs.Add(	new AssemblyCatalog(Assembly.GetCallingAssembly())	);
													CompositionContainer	compositionContainer = new CompositionContainer(agcatalog);
													compositionContainer.ComposeParts(this);
											}

											_dictionary[binder.Name] = (result = ViewModels.Single(v => v.Metadata.Name.Equals(name)).Value);
											return result != null;
									}
									catch (Exception ex)
									{
											Console.WriteLine(ex);
									}
							return true;
					}

					public override bool TrySetMember(SetMemberBinder binder, object value)
					{
							_dictionary[binder.Name] = value;
							return true;
					}
					#endregion

					#region ITypedList implementation
					public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
						{
							PropertyDescriptorCollection	result = new PropertyDescriptorCollection(null);
							foreach (KeyValuePair<string, object> m in _dictionary)
									result.Add(new ModelViewPropertyDescriptor(m.Key, m.Value));
							return result;
						}

					public string GetListName(PropertyDescriptor[] listAccessors)
					{
							return "Models";
					}
					#endregion

					#region ModelViewPropertyDescriptor
					/// <summary>
					/// A property descriptor which exposes an ICommand instance
					/// </summary>
					internal class ModelViewPropertyDescriptor : PropertyDescriptor
					{
							internal object ModelView { get; set; }

							/// <summary>
							/// Construct the descriptor
							/// </summary>
							/// <param name="command"></param>
							public ModelViewPropertyDescriptor(string name, object modelView) : base(name, null)
							{
									ModelView = modelView;
							}

							/// <summary>
							/// Always read only in this case
							/// </summary>
							public override bool IsReadOnly
							{
									get { return true; }
							}

							/// <summary>
							/// Nope, it's read only
							/// </summary>
							/// <param name="component"></param>
							/// <returns></returns>
							public override bool CanResetValue(object component)
							{
									return false;
							}

							/// <summary>
							/// Not needed
							/// </summary>
							public override Type ComponentType
							{
									get { return ModelView.GetType(); }
							}

							/// <summary>
							/// Get the ICommand from the parent command map
							/// </summary>
							/// <param name="component"></param>
							/// <returns></returns>
							public override object GetValue(object component)
							{
									return ModelView;
							}

							/// <summary>
							/// Get the type of the property
							/// </summary>
							public override Type PropertyType
							{
									get { return ModelView.GetType(); }
							}

							/// <summary>
							/// Not needed
							/// </summary>
							/// <param name="component"></param>
							public override void ResetValue(object component)
							{
									throw new NotImplementedException();
							}

							/// <summary>
							/// Not needed
							/// </summary>
							/// <param name="component"></param>
							/// <param name="value"></param>
							public override void SetValue(object component, object value)
							{
									throw new NotImplementedException();
							}

							/// <summary>
							/// Not needed
							/// </summary>
							/// <param name="component"></param>
							/// <returns></returns>
							public override bool ShouldSerializeValue(object component)
							{
									return true;
							}

							public override string ToString()
							{
									return this.Name;
							}
					}
					#endregion

					#region ModelViewMapDescriptionProvider
					/// <summary>
					/// Expose the dictionary entries of a CommandMap as properties
					/// </summary>
					private class ModelViewMapDescriptionProvider : TypeDescriptionProvider
					{
							/// <summary>
							/// Standard constructor
							/// </summary>
							public ModelViewMapDescriptionProvider() : this(TypeDescriptor.GetProvider(typeof(VMLocator)))
							{
							}

							/// <summary>
							/// Construct the provider based on a parent provider
							/// </summary>
							/// <param name="parent"></param>
							public ModelViewMapDescriptionProvider(TypeDescriptionProvider parent) : base(parent)
							{
							}

							/// <summary>
							/// Get the type descriptor for a given object instance
							/// </summary>
							/// <param name="objectType">The type of object for which a type descriptor is requested</param>
							/// <param name="instance">The instance of the object</param>
							/// <returns>A custom type descriptor</returns>
							public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
							{
									return new ModelViewDescriptor(base.GetTypeDescriptor(objectType, instance));
							}
					}
					#endregion

					#region ModelViewDescriptor
					/// <summary>
					/// This class is responsible for providing custom properties to WPF - in this instance
					/// allowing you to bind to commands by name
					/// </summary>
					private class ModelViewDescriptor : CustomTypeDescriptor
					{
							/// <summary>
							/// Store the command map for later
							/// </summary>
							/// <param name="descriptor"></param>
							/// <param name="map"></param>
							public ModelViewDescriptor(ICustomTypeDescriptor descriptor) : base(descriptor)
							{
							}

							/// <summary>
							/// Get the properties for this command map
							/// </summary>
							/// <returns>A collection of synthesized property descriptors</returns>
							public override PropertyDescriptorCollection GetProperties()
							{
									PropertyDescriptorCollection result = new PropertyDescriptorCollection(null);
									foreach (KeyValuePair<string, object> m in _dictionary)
											result.Add(new ModelViewPropertyDescriptor(m.Key, m.Value));
									return result;
							}
					}
					#endregion
			}
	}
