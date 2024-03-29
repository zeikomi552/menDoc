﻿using menDoc.Common;
using menDoc.Common.Utilities;
using menDoc.Models.ERDiagram;
using menDoc.Views;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static menDoc.Common.TempletePath;

namespace menDoc.ViewModels
{
    public class ERDocVM : WebViewPrevVM
	{
		#region 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// <summary>
		/// 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// </summary>
		public override string TempHtmlPath
		{
			get
			{
				return ERDiagramPath.TmploraryFilePath;
			}
		}
		#endregion

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
			try
			{
				base.Init();	// 親の初期化処理を使用する
				this.TableList.SaveTemporary();	// 一時ファイルの保存
			}
			catch (Exception ex)
			{
				_logger.Error(ex.Message);
				ShowMessage.ShowErrorOK(ex.Message, "Error");
			}
		}
		#endregion

		#region Previewの更新処理
		/// <summary>
		/// Previewの更新処理
		/// </summary>
		public void RefreshPreview()
		{
			try
			{
				this.TableList.SaveTemporary(); // 一時ファイルの保存
				this.WebviewObject.Reload();
			}
			catch (Exception ex)
			{
				ShowMessage.ShowErrorOK(ex.Message, "Error");
			}
		}
		#endregion

		#region ソースコードのリフレッシュ
		/// <summary>
		/// ソースコードのリフレッシュ
		/// </summary>
		public void RefreshCode()
		{
			try
			{
				this.TableList.RefreshCode();
			}
			catch (Exception ex)
			{
				ShowMessage.ShowErrorOK(ex.Message, "Error");
			}
		}
		#endregion

		#region プレビューおよびコードのリフレッシュ
		/// <summary>
		/// プレビューおよびコードのリフレッシュ
		/// </summary>
		public void Refresh()
		{
			try
			{
				RefreshPreview();
				RefreshCode();
			}
			catch (Exception ex)
			{
				ShowMessage.ShowErrorOK(ex.Message, "Error");
			}
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

					// 一時ファイルの保存
					string path = this.TableList.SaveTemporary();

					// リフレッシュ
					Refresh();
				}

			}
			catch (Exception e)
			{
				_logger.Error(e.Message);
				ShowMessage.ShowErrorOK(e.Message + "ClassDoc_SetRelationVM", "Error");
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
