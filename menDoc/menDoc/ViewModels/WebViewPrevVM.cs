using menDoc.Common;
using menDoc.Common.Utilities;
using Microsoft.Web.WebView2.Wpf;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace menDoc.ViewModels
{
    public class WebViewPrevVM : ViewModelBase
    {
		#region WebView2用オブジェクト[WebviewObject]プロパティ
		/// <summary>
		/// WebView2用オブジェクト[WebviewObject]プロパティ用変数
		/// </summary>
		WebView2 _WebviewObject = new WebView2();
		/// <summary>
		/// WebView2用オブジェクト[WebviewObject]プロパティ
		/// </summary>
		public WebView2 WebviewObject
		{
			get
			{
				return _WebviewObject;
			}
			set
			{
				if (_WebviewObject == null || !_WebviewObject.Equals(value))
				{
					_WebviewObject = value;
				}
			}
		}
		#endregion

		#region 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// <summary>
		/// 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// </summary>
		public virtual string TempHtmlPath
		{
			get
			{
				return string.Empty;
			}
		}
		#endregion


		#region ブラウザのパス[DefaultBrowzerPath]プロパティ
		/// <summary>
		/// ブラウザのパス[DefaultBrowzerPath]プロパティ用変数
		/// </summary>
		string _DefaultBrowzerPath = string.Empty;
		/// <summary>
		/// ブラウザのパス[DefaultBrowzerPath]プロパティ
		/// </summary>
		public string DefaultBrowzerPath
		{
			get
			{
				return _DefaultBrowzerPath;
			}
			set
			{
				if (!_DefaultBrowzerPath.Equals(value))
				{
					_DefaultBrowzerPath = value;
					NotifyPropertyChanged("DefaultBrowzerPath");
				}
			}
		}
		#endregion

		#region プレビュー処理
		/// <summary>
		/// プレビュー処理
		/// </summary>
		public virtual void Preview()
		{
			try
			{
				// プレビューの表示処理（ブラウザを使用する）
				System.Diagnostics.Process.Start(this.DefaultBrowzerPath, this.TempHtmlPath);
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
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
				if (!Directory.Exists(menDoc.Common.Utilities.Utilities.TempDir))
				{
					Directory.CreateDirectory(menDoc.Common.Utilities.Utilities.TempDir);
				}

				var conf = ConfigManager.LoadConf();
				this.DefaultBrowzerPath = conf.DefaultBrowzerPath;
			}
			catch (Exception ex)
			{
				_logger.Error(ex.Message);
				ShowMessage.ShowErrorOK(ex.Message, "Error");
			}
		}
		#endregion

		#region WebView用のオブジェクトのセット処理
		/// <summary>
		/// WebView用のオブジェクトのセット処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void SetWebViewObject(object sender, EventArgs e)
		{
			var wv = sender as WebView2;

			// nullチェック
			if (wv != null)
			{
				// オブジェクトの保持
				SetWebviewObject(wv);
			}
		}
		#endregion

		#region WebViewObjectのセット
		/// <summary>
		/// WebViewObjectのセット
		/// </summary>
		/// <param name="webview"></param>
		async public void SetWebviewObject(WebView2 webview)
        {
			await webview.EnsureCoreWebView2Async(null);

			this.WebviewObject = webview;
		}
		#endregion


		#region リロード処理
		/// <summary>
		/// リロード処理
		/// </summary>
		public virtual void WebViewReload()
		{
			if (this.WebviewObject != null)
            {
				this.WebviewObject.Reload();
			}
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

	}
}
