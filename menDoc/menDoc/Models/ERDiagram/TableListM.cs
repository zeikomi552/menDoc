using menDoc.Common.Utilities;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ERDiagram
{
    public class TableListM : ModelBase
	{
		#region テーブルリスト[TableItems]プロパティ
		/// <summary>
		/// テーブルリスト[TableItems]プロパティ用変数
		/// </summary>
		ModelList<TableM> _TableItems = new ModelList<TableM>();
		/// <summary>
		/// テーブルリスト[TableItems]プロパティ
		/// </summary>
		public ModelList<TableM> TableItems
		{
			get
			{
				return _TableItems;
			}
			set
			{
				if (_TableItems == null || !_TableItems.Equals(value))
				{
					_TableItems = value;
					NotifyPropertyChanged("TableItems");
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

		#region マークダウンの作成処理
		/// <summary>
		/// マークダウンの作成処理
		/// </summary>
		/// <returns>マークダウン</returns>
		public string CreateMarkdown()
		{
			return ERDiagramMarkdown();
		}
		#endregion

		

		#region ER図用マークダウン
		/// <summary>
		/// ER図用マークダウン
		/// </summary>
		/// <returns></returns>
		private string ERDiagramMarkdown()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("```mermaid");
			code.AppendLine("erDiagram");

			// テーブルの要素を一通り並べる
			foreach (var table in this.TableItems)
			{
				code.AppendLine(table.Name + " {");

				foreach (var param in table.ParameterItems)
				{
					code.AppendLine(param.Type + " " + param.Name);
				}

				code.AppendLine("}");
			}

			foreach (var table in this.TableItems)
			{
				foreach (var rel in table.TableRelationList)
				{
					code.AppendLine(table.Name
						+ " "
						+ Utilities.ConvertMultiplicity(Common.Enums.TableDirection.Source, rel.OwnMultiplicity)
						+ "--"
						+ Utilities.ConvertMultiplicity(Common.Enums.TableDirection.Target, rel.TargetMultiplicity)
						+ " "
						+ rel.TagetTable
						+ " : " + (string.IsNullOrWhiteSpace(rel.Description) ? "is" : rel.Description)
						);
				}
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

		public IEnumerator<TableM> GetEnumerator()
		{
			foreach (var item in this.TableItems)
			{
				yield return item;  // ここでパーツを返す
			}
		}
	}
}
