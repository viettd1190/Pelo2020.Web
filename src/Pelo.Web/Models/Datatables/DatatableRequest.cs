using Newtonsoft.Json;

namespace Pelo.Web.Models.Datatables
{
    public class DatatableRequest
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("columns")]
        public ColumnRequestItem[] Columns { get; set; }

        [JsonProperty("order")]
        public OrderRequestItem[] Order { get; set; }

        [JsonProperty("search")]
        public SearchRequestItem Search { get; set; }
    }

    public class ColumnRequestItem
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("searchable")]
        public bool Searchable { get; set; }

        [JsonProperty("orderable")]
        public bool Orderable { get; set; }

        [JsonProperty("search")]
        public SearchRequestItem Search { get; set; }
    }

    public class OrderRequestItem
    {
        [JsonProperty("column")]
        public int Column { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }
    }

    public class SearchRequestItem
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("regex")]
        public bool Regex { get; set; }
    }
}