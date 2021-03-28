using menDoc.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ERDiagram
{
    public class TableRelationM : ModelBase
    {
		#region 接続先のテーブル[TagetTable]プロパティ
		/// <summary>
		/// 接続先のテーブル[TagetTable]プロパティ用変数
		/// </summary>
		string _TagetTable = string.Empty;
		/// <summary>
		/// 接続先のテーブル[TagetTable]プロパティ
		/// </summary>
		public string TagetTable
		{
			get
			{
				return _TagetTable;
			}
			set
			{
				if (!_TagetTable.Equals(value))
				{
					_TagetTable = value;
					NotifyPropertyChanged("TagetTable");
				}
			}
		}
		#endregion

		#region 自分の多重度[OwnMultiplicity]プロパティ
		/// <summary>
		/// 自分の多重度[OwnMultiplicity]プロパティ用変数
		/// </summary>
		Multiplicity _OwnMultiplicity = Multiplicity.ZeroMulti;
		/// <summary>
		/// 自分の多重度[OwnMultiplicity]プロパティ
		/// </summary>
		public Multiplicity OwnMultiplicity
		{
			get
			{
				return _OwnMultiplicity;
			}
			set
			{
				if (!_OwnMultiplicity.Equals(value))
				{
					_OwnMultiplicity = value;
					NotifyPropertyChanged("OwnMultiplicity");
				}
			}
		}
		#endregion
		#region 接続先の多重度[TargetMultiplicity]プロパティ
		/// <summary>
		/// 接続先の多重度[TargetMultiplicity]プロパティ用変数
		/// </summary>
		Multiplicity _TargetMultiplicity = Multiplicity.ZeroMulti;
		/// <summary>
		/// 接続先の多重度[TargetMultiplicity]プロパティ
		/// </summary>
		public Multiplicity TargetMultiplicity
		{
			get
			{
				return _TargetMultiplicity;
			}
			set
			{
				if (!_TargetMultiplicity.Equals(value))
				{
					_TargetMultiplicity = value;
					NotifyPropertyChanged("TargetMultiplicity");
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
