using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using WanwanLand.Slayer.Editor.Domain.Assets;
using Debug = UnityEngine.Debug;

namespace WanwanLand.Slayer.Editor.Generators
{
    public class MasterMemoryCodeGenerator
    {
        private const string GeneratorDirectoryAssetPath = @"GeneratorTools\MasterMemory.Generator";

        public void Generate(AssetPath input, AssetPath output, string @namespace)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            var generatorPath = Path.Combine(projectDirectory, GeneratorDirectoryAssetPath);

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

            var importDirectoryPath = Path.Combine(projectDirectory, input.Value);
            var outputDirectoryPath = Path.Combine(projectDirectory, output.Value);
            var processArgs = $@"-i ""{importDirectoryPath}"" -o ""{outputDirectoryPath}"" -n ""{@namespace}""";
            
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
            };
        }
    }
}