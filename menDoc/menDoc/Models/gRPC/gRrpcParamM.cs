using menDoc.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models
{

	public class gRrpcParamM : ModelBase
    {
		#region 単体・応答識別[SingleRepeat]プロパティ
		/// <summary>
		/// 単体・応答識別[SingleRepeat]プロパティ用変数
		/// </summary>
		SingleRepeatEnum _SingleRepeat = new SingleRepeatEnum();
		/// <summary>
		/// 単体・応答識別[SingleRepeat]プロパティ
		/// </summary>
		public SingleRepeatEnum SingleRepeat
		{
			get
			{
				return _SingleRepeat;
			}
			set
			{
				if (!_SingleRepeat.Equals(value))
				{
					_SingleRepeat = value;
					NotifyPropertyChanged("SingleRepeat");
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

	}
}
