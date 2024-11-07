using ClasesObjetos;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;

Biblioteca biblioteca = new Biblioteca();
biblioteca.CargarDatos();

if(biblioteca.Usuarios.Count == 0 && biblioteca.Libros.Count == 0)
{
    Libro HP = new Libro("Harry Potter y la Piedra Filosofal", "J. K. Rowling", "kjlhadsGDF");
    biblioteca.AgregarLibro(HP);
    Libro ATC = new Libro("Ama tu caos", "Albert Espinosa", "FGDDF45UTY");
    biblioteca.AgregarLibro(ATC);
    Libro ECDLC = new Libro("El Cuento de la Criada", "Margaret Atwood", "7823GDFG");
    biblioteca.AgregarLibro(ECDLC);


    User Mariana = new User("as678dgsd", "Mariana");
    biblioteca.IngresarUsuario(Mariana);
    User Dumas = new User("kljhdas78234", "Dumas");
    biblioteca.IngresarUsuario(Dumas);

    biblioteca.GuardarDatos();
}

bool App = true;

while (App) {
    Console.WriteLine("Bienvenido a la biblioteca \n 1.Agregar libro \n 2.Registrar usuario \n 3.Pedir prestado un libro \n 4.Devolver libro " +
    "\n 5.Ver estado de todos los libros \n 6.Ver usuarios ingresados \n 7.Ver libros prestados a un usuario \n 8.Salir \n Seleccione una opción: ");

    string input = Console.ReadLine();

    if(int.TryParse(input, out int option))
    {
        switch(option)
        {
            //Agregar libro
            case 1:
                Console.WriteLine("Ingrese el título del libro: ");
                string titulo = Console.ReadLine();
                Console.WriteLine("Ingrese el autor del libro: ");
                string autor = Console.ReadLine();
                Console.WriteLine("Ingrese el ISBN del libro: ");
                string isbn = Console.ReadLine();
                Libro libroIngresado = new Libro(titulo, autor, isbn);
                biblioteca.AgregarLibro(libroIngresado);
                biblioteca.GuardarDatos();
                Console.WriteLine($"LIBRO AGREGADO CON ÉXITO: \n TITULO: {titulo} \n AUTOR: {autor} \n ISBN: {isbn}"); 

                break;
            //Registrar usuario
            case 2: 
                Console.WriteLine("Ingrese el nombre del usuario: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el ID del usuario: ");
                string id = Console.ReadLine();
                User usuarioIngresado = new User(nombre, id);
                biblioteca.IngresarUsuario(usuarioIngresado);
                biblioteca.GuardarDatos();
                Console.WriteLine($"USUARIO REGISTRADO CON ÉXITO: \n NOMBRE: {nombre} \n ID: {id}"); 

                break;
            //Pedir prestado un libro
            case 3: 
                Console.WriteLine("Ingrese el ISBN del libro a prestar: ");
                isbn = Console.ReadLine();
                Console.WriteLine("Ingrese su ID: ");
                id = Console.ReadLine();
                var libroPrestado = biblioteca.BuscarLibro(isbn);
                var usuarioPrestamo = biblioteca.BuscarUsuario(id);
                
                if(libroPrestado != null && usuarioPrestamo != null && libroPrestado.Disponible)
                {
                    usuarioPrestamo.RecibirLibroPrestado(libroPrestado);
                    biblioteca.GuardarDatos();
                    Console.WriteLine($"LIBRO PRESTADO CON ÉXITO: \n TITULO: {libroPrestado.Titulo} \n AUTOR: {libroPrestado.Autor} \n " +
                        $"ISBN: {libroPrestado.ISBN} \n DISPONIBLE: {libroPrestado.Disponible}");
                } else
                {
                    Console.WriteLine("NO SE PUDO REALIZAR EL PRÉSTAMO. VERIFIQUE SU ID O LA DISPONIBILIDAD DEL LIBRO SELECCIONADO APRETANDO '5'");
                }

                break;
            //Devolver un libro
            case 4:
                Console.WriteLine("Ingrese el ISBN del libro a devolver: ");
                isbn = Console.ReadLine();
                Console.WriteLine("Ingrese su ID: ");
                id = Console.ReadLine();
                var libroRegresado = biblioteca.BuscarLibro(isbn);
                var usuarioRegreso = biblioteca.BuscarUsuario(id);

                if(libroRegresado != null && usuarioRegreso != null && libroRegresado.Disponible == false)
                {
                    usuarioRegreso.DevolverLibroPrestado(libroRegresado);
                    biblioteca.GuardarDatos();
                    Console.WriteLine($"LIBRO DEVUELTO CON ÉXITO: \n TITULO: {libroRegresado.Titulo} \n AUTOR: {libroRegresado.Autor} \n " +
                        $"ISBN: {libroRegresado.ISBN} \n DISPONIBLE: {libroRegresado.Disponible}");
                } else
                {
                    Console.WriteLine("NO SE PUDO REALIZAR LA ENTREGA DEL LIBRO. VERIFIQUE SU ID O LA DISPONIBILIDAD DEL LIBRO SELECCIONADO APRETANDO '5'");
                }

                break;
            //Ver estado de todos los libros
            case 5:
                Console.WriteLine("ESTADO DE LOS LIBROS: \n");
                foreach (Libro libro in biblioteca.Libros)
                {
                    Console.WriteLine($"TITULO: {libro.Titulo} \n AUTOR: {libro.Autor} \n" +
                    $"ISBN: {libro.ISBN} \n DISPONIBLE: {libro.Disponible}");
                }

                break;
            //Ver usuarios ingresados
            case 6:
                Console.WriteLine("USUARIOS EXISTENTES: \n");
                foreach(User user in biblioteca.Usuarios)
                {
                    Console.WriteLine($"NOMBRE: {user.Nombre} \n ID: {user.Id}");
                }

                break;
            //Ver todos los libros prestados a un usuario
            case 7:
                Console.WriteLine("Ingrese el ID del usuario que desea ver:");
                id = Console.ReadLine();
                var usuario = biblioteca.BuscarUsuario(id);

                if (usuario != null)
                {
                    foreach (var libro in usuario.Prestados)
                    {
                        Console.WriteLine($"TITULO: {libro.Titulo} \n AUTOR: {libro.Autor} \n ISBN: {libro.ISBN} \n DISPONIBLE: {libro.Disponible}");
                    }
                }
                else
                {
                    Console.WriteLine("NO HA SIDO POSIBLE CARGAR LOS LIBROS");
                }

                break;
            //Salir
            case 8:
                Console.WriteLine("GRACIAS POR VISITARNOS, VUELVA PRONTOS");
                App = false;

                break;

            default:
                Console.WriteLine("OPCIÓN INVÁLIDA. POR FAVOR, SELECCIONE UN NÚMERO DEL 1 AL 8.");
                break;
        }
    } else
        {
            Console.WriteLine("ENTRADA INVÁLIDA. POR FAVOR, SELECCIONE UN NÚMERO.");
        }
    }
