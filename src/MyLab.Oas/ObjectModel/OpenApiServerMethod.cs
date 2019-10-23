using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ApiDataContract ResponseContract { get; set; }
        public ApiResponseDescription[] ResponsesDescriptions { get; set; }
        public ApiMethodParameter[] Parameters { get; set; }

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
                            Contract = ApiDataContract.Create(c.Value.Schema, cProvider)
                        })
                    .ToArray();
            }

            if (operation.Responses != null)
            {
                if (!operation.Responses.TryGetValue("200", out var succResp))
                    succResp = operation.Responses.FirstOrDefault(r => r.Key[0] == '2').Value;
                if(succResp?.Content != null && succResp.Content.Count != 0)
                {
                    var succRespContentSchema = succResp.Content.First().Value.Schema;
                    res.ResponseContract = ApiDataContract.Create(succRespContentSchema,cProvider);
                }

                res.ResponsesDescriptions = operation.Responses.Select(
                    r =>
                    {
                        try
                        {
                            return new ApiResponseDescription
                            {
                                Code = Enum.Parse<HttpStatusCode>(r.Key, true),
                                Comment = r.Value.Description
                            };
                        }
                        catch (ArgumentException e)
                        {
                            throw new InvalidOperationException(
                                $"Http code parsing error. Path: '{path}' Method: '{method}'",
                                e);
                        }
                    }).ToArray();
            }

            if (operation.Parameters != null)
            {
                res.Parameters = operation.Parameters.Select(p => ApiMethodParameter.Create(p, cProvider)).ToArray();
            }

            return res;
        }
    }

    class ApiResponseDescription
    {
        public HttpStatusCode Code { get; set; }
        public string Comment { get; set; }
    }
}