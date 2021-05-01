using menDoc.Common;
using menDoc.Common.Utilities;
using Microsoft.Web.WebView2.Wpf;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
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

		#region 初期化処理
		/// <summary>
		/// 初期化処理
		/// </summary>
		public override void Init()
		{
			try
			{
				var conf = ConfigManager.LoadConf();
				this.DefaultBrowzerPath = conf.DefaultBrowzerPath;
			}
			catch (Exception ex)
			{
				ShowMessage.ShowErrorOK(ex.Message, "Error");
			}
		}
		#endregion

		/// <summary>
		/// WebViewの初期化を待つ
		/// </summary>
		/// <param name="webview"></param>
		async public void SetWebViewObject(WebView2 webview)
        {
			await webview.EnsureCoreWebView2Async(null);

			this.WebviewObject = webview;
		}

		#region リロード処理
		/// <summary>
		/// リロード処理
		/// </summary>
		public void WebViewReload()
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
