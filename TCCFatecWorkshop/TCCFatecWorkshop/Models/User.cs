namespace TCCFatecWorkshop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //OneToMany
        public ICollection<Workshop>? Workshops { get; set; } = new HashSet<Workshop>();

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public User(string username, string email, string password) : this()
        {
            Username = username;
            Email = email;
            Password = password;

        }

        public void AddWorkshop(Workshop workshop)
        {
            Workshops.Add(workshop);   
        }

        public void RemoveWorkshop(Workshop workshop) 
        {
            var item = Workshops.FirstOrDefault(x => x.Id == workshop.Id);
            if (item != null)
            {
                Workshops.Remove(item);
            }
        }


    }
}

