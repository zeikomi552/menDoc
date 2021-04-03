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

		#region EntityFramework用コード
		/// <summary>
		/// EntityFramework用コード
		/// </summary>
		public string EntityCode
		{
			get
			{
				return EntityFrameworkCode();
			}
		}
		#endregion

		#region Interfaceクラス用コード
		public string InterfaceCode
		{
			get
            {
				return InterfaceClassCode();

            }
		}
		#endregion

		#region DbContext用コード
		/// <summary>
		/// DbContext用コード
		/// </summary>
		public string DbContext
        {
			get
			{
				return DbContextCode();
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

		#region .proto用コード
		/// <summary>
		/// .proto用コード
		/// </summary>
		public string ProtoCode
		{
			get
			{
				return ProtoCodeMessageCode();
			}
		}
		#endregion


		#region コードの更新
		/// <summary>
		/// コードの更新
		/// </summary>
		public void RefleshCode()
		{
			NotifyPropertyChanged("Markdown");
			NotifyPropertyChanged("EntityCode");
			NotifyPropertyChanged("InterfaceCode");
			NotifyPropertyChanged("ProtoCode");
			NotifyPropertyChanged("DbContext");
		}
		#endregion

		string DbContextTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContext.mdtmpl";
		string DbContextDbSetTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContextDbSet.mdtmpl";
		string DbContextDbEntityTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContextEntity.mdtmpl";
		string DbContextEntityPrimaryKeyTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContextEntityPrimaryKey.mdtmpl";

		#region DbContext用C#コードの作成関数
		/// <summary>
		/// DbContext用C#コードの作成関数
		/// </summary>
		/// <returns>DbContext用C#コードの作成関数</returns>
		public string DbContextCode()
		{
			// UTF-8
			StreamReader sr = new StreamReader(DbContextTempletePath, Encoding.UTF8);

			// テンプレートファイル読み出し
			string dbcontext = sr.ReadToEnd();

			string dbset_code = CreateDbSetCode();
			dbcontext = dbcontext.Replace("{mendoc:dbsets}", dbset_code);

			string entity_code = CreateEntityCode();
			string entity =
			dbcontext = dbcontext.Replace("{mendoc:entities}", entity_code);

			return dbcontext;

		}
		#endregion

		#region DbSet用C#コード
		/// <summary>
		/// DbSet用C#コードの作成関数
		/// </summary>
		/// <returns>DbSet用C#コード</returns>
		private string CreateDbSetCode()
		{
			StreamReader sr = new StreamReader(DbContextDbSetTempletePath, Encoding.UTF8);
			string templete = sr.ReadToEnd();

			StringBuilder parameters_code = new StringBuilder();

			// テーブル数分回す
			foreach (var table in this.TableItems)
			{
				string parameter = templete;
				parameter = parameter.Replace("{mendoc:name}", table.Name);    // テーブル名部の置換
				parameters_code.AppendLine(parameter);
			}

			return parameters_code.ToString();
		}
		#endregion

		#region DbContext用 Entity部のコードを作成する
		/// <summary>
		/// DbContext用 Entity部のコードを作成する
		/// </summary>
		/// <returns>DbContext用 Entity部のコード</returns>
		private string CreateEntityCode()
		{
			// DbSet部用テンプレートファイルの読み込み
			StreamReader sr = new StreamReader(DbContextDbEntityTempletePath, Encoding.UTF8);
			string templete = sr.ReadToEnd();

			// Entity部用のテンプレートファイルの読み込み
			StreamReader pk_sr = new StreamReader(DbContextEntityPrimaryKeyTempletePath, Encoding.UTF8);
			string pk_templete = pk_sr.ReadToEnd();

			// コード保持用
			StringBuilder parameters_code = new StringBuilder();

			// テーブル数分回す
			foreach (var table in this.TableItems)
			{
				string parameter = templete;
				parameter = parameter.Replace("{mendoc:name}", table.Name);    // テーブル名部の置換

				StringBuilder pk = new StringBuilder();

				bool pk_find = false;	// primary keyを見つけたかどうかを判定するフラグ

				// パラメータ分回す
				foreach (var col in table.ParameterItems)
				{
					// 主キー？
					if (col.PrimaryKey)
					{
						// 既に見つかってる？
						if (!pk_find)
						{
							pk.Append(pk_templete.Replace("{mendoc:name}", col.Name));
						}
						else
						{
							pk.Append("," + pk_templete.Replace("{mendoc:name}", col.Name));
						}
						pk_find = true;
					}
				}
				parameter = parameter.Replace("{mendoc:primarykeys}", pk.ToString());
				parameters_code.AppendLine(parameter);
			}

			return parameters_code.ToString();
		}
		#endregion

		#region EntityFramework(CSharp)用コード
		/// <summary>
		/// EntityFramework(CSharp)用コード
		/// </summary>
		/// <returns></returns>
		public string EntityFrameworkCode()
        {
            if (this.TableItems.SelectedItem != null)
            {
                return this.TableItems.SelectedItem.CreateClassCode();

            }
            else
			{
				return string.Empty;
			}
		}
		#endregion

		#region インターフェースクラス用コード
		/// <summary>
		/// インターフェースクラス用コード
		/// </summary>
		/// <returns></returns>
		public string InterfaceClassCode()
		{
			if (this.TableItems.SelectedItem != null)
			{
				return this.TableItems.SelectedItem.CreateInterfaceClassCode();

			}
			else
			{
				return string.Empty;
			}
		}
		#endregion

		#region .protoファイル用コード
		/// <summary>
		/// .protoファイル用コード
		/// </summary>
		/// <returns>.protoファイル用コード</returns>
		public string ProtoCodeMessageCode()
		{
			if (this.TableItems.SelectedItem != null)
			{
				return this.TableItems.SelectedItem.CreateProtoMessageCode();

			}
			else
			{
				return string.Empty;
			}
		}
		#endregion

		#region マークダウン用コード
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
					param.PrimaryKey ? "〇" : "-",
					param.NotNull ? "〇" : "-",
					param.Type,
					param.Size != null ? param.Size : "-",
					param.Name,
					param.Description
					));
			}

			return code.ToString();
		}
		#endregion
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
