using System;
using System.Collections.Generic;
using System.Text;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    class ApiMethodParameter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ApiMethodParameterLocation Location { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Obsolete { get; set; }

        public ApiMethodParameterStyle Style { get; set; }

        public ApiDataContract Contract { get; set; }

        public static ApiMethodParameter Create(OpenApiParameter apiParameter, ComponentProvider cProvider)
        {
            OpenApiParameter p;
            string pId;

            if (apiParameter.Ref != null)
            {
                var foundP = cProvider.ProvideParameter(apiParameter.Ref);
                p = foundP.Component;
                pId = foundP.Key;
            }
            else
            {
                p = apiParameter;
                pId = apiParameter.XId;
            }

            var res = new ApiMethodParameter
            {
                Name = p.Name,
                Id = pId,
                Required = p.Required,
                Description = p.Description,
                Obsolete = p.Deprecated,
                Contract = ApiDataContract.Create(p.Schema, cProvider)
            };

            try
            {
                res.Location = Enum.Parse<ApiMethodParameterLocation>(p.In, true);
            }
            catch (ArgumentException e)
            {
                throw new InvalidOperationException($"Wrong parameter location. Parameter: '{p.Name}' Location: '{p.In}'", e);
            }

            if (p.Style != null)
            {
                try
                {
                    res.Style = Enum.Parse<ApiMethodParameterStyle>(p.Style, true);
                }
                catch (ArgumentException e)
                {
                    throw new InvalidOperationException(
                        $"Wrong parameter style. Parameter: '{p.Name}' Style: '{p.Style}'", e);
                }

                if (res.Style != ApiMethodParameterStyle.Simple && res.Style != ApiMethodParameterStyle.Form)
                    throw new NotSupportedException($"Style not supported. Style: '{res.Style}'");
            }
            else
            {
                switch (res.Location)
                {
                    case ApiMethodParameterLocation.Query:
                    case ApiMethodParameterLocation.Cookie:
                        res.Style = ApiMethodParameterStyle.Form;
                        break;
                    case ApiMethodParameterLocation.Header:
                    case ApiMethodParameterLocation.Path:
                        res.Style = ApiMethodParameterStyle.Simple;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return res;
        }
    }
}
