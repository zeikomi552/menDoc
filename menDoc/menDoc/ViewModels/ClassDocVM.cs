using menDoc.Common;
using menDoc.Models.ClassDiagram;
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
    public class ClassDocVM : ViewModelBase
    {
		#region クラスのリスト[ClassList]プロパティ
		/// <summary>
		/// クラスのリスト[ClassList]プロパティ
		/// </summary>
		public ClassListM ClassList
		{
			get
			{
				return GlobalValue.ClassList;
			}
			set
			{
				if (GlobalValue.ClassList == null || !GlobalValue.ClassList.Equals(value))
				{
					GlobalValue.ClassList = value;
					NotifyPropertyChanged("ClassList");
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
			this.ClassList.RefleshCode();
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
				dialog.Filter = "menDoc Class図用ファイル (*.mdclass)|*.mdclass";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// 保存ファイルから読み込み
					this.ClassList = XMLUtil.Deserialize<ClassListM>(dialog.FileName);

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
				dialog.Filter = "menDoc Class図用ファイル (*.mdclass)|*.mdclass";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// ファイルに保存
					XMLUtil.Seialize<ClassListM>(dialog.FileName, this.ClassList);

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


		/// <summary>
		/// 上へ移動
		/// </summary>
		public void MoveUp_ClassList()
		{
			this.ClassList.ClassItems.MoveUP();
		}

		/// <summary>
		/// 下へ移動
		/// </summary>
		public void MoveDown_ClassList()
		{
			this.ClassList.ClassItems.MoveDown();
		}
	}
}
