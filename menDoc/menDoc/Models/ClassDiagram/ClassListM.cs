using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ClassDiagram
{
    public class ClassListM : ModelBase
    {
		public IEnumerator<ClassM> GetEnumerator()
		{
			foreach (var item in this.ClassItems)
			{
				yield return item;  // ここでパーツを返す
			}
		}

		#region クラスのリスト[ClassItems]プロパティ
		/// <summary>
		/// クラスのリスト[ClassItems]プロパティ用変数
		/// </summary>
		ModelList<ClassM> _ClassItems = new ModelList<ClassM>();
		/// <summary>
		/// クラスのリスト[ClassItems]プロパティ
		/// </summary>
		public ModelList<ClassM> ClassItems
		{
			get
			{
				return _ClassItems;
			}
			set
			{
				if (_ClassItems == null || !_ClassItems.Equals(value))
				{
					_ClassItems = value;
					NotifyPropertyChanged("ClassItems");
				}
			}
		}
		#endregion

	}
}
