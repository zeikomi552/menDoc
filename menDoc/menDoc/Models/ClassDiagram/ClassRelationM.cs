using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ClassDiagram
{
    public class ClassRelationM : ModelBase
	{
		#region 関係のクラス[TargetClass]プロパティ
		/// <summary>
		/// 関係のクラス[TargetClass]プロパティ用変数
		/// </summary>
		string _TargetClass = string.Empty;
		/// <summary>
		/// 関係のクラス[TargetClass]プロパティ
		/// </summary>
		public string TargetClass
		{
			get
			{
				return _TargetClass;
			}
			set
			{
				if (!_TargetClass.Equals(value))
				{
					_TargetClass = value;
					NotifyPropertyChanged("TargetClass");
				}
			}
		}
		#endregion

	}
}
