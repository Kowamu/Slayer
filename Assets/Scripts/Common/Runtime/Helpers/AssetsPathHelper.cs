using System;
using System.IO;
using UnityEngine;

namespace Pets.Helpers.AssetPaths
{
    /// <summary>
    /// アセットパスを扱うヘルパークラス
    /// </summary>
    public class AssetsPathHelper
    {
        /// <summary>
        /// 絶対パスからアセットパスに変換する
        /// </summary>
        /// <param name="absolutePath">絶対パス</param>
        public static string FromAbsolutePath(string absolutePath)
        {
            return absolutePath.Replace(Application.dataPath, "Assets");
        }

        /// <summary>
        /// アセットパスから絶対パスに変換する
        /// </summary>
        /// <param name="assetsPath">アセットパス</param>
        public static string ToAbsolutePath(string assetsPath)
        {
            if (!assetsPath.StartsWith("Assets"))
            {
                throw new ArgumentException(@"""Assets""から始まるアセットパスを指定してください。", nameof(assetsPath));
            }
            return assetsPath.Replace("Assets", Application.dataPath);
        }

        /// <summary>
        /// 再帰的にディレクトリを作成する
        /// </summary>
        /// <param name="assetsPath">Assetsから始まるディレクトリパス</param>
        public static void CreateDirectoryRecursive(string assetsPath)
        {
            // Assetsから始まってない場合は処理できない
            if (!assetsPath.StartsWith("Assets/"))
            {
                throw new ArgumentException("Assetsから始まるパスを指定してください", nameof(assetsPath));
            }

            // 末尾の'/'を削除
            if (assetsPath.EndsWith("/"))
            {
                assetsPath = assetsPath[..^1];
            }

            // フォルダを作成する
            var absolutePath = ToAbsolutePath(assetsPath);
            Directory.CreateDirectory(absolutePath);
        }
    }
}