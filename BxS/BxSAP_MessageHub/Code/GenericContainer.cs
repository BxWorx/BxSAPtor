using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal class GenericContainer
			{

					private Dictionary<Type, object> MyDict;

					internal GenericContainer()
						{
							this.MyDict = new Dictionary<Type, object>();
						}

					//internal void NewEntry<T>() { this.MyDict.Add() }


	}

	public static class GenericLists
{
		private static Dictionary<Type, object> MyDict = new Dictionary<Type, object>();
		public static void NewEntry<T>()
		{
				MyDict.Add(typeof(T), new List<T>());
		}

		public static List<T> GetEntry<T>()
		{
				return (List<T>)MyDict[typeof(T)];
		}

		public static void RemoveEntry<T>()
		{
				MyDict.Remove(typeof(T));
		}

}
	}
