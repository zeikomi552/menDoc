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
        public static string MarkdownEscape(string text)
        {
            return text.Replace("<", @"\<");
        }

        #region Markdown クラス図
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
        #endregion

        #region 関係を日本語文字列に変換する
        /// <summary>
        /// 関係を日本語文字列に変換する
        /// </summary>
        /// <param name="relation_type">関係のタイプ</param>
        /// <returns>文字列</returns>
        public static string ConvertRelation(ClassRelationType relation_type)
        {
            string relation = string.Empty;
            switch (relation_type)
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
                case ClassRelationType.Composit:
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

            return relation;
        }
        #endregion

        #region 修飾子を文字列に変換する
        /// <summary>
        /// 修飾子を文字列に変換する
        /// </summary>
        /// <param name="accessor">修飾子</param>
        /// <returns>文字列</returns>
        public static string ConvertAccessor(AccessModifier accessor)
        {
            switch (accessor)
            {
                case AccessModifier.Public:
                default:
                    {
                        return "public";
                    }
                case AccessModifier.Private:
                    {
                        return "private";
                    }
                case AccessModifier.Protected:
                    {
                        return "protected";
                    }
                case AccessModifier.Package:
                    {
                        return "package";
                    }
            }

        }
        #endregion

        #region Markdown クラス一覧
        /// <summary>
        /// クラス用一覧
        /// </summary>
        /// <param name="class_items">クラス要素</param>
        /// <returns>マークダウン</returns>
        public static string GetClassClassList(ClassListM class_items)
        {
            StringBuilder code = new StringBuilder();
            code.AppendLine(GetClassFigMarkdownHeader());
            code.AppendLine(GetClassFigMarkdownBody(class_items));
            return code.ToString();
        }
        #region マークダウンの説明のヘッダ部
        /// <summary>
        /// マークダウンの説明のヘッダ部
        /// </summary>
        /// <returns>ヘッダ部</returns>
        public static string GetClassFigMarkdownHeader()
        {
            StringBuilder code = new StringBuilder();

            code.AppendLine("|No.|クラス名|クラスの説明|作成日|作成者|");
            code.Append("|---|---|---|---|---|");

            return code.ToString();
        }
        #endregion
        #region マークダウンの説明のボディ部
        /// <summary>
        /// マークダウンの説明のボディ部
        /// </summary>
        /// <returns>ボディ部</returns>
        public static string GetClassFigMarkdownBody(ClassListM class_items)
        {
            StringBuilder code = new StringBuilder();

            int index = 1;
            foreach (var tmp in class_items)
            {
                code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|", index ++, tmp.Name, tmp.Description, tmp.CreateDate.ToShortDateString(), tmp.CreateUser));
            }
            return code.ToString();
        }
        #endregion

        #endregion

        #region Markdown クラス詳細
        public static string GetClassDetails(ClassListM list)
        {
            StringBuilder code = new StringBuilder();
            foreach(var class_item in list)
            {
                code.AppendLine(string.Format("### クラス名:{0}", class_item.Name));
                code.AppendLine(GetClassDetail(class_item));
            }

            return code.ToString();
        }
        /// <summary>
        /// クラス詳細をマークダウンで返却する
        /// </summary>
        /// <param name="class_item">クラス要素</param>
        /// <returns>マークダウン</returns>
        public static string GetClassDetail(ClassM class_item)
        {
            StringBuilder code = new StringBuilder();
            code.AppendLine("- 変数情報");
            code.AppendLine();

            code.AppendLine("|No.|型|パラメータ名|修飾子|説明|");
            code.AppendLine("|---|---|---|---|---|");

            int index = 1;
            foreach (var param in class_item.ParameterItems)
            {
                code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|",
                    index++, MarkdownEscape(param.TypeName), MarkdownEscape(param.ValueName), ConvertAccessor(param.Accessor), param.Description));
            }
            code.AppendLine();

            code.AppendLine("- 関数情報");
            code.AppendLine();

            code.AppendLine("|No.|戻り値|関数名|修飾子|説明|");
            code.AppendLine("|---|---|---|---|---|");

            index = 1;
            foreach (var param in class_item.MethodItems)
            {
                code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|",
                    index++, MarkdownEscape(param.ReturnValue), MarkdownEscape(param.MethodName), ConvertAccessor(param.Accessor), param.Description));
            }

            return code.ToString();
        }
        #endregion
    }
}
