using System;
using System.Collections.Generic;

namespace Glasswall.Core.Engine.Common.PolicyConfig
{
    [Serializable]
    public class TiffContentManagement : ContentManagementFlagsBase
    {
        public ContentManagementFlagAction? Geotiff { get; set; }

        public List<int> GeotiffAllowlist { get; set; }

        public List<int> GeotiffDenylist { get; set; }

        public List<int> GeotiffRequiredlist { get; set; }
    }
}