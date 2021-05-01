using Markdig;
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
using static menDoc.Common.TempletePath;

namespace menDoc.Models.ClassDiagram
{
	public class ClassListM : ModelBase
	{
		#region foreach用 Enumerator
		/// <summary>
		/// foreach用 Enumerator
		/// </summary>
		/// <returns></returns>
		public IEnumerator<ClassM> GetEnumerator()
		{
			foreach (var item in this.ClassItems)
			{
				yield return item;  // ここでパーツを返す
			}
		}
		#endregion

		#region テーブルからクラスを作成してセットする
		/// <summary>
		/// テーブルからクラスを作成してセットする
		/// </summary>
		/// <param name="table">テーブル情報</param>
		public void SetTable(TableM table)
		{
			var class_item = ClassM.ConvertTableToClass(table);

			class_item.MethodItems.Items.Add(new ClassMethodM()
			{
				MethodName = string.Format("Insert({0} item)", table.Name),
				ReturnValue = "void",
				Description = "データベースへInsertを行います",
			}
			);
			class_item.MethodItems.Items.Add(new ClassMethodM()
			{
				MethodName = "Select()",
				ReturnValue = string.Format("List<{0}>", table.Name),
				Description = "データベースへSelectを行います",
			}
			);
			class_item.MethodItems.Items.Add(new ClassMethodM()
			{
				MethodName = string.Format("Update({0} pk_item, {0} insert_item)", table.Name),
				ReturnValue = "void",
				Description = "データベースへUpdateを行います",

			}
			);
			class_item.MethodItems.Items.Add(new ClassMethodM()
			{
				MethodName = string.Format("Delete({0} item)", table.Name),
				ReturnValue = "void",
				Description = "データベースへDeleteを行います",
			}
			);
			this.ClassItems.Items.Add(class_item);
		}
		#endregion

		#region クラスのリスト[ClassItems]プロパティ
		/// <summary>
		/// クラスのリスト[ClassItems]プロパティ用変数
		/// </summary>
		ModelList<ClassM> _ClassItems = new ModelList<ClassM>();
		/// <summary>
		/// クラスのリスト[ClassItems]プロパティ
		/// </summary>
		public ModelList<ClassM> ClassItems
		{
			get
			{
				return _ClassItems;
			}
			set
			{
				if (_ClassItems == null || !_ClassItems.Equals(value))
				{
					_ClassItems = value;
					NotifyPropertyChanged("ClassItems");
				}
			}
		}
		#endregion

		#region マークダウン
		/// <summary>
		/// マークダウン
		/// </summary>
		public string Markdown
		{
			get
			{
				return CreateMarkdown();
			}
		}
		#endregion

		#region HTML
		/// <summary>
		/// HTML
		/// </summary>
		public string Html
		{
			get
			{
				var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
				return Markdig.Markdown.ToHtml(this.Markdown, pipeline);
			}
		}
		#endregion

		#region 一時ファイルのURI
		/// <summary>
		/// 一時ファイルのURI
		/// </summary>
		public Uri TmpURI
		{
			get
			{
				return new Uri(ClassDiagramPath.TmploraryFilePath);
			}
		}
		#endregion

		#region マークダウンの作成
		/// <summary>
		/// マークダウンの作成
		/// </summary>
		/// <returns>マークダウン</returns>
		private string CreateMarkdown()
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine("## クラス図");
			code.AppendLine(string.Format("- 作成日:{0}", DateTime.Today.ToShortDateString()));
			code.AppendLine(string.Format("- 作成者:{0}", Environment.UserName));
			code.AppendLine();
			code.AppendLine("```mermaid");
			code.AppendLine("classDiagram");
			foreach (var classitem in this.ClassItems)
			{
				var class_markdown = classitem.Markdown;
				code.AppendLine(class_markdown);
			}
			code.AppendLine("```");

			code.AppendLine();
			code.AppendLine("## クラス一覧");
			code.AppendLine(GetMarkdownForClassList());
			code.AppendLine();
			code.AppendLine("## クラス詳細");
			code.AppendLine(GetMarkdownForClassDetail());
			return code.ToString();
		}
		#endregion

		#region クラスの一覧マークダウン作成
		/// <summary>
		/// クラスの一覧マークダウン作成
		/// </summary>
		/// <returns>マークダウン</returns>
		public string GetMarkdownForClassList()
		{
			return Utilities.GetClassClassList(this);
		}
		#endregion

		#region クラス情報の詳細マークダウン作成
		/// <summary>
		/// クラス情報の詳細マークダウン作成
		/// </summary>
		/// <returns>マークダウン</returns>
		public string GetMarkdownForClassDetail()
		{
			return Utilities.GetClassDetails(this);
		}
		#endregion

		#region コードの更新
		/// <summary>
		/// コードの更新
		/// </summary>
		public void RefleshCode()
		{
			NotifyPropertyChanged("Markdown");
			SaveTemporary();

			NotifyPropertyChanged("TmpURI");
		}
		#endregion

		#region テンポラリデータの保存処理
		/// <summary>
		/// テンポラリデータの保存処理
		/// </summary>
		/// <returns>保存処理</returns>
		public string SaveTemporary()
		{
			try
			{
				// UTF-8
				StreamReader html_sr = new StreamReader(ClassDiagramPath.OutputHtmlTmpletePath, Encoding.UTF8);

				// テンプレートファイル読み出し
				string html_txt = html_sr.ReadToEnd();

				html_txt = html_txt.Replace("{menDoc:jsdir}", Utilities.JSDir);
				html_txt = html_txt.Replace("{menDoc:htmlbody}", this.Html);
				File.WriteAllText(ClassDiagramPath.TmploraryFilePath, html_txt);

				// 一時フォルダの作成
				Utilities.CreateTemporaryDir();

				return ClassDiagramPath.TmploraryFilePath;
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
				return string.Empty;
			}
		}
		#endregion
	}
}
