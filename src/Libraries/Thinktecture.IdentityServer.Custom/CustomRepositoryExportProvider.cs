using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using Thinktecture.IdentityServer.Repositories;

namespace Thinktecture.IdentityServer.Custom
{
    public class CustomRepositoryExportProvider : ExportProvider
    {
        private readonly Dictionary<string, string> _mappings;

        public CustomRepositoryExportProvider()
        {
            var section = ConfigurationManager.GetSection(CustomConfigurationSection.SectionName) as CustomConfigurationSection;

            _mappings = new Dictionary<string, string>
            {
                { typeof(ICustomUserRepository).FullName, section.CustomUserRepository}
            };
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            var exports = new List<Export>();

            string implementingType;

            if (_mappings.TryGetValue(definition.ContractName, out implementingType))
            {
                var t = Type.GetType(implementingType);
                if (t == null)
                {
                    throw new InvalidOperationException("Type not found for interface: " + definition.ContractName);
                }

                var instance = t.GetConstructor(Type.EmptyTypes).Invoke(null);
                var exportDefintion = new ExportDefinition(definition.ContractName, new Dictionary<string, object>());
                var toAdd = new Export(exportDefintion, () => instance);

                exports.Add(toAdd);
            }

            return exports;
        }
    }
}