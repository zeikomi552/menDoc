using menDoc.Models.ERDiagram;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.ViewModels
{
    public class ERDocVM : ViewModelBase
	{
		#region テーブルリスト[TableList]プロパティ
		/// <summary>
		/// テーブルリスト[TableList]プロパティ用変数
		/// </summary>
		TableListM _TableList = new TableListM();
		/// <summary>
		/// テーブルリスト[TableList]プロパティ
		/// </summary>
		public TableListM TableList
		{
			get
			{
				return _TableList;
			}
			set
			{
				if (_TableList == null || !_TableList.Equals(value))
				{
					_TableList = value;
					NotifyPropertyChanged("TableList");
				}
			}
		}
		#endregion
		#region 初期化処理
		/// <summary>
		/// 初期化処理
		/// </summary>
		public override void Init()
		{


		}
		#endregion
		#region 画面を閉じる処理
		/// <summary>
		/// 画面を閉じる処理
		/// </summary>
		public override void Close()
		{

		}
		#endregion
		#region コードの更新
		/// <summary>
		/// コードの更新
		/// </summary>
		public void RefleshCode()
		{
			this.TableList.RefleshCode();
		}
		#endregion
		#region 読み込み処理
		/// <summary>
		/// 読み込み処理
		/// </summary>
		public void Load()
		{
			try
			{
				// ダイアログのインスタンスを生成
				var dialog = new OpenFileDialog();

				// ファイルの種類を設定
				dialog.Filter = "menDoc Class図用ファイル (*.mder)|*.mder";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// 保存ファイルから読み込み
					this.TableList = XMLUtil.Deserialize<TableListM>(dialog.FileName);

					// 成功メッセージ
					//ShowMessage.ShowNoticeOK("Load Success!!", "Information");
				}

			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion
		#region 保存処理
		/// <summary>
		/// 保存処理
		/// </summary>
		public void Save()
		{
			try
			{
				// ダイアログのインスタンスを生成
				var dialog = new SaveFileDialog();

				// ファイルの種類を設定
				dialog.Filter = "menDoc ER図用ファイル (*.mder)|*.mder";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// ファイルに保存
					XMLUtil.Seialize<TableListM>(dialog.FileName, this.TableList);

					// 成功メッセージ
					ShowMessage.ShowNoticeOK("Save Success!!", "Information");

				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion
	}
}
