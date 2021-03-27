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
		/// <summary>
		/// public +
		/// </summary>
		Public,
		/// <summary>
		/// private -
		/// </summary>
		Private,
		/// <summary>
		/// protected #
		/// </summary>
		Protected,
		/// <summary>
		/// package ~
		/// </summary>
		Package
	}
    #endregion

    public enum ClassRelationType
    {
		/// <summary>
		/// 関連
		/// </summary>
		Association,
		/// <summary>
		/// 集約
		/// </summary>
		Aggregation,
		/// <summary>
		/// 集約(逆向き)
		/// </summary>
		AggregationR,
		/// <summary>
		/// コンポジット
		/// </summary>
		Composit,
		/// <summary>
		/// コンポジット(逆向き)
		/// </summary>
		CompositR,
		/// <summary>
		/// 依存
		/// </summary>
		Dependency,
		/// <summary>
		/// 依存(逆向き)
		/// </summary>
		DependencyR,
		/// <summary>
		/// 汎化
		/// </summary>
		Generalization,
		/// <summary>
		/// 汎化(逆向き)
		/// </summary>
		GeneralizationR,
		/// <summary>
		/// 実現
		/// </summary>
		Realization,
		/// <summary>
		/// 実現(逆向き)
		/// </summary>
		RealizationR
	}

}
