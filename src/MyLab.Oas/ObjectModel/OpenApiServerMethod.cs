using System.Linq;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    internal class OpenApiServerMethod
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string Service { get; set; }
        public string Comment { get; set; }
        public ApiRequestContent[] RequestContents { get; set; }

        public static OpenApiServerMethod Create(string path, string method, OpenApiOperation operation, ComponentProvider cProvider)
        {
            var res = new OpenApiServerMethod
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
}