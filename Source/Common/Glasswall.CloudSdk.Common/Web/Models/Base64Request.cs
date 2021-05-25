using Glasswall.Core.Engine.Common.PolicyConfig;
using System.ComponentModel.DataAnnotations;

namespace Glasswall.CloudSdk.Common.Web.Models
{
    public class Base64Request
    {
        public string FileName { get; set; }

        [Required]
        public string Base64 { get; set; }

        public ContentManagementFlags ContentManagementFlags { get; set; }
    }
}