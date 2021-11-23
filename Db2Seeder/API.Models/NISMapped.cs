using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db2Seeder.API.Models
{
    public class NISMapped
    {
        [JsonProperty("userAccountNISMappingId")]
        public int UserAccountNISMappingId { get; set; }

        [JsonProperty("userAccountID")]
        public int UserAccountID { get; set; }

        [JsonProperty("nisNumber")]
        public string NisNumber { get; set; }

        [JsonProperty("nisNumberTypeID")]
        public int NisNumberTypeID { get; set; }

        [JsonProperty("nisTypeName")]
        public string NisTypeName { get; set; }
    }
}
