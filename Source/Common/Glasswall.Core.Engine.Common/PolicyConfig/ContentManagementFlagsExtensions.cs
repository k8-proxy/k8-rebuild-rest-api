using System.Linq;

namespace Glasswall.Core.Engine.Common.PolicyConfig
{
    public static class ContentManagementFlagsExtensions
    {
        /// <summary>
        /// Validates the input content management and sets the unspecified fields to default
        /// </summary>
        /// <param name="contentManagementFlags"></param>
        /// <returns></returns>
        public static ContentManagementFlags ValidatedOrDefault(this ContentManagementFlags contentManagementFlags)
        {
            if (contentManagementFlags == null)
            {
                return Policy.DefaultContentManagementFlags;
            }

            foreach (System.Reflection.PropertyInfo property in typeof(ContentManagementFlags).GetProperties())
            {
                if (!property.PropertyType.IsSubclassOf(typeof(ContentManagementFlagsBase)))
                {
                    continue;
                }

                object inputFlagSection = property.GetValue(contentManagementFlags);
                object defaultFlagSection = property.GetValue(Policy.DefaultContentManagementFlags);

                if (inputFlagSection == null)
                {
                    property.SetValue(contentManagementFlags, defaultFlagSection);
                }
                else
                {
                    foreach (System.Reflection.PropertyInfo flagProps in
                        inputFlagSection.GetType()
                            .GetProperties()
                            .Where(s => s.PropertyType == typeof(ContentManagementFlagAction?)))
                    {
                        object inputFlag = flagProps.GetValue(inputFlagSection);
                        object defaultFlag = flagProps.GetValue(defaultFlagSection);

                        if (inputFlag == null)
                        {
                            flagProps.SetValue(inputFlagSection, defaultFlag);
                        }
                    }
                }
            }

            return contentManagementFlags;
        }
    }
}
