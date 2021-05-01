using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common
{
	public class TempletePath
	{
		/// <summary>
		/// ER図用のファイルパス
		/// </summary>
		public class ERDiagramPath
		{
			#region 一時ファイル名
			/// <summary>
			/// 一時ファイル名
			/// </summary>
			public const string TmploraryFileName = "ERTmp.html";
			#endregion

			#region 一時ファイルのパス
			/// <summary>
			/// 一時ファイルのパス
			/// </summary>
			public static string TmploraryFilePath
			{
				get
				{
					return menDoc.Common.Utilities.Utilities.TempDir + @"\" + TmploraryFileName;
				}
			}
			#endregion

			public const string OutputHtmlTmpletePath = @".\Common\Templete\HtmlCode\ERDiagramHtml.mdtmpl";
			public const string DbContextTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContext.mdtmpl";
			public const string DbContextDbSetTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContextDbSet.mdtmpl";
			public const string DbContextDbEntityTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContextEntity.mdtmpl";
			public const string DbContextEntityPrimaryKeyTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\DbContextEntityPrimaryKey.mdtmpl";
		}

		/// <summary>
		/// クラス図用のファイルパス
		/// </summary>
		public class ClassDiagramPath
		{
			#region 一時ファイル名
			/// <summary>
			/// 一時ファイル名
			/// </summary>
			public const string TmploraryFileName = "ClassTmp.html";
			#endregion
			#region 一時ファイルのパス
			/// <summary>
			/// 一時ファイルのパス
			/// </summary>
			public static string TmploraryFilePath
			{
				get
				{
					return menDoc.Common.Utilities.Utilities.TempDir + @"\" + TmploraryFileName;
				}
			}
			#endregion
			public const string OutputHtmlTmpletePath = @".\Common\Templete\HtmlCode\ClassDiagramHtml.mdtmpl";

		}


	}
}
