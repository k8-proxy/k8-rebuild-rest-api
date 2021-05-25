using Glasswall.Core.Engine.Common;
using Glasswall.Core.Engine.Common.FileProcessing;
using Glasswall.Core.Engine.Common.GlasswallEngineLibrary;
using Glasswall.Core.Engine.Common.PolicyConfig;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Glasswall.Core.Engine.FileProcessing
{
    public class FileProtector : IFileProtector
    {
        private readonly IGlasswallFileOperations _glasswallFileOperations;
        private readonly IAdaptor<ContentManagementFlags, string> _glasswallConfigurationAdaptor;
        private readonly ILogger<FileProtector> _logger;

        public FileProtector(IGlasswallFileOperations glasswallFileOperations,
            IAdaptor<ContentManagementFlags, string> glasswallConfigurationAdaptor,
            ILogger<FileProtector> logger)
        {
            _glasswallFileOperations = glasswallFileOperations ?? throw new ArgumentNullException(nameof(glasswallFileOperations));
            _glasswallConfigurationAdaptor = glasswallConfigurationAdaptor ?? throw new ArgumentNullException(nameof(glasswallConfigurationAdaptor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IFileProtectResponse GetProtectedFile(ContentManagementFlags contentManagementFlags, string fileType, byte[] fileBytes)
        {
            FileProtectResponse response = new FileProtectResponse { ProtectedFile = Enumerable.Empty<byte>().ToArray() };

            string glasswallConfiguration = _glasswallConfigurationAdaptor.Adapt(contentManagementFlags);
            EngineOutcome configurationOutcome = _glasswallFileOperations.SetConfiguration(glasswallConfiguration);
            if (configurationOutcome != EngineOutcome.Success)
            {
                _logger.Log(LogLevel.Error, "Error processing configuration");
                response.Outcome = configurationOutcome;
                return response;
            }

            string version = _glasswallFileOperations.GetLibraryVersion();
            _logger.LogInformation($"Engine version: {version}");

            EngineOutcome engineOutcome = _glasswallFileOperations.ProtectFile(fileBytes, fileType, out byte[] protectedFile);
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
