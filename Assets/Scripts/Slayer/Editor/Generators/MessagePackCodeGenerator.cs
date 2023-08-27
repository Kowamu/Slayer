using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using WanwanLand.Slayer.Editor.Domain.Assets;
using Debug = UnityEngine.Debug;

namespace  WanwanLand.Slayer.Editor.Generators
{
    public class MessagePackCodeGenerator
    {
        private const string GeneratorDirectoryAssetPath = @"GeneratorTools\mpc";

        public void Generate(AssetPath input, AssetPath output) 
        {
            var projectDirectoryPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            var generatorPath = Path.Combine(projectDirectoryPath, GeneratorDirectoryAssetPath);

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

            var importDirectoryPath = Path.Combine(projectDirectoryPath, input.Value);
            var outputDirectoryPath = Path.Combine(projectDirectoryPath, output.Value);
            var processArgs = $@"-i ""{importDirectoryPath}"" -o ""{outputDirectoryPath}""";
            
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
            };
        }
    }
}