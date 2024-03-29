﻿using menDoc.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Models.ClassDiagram
{
    public class ClassMethodM : ModelBase
    {
		#region アクセス修飾子[Accessor]プロパティ
		/// <summary>
		/// アクセス修飾子[Accessor]プロパティ用変数
		/// </summary>
		AccessModifier _Accessor = new AccessModifier();
		/// <summary>
		/// アクセス修飾子[Accessor]プロパティ
		/// </summary>
		public AccessModifier Accessor
		{
			get
			{
				return _Accessor;
			}
			set
			{
				if (!_Accessor.Equals(value))
				{
					_Accessor = value;
					NotifyPropertyChanged("Accessor");
				}
			}
		}
		#endregion
		#region 戻り値[ReturnValue]プロパティ
		/// <summary>
		/// 戻り値[ReturnValue]プロパティ用変数
		/// </summary>
		string _ReturnValue = string.Empty;
		/// <summary>
		/// 戻り値[ReturnValue]プロパティ
		/// </summary>
		public string ReturnValue
		{
			get
			{
				return _ReturnValue;
			}
			set
			{
				if (!_ReturnValue.Equals(value))
				{
					_ReturnValue = value;
					NotifyPropertyChanged("ReturnValue");
				}
			}
		}
		#endregion
		#region 関数名[MethodName]プロパティ
		/// <summary>
		/// 関数名[MethodName]プロパティ用変数
		/// </summary>
		string _MethodName = string.Empty;
		/// <summary>
		/// 関数名[MethodName]プロパティ
		/// </summary>
		public string MethodName
		{
			get
			{
				return _MethodName;
			}
			set
			{
				if (!_MethodName.Equals(value))
				{
					_MethodName = value;
					NotifyPropertyChanged("MethodName");
				}
			}
		}
		#endregion
		#region 説明[Description]プロパティ
		/// <summary>
		/// 説明[Description]プロパティ用変数
		/// </summary>
		string _Description = string.Empty;
		/// <summary>
		/// 説明[Description]プロパティ
		/// </summary>
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if (!_Description.Equals(value))
				{
					_Description = value;
					NotifyPropertyChanged("Description");
				}
			}
		}
		#endregion

		#region クラス図用のマークダウンを取得する
		/// <summary>
		/// クラス図用のマークダウンを取得する
		/// </summary>
		/// <returns></returns>
		public string GetMarkdownForClassDiagram()
		{
			StringBuilder code_method = new StringBuilder();

			// 修飾子を確認
            switch (this.Accessor)
            {
				case AccessModifier.Public:	// public
				default:
					{
						code_method.Append('+');
						break;
					}
				case AccessModifier.Private: // private
					{
						code_method.Append('-');
						break;
					}
				case AccessModifier.Protected: // protected
					{
						code_method.Append('#');
						break;
					}
				case AccessModifier.Package: // package
					{
						code_method.Append('~');
						break;
					}
			}

			// 戻り値のセット
			code_method.Append(this.ReturnValue + " ");

			// 関数名のセット
			code_method.Append(this.MethodName);

			// 値を返却する
			return code_method.ToString();
		}
		#endregion
	}
}
