using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    class ApiDescription
    {
        public OpenApiServerMethod[] ServerMethods { get; set; }

        public ApiDataContract[] DataContracts { get; set; }

        public static ApiDescription Create(OpenApiDescription description)
        {
            var res = new ApiDescription();
            if(description.Paths != null)
                res.ServerMethods = GetMethods(description.Paths, new ComponentProvider(description.Components));

            return res;
        }

        private static OpenApiServerMethod[] GetMethods(IDictionary<string, OpenApiPath> paths, ComponentProvider cProvider)
        {
            return paths.SelectMany(p =>
            {
                var resList = new List<OpenApiServerMethod>();
                if (p.Value.Delete != null) resList.Add(OpenApiServerMethod.Create(p.Key, "DELETE", p.Value.Delete, cProvider));
                if (p.Value.Get != null) resList.Add(OpenApiServerMethod.Create(p.Key, "GET", p.Value.Get, cProvider));
                if (p.Value.Head != null) resList.Add(OpenApiServerMethod.Create(p.Key, "HEAD", p.Value.Head, cProvider));
                if (p.Value.Put != null) resList.Add(OpenApiServerMethod.Create(p.Key, "PUT", p.Value.Put, cProvider));
                if (p.Value.Options != null) resList.Add(OpenApiServerMethod.Create(p.Key, "OPTIONS", p.Value.Options, cProvider));
                if (p.Value.Trace != null) resList.Add(OpenApiServerMethod.Create(p.Key, "TRACE", p.Value.Trace, cProvider));
                if (p.Value.Patch != null) resList.Add(OpenApiServerMethod.Create(p.Key, "PATCH", p.Value.Patch, cProvider));
                if (p.Value.Post != null) resList.Add(OpenApiServerMethod.Create(p.Key, "POST", p.Value.Post, cProvider));

                return resList;
            }).ToArray();
        }
    }
}
