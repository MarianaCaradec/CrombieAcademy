namespace BibliotecaAPIWeb.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public string ISBNBook { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }

    public class SalesDto
    {
        public int Id { get; set; }
        public string ISBNBook { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BookDto Book { get; set; }
    }

}
