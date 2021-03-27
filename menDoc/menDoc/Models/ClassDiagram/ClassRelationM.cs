using menDoc.Common.Enums;
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
        #region マークダウンの説明のヘッダ部
        /// <summary>
        /// マークダウンの説明のヘッダ部
        /// </summary>
        /// <returns>ヘッダ部</returns>
        public string GetMarkdownHeader()
        {
            StringBuilder code = new StringBuilder();

            code.AppendLine("|関係クラス|関係|説明|");
            code.AppendLine("|---|---|---|");

            return code.ToString();
        }
        #endregion
        #region マークダウンの説明のボディ部
        /// <summary>
        /// マークダウンの説明のボディ部
        /// </summary>
        /// <returns>ボディ部</returns>
        public string GetMarkdownBody()
        {
            StringBuilder code = new StringBuilder();

            string relation = string.Empty;

            switch (this.Relation)
            {
                case ClassRelationType.Association:
                default:
                    {
                        relation = "関係";
                        break;
                    }
                case ClassRelationType.Aggregation:
                    {
                        relation = "集約";
                        break;
                    }
                case ClassRelationType.Composition:
                    {
                        relation = "コンポジション";
                        break;
                    }
                case ClassRelationType.Dependency:
                    {
                        relation = "依存";
                        break;
                    }
                case ClassRelationType.Generalization:
                    {
                        relation = "汎化";
                        break;
                    }
                case ClassRelationType.Realization:
                    {
                        relation = "実現";
                        break;
                    }
            }


            code.AppendLine(string.Format("|{0}|{1}|{2}|", this.TargetClass, relation, this.Description));

            return code.ToString();
        }
        #endregion
    }
}
