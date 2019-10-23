using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    class ApiDescription
    {
        public ApiServerMethod[] ServerMethods { get; set; }

        public ApiDataContract[] DataContracts { get; set; }

        public static ApiDescription Create(OpenApiDescription description)
        {
            var res = new ApiDescription();
            if(description.Paths != null)
                res.ServerMethods = GetMethods(description.Paths, new ComponentProvider(description.Components));

            return res;
        }

        private static ApiServerMethod[] GetMethods(IDictionary<string, OpenApiPath> paths, ComponentProvider cProvider)
        {
            return paths.SelectMany(p =>
            {
                var resList = new List<ApiServerMethod>();
                if (p.Value.Delete != null) resList.Add(ApiServerMethod.Create(p.Key, "DELETE", p.Value.Delete, cProvider));
                if (p.Value.Get != null) resList.Add(ApiServerMethod.Create(p.Key, "GET", p.Value.Get, cProvider));
                if (p.Value.Head != null) resList.Add(ApiServerMethod.Create(p.Key, "HEAD", p.Value.Head, cProvider));
                if (p.Value.Put != null) resList.Add(ApiServerMethod.Create(p.Key, "PUT", p.Value.Put, cProvider));
                if (p.Value.Options != null) resList.Add(ApiServerMethod.Create(p.Key, "OPTIONS", p.Value.Options, cProvider));
                if (p.Value.Trace != null) resList.Add(ApiServerMethod.Create(p.Key, "TRACE", p.Value.Trace, cProvider));
                if (p.Value.Patch != null) resList.Add(ApiServerMethod.Create(p.Key, "PATCH", p.Value.Patch, cProvider));
                if (p.Value.Post != null) resList.Add(ApiServerMethod.Create(p.Key, "POST", p.Value.Post, cProvider));

                return resList;
            }).ToArray();
        }
    }

    class ApiEnumValue
    {
        public string Value { get; set; }
        public string Comment { get; set; }
    }

    class ApiDataContractProperty
    {
        public ApiDataContract Contract { get; set; }
        public string Name { get; set; }

        public bool Required { get; set; }
    }

    internal class ApiDataContract
    {
        public ContractType Type { get; set; }

        public ApiDataContract ItemsContract { get; set; }

        public string Comment { get; set; }

        public ApiDataContractProperty[] Properties { get; set; }

        public ApiEnumValue[] EnumValues { get; set; }

        public static ApiDataContract Create(OpenApiSchema schema, ComponentProvider cProvider)
        {
            var res = new ApiDataContract
            {
                Type = DetectType(schema.Type, schema.Format),
                Comment = schema.Description
            };

            if (schema.Properties != null)
            {
                if(res.Type != ContractType.Object)
                    throw new InvalidOperationException($"The properties specified for not object type '{schema.Type}'");

                res.Properties = schema.Properties.Select(p => new ApiDataContractProperty
                {
                    Name = p.Key,
                    Contract = Create(
                        p.Value.Ref != null
                            ? cProvider.ProvideSchema(p.Value.Ref)
                            : p.Value,
                        cProvider),
                    Required = schema.Required != null && Array.Exists(schema.Required, s => s == p.Key)
                }).ToArray();
            }
            if (schema.Items != null)
            {
                if (res.Type != ContractType.Array)
                    throw new InvalidOperationException($"The items type specified for not array type '{schema.Type}'");
                res.ItemsContract = Create(
                    schema.Items.Ref != null
                        ? cProvider.ProvideSchema(schema.Items.Ref)
                        : schema.Items,
                        cProvider);
            }

            if (schema.Enum != null)
            {
                if (res.Type != ContractType.String)
                    throw new InvalidOperationException($"The enum items specified for not string type '{schema.Type}'");

                res.EnumValues = schema.Enum.Select
                (e => new ApiEnumValue
                {
                    Value = e,
                    Comment = schema.XEnumDescription?.FirstOrDefault(d => d.Item == e)?.Description
                }).ToArray();
            }

            return res;
        }

        static ContractType DetectType(string type, string format)
        {
            switch (type)
            {
                case "integer":
                    {
                        switch (format)
                        {
                            case "int32": return ContractType.Integer;
                            case "int64": return ContractType.Long;
                            default: throw new NotSupportedException($"Format '{format}' not supported");
                        }
                    }
                case "number":
                    {
                        switch (format)
                        {
                            case "float": return ContractType.Float;
                            case "double": return ContractType.Double;
                            default: throw new NotSupportedException($"Format '{format}' not supported");
                        }
                    } 
                case "string":
                    {
                        switch (format)
                        {
                            case "": return ContractType.String;
                            case "byte": return ContractType.Byte;
                            case "binary": return ContractType.Binary;
                            case "date": return ContractType.Date;
                            case "date-time": return ContractType.DateTime;
                            case "password": return ContractType.Password;
                            default: throw new NotSupportedException($"Format '{format}' not supported");
                        }
                    }
                case "boolean":
                    {
                        switch (format)
                        {
                            case "": return ContractType.Boolenan;
                            default: throw new NotSupportedException($"Format '{format}' not supported"); 
                        }
                    }
                case "object": return ContractType.Object;
                case "array": return ContractType.Array;
                default: throw new NotSupportedException($"Type '{type}' not supported");
            }
        }
    }

    enum ContractType
    {
        Integer,
        Long,
        Float,
        Double,
        String, 
        Byte,
        Binary,
        Boolenan,
        Date,
        DateTime,
        Password,
        Object,
        Array
    }

    class ApiRequestContent
    {
        public string MimeType { get; set; }
        public ApiDataContract Contract { get; set; }
    }

    internal class ApiServerMethod
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string Service { get; set; }
        public string Comment { get; set; }
        public ApiRequestContent[] RequestContents { get; set; }

        public static ApiServerMethod Create(string path, string method, OpenApiOperation operation, ComponentProvider cProvider)
        {
            var res = new ApiServerMethod
            {
                Method = method,
                Path = path,
                Description = operation.Description,
                Service = operation.Tags.FirstOrDefault(),
                Comment = (operation.Summary ?? string.Empty) + " " + operation.Description
            };

            if (operation.RequestBody?.Content != null)
            {
                res.RequestContents = operation.RequestBody.Content
                    .Select(c => 
                    new ApiRequestContent
                    {
                        MimeType = c.Key,
                        Contract = ApiDataContract.Create(
                            c.Value.Schema.Ref != null
                                ? cProvider.ProvideSchema(c.Value.Schema.Ref)
                                : c.Value.Schema
                            , cProvider)
                    })
                    .ToArray();
            }

            return res;
        }
    }

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
