namespace BibliotecaAPIWeb.Models
{
    public class Profesor : User
    {
        public override string UserType { get; set; } = "Profesor";

        public override int MaxBooksAllowed => 5;

        public override int LoanDays => 14;

    }
}
