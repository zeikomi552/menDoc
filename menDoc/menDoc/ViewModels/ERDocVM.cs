using menDoc.Common;
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
		/// テーブルリスト[TableList]プロパティ
		/// </summary>
		public TableListM TableList
		{
			get
			{
				return GlobalValue.TableList;
			}
			set
			{
				if (GlobalValue.TableList == null || !GlobalValue.TableList.Equals(value))
				{
					GlobalValue.TableList = value;
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

		#region Class図の方へ値を入力する
		/// <summary>
		/// Class図の方へ値を入力する
		/// </summary>
		public void SetClass()
        {
			try
			{
				if (ShowMessage.ShowQuestionYesNo("クラスへ登録します。よろしいですか？", "確認") == System.Windows.MessageBoxResult.Yes)
				{
					// nullチェック
					if (this.TableList.TableItems.SelectedItem != null)
					{
						// 選択箇所をクラスに変換してセットする
						GlobalValue.ClassList.SetTable(this.TableList.TableItems.SelectedItem);
					}
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region gRPCへセットする
		/// <summary>
		/// gRPCへセットする
		/// </summary>
		public void SetgRPC()
		{
			try
			{
				if (ShowMessage.ShowQuestionYesNo("gRPCへ登録します。よろしいですか？", "確認") == System.Windows.MessageBoxResult.Yes)
				{
					// nullチェック
					if (this.TableList.TableItems.SelectedItem != null)
					{
						// 選択箇所をクラスに変換してセットする
						GlobalValue.Service.SetTable(this.TableList.TableItems.SelectedItem);
					}
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
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

		#region テーブルリスト上へ移動
		/// <summary>
		/// テーブルリスト上へ移動
		/// </summary>
		public void MoveUp_ClassList()
		{
			try
			{
				// nullチェック
				if (this.TableList.TableItems.SelectedItem != null)
				{
					// 上へ移動
					this.TableList.TableItems.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region テーブルリスト下へ移動
		/// <summary>
		/// テーブルリスト下へ移動
		/// </summary>
		public void MoveDown_ClassList()
		{
			try
			{
				// nullチェック
				if (this.TableList.TableItems.SelectedItem != null)
				{
					// 下へ移動
					this.TableList.TableItems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region パラメータリスト上へ移動
		/// <summary>
		/// パラメータリスト上へ移動
		/// </summary>
		public void MoveUp_ParamList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.TableList.TableItems.SelectedItem != null
					&& this.TableList.TableItems.SelectedItem.ParameterItems.SelectedItem != null)
				{
					// 上へ移動
					this.TableList.TableItems.SelectedItem.ParameterItems.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region パラメータリスト下へ移動
		/// <summary>
		/// パラメータリスト下へ移動
		/// </summary>
		public void MoveDown_ParamList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.TableList.TableItems.SelectedItem != null
					&& this.TableList.TableItems.SelectedItem.ParameterItems.SelectedItem != null)
				{
					// 下へ移動
					this.TableList.TableItems.SelectedItem.ParameterItems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region 関係リスト上へ移動
		/// <summary>
		/// 関係リスト上へ移動
		/// </summary>
		public void MoveUp_MethodList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.TableList.TableItems.SelectedItem != null
					&& this.TableList.TableItems.SelectedItem.TableRelationList.SelectedItem != null)
				{
					// 上へ移動
					this.TableList.TableItems.SelectedItem.TableRelationList.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region 関数リスト下へ移動
		/// <summary>
		/// 関数リスト下へ移動
		/// </summary>
		public void MoveDown_MethodList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.TableList.TableItems.SelectedItem != null
					&& this.TableList.TableItems.SelectedItem.TableRelationList.SelectedItem != null)
				{
					// 下へ移動
					this.TableList.TableItems.SelectedItem.TableRelationList.MoveDown();
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
