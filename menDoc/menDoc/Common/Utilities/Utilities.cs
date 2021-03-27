using menDoc.Common.Enums;
using menDoc.Models.ClassDiagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menDoc.Common.Utilities
{
    public class Utilities
    {
        #region クラス図の関係をマークダウンに変換する
        /// <summary>
        /// クラス図の関係をマークダウンに変換する
        /// </summary>
        /// <param name="source_class">関係元のクラス</param>
        /// <param name="class_relation">関係情報</param>
        /// <returns>マークダウン</returns>
        public static string GetClassRelationMarkdown(string source_class, ClassRelationM class_relation)
        {
            // 空文字確認
            if (string.IsNullOrWhiteSpace(class_relation.Description))
            {
                // 関係の詳細説明なしで返却
                return source_class + " " + GetClassRelationMarkdown(class_relation.Relation) + " " + class_relation.TargetClass;
            }
            else
            {
                // 関係の詳細説明ありで返却する
                return source_class + " " + GetClassRelationMarkdown(class_relation.Relation) + " " + class_relation.TargetClass + " : " + class_relation.Description;
            }
        }

        /// <summary>
        /// クラス図の関係をマークダウンに変換する
        /// 関係部のみ
        /// </summary>
        /// <param name="relation">関係</param>
        /// <returns>マークダウン</returns>
        public static string GetClassRelationMarkdown(ClassRelationType relation)
        {

            StringBuilder code_method = new StringBuilder();

            // 修飾子を確認
            switch (relation)
            {
                case ClassRelationType.Association: // 関連
                default:
                    {
                        code_method.Append("--");
                        break;
                    }
                case ClassRelationType.Aggregation: // 集約
                    {
                        code_method.Append("--o");
                        break;
                    }
                case ClassRelationType.AggregationR: // 集約(逆)
                    {
                        code_method.Append("o--");
                        break;
                    }
                case ClassRelationType.Composit: // コンポジット
                    {
                        code_method.Append("--*");
                        break;
                    }
                case ClassRelationType.CompositR: // コンポジット(逆)
                    {
                        code_method.Append("*--");
                        break;
                    }
                case ClassRelationType.Dependency: // 依存
                    {
                        code_method.Append("..>");
                        break;
                    }
                case ClassRelationType.DependencyR: // 依存(逆)
                    {
                        code_method.Append("<..");
                        break;
                    }
                case ClassRelationType.Generalization: // 汎化
                    {
                        code_method.Append("--|>");
                        break;
                    }
                case ClassRelationType.GeneralizationR: // 汎化(逆)
                    {
                        code_method.Append("<|--");
                        break;
                    }
                case ClassRelationType.Realization: // 実現
                    {
                        code_method.Append("..|>");
                        break;
                    }
                case ClassRelationType.RealizationR: // 実現(逆)
                    {
                        code_method.Append("<|..");
                        break;
                    }
            }
            return code_method.ToString();
        }
        #endregion
        #region クラス図のパラメータをマークダウンに変換する
        /// <summary>
        /// クラス図のパラメータをマークダウンに変換する
        /// </summary>
        /// <param name="accessor">修飾子</param>
        /// <param name="type">型</param>
        /// <param name="value_name">変数名</param>
        /// <returns>マークダウン</returns>
        public static string GetClassParameterMarkdown(AccessModifier accessor, string type, string value_name)
        {
            StringBuilder code_method = new StringBuilder();

            // 修飾子を確認
            switch (accessor)
            {
                case AccessModifier.Public: // public
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

            // 型のセット
            code_method.Append(type + " ");

            // 変数名のセット
            code_method.Append(value_name);

            // 値を返却する
            return code_method.ToString();
        }

        /// <summary>
        /// クラス図用変数のマークダウン変換
        /// </summary>
        /// <param name="param">パラメータ</param>
        /// <returns>マークダウン</returns>
        public static string GetClassParameterMarkdown(ClassParamM param)
        {
            return GetClassParameterMarkdown(param.Accessor, param.TypeName, param.ValueName);
        }
        #endregion
        #region クラス図の関数をマークダウンに変換する
        /// <summary>
        /// クラス図の関数をマークダウンに変換する
        /// </summary>
        /// <param name="method">関数情報</param>
        /// <returns>マークダウン</returns>
        public static string GetClassMethodMarkdown(ClassMethodM method)
        {
            StringBuilder code_method = new StringBuilder();

            // 修飾子を確認
            switch (method.Accessor)
            {
                case AccessModifier.Public: // public
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
            code_method.Append(method.ReturnValue + " ");

            // 関数名のセット
            code_method.Append(method.MethodName);

            // 値を返却する
            return code_method.ToString();
        }
        #endregion
        #region クラス図のマークダウン取得
        /// <summary>
        /// クラス図のマークダウン取得
        /// </summary>
        /// <param name="class_item">クラス要素</param>
        /// <returns></returns>
        public static string GetClassMarkdown(ClassM class_item)
        {
            StringBuilder code_method = new StringBuilder();

            // クラス要素の作成
            if (class_item.MethodItems.Items.Count > 0
                || class_item.ParameterItems.Items.Count > 0)
            {
                code_method.AppendLine("class " + class_item.Name + " {");

                // 変数用のマークダウンを取得する
                foreach (var param in class_item.ParameterItems)
                {
                    string param_markdown = GetClassParameterMarkdown(param);
                    code_method.AppendLine(param_markdown);
                }

                // 関数用のマークダウンを取得する
                foreach (var method in class_item.MethodItems)
                {
                    string method_markdown = GetClassMethodMarkdown(method);
                    code_method.AppendLine(method_markdown);
                }

                code_method.AppendLine("}");
            }


            // クラスの関係を作成
            foreach (var relation in class_item.RelationItems)
            {
                string relation_markdown = GetClassRelationMarkdown(class_item.Name, relation);
                code_method.AppendLine(relation_markdown);
            }

            return code_method.ToString();
        }
        #endregion
    }
}
