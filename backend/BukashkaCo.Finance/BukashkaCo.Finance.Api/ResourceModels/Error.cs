using System.Runtime.Serialization;

namespace BukashkaCo.Finance.Api.ResourceModels
{
    [DataContract(Name = "Error")]
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}