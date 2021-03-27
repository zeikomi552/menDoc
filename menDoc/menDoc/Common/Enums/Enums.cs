using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common.Enums
{
	#region Single/Repeat 列挙子
	/// <summary>
	/// Single/Repeat 列挙子
	/// </summary>
	public enum SingleRepeatEnum
	{
		/// <summary>
		/// 単体
		/// </summary>
		Single = 0,
		/// <summary>
		/// 繰り返し
		/// </summary>
		Repeat
	}
	#endregion

	#region アクセス修飾子
	/// <summary>
	/// アクセス修飾子
	/// </summary>
	public enum AccessModifier
    {
		Public,
		Private,
		Protected
    }
	#endregion
}
