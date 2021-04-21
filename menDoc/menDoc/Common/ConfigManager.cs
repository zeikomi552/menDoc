using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common
{
	public class ConfigManager : ModelBase
	{
		#region ブラウザのパス[DefaultBrowzerPath]プロパティ
		/// <summary>
		/// ブラウザのパス[DefaultBrowzerPath]プロパティ用変数
		/// </summary>
		string _DefaultBrowzerPath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
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

		/// <summary>
		/// コンフィグファイルの保存
		/// </summary>
		public static void SaveConf(ConfigManager conf)
		{
			menDoc.Common.Utilities.Utilities.CreateConfDir();
			XMLUtil.Seialize<ConfigManager>(menDoc.Common.Utilities.Utilities.ConfFilePath, conf);
		}

		/// <summary>
		/// コンフィグファイルのロード処理
		/// </summary>
		/// <returns></returns>
		public static ConfigManager LoadConf()
		{
			if (File.Exists(menDoc.Common.Utilities.Utilities.ConfFilePath))
			{
				menDoc.Common.Utilities.Utilities.CreateConfDir();
				return XMLUtil.Deserialize<ConfigManager>(menDoc.Common.Utilities.Utilities.ConfFilePath);
			}
			else
			{
				ConfigManager tmp = new ConfigManager();
				SaveConf(tmp);
				return tmp;
			}
		}
	}
}
