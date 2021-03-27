using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models
{
    public class gRPCAPIM : ModelBase
    {



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

		#region リクエストアイテム[RequestItems]プロパティ
		/// <summary>
		/// リクエストアイテム[RequestItems]プロパティ用変数
		/// </summary>
		ModelList<gRrpcParamM> _RequestItems = new ModelList<gRrpcParamM>();
		/// <summary>
		/// リクエストアイテム[RequestItems]プロパティ
		/// </summary>
		public ModelList<gRrpcParamM> RequestItems
		{
			get
			{
				return _RequestItems;
			}
			set
			{
				if (_RequestItems == null || !_RequestItems.Equals(value))
				{
					_RequestItems = value;
					NotifyPropertyChanged("RequestItems");
				}
			}
		}
		#endregion

		#region 応答アイテム[Replytems]プロパティ
		/// <summary>
		/// 応答アイテム[Replytems]プロパティ用変数
		/// </summary>
		ModelList<gRrpcParamM> _Replytems = new ModelList<gRrpcParamM>();
		/// <summary>
		/// 応答アイテム[Replytems]プロパティ
		/// </summary>
		public ModelList<gRrpcParamM> Replytems
		{
			get
			{
				return _Replytems;
			}
			set
			{
				if (_Replytems == null || !_Replytems.Equals(value))
				{
					_Replytems = value;
					NotifyPropertyChanged("Replytems");
				}
			}
		}
		#endregion
	}
}
