namespace TCCFatecWorkshop.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public string CPFCNPJ { get; set; }
        
        public string Document { get; set; }
        public string Phone { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();

        public Client() { }

        public Client(string name, string email, string cPFCNPJ, string document, string phone)
        {
            Name = name;
            Email = email;
            CPFCNPJ = cPFCNPJ;
            Document = document;
            Phone = phone;
        }

        public void addVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
        }
    }
}
