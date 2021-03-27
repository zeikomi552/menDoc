using menDoc.Common.Enums;
using menDoc.Common.Utilities;
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
        #region 接続の関係[Relation]プロパティ
        /// <summary>
        /// 接続の関係[Relation]プロパティ用変数
        /// </summary>
        ClassRelationType _Relation = ClassRelationType.Association;
        /// <summary>
        /// 接続の関係[Relation]プロパティ
        /// </summary>
        public ClassRelationType Relation
        {
            get
            {
                return _Relation;
            }
            set
            {
                if (!_Relation.Equals(value))
                {
                    _Relation = value;
                    NotifyPropertyChanged("Relation");
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
        /// <returns>マークダウン</returns>
        public string GetMarkdownForClassDiagram(string own_class)
        {
            // 値を返却する
            return Utilities.GetClassRelationMarkdown(own_class, this);
        }
        #endregion
    }
}
