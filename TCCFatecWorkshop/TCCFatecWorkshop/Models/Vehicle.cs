namespace TCCFatecWorkshop.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public string LicencePlate { get; set; }
        public string ChassisNumber { get; set; }

        public ICollection<Service> Services { get; set; } = new HashSet<Service>();

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public Vehicle() { }

        public Vehicle (string model, int year, string brand, string licencePlate, string chassisNumber, Client client)
        {
            Model = model;
            Year = year;
            Brand = brand;
            LicencePlate = licencePlate;
            ChassisNumber = chassisNumber;
            Client = client;
        }

        public void AddServices(Service service)
        {
            Services.Add(service);
        }
    }
}
