namespace TestWebAPI.DTOs
{
    public abstract class BaseResourceQueryParams
    {
        private int pageSize = 10;

        public const int MaxPageSize = 50;

        public int PageSize
        {
            get => this.pageSize; 
            set => this.pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int PageNumber { get; set; } = 1;
    }
}
