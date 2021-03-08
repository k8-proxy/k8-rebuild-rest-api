using System;
using System.IO;
using System.Linq;
using Glasswall.Core.Engine.Common;
using Glasswall.Core.Engine.Common.FileProcessing;
using Glasswall.Core.Engine.Common.GlasswallEngineLibrary;
using Glasswall.Core.Engine.Common.PolicyConfig;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Glasswall.Core.Engine.FileProcessing
{
    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }

    public class FileProxyProtector : IFileProtector
    {
        private readonly IGlasswallFileOperations _glasswallFileOperations;
        private readonly IAdaptor<ContentManagementFlags, string> _glasswallConfigurationAdaptor;
        private readonly ILogger<FileProxyProtector> _logger;

        public FileProxyProtector(IGlasswallFileOperations glasswallFileOperations,
            IAdaptor<ContentManagementFlags, string> glasswallConfigurationAdaptor,
            ILogger<FileProxyProtector> logger)
        {
            _glasswallFileOperations = glasswallFileOperations ?? throw new ArgumentNullException(nameof(glasswallFileOperations));
            _glasswallConfigurationAdaptor = glasswallConfigurationAdaptor ?? throw new ArgumentNullException(nameof(glasswallConfigurationAdaptor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        EngineOutcome ProxyProtectFile(byte[] fileContent, string fileType, out byte[] protectedFile)
        {
            protectedFile = new byte[0];
            EngineOutcome returnValue = EngineOutcome.Error;

            string uuid = Guid.NewGuid().ToString();
            string tempDir = "/var/temp/";
            string inputPath = tempDir + uuid;
            string outputPath = tempDir + uuid + ".out";
            string cloudProxyAppPath = "/proxy/cloud-proxy-app";
            string command = cloudProxyAppPath + " -i " + inputPath + " -o " + outputPath;

            try
            {
                do
                {
                    if (!File.Exists(cloudProxyAppPath))
                    {
                        _logger.Log(LogLevel.Error, "ProxyProtectFile cannot find {0}", cloudProxyAppPath);
                        break;
                    }

                    File.WriteAllBytes(inputPath, fileContent);
                    if (!File.Exists(inputPath))
                    {
                        _logger.Log(LogLevel.Error, "ProxyProtectFile cannot find {0}", inputPath);
                        break;
                    }

                    ShellHelper.Bash(command);

                    if (!File.Exists(outputPath))
                    {
                        _logger.Log(LogLevel.Error, "ProxyProtectFile cannot find {0}", outputPath);
                        break;
                    }
                    protectedFile = File.ReadAllBytes(outputPath);

                    returnValue = EngineOutcome.Success;
                    _logger.Log(LogLevel.Information, "ProxyProtectFile completed successfully");
                }
                while (false);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "ProxyProtectFile ERROR");
            }

            // Clean it up
            if (File.Exists(inputPath))
            {
                File.Delete(inputPath);
            }

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            return returnValue;
        }

        public IFileProtectResponse GetProtectedFile(ContentManagementFlags contentManagementFlags, string fileType, byte[] fileBytes)
        {
            var response = new FileProtectResponse { ProtectedFile = Enumerable.Empty<byte>().ToArray() };

            var glasswallConfiguration = _glasswallConfigurationAdaptor.Adapt(contentManagementFlags);
            var configurationOutcome = _glasswallFileOperations.SetConfiguration(glasswallConfiguration);
            if (configurationOutcome != EngineOutcome.Success)
            {
                _logger.Log(LogLevel.Error, "Error processing configuration");
                response.Outcome = configurationOutcome;
                return response;
            }

            var version = _glasswallFileOperations.GetLibraryVersion();
            _logger.LogInformation($"Engine version: {version}");

            var engineOutcome = ProxyProtectFile(fileBytes, fileType, out var protectedFile);
            response.Outcome = engineOutcome;
            response.ProtectedFile = protectedFile;

            if (engineOutcome != EngineOutcome.Success)
            {
                response.ErrorMessage = _glasswallFileOperations.GetEngineError();
                _logger.Log(LogLevel.Error, $"Unable to protect file, reason: {engineOutcome}. Error Message: {response.ErrorMessage}");
            }

            return response;
        }
    }

}
