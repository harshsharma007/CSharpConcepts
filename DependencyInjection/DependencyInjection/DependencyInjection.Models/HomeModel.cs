namespace DependencyInjection.Models
{
    public class HomeModel
    {
        public int HomeId { get; set; }
        public string HomeName { get; set; } = string.Empty;
        public bool IsBeautiful { get; set; }
        public DateTime HomeDate { get; set; }
        public decimal HomeValue { get; set; }
    }
}
