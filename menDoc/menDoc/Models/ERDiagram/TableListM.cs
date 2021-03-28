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
			StringBuilder code = new StringBuilder();
			code.AppendLine("## ER図");
			code.AppendLine(string.Format("- 作成日:{0}", DateTime.Today.ToShortDateString()));
			code.AppendLine(string.Format("- 作成者:{0}", Environment.UserName));
			code.AppendLine();
			code.AppendLine(ERDiagramMarkdown());
			code.AppendLine();
			code.AppendLine("## テーブル一覧");
			code.AppendLine(CreateTableListMarkdown());
			code.AppendLine();

			foreach (var table in this.TableItems)
			{
				code.AppendLine(string.Format("## テーブル：{0}情報", table.Name));
				code.AppendLine(GetTableMarkdown(table));

			}

			return code.ToString();
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
			code.AppendLine();

			// テーブルの要素を一通り並べる
			foreach (var table in this.TableItems)
			{
				code.AppendLine(table.Name + " {");

				foreach (var param in table.ParameterItems)
				{
					code.AppendLine(param.Type + " " + param.Name);
				}

				code.AppendLine("}");
				code.AppendLine();
			}

			// テーブル同士の関係を列挙する
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

		#region テーブル一覧用マークダウンの作成
		public string CreateTableListMarkdown()
		{
			StringBuilder code = new StringBuilder();

			code.AppendLine("|No.|テーブル名|説明|作成日|作成者|");
			code.AppendLine("|---|---|---|---|---|");

			int index = 1;
			foreach (var table in this.TableItems)
            {
				code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|", index++, table.Name, table.Description, table.CreateDate.ToShortDateString(), table.CreateUser));
			}

			return code.ToString();
		}
		#endregion

		#region テーブル情報のマークダウン作成
		/// <summary>
		/// テーブル情報のマークダウン作成
		/// </summary>
		/// <param name="table">テーブル情報</param>
		/// <returns>マークダウン</returns>
		public string GetTableMarkdown(TableM table)
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine("テーブルの説明：" + table.Description);
			code.AppendLine();
			
			code.AppendLine("|No.|PK|NotNull|型|サイズ|変数名|説明|");
			code.AppendLine("|---|---|---|---|---|---|---|");
			int index = 1;
			foreach (var param in table.ParameterItems)
            {
				code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|",
					index++,
					param.PrimaryKey ? "x" : "",
					param.NotNull ? "x" : "",
					param.Type,
					param.Size,
					param.Name,
					param.Description
					));
			}

			return code.ToString();
		}
		#endregion

		#region 列挙 foreach用
		/// <summary>
		/// 列挙 foreach用
		/// </summary>
		/// <returns></returns>
		public IEnumerator<TableM> GetEnumerator()
		{
			foreach (var item in this.TableItems)
			{
				yield return item;  // ここでパーツを返す
			}
		}
		#endregion
	}
}
