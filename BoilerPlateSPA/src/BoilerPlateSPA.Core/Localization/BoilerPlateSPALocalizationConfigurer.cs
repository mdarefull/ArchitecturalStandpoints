using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace BoilerPlateSPA.Localization
{
    public static class BoilerPlateSPALocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(BoilerPlateSPAConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BoilerPlateSPALocalizationConfigurer).GetAssembly(),
                        "BoilerPlateSPA.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
