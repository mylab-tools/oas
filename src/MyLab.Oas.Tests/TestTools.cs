using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyLab.Oas.ObjectModel;
using MyLab.Oas.SpecModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MyLab.Oas.Tests
{
    static class TestTools
    {
        public static OpenApiDescription OasDesc { get; }
    
        public static ApiDescription ApiDesc { get; }

        static TestTools()
        {
            var fn = Path.Combine(Directory.GetCurrentDirectory(), "example.yml");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            OasDesc = deserializer.Deserialize<OpenApiDescription>(File.ReadAllText(fn));
            ApiDesc = ApiDescription.Create(OasDesc);
        }
    }
}
