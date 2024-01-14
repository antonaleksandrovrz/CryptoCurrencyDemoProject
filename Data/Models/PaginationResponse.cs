namespace CryptoCurrencyDemoProject.Data.Models
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalPages { get; set; }
    }
}
