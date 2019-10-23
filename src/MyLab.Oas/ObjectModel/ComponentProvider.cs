using System;
using System.Linq;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    internal class ComponentProvider
    {
        private readonly OpenApiComponents _components;

        /// <summary>
        /// Initializes a new instance of <see cref="ComponentProvider"/>
        /// </summary>
        public ComponentProvider(OpenApiComponents components)
        {
            _components = components;
        }

        public OpenApiSchema ProvideSchema(string reference)
        {
            ValidateReference(reference);

            var r = ParseReference(reference);

            const string path = "/components/schemas/";

            var key = GetKey(r.Path, path);

            var schema = _components.Schemas?.FirstOrDefault(s => s.Key == key);

            if(!schema.HasValue)
                throw new InvalidOperationException($"Schema not found. Reference: '{reference}'");

            return schema.Value.Value;
        }

        private string GetKey(string refPath, string basePath)
        {
            if (!refPath.StartsWith(basePath))
                throw new InvalidOperationException($"Wrong path. Expected base path: '{basePath}' but got '{refPath}'");

            return refPath.Substring(basePath.Length);
        }

        private (string File, string Path) ParseReference(string reference)
        {
            int separatorPos = reference.IndexOf('#');

            return (reference.Remove(separatorPos), reference.Substring(separatorPos + 1));
        }

        private static void ValidateReference(string reference)
        {
            if (string.IsNullOrWhiteSpace(reference))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(reference));

            if (!reference.StartsWith("#/"))
                throw new NotSupportedException($"External references not supported. Wrong reference: '{reference}'");

            if(reference.Count(ch => ch == '#') != 1)
                throw new InvalidOperationException($"Wrong reference format: '{reference}'");
        }
    }
}