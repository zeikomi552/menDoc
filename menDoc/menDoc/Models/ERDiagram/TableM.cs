using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ERDiagram
{
    public class TableM : ModelBase
	{
		#region テーブル名[Name]プロパティ
		/// <summary>
		/// テーブル名[Name]プロパティ用変数
		/// </summary>
		string _Name = string.Empty;
		/// <summary>
		/// テーブル名[Name]プロパティ
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
		#region 作成日[CreateDate]プロパティ
		/// <summary>
		/// 作成日[CreateDate]プロパティ用変数
		/// </summary>
		DateTime _CreateDate = DateTime.Today;
		/// <summary>
		/// 作成日[CreateDate]プロパティ
		/// </summary>
		public DateTime CreateDate
		{
			get
			{
				return _CreateDate;
			}
			set
			{
				if (!_CreateDate.Equals(value))
				{
					_CreateDate = value;
					NotifyPropertyChanged("CreateDate");
				}
			}
		}
		#endregion
		#region 作成者[CreateUser]プロパティ
		/// <summary>
		/// 作成者[CreateUser]プロパティ用変数
		/// </summary>
		string _CreateUser = Environment.UserName;
		/// <summary>
		/// 作成者[CreateUser]プロパティ
		/// </summary>
		public string CreateUser
		{
			get
			{
				return _CreateUser;
			}
			set
			{
				if (!_CreateUser.Equals(value))
				{
					_CreateUser = value;
					NotifyPropertyChanged("CreateUser");
				}
			}
		}
		#endregion

		#region 変数一覧[ParameterItems]プロパティ
		/// <summary>
		/// 変数一覧[ParameterItems]プロパティ用変数
		/// </summary>
		ModelList<TableParameterM> _ParameterItems = new ModelList<TableParameterM>();
		/// <summary>
		/// 変数一覧[ParameterItems]プロパティ
		/// </summary>
		public ModelList<TableParameterM> ParameterItems
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

		#region テーブルの関係リスト[TableRelationList]プロパティ
		/// <summary>
		/// テーブルの関係リスト[TableRelationList]プロパティ用変数
		/// </summary>
		ModelList<TableRelationM> _TableRelationList = new ModelList<TableRelationM>();
		/// <summary>
		/// テーブルの関係リスト[TableRelationList]プロパティ
		/// </summary>
		public ModelList<TableRelationM> TableRelationList
		{
			get
			{
				return _TableRelationList;
			}
			set
			{
				if (_TableRelationList == null || !_TableRelationList.Equals(value))
				{
					_TableRelationList = value;
					NotifyPropertyChanged("TableRelationList");
				}
			}
		}
		#endregion
	}
}
