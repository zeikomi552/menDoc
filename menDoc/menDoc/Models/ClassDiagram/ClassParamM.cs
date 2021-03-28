using menDoc.Common.Enums;
using menDoc.Common.Utilities;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ClassDiagram
{
    public class ClassParamM : ModelBase
    {
		#region アクセス修飾子[Accessor]プロパティ
		/// <summary>
		/// アクセス修飾子[Accessor]プロパティ用変数
		/// </summary>
		AccessModifier _Accessor = AccessModifier.Public;
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
		#region 型名[TypeName]プロパティ
		/// <summary>
		/// 型名[TypeName]プロパティ用変数
		/// </summary>
		string _TypeName = string.Empty;
		/// <summary>
		/// 型名[TypeName]プロパティ
		/// </summary>
		public string TypeName
		{
			get
			{
				return _TypeName;
			}
			set
			{
				if (!_TypeName.Equals(value))
				{
					_TypeName = value;
					NotifyPropertyChanged("TypeName");
				}
			}
		}
		#endregion
		#region 変数名[ValueName]プロパティ
		/// <summary>
		/// 変数名[ValueName]プロパティ用変数
		/// </summary>
		string _ValueName = string.Empty;
		/// <summary>
		/// 変数名[ValueName]プロパティ
		/// </summary>
		public string ValueName
		{
			get
			{
				return _ValueName;
			}
			set
			{
				if (!_ValueName.Equals(value))
				{
					_ValueName = value;
					NotifyPropertyChanged("ValueName");
				}
			}
		}
		#endregion
		#region 説明[Description]プロパティ
		/// <summary>
		/// 説明[Description]プロパティ用変数
		/// </summary>
		string _Description = string.Empty;
		/// <summary>
		/// 説明[Description]プロパティ
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
		#region 主キー属性(EntityFramework用)[PrimaryKeyAttribute]プロパティ
		/// <summary>
		/// 主キー属性(EntityFramework用)[PrimaryKeyAttribute]プロパティ用変数
		/// </summary>
		bool _PrimaryKeyAttribute = false;
		/// <summary>
		/// 主キー属性(EntityFramework用)[PrimaryKeyAttribute]プロパティ
		/// </summary>
		public bool PrimaryKeyAttribute
		{
			get
			{
				return _PrimaryKeyAttribute;
			}
			set
			{
				if (!_PrimaryKeyAttribute.Equals(value))
				{
					_PrimaryKeyAttribute = value;
					NotifyPropertyChanged("PrimaryKeyAttribute");
				}
			}
		}
		#endregion
		#region カラム属性(EntityFramework用)[ColumnAttribute]プロパティ
		/// <summary>
		/// カラム属性(EntityFramework用)[ColumnAttribute]プロパティ用変数
		/// </summary>
		string _ColumnAttribute = string.Empty;
		/// <summary>
		/// カラム属性(EntityFramework用)[ColumnAttribute]プロパティ
		/// </summary>
		public string ColumnAttribute
		{
			get
			{
				return _ColumnAttribute;
			}
			set
			{
				if (!_ColumnAttribute.Equals(value))
				{
					_ColumnAttribute = value;
					NotifyPropertyChanged("ColumnAttribute");
				}
			}
		}
		#endregion


		#region クラス図用のマークダウンを取得する
		/// <summary>
		/// クラス図用のマークダウンを取得する
		/// </summary>
		/// <returns></returns>
		public string GetMarkdownForClassDiagram()
		{
			// 値を返却する
			return Utilities.GetClassParameterMarkdown(this);
		}
		#endregion
	}
}
