using System.Text.Json.Serialization;

namespace BibliotecaAPIWeb.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public string ISBNBook { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }

}
