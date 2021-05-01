using System;
using System.Collections.Generic;
using System.IO;
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

			public const string TempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ClassCode.mdtmpl";
			public const string InterfaceClassTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\InterfaceClassCode.mdtmpl";
			public const string InterfacePropertyTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\InterfacePropertyCode.mdtmpl";
			public const string ProtoClassTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ProtoMessageClassCode.mdtmpl";
			public const string ProtoPropertyTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ProtoMessagePropertyCode.mdtmpl";
			public const string ClassMethodTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ClassMethod.mdtmpl";
			public const string ClassMethodPKTempletePath = @".\Common\Templete\CSharpCode\EntityFramework\ClassMethodPK.mdtmpl";
			public const string CopyParametersPath = @".\Common\Templete\CSharpCode\EntityFramework\CopyParameters.mdtmpl";
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
		/// <summary>
		/// gRPC用HTMLファイルパス
		/// </summary>
		public class gRPCPath
		{
			#region 一時ファイル名
			/// <summary>
			/// 一時ファイル名
			/// </summary>
			public const string TmploraryFileName = "gRPCtmp.html";
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
			public const string OutputHtmlTmpletePath = @".\Common\Templete\HtmlCode\gRPCHtml.mdtmpl";
			public const string RecieveClassTempletePath = @".\Common\Templete\CSharpCode\gRPC\ServiceClass.mdtmpl";
			public const string RecieveMethodTempletePath = @".\Common\Templete\CSharpCode\gRPC\RequestMethod.mdtmpl";
			public const string RecieveEventHandlerTempletePath = @".\Common\Templete\CSharpCode\gRPC\RecieveEventHandler.mdtmpl";

		}
	}
}
