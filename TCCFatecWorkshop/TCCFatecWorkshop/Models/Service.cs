using TCCFatecWorkshop.Models.Enums;

namespace TCCFatecWorkshop.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public double TotalValue { get; set; }

        public ServicesSituation Situation { get; set; }

        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }

        public ICollection<ProductsService> ProductsServices { get; set; } = new List<ProductsService>();

        public Workshop Workshop { get; set; }

        public Vehicle Vehicle { get; set; }

        public Service() 
        {
            InitialDate = DateTime.UtcNow;
        }

        public Service(int id, string description, double totalValue, ServicesSituation situation, DateTime finalDate,Workshop workshop, Vehicle vehicle) : this()
        {
            Id = id;
            Description = description;
            TotalValue = totalValue;
            Situation = situation;
            FinalDate = finalDate;
            Workshop = workshop;
            Vehicle = vehicle;
        }

    }
}
