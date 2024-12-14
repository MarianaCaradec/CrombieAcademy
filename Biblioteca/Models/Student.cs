using DocumentFormat.OpenXml.Office2010.Excel;

namespace BibliotecaAPIWeb.Models
{
    public class Student : User
    {
        public override string UserType { get; set; } = "Estudiante";

        public override int MaxBooksAllowed => 3;

    }
}
