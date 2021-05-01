using menDoc.Common.Enums;
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
	public class ClassM : ModelBase
	{
		#region クラス名[Name]プロパティ
		/// <summary>
		/// クラス名[Name]プロパティ用変数
		/// </summary>
		string _Name = string.Empty;
		/// <summary>
		/// クラス名[Name]プロパティ
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
		#region クラスの説明[Description]プロパティ
		/// <summary>
		/// クラスの説明[Description]プロパティ用変数
		/// </summary>
		string _Description = string.Empty;
		/// <summary>
		/// クラスの説明[Description]プロパティ
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
		#region 作成日時[CreateDate]プロパティ
		/// <summary>
		/// 作成日時[CreateDate]プロパティ用変数
		/// </summary>
		DateTime _CreateDate = DateTime.Today;
		/// <summary>
		/// 作成日時[CreateDate]プロパティ
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
		#region テーブル属性(EntityFramework用)[TableAttribute]プロパティ
		/// <summary>
		/// テーブル属性(EntityFramework用)[TableAttribute]プロパティ用変数
		/// </summary>
		string _TableAttribute = string.Empty;
		/// <summary>
		/// テーブル属性(EntityFramework用)[TableAttribute]プロパティ
		/// </summary>
		public string TableAttribute
		{
			get
			{
				return _TableAttribute;
			}
			set
			{
				if (!_TableAttribute.Equals(value))
				{
					_TableAttribute = value;
					NotifyPropertyChanged("TableAttribute");
				}
			}
		}
		#endregion

		#region 変数リスト[ParameterItems]プロパティ
		/// <summary>
		/// 変数リスト[ParameterItems]プロパティ用変数
		/// </summary>
		ModelList<ClassParamM> _ParameterItems = new ModelList<ClassParamM>();
		/// <summary>
		/// 変数リスト[ParameterItems]プロパティ
		/// </summary>
		public ModelList<ClassParamM> ParameterItems
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
		#region 関数リスト[MethodItems]プロパティ
		/// <summary>
		/// 関数リスト[MethodItems]プロパティ用変数
		/// </summary>
		ModelList<ClassMethodM> _MethodItems = new ModelList<ClassMethodM>();
		/// <summary>
		/// 関数リスト[MethodItems]プロパティ
		/// </summary>
		public ModelList<ClassMethodM> MethodItems
		{
			get
			{
				return _MethodItems;
			}
			set
			{
				if (_MethodItems == null || !_MethodItems.Equals(value))
				{
					_MethodItems = value;
					NotifyPropertyChanged("MethodItems");
				}
			}
		}
		#endregion
		#region 関係情報リスト[RelationItems]プロパティ
		/// <summary>
		/// 関係情報リスト[RelationItems]プロパティ用変数
		/// </summary>
		ModelList<ClassRelationM> _RelationItems = new ModelList<ClassRelationM>();
		/// <summary>
		/// 関係情報リスト[RelationItems]プロパティ
		/// </summary>
		public ModelList<ClassRelationM> RelationItems
		{
			get
			{
				return _RelationItems;
			}
			set
			{
				if (_RelationItems == null || !_RelationItems.Equals(value))
				{
					_RelationItems = value;
					NotifyPropertyChanged("RelationItems");
				}
			}
		}
		#endregion

		#region ディープコピー
		/// <summary>
		/// ディープコピー
		/// </summary>
		/// <returns>コピー結果</returns>
		public ClassM DeepCopy()
		{
			var tmp = ShallowCopy<ClassM>();

			var p_list = new ModelList<ClassParamM>();
			foreach (var p in ParameterItems)
			{
				p_list.Items.Add(p.ShallowCopy<ClassParamM>());
			}
			tmp.ParameterItems = p_list;

			var p_items = new ModelList<ClassMethodM>();
			foreach (var m in MethodItems)
			{
				p_items.Items.Add(m.ShallowCopy<ClassMethodM>());
			}
			tmp.MethodItems = p_items;


			var r_items = new ModelList<ClassRelationM>();
			foreach (var r in RelationItems)
			{
				r_items.Items.Add(r.ShallowCopy<ClassRelationM>());
			}
			tmp.RelationItems = r_items;

			return tmp;
		}
		#endregion


		#region テーブル情報からクラスをつくる関数
		/// <summary>
		/// テーブル情報からクラスをつくる関数
		/// </summary>
		/// <param name="table">テーブル情報</param>
		/// <returns>クラス情報</returns>
		public static ClassM ConvertTableToClass(TableM table)
        {
			// 宣言
			ClassM class_item = new ();

			// 作成日を今日でセット
			class_item.CreateDate = DateTime.Today;

			// 作成者はPCのユーザー名
			class_item.CreateUser = Environment.UserName;

			// クラス名はテーブル名にBaseをつける
			class_item.Name = table.Name + "Base";

			// 属性にテーブル名を入れる
			class_item.TableAttribute = table.Name;

			// 説明はテーブルをコンバートした旨を記載
			class_item.Description = string.Format("テーブル:{0}に相当するクラス。<br>テーブル説明:{1}", table.Name, table.Description);

			ModelList<ClassParamM> param_list = new ModelList<ClassParamM>();
			// 変数要素の追加
			foreach (var col in table.ParameterItems)
			{
				// クラスのオブジェクトを作成
				ClassParamM param = new ClassParamM();

				// 変数名は一致させる
				param.ValueName = col.Name;

				// CSharpの型に変換する
				param.TypeName = Utilities.ConvertTypeDBtoCSharp(Utilities.DBtype.MSSQLServer, col.NotNull, col.Type);

				// 主キー属性をセット
				param.PrimaryKeyAttribute = col.PrimaryKey;

				// カラム属性をセット
				param.ColumnAttribute = col.Name;

				// カラムの説明をそのまま転用
				param.Description = col.Description;

				param_list.Items.Add(param);
			}

			// 作成した変数のリストをセット
			class_item.ParameterItems = param_list;

			return class_item;
		}
		#endregion

		#region マークダウン
		/// <summary>
		/// マークダウン
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public string Markdown
		{
			get
			{
				return GetMarkdownForClass();
			}
		}
		#endregion


		#region クラス用のマークダウンの取得
		/// <summary>
		/// クラス用のマークダウンの取得
		/// </summary>
		/// <returns>クラス用のマークダウン</returns>
		public string GetMarkdownForClass()
        {	
			return Utilities.GetClassMarkdown(this);
		}
		#endregion
	}
}
