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
        public Client Client { get; set; }
        public Vehicle() { }

        public Vehicle (int id, string model, int year, string brand, string licencePlate, string chassisNumber, Client client)
        {
            Id = id;
            Model = model;
            Year = year;
            Brand = brand;
            LicencePlate = licencePlate;
            ChassisNumber = chassisNumber;
            Client = client;
        }
    }
}
