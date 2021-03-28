using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ERDiagram
{
    public class TableParameterM : ModelBase
    {
		#region 主キー[PrimaryKey]プロパティ
		/// <summary>
		/// 主キー[PrimaryKey]プロパティ用変数
		/// </summary>
		bool _PrimaryKey = false;
		/// <summary>
		/// 主キー[PrimaryKey]プロパティ
		/// </summary>
		public bool PrimaryKey
		{
			get
			{
				return _PrimaryKey;
			}
			set
			{
				if (!_PrimaryKey.Equals(value))
				{
					_PrimaryKey = value;
					NotifyPropertyChanged("PrimaryKey");
				}
			}
		}
		#endregion
		#region Null非許容[NotNull]プロパティ
		/// <summary>
		/// Null非許容[NotNull]プロパティ用変数
		/// </summary>
		bool _NotNull = false;
		/// <summary>
		/// Null非許容[NotNull]プロパティ
		/// </summary>
		public bool NotNull
		{
			get
			{
				return _NotNull;
			}
			set
			{
				if (!_NotNull.Equals(value))
				{
					_NotNull = value;
					NotifyPropertyChanged("NotNull");
				}
			}
		}
		#endregion
		#region 型[Type]プロパティ
		/// <summary>
		/// 型[Type]プロパティ用変数
		/// </summary>
		string _Type = string.Empty;
		/// <summary>
		/// 型[Type]プロパティ
		/// </summary>
		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if (!_Type.Equals(value))
				{
					_Type = value;
					NotifyPropertyChanged("Type");
				}
			}
		}
		#endregion
		#region サイズ[Size]プロパティ
		/// <summary>
		/// サイズ[Size]プロパティ用変数
		/// </summary>
		int? _Size = new int?();
		/// <summary>
		/// サイズ[Size]プロパティ
		/// </summary>
		public int? Size
		{
			get
			{
				return _Size;
			}
			set
			{
				if (_Size == null || !_Size.Equals(value))
				{
					_Size = value;
					NotifyPropertyChanged("Size");
				}
			}
		}
		#endregion

		#region 名前[Name]プロパティ
		/// <summary>
		/// 名前[Name]プロパティ用変数
		/// </summary>
		string _Name = string.Empty;
		/// <summary>
		/// 名前[Name]プロパティ
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

	}
}
