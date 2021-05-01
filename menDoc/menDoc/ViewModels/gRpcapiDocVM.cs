using menDoc.Common;
using menDoc.Common.Utilities;
using menDoc.Models;
using menDoc.Views;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static menDoc.Common.TempletePath;

namespace menDoc.ViewModels
{
    public class gRpcapiDocVM : WebViewPrevVM
	{
		#region サービス[Service]プロパティ
		/// <summary>
		/// サービス[Service]プロパティ
		/// </summary>
		public gRPCServiceM Service
		{
			get
			{
				return GlobalValue.Service;
			}
			set
			{
				if (GlobalValue.Service == null || !GlobalValue.Service.Equals(value))
				{
					GlobalValue.Service = value;
					NotifyPropertyChanged("Service");
				}
			}
		}
		#endregion

		#region 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// <summary>
		/// 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// </summary>
		public override string TempHtmlPath
		{
			get
			{
				return gRPCPath.TmploraryFilePath;
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
				base.Init();    // 親の初期化処理を使用する
				this.Service.SaveTemporary(); // 一時ファイルの保存
				//this.Service.Backup();        // バックアップデータの作成
			}
			catch (Exception ex)
			{
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
				this.Service.SaveTemporary(); // 一時ファイルの保存
				this.WebviewObject.Reload();
			}
			catch (Exception ex)
			{
				ShowMessage.ShowErrorOK(ex.Message, "Error");
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
				dialog.Filter = "menDoc gRPC用ファイル (*.mdgrpc)|*.mdgrpc";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// 保存ファイルから読み込み
					this.Service = XMLUtil.Deserialize<gRPCServiceM>(dialog.FileName);

					// プレビューの更新
					RefreshPreview();
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
				dialog.Filter = "menDoc gRPC用ファイル (*.mdgrpc)|*.mdgrpc";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// ファイルに保存
					XMLUtil.Seialize<gRPCServiceM>(dialog.FileName, this.Service);

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

		#region 自動生成コードのリフレッシュ
		/// <summary>
		/// 自動生成コードのリフレッシュ
		/// </summary>
		public void RefleshProtoCode()
		{
			this.Service.RefleshCode();
		}
		#endregion

		#region APIを下へ移動
		/// <summary>
		/// APIを下へ移動
		/// </summary>
		public void MoveDown_gAPI()
		{
			try
			{
				// nullチェック
				if (this.Service.APIs.SelectedItem != null)
				{
					// 上へ移動
					this.Service.APIs.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region APIを上へ移動
		/// <summary>
		/// APIを上へ移動
		/// </summary>
		public void MoveUp_gAPI()
		{
			try
			{
				// nullチェック
				if (this.Service.APIs.SelectedItem != null)
				{
					// 上へ移動
					this.Service.APIs.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region Requestを下へ移動
		/// <summary>
		/// Requestを下へ移動
		/// </summary>
		public void MoveDown_RequestItesm()
		{
			try
			{
				// nullチェック
				if (this.Service.APIs.SelectedItem != null && this.Service.APIs.SelectedItem.RequestItems.SelectedItem != null)
				{
					// 上へ移動
					this.Service.APIs.SelectedItem.RequestItems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region Requestを上へ移動
		/// <summary>
		/// Requestを上へ移動
		/// </summary>
		public void MoveUp_RequestItems()
		{
			try
			{
				// nullチェック
				if (this.Service.APIs.SelectedItem != null && this.Service.APIs.SelectedItem.RequestItems.SelectedItem != null)
				{
					// 上へ移動
					this.Service.APIs.SelectedItem.RequestItems.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region Replyを下へ移動
		/// <summary>
		/// Replyを下へ移動
		/// </summary>
		public void MoveDown_ReplyItesm()
		{
			try
			{
				// nullチェック
				if (this.Service.APIs.SelectedItem != null && this.Service.APIs.SelectedItem.Replytems.SelectedItem != null)
				{
					// 上へ移動
					this.Service.APIs.SelectedItem.Replytems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region Requestを上へ移動
		/// <summary>
		/// Requestを上へ移動
		/// </summary>
		public void MoveUp_ReplyItems()
		{
			try
			{
				// nullチェック
				if (this.Service.APIs.SelectedItem != null && this.Service.APIs.SelectedItem.RequestItems.SelectedItem != null)
				{
					// 上へ移動
					this.Service.APIs.SelectedItem.Replytems.MoveUP();
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
