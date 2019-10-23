using System;
using System.Linq;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    internal class ApiDataContract
    {
        public string Id { get; set; }
        public ContractType Type { get; set; }

        public ApiDataContract ItemsContract { get; set; }

        public string Comment { get; set; }

        public ApiDataContractProperty[] Properties { get; set; }

        public ApiEnumValue[] EnumValues { get; set; }

        public static ApiDataContract Create(OpenApiSchema schemaOrigin, ComponentProvider cProvider)
        {
            OpenApiSchema schema;
            string schemaId;

            if (schemaOrigin.Ref != null)
            {
                var foundSchema = cProvider.ProvideSchema(schemaOrigin.Ref);
                schema = foundSchema.Component;
                schemaId = foundSchema.Key;
            }
            else
            {
                schema = schemaOrigin;
                schemaId = schemaOrigin.XId;
            }

            var res = new ApiDataContract
            {
                Type = DetectType(schema.Type, schema.Format),
                Comment = schema.Description,
                Id = schemaId
            };

            if (schema.Properties != null)
            {
                if(res.Type != ContractType.Object)
                    throw new InvalidOperationException($"The properties specified for not object type '{schema.Type}'");

                res.Properties = schema.Properties.Select(p => new ApiDataContractProperty
                {
                    Name = p.Key,
                    Contract = Create(p.Value,cProvider),
                    Required = schema.Required != null && Array.Exists(schema.Required, s => s == p.Key)
                }).ToArray();
            }
            if (schema.Items != null)
            {
                if (res.Type != ContractType.Array)
                    throw new InvalidOperationException($"The items type specified for not array type '{schema.Type}'");
                res.ItemsContract = Create(schema.Items,cProvider);
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
                        case null:
                        case "":
                        case "int32": return ContractType.Integer;
                        case "int64": return ContractType.Long;
                        default: throw new NotSupportedException($"Format '{format}' not supported for type '{type}'");
                    }
                }
                case "number":
                {
                    switch (format)
                    {
                        case "float": return ContractType.Float;
                        case "double": return ContractType.Double;
                        default: throw new NotSupportedException($"Format '{format}' not supported for type '{type}'");
                    }
                } 
                case "string":
                {
                    switch (format)
                    {
                        case "":
                        case null: return ContractType.String;
                        case "byte": return ContractType.Byte;
                        case "binary": return ContractType.Binary;
                        case "date": return ContractType.Date;
                        case "date-time": return ContractType.DateTime;
                        case "password": return ContractType.Password;
                        default: throw new NotSupportedException($"Format '{format}' not supported for type '{type}'") ;
                    }
                }
                case "boolean":
                {
                    switch (format)
                    {
                        case "":
                        case null: return ContractType.Boolenan;
                        default: throw new NotSupportedException($"Format '{format}' not supported for type '{type}'"); 
                    }
                }
                case "object": return ContractType.Object;
                case "array": return ContractType.Array;
                default: throw new NotSupportedException($"Type '{type}' not supported for type '{type}'");
            }
        }
    }
}