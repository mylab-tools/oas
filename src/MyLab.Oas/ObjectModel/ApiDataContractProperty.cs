namespace MyLab.Oas.ObjectModel
{
    class ApiDataContractProperty
    {
        public ApiDataContract Contract { get; set; }
        public string Name { get; set; }

        public bool Required { get; set; }
    }
}