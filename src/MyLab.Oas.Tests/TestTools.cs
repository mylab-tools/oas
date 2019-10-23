using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyLab.Oas.SpecModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MyLab.Oas.Tests
{
    static class TestTools
    {
        private static readonly Lazy<OpenApiDescription> _oasDesc;
        public static OpenApiDescription OasDesc => _oasDesc.Value;

        static TestTools()
        {
            _oasDesc = new Lazy<OpenApiDescription>(() =>
            {
                var fn = Path.Combine(Directory.GetCurrentDirectory(), "example.yml");
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                return deserializer.Deserialize<OpenApiDescription>(File.ReadAllText(fn));
            });
        }
    }
}
