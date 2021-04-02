using menDoc.Common.Utilities;
using menDoc.Models.ERDiagram;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ClassDiagram
{
	public class ClassListM : ModelBase
	{
		public IEnumerator<ClassM> GetEnumerator()
		{
			foreach (var item in this.ClassItems)
			{
				yield return item;  // ここでパーツを返す
			}
		}

		#region テーブルからクラスを作成してセットする
		/// <summary>
		/// テーブルからクラスを作成してセットする
		/// </summary>
		/// <param name="table">テーブル情報</param>
		public void SetTable(TableM table)
		{
			this.ClassItems.Items.Add(ClassM.ConvertTableToClass(table));
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
		}
		#endregion
	}
}
