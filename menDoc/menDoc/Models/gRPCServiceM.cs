using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models
{
	public class gRPCServiceM : ModelBase
	{
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
		#region マークダウン[Markdown]プロパティ
		/// <summary>
		/// マークダウン[Markdown]プロパティ
		/// </summary>
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
				code.AppendLine(string.Format("\trpc ({0}Request) returns({0}Reply) {{}}", api.Name));
				code.AppendLine("");
			}
			code.AppendLine("}");

			foreach (var api in this.APIs)
			{
				code.AppendLine(string.Format("message {0}Request {{", api.Name));

				int index = 1;
				foreach (var request in api.RequestItems)
				{
					code.AppendLine(string.Format("\t{0} {1} = {2};", request.TypeName, request.ValueName, index));
					index++;
				}
				code.AppendLine("}");


				code.AppendLine(string.Format("message {0}Reply {{", api.Name));

				index = 1;
				foreach (var reply in api.Replytems)
				{
					code.AppendLine(string.Format("\t{0} {1} = {2};", reply.TypeName, reply.ValueName, index));
					index++;
				}
				code.AppendLine("}");
			}

			return code.ToString();
		}
		#endregion
		#region .protoファイル用コードの更新
		/// <summary>
		/// .protoファイル用コードの更新
		/// </summary>
		public void RefleshCode()
		{
			NotifyPropertyChanged("ProtoCode");
			NotifyPropertyChanged("Markdown");
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
				foreach (var request in api.RequestItems)
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
	}
}
