using System;
using System.Diagnostics;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Common.Editor.Generators
{
    public static class MessagePackCodeGenerator
    {
        private const string GeneratorDirectoryAssetPath = @"..\GeneratorTools\mpc";
        private const string Namespace = "Slayer.Domains";
        private const string ImportDirectoryAssetPath = @"Scripts\Slayer\Runtime\Domains";
        private const string OutputDirectoryAssetPath = @"Scripts\Slayer\Runtime\Domains\Generated\MessagePack";

        [MenuItem("Generator/MessagePack")]
        private static void Generate()
        {
            var assetsDirectoryPath = Path.GetFullPath(Application.dataPath);
            var generatorPath = Path.Combine(assetsDirectoryPath, GeneratorDirectoryAssetPath);

#if UNITY_EDITOR_WIN
            generatorPath = Path.Combine(generatorPath, "win/mpc.exe");
#elif UNITY_EDITOR_OSX
            path = Path.Combine(generatorPath, "osx/mpc");
#elif UNITY_EDITOR_LINUX
            path = Path.Combine(generatorPath, "linux/mpc");
#else
            throw new System.NotImplementedException("このプラットフォームはサポートされていません");
#endif
            
            if (!File.Exists(generatorPath))
            {
                throw new FileNotFoundException($"{nameof(MessagePackCodeGenerator)} : ファイルが存在しません。");
            }

            var importDirectoryPath = Path.Combine(assetsDirectoryPath, ImportDirectoryAssetPath);
            var outputDirectoryPath = Path.Combine(assetsDirectoryPath, OutputDirectoryAssetPath);
            var processArgs = $@"-i ""{importDirectoryPath}"" -o ""{outputDirectoryPath}"" -c -n ""{Namespace}""";
            
            var processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                FileName = generatorPath,
                Arguments = processArgs
            };
            
            Debug.Log($"{nameof(MessagePackCodeGenerator)} : 生成開始");

            var process = Process.Start(processStartInfo);
            if (process == null)
            {
                throw new SystemException($"{nameof(MessagePackCodeGenerator)} : プロセスの起動に失敗しました。");
            }
            
            process.EnableRaisingEvents = true;
            process.Exited += (_, _) =>
            {
                var stdOutput = process.StandardOutput.ReadToEnd();
                process.Dispose();
                
                Debug.Log($"{nameof(MessagePackCodeGenerator)} : {stdOutput}");
                Debug.Log($"{nameof(MessagePackCodeGenerator)} : 生成終了");
                
                // メインスレッドに戻してからコンパイルを実行する
                UniTask.Void(async () =>
                {
                    await UniTask.SwitchToMainThread();
                    AssetDatabase.Refresh();
                });
            };
        }
    }
}