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
			code.AppendLine("```mermaid");
			code.AppendLine("classDiagram");
			foreach (var classitem in this.ClassItems)
			{
				var class_markdown = classitem.Markdown;
				code.AppendLine(class_markdown);
			}
			code.AppendLine("```");

			return code.ToString();
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
