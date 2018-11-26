using System.Dynamic;
using MyLab.Oas.SpecModel;

namespace MyLab.Oas.ObjectModel
{
    class ApiDescription
    {
        public ApiServerMethod[] ServerMethods { get; set; }

        public ApiClientMethod[] ClientMethods { get; set; }

        public ApiDataContract[] DataContracts { get; set; }

        public static ApiDescription Create(OpenApiDescription description)
        {

        }
    }

    internal class ApiDataContract
    {
    }

    internal class ApiClientMethod
    {
    }

    internal class ApiServerMethod
    {

    }
}
