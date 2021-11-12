using menDoc.Common;
using menDoc.Common.Utilities;
using menDoc.Models.ClassDiagram;
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
	public class ClassDocVM : WebViewPrevVM
	{
		#region 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// <summary>
		/// 一時Htmlファイルパス[TempHtmlPath]プロパティ
		/// </summary>
		public override string TempHtmlPath
		{
			get
			{
				return ClassDiagramPath.TmploraryFilePath;
			}
		}
		#endregion

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
			try
			{
				base.Init();    // 親の初期化処理を使用する
				this.ClassList.SaveTemporary(); // 一時ファイルの保存
				//this.ClassList.Backup();        // バックアップデータの作成
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
				this.ClassList.SaveTemporary(); // 一時ファイルの保存
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
				dialog.Filter = "menDoc Class図用ファイル (*.mdclass)|*.mdclass";

				// ダイアログを表示する
				if (dialog.ShowDialog() == true)
				{
					// 保存ファイルから読み込み
					this.ClassList = XMLUtil.Deserialize<ClassListM>(dialog.FileName);
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

		#region クラスリスト上へ移動
		/// <summary>
		/// クラスリスト上へ移動
		/// </summary>
		public void MoveUp_ClassList()
		{
			try
			{
				// nullチェック
				if (this.ClassList.ClassItems.SelectedItem != null)
				{
					// 上へ移動
					this.ClassList.ClassItems.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region クラスリスト下へ移動
		/// <summary>
		/// クラスリスト下へ移動
		/// </summary>
		public void MoveDown_ClassList()
		{
			try
			{
				// nullチェック
				if(this.ClassList.ClassItems.SelectedItem != null)
                {
					// 下へ移動
					this.ClassList.ClassItems.MoveDown();
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
				if (this.ClassList.ClassItems.SelectedItem != null
					&& this.ClassList.ClassItems.SelectedItem.ParameterItems.SelectedItem != null)
				{
					// 上へ移動
					this.ClassList.ClassItems.SelectedItem.ParameterItems.MoveUP();
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
				if (this.ClassList.ClassItems.SelectedItem != null
					&& this.ClassList.ClassItems.SelectedItem.ParameterItems.SelectedItem != null)
				{
					// 下へ移動
					this.ClassList.ClassItems.SelectedItem.ParameterItems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region 関数リスト上へ移動
		/// <summary>
		/// 関数リスト上へ移動
		/// </summary>
		public void MoveUp_MethodList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.ClassList.ClassItems.SelectedItem != null
					&& this.ClassList.ClassItems.SelectedItem.MethodItems.SelectedItem != null)
				{
					// 上へ移動
					this.ClassList.ClassItems.SelectedItem.MethodItems.MoveUP();
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
				if (this.ClassList.ClassItems.SelectedItem != null
					&& this.ClassList.ClassItems.SelectedItem.MethodItems.SelectedItem != null)
				{
					// 下へ移動
					this.ClassList.ClassItems.SelectedItem.MethodItems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region リレーションリスト上へ移動
		/// <summary>
		/// リレーションリスト上へ移動
		/// </summary>
		public void MoveUp_RelationList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.ClassList.ClassItems.SelectedItem != null
					&& this.ClassList.ClassItems.SelectedItem.RelationItems.SelectedItem != null)
				{
					// 上へ移動
					this.ClassList.ClassItems.SelectedItem.RelationItems.MoveUP();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region リレーションリスト下へ移動
		/// <summary>
		/// リレーションリスト下へ移動
		/// </summary>
		public void MoveDown_RelationList()
		{
			try
			{
				// 選択されているかどうかをチェック
				if (this.ClassList.ClassItems.SelectedItem != null
					&& this.ClassList.ClassItems.SelectedItem.RelationItems.SelectedItem != null)
				{
					// 下へ移動
					this.ClassList.ClassItems.SelectedItem.RelationItems.MoveDown();
				}
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region クラスの関係を追加する処理
		/// <summary>
		/// クラスの関係を追加する処理
		/// </summary>
		public void SetRelation()
        {
			try
			{
				if (this.ClassList.ClassItems.SelectedItem == null)
				{
					ShowMessage.ShowNoticeOK("Classが選択されていません。", "通知");
					return;
				}

				ClassDoc_SetRelationV wnd = new ClassDoc_SetRelationV();
				var vm = wnd.BaseGird.DataContext as ClassDoc_SetRelationVM;

				// クラスのリストをビューモデルに渡す
				vm.ClassList = this.ClassList;


				if (wnd.ShowDialog() == true)
				{
					// nullチェック
					if (vm.ClassNames.SelectedItem == null)
					{
						ShowMessage.ShowNoticeOK("登録相手のClassが選択されませんでした。", "通知");
						return;
					}

					ClassRelationM relation = new ClassRelationM();
					relation.TargetClass = vm.ClassNames.SelectedItem;

					vm.ClassList.ClassItems.SelectedItem.RelationItems.Items.Add(relation);
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
