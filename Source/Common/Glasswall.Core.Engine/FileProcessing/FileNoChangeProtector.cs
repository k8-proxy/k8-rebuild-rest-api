using System;
using System.IO;
using System.Linq;
using Glasswall.Core.Engine.Common;
using Glasswall.Core.Engine.Common.FileProcessing;
using Glasswall.Core.Engine.Common.GlasswallEngineLibrary;
using Glasswall.Core.Engine.Common.PolicyConfig;
using Microsoft.Extensions.Logging;

namespace Glasswall.Core.Engine.FileProcessing
{
    public class FileNoChangeProtector : IFileProtector
    {
        private readonly IGlasswallFileOperations _glasswallFileOperations;
        private readonly IAdaptor<ContentManagementFlags, string> _glasswallConfigurationAdaptor;
        private readonly ILogger<FileNoChangeProtector> _logger;

        public FileNoChangeProtector(IGlasswallFileOperations glasswallFileOperations,
            IAdaptor<ContentManagementFlags, string> glasswallConfigurationAdaptor,
            ILogger<FileNoChangeProtector> logger)
        {
            _glasswallFileOperations = glasswallFileOperations ?? throw new ArgumentNullException(nameof(glasswallFileOperations));
            _glasswallConfigurationAdaptor = glasswallConfigurationAdaptor ?? throw new ArgumentNullException(nameof(glasswallConfigurationAdaptor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            var engineOutcome = EngineOutcome.Success;
            response.Outcome = engineOutcome;
            response.ProtectedFile = fileBytes;

            if (engineOutcome != EngineOutcome.Success)
            {
                response.ErrorMessage = _glasswallFileOperations.GetEngineError();
                _logger.Log(LogLevel.Error, $"Unable to protect file, reason: {engineOutcome}. Error Message: {response.ErrorMessage}");
            }

            return response;
        }
    }
}
