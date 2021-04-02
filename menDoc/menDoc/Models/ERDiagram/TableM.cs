using menDoc.Common.Utilities;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ERDiagram
{
    public class TableM : ModelBase
	{
		string TempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ClassCode.mdtmpl";
		string InterfaceClassTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\InterfaceClassCode.mdtmpl";
		string InterfacePropertyTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\InterfacePropertyCode.mdtmpl";
		string ProtoClassTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ProtoMessageClassCode.mdtmpl";
		string ProtoPropertyTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ProtoMessagePropertyCode.mdtmpl";

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

        #region プロパティ用テンプレートコード
        /// <summary>
        /// プロパティ用テンプレートコード
        /// </summary>
        string PropertyCode = @".\Common\Templete\CSharpCode\EntityFramework\PropertyCode.mdtmpl";
		#endregion
		#region EntityFramework用C#のClass作成コード
		/// <summary>
		/// EntityFramework用C#のClass作成コード
		/// </summary>
		/// <returns>C#コード</returns>
		public string CreateClassCode()
		{
			// EntityFramework用コードを渡す
			return CreateCode(TempletePath, PropertyCode);
		}
		#endregion
		#region 変数用コードの作成処理
		/// <summary>
		/// 変数用コードの作成処理
		/// </summary>
		/// <returns>変数用のコード</returns>
		public string ParameterCode(string path)
		{
			StreamReader sr = new StreamReader(path, Encoding.UTF8);

			string templete = sr.ReadToEnd();

			StringBuilder parameters_code = new StringBuilder();

			int index = 1;
			// カラム分回す
			foreach (var col in this.ParameterItems)
			{

				string parameter = templete;
				parameter = parameter.Replace("{mendoc:primarykey}", col.PrimaryKey ? "[Key]" : "");	// primarykey部の置換
				parameter = parameter.Replace("{mendoc:column}", col.Name);	// column部の置換
				parameter = parameter.Replace("{mendoc:name}", col.Name);	// name部の置換
				parameter = parameter.Replace("{mendoc:description}", col.Description); // description部の置換
				parameter = parameter.Replace("{mendoc:type}", Utilities.ConvertTypeDBtoCSharp(Utilities.DBtype.MSSQLServer, col.NotNull, col.Type));    // type部の置換
				parameter = parameter.Replace("{mendoc:no}", (index++).ToString());    // no部の置換


				parameter = parameter.Replace("{mendoc:initparam}", Utilities.CSharpTypeInitCode(Utilities.DBtype.MSSQLServer, col.NotNull, col.Type));    // type部の置換

				parameters_code.AppendLine(parameter);

			}

			return parameters_code.ToString();
		}
		#endregion
		#region インターフェース用クラスコードの作成
		/// <summary>
		/// インターフェース用クラスコードの作成
		/// </summary>
		/// <returns></returns>
		public string CreateInterfaceClassCode()
        {
			// インターフェース用クラスコードのパスを渡す
			return CreateCode(InterfaceClassTempletePath, InterfacePropertyTempletePath);
		}
		#endregion
		#region .proto用クラスコードの作成
		/// <summary>
		/// .proto用クラスコードの作成
		/// </summary>
		/// <returns>.proto用クラスコード</returns>
		public string CreateProtoMessageCode()
		{
			// インターフェース用クラスコードのパスを渡す
			return CreateProtoCode(ProtoClassTempletePath, ProtoPropertyTempletePath);
		}
		#endregion
		#region テンプレートを使用したコードの作成
		/// <summary>
		/// テンプレートを使用したコードの作成
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		private string CreateProtoCode(string class_path, string property_path)
		{
			// UTF-8
			StreamReader sr = new StreamReader(class_path, Encoding.UTF8);

			// テンプレートファイル読み出し
			string class_tmpl = sr.ReadToEnd();

			// name部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:name}", this.Name);

			// description部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:description}", this.Description);

			// createdate部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:createdate}", this.CreateDate.ToShortDateString());

			// createuser部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:createuser}", this.CreateUser);

			string parameters = ProtoParameterCode(property_path);

			// 変数のセット
			class_tmpl = class_tmpl.Replace("{mendoc:parameters}", parameters);

			// コードを戻す
			return class_tmpl;
		}
		#endregion
		#region 変数用コードの作成処理
		/// <summary>
		/// 変数用コードの作成処理
		/// </summary>
		/// <returns>変数用のコード</returns>
		public string ProtoParameterCode(string path)
		{
			StreamReader sr = new StreamReader(path, Encoding.UTF8);

			string templete = sr.ReadToEnd();

			StringBuilder parameters_code = new StringBuilder();

			int index = 1;
			// カラム分回す
			foreach (var col in this.ParameterItems)
			{

				string parameter = templete;
				parameter = parameter.Replace("{mendoc:primarykey}", col.PrimaryKey ? "[Key]" : "");    // primarykey部の置換
				parameter = parameter.Replace("{mendoc:column}", col.Name); // column部の置換
				parameter = parameter.Replace("{mendoc:name}", col.Name);   // name部の置換
				parameter = parameter.Replace("{mendoc:description}", col.Description); // description部の置換
				parameter = parameter.Replace("{mendoc:type}", Utilities.ConvertTypeDBtoProtop(col.Type));    // type部の置換
				parameter = parameter.Replace("{mendoc:no}", (index++).ToString());    // no部の置換


				parameter = parameter.Replace("{mendoc:initparam}", Utilities.CSharpTypeInitCode(Utilities.DBtype.MSSQLServer, col.NotNull, col.Type));    // type部の置換

				parameters_code.AppendLine(parameter);

			}

			return parameters_code.ToString();
		}
		#endregion
		#region テンプレートを使用したコードの作成
		/// <summary>
		/// テンプレートを使用したコードの作成
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		private string CreateCode(string class_path, string property_path)
		{
			// UTF-8
			StreamReader sr = new StreamReader(class_path, Encoding.UTF8);

			// テンプレートファイル読み出し
			string class_tmpl = sr.ReadToEnd();

			// name部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:name}", this.Name);

			// description部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:description}", this.Description);

			// createdate部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:createdate}", this.CreateDate.ToShortDateString());

			// createuser部の置換
			class_tmpl = class_tmpl.Replace("{mendoc:createuser}", this.CreateUser);

			string parameters = ParameterCode(property_path);

			// 変数のセット
			class_tmpl = class_tmpl.Replace("{mendoc:parameters}", parameters);

			// コードを戻す
			return class_tmpl;
		}
		#endregion
	}
}
