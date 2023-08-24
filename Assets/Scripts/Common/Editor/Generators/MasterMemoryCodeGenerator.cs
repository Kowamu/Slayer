using System;
using System.Diagnostics;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Common.Editor.Generators
{
    public static class MasterMemoryCodeGenerator
    {
        
        private const string GeneratorDirectoryAssetPath = @"..\GeneratorTools\MasterMemory.Generator";
        private const string Namespace = "Slayer.Domain";
        private const string ImportDirectoryAssetPath = @"Scripts\Slayer\Runtime\Domain";
        private const string OutputDirectoryAssetPath = @"Scripts\Slayer\Runtime\Domain\Generated\MasterMemory";

        [MenuItem("Generator/MasterMemory")]
        private static void Generate()
        {
            var assetsDirectoryPath = Application.dataPath;
            var generatorPath = Path.Combine(assetsDirectoryPath, GeneratorDirectoryAssetPath);

#if UNITY_EDITOR_WIN
            generatorPath = Path.Combine(generatorPath, "win-x64/MasterMemory.Generator.exe");
#elif UNITY_EDITOR_OSX
            path = Path.Combine(generatorPath, "osx-x64/MasterMemory.Generator");
#elif UNITY_EDITOR_LINUX
            path = Path.Combine(generatorPath, "linux-x64/MasterMemory.Generator");
#else
            throw new System.NotImplementedException("このプラットフォームはサポートされていません");
#endif
            
            if (!File.Exists(generatorPath))
            {
                throw new FileNotFoundException($"{nameof(MasterMemoryCodeGenerator)} : ファイルが存在しません。");
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
            
            Debug.Log($"{nameof(MasterMemoryCodeGenerator)} : 生成開始");

            var process = Process.Start(processStartInfo);
            if (process == null)
            {
                throw new SystemException($"{nameof(MasterMemoryCodeGenerator)} : プロセスの起動に失敗しました。");
            }
            
            process.EnableRaisingEvents = true;
            process.Exited += (_, _) =>
            {
                var stdOutput = process.StandardOutput.ReadToEnd();
                process.Dispose();
                
                Debug.Log($"{nameof(MasterMemoryCodeGenerator)} : {stdOutput}");
                Debug.Log($"{nameof(MasterMemoryCodeGenerator)} : 生成終了");
                
                // 生成したファイルの修正
                var memoryDatabaseCodePath = Path.Combine(outputDirectoryPath, "MemoryDatabase.cs");
                if (!File.Exists(memoryDatabaseCodePath))
                {
                    throw new FileNotFoundException($"{nameof(MasterMemoryCodeGenerator)} : ファイルが存在しません。");
                }
                
                var memoryDatabaseCode = File.ReadAllText(memoryDatabaseCodePath);
                memoryDatabaseCode = memoryDatabaseCode.Replace(
                    "MessagePack.MessagePackSerializerOptions options)", 
                    "MessagePack.MessagePackSerializerOptions options, int maxDegreeOfParallelism)");
                File.WriteAllText(memoryDatabaseCodePath, memoryDatabaseCode);
                
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