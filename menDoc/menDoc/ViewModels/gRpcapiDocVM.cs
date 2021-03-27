﻿using menDoc.Models;
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
    public class gRpcapiDocVM : ViewModelBase
    {
		#region サービス[Service]プロパティ
		/// <summary>
		/// サービス[Service]プロパティ用変数
		/// </summary>
		gRPCServiceM _Service = new gRPCServiceM();
		/// <summary>
		/// サービス[Service]プロパティ
		/// </summary>
		public gRPCServiceM Service
		{
			get
			{
				return _Service;
			}
			set
			{
				if (_Service == null || !_Service.Equals(value))
				{
					_Service = value;
					NotifyPropertyChanged("Service");
				}
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
	}
}
