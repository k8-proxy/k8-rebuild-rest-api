using Glasswall.Core.Engine.Common;
using Glasswall.Core.Engine.Common.FileProcessing;
using Glasswall.Core.Engine.Common.GlasswallEngineLibrary;
using Glasswall.Core.Engine.Common.PolicyConfig;
using Microsoft.Extensions.Logging;
using System;

namespace Glasswall.Core.Engine.FileProcessing
{
    public class FileAnalyser : IFileAnalyser
    {
        private readonly IGlasswallFileOperations _glasswallFileOperations;
        private readonly IAdaptor<ContentManagementFlags, string> _glasswallConfigurationAdaptor;
        private readonly ILogger<FileAnalyser> _logger;

        public FileAnalyser(
            IGlasswallFileOperations glasswallFileOperations,
            IAdaptor<ContentManagementFlags, string> glasswallConfigurationAdaptor,
            ILogger<FileAnalyser> logger)
        {
            _glasswallFileOperations = glasswallFileOperations ?? throw new ArgumentNullException(nameof(glasswallFileOperations));
            _glasswallConfigurationAdaptor = glasswallConfigurationAdaptor ?? throw new ArgumentNullException(nameof(glasswallConfigurationAdaptor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GetReport(ContentManagementFlags flags, string fileType, byte[] fileBytes)
        {
            string analysisReport = string.Empty;

            string glasswallConfiguration = _glasswallConfigurationAdaptor.Adapt(flags);

            if (glasswallConfiguration == null)
            {
                return analysisReport;
            }

            EngineOutcome setConfigurationEngineOutcome = _glasswallFileOperations.SetConfiguration(glasswallConfiguration);

            if (setConfigurationEngineOutcome != EngineOutcome.Success)
            {
                return analysisReport;
            }

            _glasswallFileOperations.AnalyseFile(fileBytes, fileType, out analysisReport);


            return analysisReport;
        }
    }
}
