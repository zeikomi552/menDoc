using menDoc.Common.Enums;
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
		#region アクセス修飾子[Accessor]プロパティ
		/// <summary>
		/// アクセス修飾子[Accessor]プロパティ用変数
		/// </summary>
		AccessModifier _Accessor = new AccessModifier();
		/// <summary>
		/// アクセス修飾子[Accessor]プロパティ
		/// </summary>
		public AccessModifier Accessor
		{
			get
			{
				return _Accessor;
			}
			set
			{
				if (!_Accessor.Equals(value))
				{
					_Accessor = value;
					NotifyPropertyChanged("Accessor");
				}
			}
		}
		#endregion
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
		#region 関数リスト[MethodItems]プロパティ
		/// <summary>
		/// 関数リスト[MethodItems]プロパティ用変数
		/// </summary>
		ModelList<ClassMethodM> _MethodItems = new ModelList<ClassMethodM>();
		/// <summary>
		/// 関数リスト[MethodItems]プロパティ
		/// </summary>
		public ModelList<ClassMethodM> MethodItems
		{
			get
			{
				return _MethodItems;
			}
			set
			{
				if (_MethodItems == null || !_MethodItems.Equals(value))
				{
					_MethodItems = value;
					NotifyPropertyChanged("MethodItems");
				}
			}
		}
		#endregion
		#region 関係情報リスト[RelationItems]プロパティ
		/// <summary>
		/// 関係情報リスト[RelationItems]プロパティ用変数
		/// </summary>
		ModelList<ClassRelationM> _RelationItems = new ModelList<ClassRelationM>();
		/// <summary>
		/// 関係情報リスト[RelationItems]プロパティ
		/// </summary>
		public ModelList<ClassRelationM> RelationItems
		{
			get
			{
				return _RelationItems;
			}
			set
			{
				if (_RelationItems == null || !_RelationItems.Equals(value))
				{
					_RelationItems = value;
					NotifyPropertyChanged("RelationItems");
				}
			}
		}
		#endregion

	}
}
