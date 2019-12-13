using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pelo.Web.Models.Datatables
{
    public class DatatableResponse<T>
        where T : class
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("data")]
        public IList<T> Data { get; set; }

        public static DatatableResponse<T> Init(int draw)
        {
            return new DatatableResponse<T>
            {
                Data = new List<T>(),
                Draw = draw,
                RecordsTotal = 0,
                RecordsFiltered = 0
            };
        }
    }
}