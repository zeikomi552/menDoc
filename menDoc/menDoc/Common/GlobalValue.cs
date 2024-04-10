using ControlzEx.Standard;
using menDoc.Models;
using menDoc.Models.ClassDiagram;
using menDoc.Models.ERDiagram;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common
{
    public static class GlobalValue
    {
		#region クラスのリスト[ClassList]プロパティ
		/// <summary>
		/// クラスのリスト[ClassList]プロパティ用変数
		/// </summary>
		static ClassListM _ClassList = new ();
		/// <summary>
		/// クラスのリスト[ClassList]プロパティ
		/// </summary>
		public static ClassListM ClassList
		{
			get
			{
				return _ClassList;
			}
			set
			{
				if (_ClassList == null || !_ClassList.Equals(value))
				{
					_ClassList = value;
				}
			}
		}
		#endregion

		#region テーブルリスト[TableList]プロパティ
		/// <summary>
		/// テーブルリスト[TableList]プロパティ用変数
		/// </summary>
		static TableListM _TableList = new ();
		/// <summary>
		/// テーブルリスト[TableList]プロパティ
		/// </summary>
		public static TableListM TableList
		{
			get
			{
				return _TableList;
			}
			set
			{
				if (_TableList == null || !_TableList.Equals(value))
				{
					_TableList = value;
				}
			}
		}
		#endregion

		#region サービス[Service]プロパティ
		/// <summary>
		/// サービス[Service]プロパティ用変数
		/// </summary>
		static gRPCServiceM _Service = new ();
		/// <summary>
		/// サービス[Service]プロパティ
		/// </summary>
		public static gRPCServiceM Service
		{
			get
			{
				return _Service;
			}
			set
			{
				if (_Service == null || !_Service.Equals(value))
				{
					_Service = value;
				}
			}
		}
        #endregion


        #region gRPC用型リスト
        /// <summary>
        /// gRPC用型リスト
        /// </summary>
        static ObservableCollection<string> _gRPCTypes = new ObservableCollection<string>();
        /// <summary>
        /// gRPC用型リスト
        /// </summary>
        public static ObservableCollection<string> gRPCTypes
        {
            get
            {
                return _gRPCTypes;
            }
            set
            {
                if (_gRPCTypes == null || !_gRPCTypes.Equals(value))
                {
                    _gRPCTypes = value;
                }
            }
        }
		#endregion

		public static void TypeInit()
		{
            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("double");
            list.Add("float");
            list.Add("int32");
            list.Add("int64");
            list.Add("uint32");
            list.Add("uint64");
            list.Add("sint32");
            list.Add("sint64");
            list.Add("fixed32");
            list.Add("fixed64");
            list.Add("sfixed32");
            list.Add("sfixed64");
            list.Add("bool");
            list.Add("string");
            list.Add("bytes");
            gRPCTypes = list;
        }



        public static void AddGrpcType(string typename)
		{
            _gRPCTypes.Add(typename);
        }
	}
}
