using Glasswall.Core.Engine.Common.PolicyConfig;
using System.Collections.Generic;

namespace Glasswall.Core.Engine.Common
{
    public static class Policy
    {
        public static ContentManagementFlags DefaultContentManagementFlags { get; } = new ContentManagementFlags
        {
            ExcelContentManagement = new ExcelContentManagement
            {
                DynamicDataExchange = ContentManagementFlagAction.Sanitise,
                EmbeddedFiles = ContentManagementFlagAction.Sanitise,
                EmbeddedImages = ContentManagementFlagAction.Sanitise,
                ExternalHyperlinks = ContentManagementFlagAction.Sanitise,
                InternalHyperlinks = ContentManagementFlagAction.Sanitise,
                Macros = ContentManagementFlagAction.Sanitise,
                Metadata = ContentManagementFlagAction.Sanitise,
                ReviewComments = ContentManagementFlagAction.Sanitise
            },
            WordContentManagement = new WordContentManagement
            {
                DynamicDataExchange = ContentManagementFlagAction.Sanitise,
                EmbeddedFiles = ContentManagementFlagAction.Sanitise,
                EmbeddedImages = ContentManagementFlagAction.Sanitise,
                ExternalHyperlinks = ContentManagementFlagAction.Sanitise,
                InternalHyperlinks = ContentManagementFlagAction.Sanitise,
                Macros = ContentManagementFlagAction.Sanitise,
                Metadata = ContentManagementFlagAction.Sanitise,
                ReviewComments = ContentManagementFlagAction.Sanitise
            },
            PowerPointContentManagement = new PowerPointContentManagement
            {
                EmbeddedFiles = ContentManagementFlagAction.Sanitise,
                EmbeddedImages = ContentManagementFlagAction.Sanitise,
                ExternalHyperlinks = ContentManagementFlagAction.Sanitise,
                InternalHyperlinks = ContentManagementFlagAction.Sanitise,
                Macros = ContentManagementFlagAction.Sanitise,
                Metadata = ContentManagementFlagAction.Sanitise,
                ReviewComments = ContentManagementFlagAction.Sanitise
            },
            PdfContentManagement = new PdfContentManagement
            {
                InternalHyperlinks = ContentManagementFlagAction.Sanitise,
                ExternalHyperlinks = ContentManagementFlagAction.Sanitise,
                EmbeddedImages = ContentManagementFlagAction.Sanitise,
                Metadata = ContentManagementFlagAction.Sanitise,
                EmbeddedFiles = ContentManagementFlagAction.Sanitise,
                Acroform = ContentManagementFlagAction.Sanitise,
                ActionsAll = ContentManagementFlagAction.Sanitise,
                Javascript = ContentManagementFlagAction.Sanitise,
                Watermark = "Glasswall Protected"
            },
            TiffContentManagement = new TiffContentManagement
            {
                Geotiff = ContentManagementFlagAction.Sanitise,
                GeotiffAllowlist = new List<int> { 3072, 3073 },
                GeotiffDenylist = new List<int> { 2049 },
                GeotiffRequiredlist = new List<int> { 1024, 1025, 1026 }
            }
        };
    }
}
