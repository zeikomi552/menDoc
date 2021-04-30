using menDoc.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ERDiagram
{
    public class TableRelationM : ModelBase
    {
		#region シャローコピー
		/// <summary>
		/// シャローコピー
		/// </summary>
		/// <returns></returns>
		public TableRelationM ShallowCopy()
		{
			return (TableRelationM)MemberwiseClone();
		}
		#endregion

		#region 比較対象と比べて値が変化しているかを確認する
		/// <summary>
		/// 比較対象と比べて値が変化しているかを確認する
		/// </summary>
		/// <param name="obj">比較対象</param>
		/// <returns>true:変化している false:一致</returns>
		public bool ChangeCheck(TableRelationM obj)
		{
			Type t = typeof(TableRelationM);
			PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var prop in propInfos)
			{
				var property = typeof(TableRelationM).GetProperty(prop.Name);
				var val = property?.GetValue(this);     // プロパティ名から値の取り出し
				var val2 = property?.GetValue(obj);     // プロパティ名から値の取り出し

				if (!val.Equals(val2))
				{
					return true;
				}
			}
			return false;
		}
		#endregion

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
