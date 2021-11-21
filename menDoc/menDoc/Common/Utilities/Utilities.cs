using menDoc.Common.Enums;
using menDoc.Models.ClassDiagram;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace menDoc.Common.Utilities
{
    public class Utilities
    {
        #region 実行ファイルのカレントディレクトリを返却する
        /// <summary>
        /// 実行ファイルのカレントディレクトリを返却する
        /// </summary>
        public static string ExeCurrentDir
        {
            get
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                string path = myAssembly.Location;
                DirectoryInfo di = new DirectoryInfo(path);
                // 親のディレクトリを取得する
                DirectoryInfo diParent = di.Parent;
                return diParent.FullName;
            }
        }
        #endregion

        #region アプリケーションフォルダの取得
        /// <summary>
        /// アプリケーションフォルダの取得
        /// </summary>
        /// <returns>アプリケーションフォルダパス</returns>
        public static string GetApplicationFolder()
        {
            var fv = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fv.CompanyName, fv.ProductName);
        }
        #endregion

        #region JSファイルの保存先
        /// <summary>
        /// JSファイルの保存先
        /// </summary>
        public static string JSDir
        {
            get
            {
                return ExeCurrentDir + @"\Common\js";
            }
        }
        #endregion

        #region テンポラリフォルダの作成
        /// <summary>
        /// テンポラリフォルダの作成
        /// </summary>
        public static string TempDir
        {
            get
            {
                string path = GetApplicationFolder() + @"\Temporary";
                return path;
            }
        }
        #endregion

        #region 一時フォルダの作成
        /// <summary>
        /// 一時フォルダの作成
        /// </summary>
        public static void CreateTemporaryDir()
        {
            // フォルダの存在を確認しない場合は作成する
            CheckAndCreate(TempDir);
        }
        #endregion

        #region コンフィグフォルダパス
        /// <summary>
        /// コンフィグファイルパス
        /// </summary>
        public static string ConfDirPath
        {
            get
            {
                return GetApplicationFolder() + @"\" + "config";
            }
        }
        #endregion

        #region コンフィグファイルパス
        /// <summary>
        /// コンフィグファイルパス
        /// </summary>
        public static string ConfFilePath
        {
            get
            {
                return ConfDirPath + @"\" + "menDoc.conf";
            }
        }
        #endregion

        #region コンフィグフォルダパス
        /// <summary>
        /// Configフォルダパス
        /// </summary>
        public static void CreateConfDir()
        {
            CheckAndCreate(ConfDirPath);
        }
        #endregion

        #region フォルダの存在確認
        /// <summary>
        /// フォルダの存在確認
        /// 存在しない場合は作成する
        /// </summary>
        /// <param name="directory_path">フォルダパス</param>
        public static void CheckAndCreate(string directory_path)
        {
            // フォルダの存在確認
            if (!Directory.Exists(directory_path))
            {
                DirectoryInfo di = new DirectoryInfo(directory_path);
                // 親のディレクトリを取得する
                DirectoryInfo diParent = di.Parent;

                // 1つ上の階層をチェックする
                CheckAndCreate(diParent.FullName);

                // フォルダの作成
                Directory.CreateDirectory(directory_path);
            }
        }
        #endregion

        #region Markdownのエスケープ文字
        /// <summary>
        /// Markdownのエスケープ文字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MarkdownEscape(string text)
        {
            return text.Replace("<", @"\<");
        }
        #endregion

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

        #region データベースタイプ
        /// <summary>
        /// データベースタイプ
        /// </summary>
        public enum DBtype
        {
            MSSQLServer,
            SQLite
        }
        #endregion

        #region データベース上の型がmatch_typeに等しい場合(大文字小文字の違いは無視)はreplace_typeに置き換える
        /// <summary>
        /// データベース上の型がmatch_typeに等しい場合(大文字小文字の違いは無視)は
        /// replace_typeに置き換える
        /// </summary>
        /// <param name="db_type">データベースの型</param>
        /// <param name="match_type">該当するデータベースの型</param>
        /// <param name="replace_type">置き換えるC#の型</param>
        /// <returns>match_typeに一致しなければそのまま返す　match_typeに一致した場合はreplace_typeを返す</returns>
        public static bool ConvertType(string db_type, string match_type)
        {
            if (db_type.ToLower().Equals(match_type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region DBの型を.protoの型に変換する
        /// <summary>
        /// DBの型を.protoの型に変換する
        /// </summary>
        /// <param name="db_type">データベースのタイプ/param>
        /// <param name="db_param_type">データベース側の型</param>
        /// <returns>.protoでの型</returns>
        public static string ConvertTypeDBtoProtop(string db_param_type)
        {
            if (db_param_type.ToLower().Equals("BigInt".ToLower())) return "int64";
            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Char".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Date".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("DateTime2".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("DateTimeOffset".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Float".ToLower())) return "double";
            if (db_param_type.ToLower().Equals("Binary".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Int".ToLower())) return "int32";
            if (db_param_type.ToLower().Equals("Money".ToLower())) return "decimal";
            if (db_param_type.ToLower().Equals("NChar".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("NText".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "decimal";
            if (db_param_type.ToLower().Equals("NVarChar".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Real".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("SmallInt".ToLower())) return "int32";
            if (db_param_type.ToLower().Equals("SmallMoney".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Variant".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Text".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Time".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("TinyInt".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("UniqueIdentifier".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("String".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Xml".ToLower())) return "string";
            if (db_param_type.ToLower().Equals("Bit".ToLower())) return "bool";
            return db_param_type;

        }
        #endregion

        #region Nullチェックすべきかどうか
        /// <summary>
        /// Nullチェックすべきかどうか
        /// </summary>
        /// <param name="csharp_type"></param>
        /// <returns></returns>
        public static bool IsNullCheck(string csharp_type)
        {
            if (csharp_type.ToLower().Equals("int")
                || csharp_type.ToLower().Equals("double")
                || csharp_type.ToLower().Equals("decimal")
                || csharp_type.ToLower().Equals("Single")
                || csharp_type.ToLower().Equals("Int16")
                || csharp_type.ToLower().Equals("Int32")
                || csharp_type.ToLower().Equals("Int64")
                || csharp_type.ToLower().Equals("byte")
                || csharp_type.ToLower().Equals("DateTime")
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region DBの型をC#の型に変換する
        /// <summary>
        /// DBの型をC#の型に変換する
        /// </summary>
        /// <param name="db_type">データベースのタイプ/param>
        /// <param name="db_param_type">データベース側の型</param>
        /// <returns>C#での型</returns>
        public static string ConvertTypeDBtoCSharp(DBtype db_type, bool notnull, string db_param_type)
        {
            switch (db_type)
            {
                case DBtype.MSSQLServer:
                    {
                        if (notnull)
                        {
                            if (db_param_type.ToLower().Equals("BigInt".ToLower())) return "Int64";
                            if (db_param_type.ToLower().Equals("Varchar".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Char".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Date".ToLower())) return "DateTime";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "DateTime";
                            if (db_param_type.ToLower().Equals("DateTime2".ToLower())) return "DateTime";
                            if (db_param_type.ToLower().Equals("DateTimeOffset".ToLower())) return "DateTimeOffset";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "Decimal";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Float".ToLower())) return "Double";
                            if (db_param_type.ToLower().Equals("Binary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Int".ToLower())) return "Int32";
                            if (db_param_type.ToLower().Equals("Money".ToLower())) return "Decimal";
                            if (db_param_type.ToLower().Equals("NChar".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("NText".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "Decimal";
                            if (db_param_type.ToLower().Equals("NVarChar".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Real".ToLower())) return "Single";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "DateTime";
                            if (db_param_type.ToLower().Equals("SmallInt".ToLower())) return "Int16";
                            if (db_param_type.ToLower().Equals("SmallMoney".ToLower())) return "Decimal";
                            if (db_param_type.ToLower().Equals("Variant".ToLower())) return "Object";
                            if (db_param_type.ToLower().Equals("Text".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Time".ToLower())) return "TimeSpan";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("TinyInt".ToLower())) return "Byte";
                            if (db_param_type.ToLower().Equals("UniqueIdentifier".ToLower())) return "Guid";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("String".ToLower())) return "VarChar";
                            if (db_param_type.ToLower().Equals("Xml".ToLower())) return "Xml";
                            if (db_param_type.ToLower().Equals("Bit".ToLower())) return "bool";
                            return db_param_type;
                        }
                        else
                        {
                            if (db_param_type.ToLower().Equals("BigInt".ToLower())) return "Int64?";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Varchar".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Char".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Date".ToLower())) return "DateTime?";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "DateTime?";
                            if (db_param_type.ToLower().Equals("DateTime2".ToLower())) return "DateTime?";
                            if (db_param_type.ToLower().Equals("DateTimeOffset".ToLower())) return "DateTimeOffset";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "Decimal?";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Float".ToLower())) return "Double?";
                            if (db_param_type.ToLower().Equals("Binary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Int".ToLower())) return "Int32?";
                            if (db_param_type.ToLower().Equals("Money".ToLower())) return "Decimal?";
                            if (db_param_type.ToLower().Equals("NChar".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("NText".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "Decimal?";
                            if (db_param_type.ToLower().Equals("NVarChar".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Real".ToLower())) return "Single?";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "DateTime?";
                            if (db_param_type.ToLower().Equals("SmallInt".ToLower())) return "Int16?";
                            if (db_param_type.ToLower().Equals("SmallMoney".ToLower())) return "Decimal?";
                            if (db_param_type.ToLower().Equals("Variant".ToLower())) return "Object";
                            if (db_param_type.ToLower().Equals("Text".ToLower())) return "String";
                            if (db_param_type.ToLower().Equals("Time".ToLower())) return "TimeSpan?";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("TinyInt".ToLower())) return "Byte?";
                            if (db_param_type.ToLower().Equals("UniqueIdentifier".ToLower())) return "Guid?";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("String".ToLower())) return "String";
                            //if (db_param_type.ToLower().Equals("Xml".ToLower())) return "Xml";
                            if (db_param_type.ToLower().Equals("Bit".ToLower())) return "bool?";
                            return db_param_type;
                        }
                    }
                default:
                    {
                        return "string";
                    }
            }

        }
        #endregion

        #region C#用変数の初期化処理を自動生成する
        /// <summary>
        /// C#用変数の初期化処理を自動生成する
        /// </summary>
        /// <param name="db_type">データベースタイプ</param>
        /// <param name="notnull">NotNull制約</param>
        /// <param name="db_param_type">データベースの型</param>
        /// <returns>C#のコード</returns>
        public static string CSharpTypeInitCode(DBtype db_type, bool notnull, string db_param_type)
        {
            switch (db_type)
            {
                case DBtype.MSSQLServer:
                    {
                        if(notnull)
                        {
                            if (db_param_type.ToLower().Equals("BigInt".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("long".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "new Byte[]";
                            if (db_param_type.ToLower().Equals("Char".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Varchar".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Date".ToLower())) return "DateTime.MinValue";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "DateTime.MinValue";
                            if (db_param_type.ToLower().Equals("DateTime2".ToLower())) return "DateTime.MinValue";
                            if (db_param_type.ToLower().Equals("DateTimeOffset".ToLower())) return "DateTimeOffset.MinValue";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("bool".ToLower())) return "false";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("Float".ToLower())) return "Double";
                            if (db_param_type.ToLower().Equals("Binary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Int".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("Money".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("NChar".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("NText".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("NVarChar".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Real".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "DateTime.MinValue";
                            if (db_param_type.ToLower().Equals("SmallInt".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("SmallMoney".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("Variant".ToLower())) return "new Object()";
                            if (db_param_type.ToLower().Equals("Text".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Time".ToLower())) return "TimeSpan()";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("TinyInt".ToLower())) return "new byte()";
                            if (db_param_type.ToLower().Equals("UniqueIdentifier".ToLower())) return "new Guid()";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("String".ToLower())) return "string.Empty";
                            //if (db_param_type.ToLower().Equals("Xml".ToLower())) return "Xml";
                            if (db_param_type.ToLower().Equals("Bit".ToLower())) return "false";
                            return db_param_type;
                        }
                        else
                        {
                            if (db_param_type.ToLower().Equals("BigInt".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("long".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "new Byte[]";
                            if (db_param_type.ToLower().Equals("Varchar".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Char".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Date".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("DateTime2".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("DateTimeOffset".ToLower())) return "DateTimeOffset.MinValue";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("bool".ToLower())) return "false";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("Float".ToLower())) return "Double";
                            if (db_param_type.ToLower().Equals("Binary".ToLower())) return "Byte[]";
                            if (db_param_type.ToLower().Equals("Int".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("Money".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("NChar".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("NText".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Decimal".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("NVarChar".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Real".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("DateTime".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("SmallInt".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("SmallMoney".ToLower())) return "0";
                            if (db_param_type.ToLower().Equals("Variant".ToLower())) return "new Object()";
                            if (db_param_type.ToLower().Equals("Text".ToLower())) return "string.Empty";
                            if (db_param_type.ToLower().Equals("Time".ToLower())) return "TimeSpan()";
                            if (db_param_type.ToLower().Equals("Timestamp".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("TinyInt".ToLower())) return "new byte()";
                            if (db_param_type.ToLower().Equals("UniqueIdentifier".ToLower())) return "new Guid()";
                            if (db_param_type.ToLower().Equals("VarBinary".ToLower())) return "null";
                            if (db_param_type.ToLower().Equals("String".ToLower())) return "string.Empty";
                            //if (db_param_type.ToLower().Equals("Xml".ToLower())) return "Xml";
                            if (db_param_type.ToLower().Equals("Bit".ToLower())) return "false";
                            return db_param_type;
                        }
                        
                    }
                default:
                    {
                        return "string";
                    }
            }
        }
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

        #region 多重度を記号で表す
        /// <summary>
        /// 多重度を記号で表す
        /// </summary>
        /// <param name="direction">方向</param>
        /// <param name="multi">多重度</param>
        /// <returns>マークダウン記号</returns>
        public static string ConvertMultiplicity(TableDirection direction, Multiplicity multi)
        {
            switch (multi)
            {
                case Multiplicity.ZeroOne:
                    {
                        if (direction == TableDirection.Source) return "|o";
                        else return "o|";
                    }
                case Multiplicity.ZeroMulti:
                    {
                        if (direction == TableDirection.Source) return "}o";
                        else return "o{";
                    }
                case Multiplicity.OneOne:
                    {
                        if (direction == TableDirection.Source) return "||";
                        else return "||";
                    }
                case Multiplicity.OneMulti:
                default:
                    {
                        if (direction == TableDirection.Source) return "}|";
                        else return "|{";
                    }
            }

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
            if (class_items.ClassItems.Items.Count > 0)
            {
                StringBuilder code = new StringBuilder();
                code.AppendLine(GetClassFigMarkdownHeader());
                code.AppendLine(GetClassFigMarkdownBody(class_items));
                return code.ToString();
            }
            else
            {
                return string.Empty;
            }

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
        /// <summary>
        /// クラス詳細
        /// </summary>
        /// <param name="list">クラス要素</param>
        /// <returns>マークダウン</returns>
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

            int index = 1;
            if (class_item.ParameterItems.Items.Count > 0)
            {
                code.AppendLine("|No.|型|パラメータ名|修飾子|説明|");
                code.AppendLine("|---|---|---|---|---|");

                foreach (var param in class_item.ParameterItems)
                {
                    code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|",
                        index++, MarkdownEscape(param.TypeName), MarkdownEscape(param.ValueName), ConvertAccessor(param.Accessor), param.Description));
                }
            }
            else
            {
                code.AppendLine("\t- なし");
            }

            code.AppendLine();

            code.AppendLine("- 関数情報");
            code.AppendLine();

            if(class_item.MethodItems.Items.Count > 0)
            {
                code.AppendLine("|No.|戻り値|関数名|修飾子|説明|");
                code.AppendLine("|---|---|---|---|---|");

                index = 1;
                foreach (var param in class_item.MethodItems)
                {
                    code.AppendLine(string.Format("|{0}|{1}|{2}|{3}|{4}|",
                        index++, MarkdownEscape(param.ReturnValue), MarkdownEscape(param.MethodName), ConvertAccessor(param.Accessor), param.Description));
                }
            }
            else
            {
                code.AppendLine("\t- なし");
            }

            return code.ToString();
        }
        #endregion


        #region 最上位のWindowの取得処理
        /// <summary>
        /// 最上位のWindowの取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DependencyObject GetWindow<T>(object sender)
        {
            DependencyObject depobj = (DependencyObject)sender;
            while (true)
            {
                depobj = VisualTreeHelper.GetParent(depobj);

                if (depobj is T)
                {
                    return depobj;
                }
                else if (depobj == null)
                {
                    return null;
                }
            }
        }
        #endregion

    }
}
