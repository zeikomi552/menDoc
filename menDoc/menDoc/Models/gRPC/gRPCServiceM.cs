using menDoc.Common.Enums;
using menDoc.Common.Utilities;
using menDoc.Models.ERDiagram;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models
{
	public class gRPCServiceM : ModelBase
	{
		#region プロジェクトタイプ enum
		/// <summary>
		/// プロジェクトタイプ enum
		/// </summary>
		public enum ProjType
		{
			Server = 0,
			Client,
			Both
		}
		#endregion

		#region .protoファイルのコード[ProtoCode]プロパティ
		/// <summary>
		/// .protoファイルのコード[ProtoCode]プロパティ
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string ProtoCode
		{
			get
			{
				return CreateProtoCode();
			}
		}
		#endregion

		#region .csproj用コード(サーバー用)プロパティ
		/// <summary>
		/// .csproj用コード(サーバー用)プロパティ
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string CsprojServer
		{
			get
			{
				return CreateCSProjClient(ProjType.Server);

			}
		}
		#endregion

		#region .csproj用コード(クライアント用)プロパティ
		/// <summary>
		/// .csproj用コード(クライアント用)プロパティ
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string CsprojClient
		{
			get
			{
				return CreateCSProjClient(ProjType.Client);
			}
		}
		#endregion

		#region .csproj用コード(兼用)プロパティ
		/// <summary>
		/// .csproj用コード(兼用)プロパティ
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string CsprojBoth
		{
			get
			{
				return CreateCSProjClient(ProjType.Both);
			}
		}
		#endregion

		#region .cs サーバー用コード
		/// <summary>
		/// .cs サーバー用コード
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string CsServer
		{
			get
			{
				return CreateCSCodeServer();
			}
		}
		#endregion

		[System.Xml.Serialization.XmlIgnore]
		public string RecieveCode
		{
			get
			{
				return CreateRecieveCode();
			}
		}
		string RecieveClassTempletePath = @".\Common\Templete\CSharpCode\gRPC\ServiceClass.mdtmpl";
		string RecieveMethodTempletePath = @".\Common\Templete\CSharpCode\gRPC\RequestMethod.mdtmpl";
		string RecieveEventHandlerTempletePath = @".\Common\Templete\CSharpCode\gRPC\RecieveEventHandler.mdtmpl";

		public string CreateRecieveCode()
		{
			// UTF-8
			StreamReader class_sr = new StreamReader(RecieveClassTempletePath, Encoding.UTF8);

			// テンプレートファイル読み出し
			string class_text = class_sr.ReadToEnd();

			// UTF-8
			StreamReader method_sr = new StreamReader(RecieveMethodTempletePath, Encoding.UTF8);

			// テンプレートファイル読み出し
			string method_text = method_sr.ReadToEnd();


			StringBuilder code = new StringBuilder();

			foreach (var api in this.APIs.Items)
			{
				string text_tmp = method_text;

				text_tmp = text_tmp.Replace("{mendoc:name}", api.Name);

				code.AppendLine(text_tmp);
			}


			// UTF-8
			StreamReader event_sr = new StreamReader(RecieveEventHandlerTempletePath, Encoding.UTF8);

			// テンプレートファイル読み出し
			string event_text = event_sr.ReadToEnd();

			StringBuilder event_code = new StringBuilder();

			foreach (var api in this.APIs.Items)
			{
				string text_tmp = event_text;

				text_tmp = text_tmp.Replace("{mendoc:name}", api.Name);

				event_code.AppendLine(text_tmp);
			}

			class_text = class_text.Replace("{mendoc:events}", event_code.ToString());

			class_text = class_text.Replace("{mendoc:name}", this.ServiceName);
			class_text = class_text.Replace("{mendoc:methods}", code.ToString());

			return class_text.ToString();
		}

		#region .cs クライアント用コード
		/// <summary>
		/// .cs クライアント用コード
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string CsClient
		{
			get
			{
				return CreateCSCodeClient();
			}
		}
		#endregion

		#region コードの更新
		/// <summary>
		/// コードの更新
		/// </summary>
		public void RefleshCode()
		{
			NotifyPropertyChanged("ProtoCode");
			NotifyPropertyChanged("Markdown");
			NotifyPropertyChanged("CsprojServer");
			NotifyPropertyChanged("CsprojClient");
			NotifyPropertyChanged("CsprojBoth");
			NotifyPropertyChanged("CsServer");
			NotifyPropertyChanged("CsClient");
		}
		#endregion

		#region マークダウン[Markdown]プロパティ
		/// <summary>
		/// マークダウン[Markdown]プロパティ
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string Markdown
		{
			get
			{
				return CreateMarkdown();
			}
		}
		#endregion

		#region サービス名[ServiceName]プロパティ
		/// <summary>
		/// サービス名[ServiceName]プロパティ用変数
		/// </summary>
		string _ServiceName = string.Empty;
		/// <summary>
		/// サービス名[ServiceName]プロパティ
		/// </summary>
		public string ServiceName
		{
			get
			{
				return _ServiceName;
			}
			set
			{
				if (!_ServiceName.Equals(value))
				{
					_ServiceName = value;
					NotifyPropertyChanged("ServiceName");
					RefleshCode();
				}
			}
		}
		#endregion

		#region サービスの説明[ServiceDescriotion]プロパティ
		/// <summary>
		/// サービスの説明[ServiceDescriotion]プロパティ用変数
		/// </summary>
		string _ServiceDescriotion = string.Empty;
		/// <summary>
		/// サービスの説明[ServiceDescriotion]プロパティ
		/// </summary>
		public string ServiceDescriotion
		{
			get
			{
				return _ServiceDescriotion;
			}
			set
			{
				if (!_ServiceDescriotion.Equals(value))
				{
					_ServiceDescriotion = value;
					NotifyPropertyChanged("ServiceDescriotion");
					RefleshCode();
				}
			}
		}
		#endregion

		#region APIリスト[APIs]プロパティ
		/// <summary>
		/// APIリスト[APIs]プロパティ用変数
		/// </summary>
		ModelList<gRPCAPIM> _APIs = new ModelList<gRPCAPIM>();
		/// <summary>
		/// APIリスト[APIs]プロパティ
		/// </summary>
		public ModelList<gRPCAPIM> APIs
		{
			get
			{
				return _APIs;
			}
			set
			{
				if (_APIs == null || !_APIs.Equals(value))
				{
					_APIs = value;
					NotifyPropertyChanged("APIs");
					NotifyPropertyChanged("ProtoCode");
				}
			}
		}
		#endregion

		#region .protoファイル用Codeの作成
		/// <summary>
		/// .protoファイル用Codeの作成
		/// </summary>
		/// <returns>コード</returns>
		private string CreateProtoCode()
		{
			StringBuilder code = new StringBuilder();

			// 宣言部
			code.AppendLine("syntax = \"proto3\";");
			code.AppendLine("");

			// サービス定義
			code.AppendLine(string.Format("// {0}", this.ServiceDescriotion));
			code.AppendLine(string.Format("service {0}{{", this.ServiceName));
			code.AppendLine("");

			// APIの宣言
			foreach (var api in this.APIs)
			{
				code.AppendLine(string.Format("\t// {0}", api.Description));
				code.AppendLine(string.Format("\trpc {0} ({0}Request) returns({0}Reply) {{}}", api.Name));
				code.AppendLine("");
			}
			code.AppendLine("}");

			foreach (var api in this.APIs)
			{
				code.AppendLine(string.Format("message {0}Request {{", api.Name));

				int index = 1;
				foreach (var request in api.RequestItems)
				{
					// repeatの判別
					if (request.SingleRepeat == SingleRepeatEnum.Single)
					{
						code.AppendLine(string.Format("\t{0} {1} = {2};", request.TypeName, request.ValueName, index));
					}
					else
					{
						code.AppendLine(string.Format("\trepeated {0} {1} = {2};", request.TypeName, request.ValueName, index));
					}
					index++;
				}
				code.AppendLine("}");


				code.AppendLine(string.Format("message {0}Reply {{", api.Name));

				index = 1;
				foreach (var reply in api.Replytems)
				{
					// repeatの判別
					if (reply.SingleRepeat == SingleRepeatEnum.Single)
					{
						code.AppendLine(string.Format("\t{0} {1} = {2};", reply.TypeName, reply.ValueName, index));
					}
					else
					{
						code.AppendLine(string.Format("\trepeated {0} {1} = {2};", reply.TypeName, reply.ValueName, index));
					}
					index++;
				}
				code.AppendLine("}");
			}

			return code.ToString();
		}
		#endregion

		#region マークダウンの作成処理
		/// <summary>
		/// マークダウンの作成処理
		/// </summary>
		/// <returns></returns>
		public string CreateMarkdown()
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine("## 基本情報");
			code.AppendLine(string.Format("- 作成日：{0}", DateTime.Today.ToString("yyyy/MM/dd")));
			code.AppendLine(string.Format("- 作成者：{0}", Environment.UserName));

			code.AppendLine();

			code.AppendLine("## APIリスト");
			code.AppendLine("APIの一覧を以下に記載する。");
			code.AppendLine("|No.|API名|説明|");
			code.AppendLine("|---|---|---|");

			int index = 1;
			foreach (var api in this.APIs)
			{
				// APIリストの作成
				code.AppendLine(string.Format("|{0}|{1}|{2}|", index++, api.Name, api.Description));
			}

			code.AppendLine();
			code.AppendLine("## リクエストおよびリプライ一覧");
			code.AppendLine("リクエスト（要求）の一覧を以下に記載する。");
			code.AppendLine();

			foreach (var api in this.APIs)
			{
				code.AppendLine(string.Format("### API名：{0}", api.Name));
				code.AppendLine();

				code.AppendLine("- リクエスト");
				code.AppendLine();
				code.AppendLine("|No.|単体/リスト|型|変数名|説明|");
				code.AppendLine("|---|---|---|---|---|");

				index = 1;
				foreach (var request in api.RequestItems)
                {
					code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|", 
						index++, request.SingleRepeat.ToString()
						, request.TypeName, request.ValueName, request.Description));
				}
				code.AppendLine();

				code.AppendLine("- リプライ");
				code.AppendLine();
				code.AppendLine("|No.|単体/リスト|型|変数名|説明|");
				code.AppendLine("|---|---|---|---|---|");

				index = 1;
				foreach (var request in api.Replytems)
				{
					code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|",
						index++, request.SingleRepeat.ToString()
						, request.TypeName, request.ValueName, request.Description));
				}
				code.AppendLine();

			}

			return code.ToString();

		}
		#endregion

		#region プロジェクトファイルのサンプル
		/// <summary>
		/// プロジェクトファイルのサンプル
		/// </summary>
		/// <param name="type">プロジェクトのタイプ：Server用 Client用 兼用</param>
		/// <returns>Code</returns>
		public string CreateCSProjClient(ProjType type)
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
			code.AppendLine("\t<ItemGroup>");
			code.AppendLine(string.Format("\t\t<Protobuf Include=\" * */*.proto\" OutputDir=\"%(RelativePath)\" CompileOutputs=\"false\" GrpcServices=\"{0}\" />", type.ToString()));
			code.AppendLine("\t</ItemGroup>");
			code.AppendLine("</Project>");
			return code.ToString();
		}
		#endregion

		#region サーバー側の処理の作成
		/// <summary>
		/// サーバー側の処理の作成
		/// </summary>
		/// <returns>コード</returns>
		public string CreateCSCodeServer()
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine("/// <summary>");
			code.AppendLine("/// サーバー側の受付開始処理");
			code.AppendLine("/// </summary>");
			code.AppendLine("public void Listen()");
			code.AppendLine("{");
			code.AppendLine(string.Format("\tvar tmp = new {0}Service();", this.ServiceName));
			code.AppendLine("\tServer server = new Server");
			code.AppendLine("\t{");
			code.AppendLine(string.Format("\t\tServices = {{ {0}.BindService(tmp) }},", this.ServiceName));
			code.AppendLine(string.Format("\t\tPorts = {{ new ServerPort(/*IP Address*/\"128.0.0.1\", /*Port No*/552552, ServerCredentials.Insecure) }}"));
			code.AppendLine("\t};");
			code.AppendLine("\tserver.Start();");

			code.AppendLine("}");
			return code.ToString();
		}
		#endregion

		#region クライアント用コードの作成
		/// <summary>
		/// クライアント用コードの作成
		/// </summary>
		/// <returns>コード</returns>
		public string CreateCSCodeClient()
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine("/// <summary>");
			code.AppendLine("/// クライアント側の送信開始処理");
			code.AppendLine("/// </summary>");
			code.AppendLine("public void Send()");
			code.AppendLine("{");
			code.AppendLine("\tvar channel = new Channel(/*IP Address*/\"127.0.0.1\", /* Port No*/552552, ChannelCredentials.Insecure);");
			code.AppendLine(string.Format("\tvar client = new {0}.{0}Client(channel);", this.ServiceName));

			foreach(var tmp in this.APIs)
            {
				code.AppendLine("\t{");
				code.AppendLine(string.Format("\t\tvar message = new {0}Request();", tmp.Name));
				code.AppendLine(string.Format("\t\tvar reply = client.{0}(message);", tmp.Name));
				code.AppendLine("\t}");
			}

			code.AppendLine("}");
			return code.ToString();

		}
		#endregion

		#region テーブル情報からセットする
		/// <summary>
		/// テーブル情報からセットする
		/// </summary>
		/// <param name="table"></param>
		public void SetTable(TableM table)
		{
			gRPCAPIM api = new gRPCAPIM();
			api.Name = table.Name;
			api.Description = table.Description;

			foreach (var col in table.ParameterItems)
			{
				gRrpcParamM param = new gRrpcParamM();
				// 型のセット
				param.TypeName = Utilities.ConvertTypeDBtoProtop(col.Type);
				// 変数名のセット
				param.ValueName = col.Name;
				// 説明のセット
				param.Description = col.Description;
				// 繰り返しなしでセット
				param.SingleRepeat = SingleRepeatEnum.Single;
				// パラメータのセット
				api.RequestItems.Items.Add(param);
				api.Replytems.Items.Add(param);
			}
			// APIリストにセット
			this.APIs.Items.Add(api);
		}
		#endregion
	}
}
