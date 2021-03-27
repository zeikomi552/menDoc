using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ClassDiagram
{
    public class ClassM : ModelBase
    {
		#region クラス名[Name]プロパティ
		/// <summary>
		/// クラス名[Name]プロパティ用変数
		/// </summary>
		string _Name = string.Empty;
		/// <summary>
		/// クラス名[Name]プロパティ
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (!_Name.Equals(value))
				{
					_Name = value;
					NotifyPropertyChanged("Name");
				}
			}
		}
		#endregion
		#region クラスの説明[Description]プロパティ
		/// <summary>
		/// クラスの説明[Description]プロパティ用変数
		/// </summary>
		string _Description = string.Empty;
		/// <summary>
		/// クラスの説明[Description]プロパティ
		/// </summary>
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if (!_Description.Equals(value))
				{
					_Description = value;
					NotifyPropertyChanged("Description");
				}
			}
		}
		#endregion
		#region 変数リスト[ParameterItems]プロパティ
		/// <summary>
		/// 変数リスト[ParameterItems]プロパティ用変数
		/// </summary>
		ModelList<ClassParamM> _ParameterItems = new ModelList<ClassParamM>();
		/// <summary>
		/// 変数リスト[ParameterItems]プロパティ
		/// </summary>
		public ModelList<ClassParamM> ParameterItems
		{
			get
			{
				return _ParameterItems;
			}
			set
			{
				if (_ParameterItems == null || !_ParameterItems.Equals(value))
				{
					_ParameterItems = value;
					NotifyPropertyChanged("ParameterItems");
				}
			}
		}
		#endregion
		#region 関数リスト[MthodItems]プロパティ
		/// <summary>
		/// 関数リスト[MthodItems]プロパティ用変数
		/// </summary>
		ModelList<ClassMethodM> _MthodItems = new ModelList<ClassMethodM>();
		/// <summary>
		/// 関数リスト[MthodItems]プロパティ
		/// </summary>
		public ModelList<ClassMethodM> MthodItems
		{
			get
			{
				return _MthodItems;
			}
			set
			{
				if (_MthodItems == null || !_MthodItems.Equals(value))
				{
					_MthodItems = value;
					NotifyPropertyChanged("MthodItems");
				}
			}
		}
		#endregion
	}
}
