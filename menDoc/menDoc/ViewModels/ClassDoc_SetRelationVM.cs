using menDoc.Common;
using menDoc.Models.ClassDiagram;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.ViewModels
{
    public class ClassDoc_SetRelationVM : ViewModelBase
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

		#region [ClassNames]プロパティ
		/// <summary>
		/// [ClassNames]プロパティ用変数
		/// </summary>
		ModelList<string> _ClassNames = new ModelList<string>();
		/// <summary>
		/// [ClassNames]プロパティ
		/// </summary>
		public ModelList<string> ClassNames
		{
			get
			{
				return _ClassNames;
			}
			set
			{
				if (_ClassNames == null || !_ClassNames.Equals(value))
				{
					_ClassNames = value;
					NotifyPropertyChanged("ClassNames");
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
				List<string> class_name = new List<string>();

				// クラス情報
				foreach (var class_item in this.ClassList.ClassItems)
				{
					class_name.Add(class_item.Name);
				}
				this.ClassNames.Items = new System.Collections.ObjectModel.ObservableCollection<string>(class_name);
			}
			catch (Exception e)
			{
				_logger.Error(e.Message);
				ShowMessage.ShowErrorOK(e.Message, "Error");
			}
		}
		#endregion

		#region 追加ボタン押下処理
		/// <summary>
		/// 追加ボタン押下処理
		/// </summary>
		public void Add()
		{
			try
			{
				this.DialogResult = true;
			}
			catch (Exception e)
			{
				ShowMessage.ShowErrorOK(e.Message, "Error");
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
