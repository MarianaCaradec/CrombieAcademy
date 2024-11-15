using BibliotecaAPIWeb.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Excel base de datos

        public class MyExcelClass
        {
            public string filePath = "BibliotecaBaseDatos.xlsx";

            public List<MyExcelClass> ObtenerDatos(string filePath)
            {
                var dataList = new List<MyExcelClass>();

                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    var lastRowUsed = worksheet.LastRowUsed().RowNumber();

                    for (int row = 2; row < lastRowUsed; row++)
                    {
                        var dataItem = new MyExcelClass
                        {
                            Id = worksheet.Cell(row, 1).GetValue<string>(),
                            Nombre = worksheet.Cell(row, 2).GetValue<string>(),
                            Prestados = worksheet.Cell(row, 3).GetValue<List<Libro>>()
                        };
                        dataList.Add(dataItem);
                    }
                }
                return dataList;
            }

            public void InsertarDatos(string filePath, List<MyExcelClass> nuevosDatos)
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                    foreach (var item in nuevosDatos)
                    {
                        lastRowUsed++;

                        worksheet.Cell(lastRowUsed, 1).Value = item.Id;
                        worksheet.Cell(lastRowUsed, 2).Value = item.Nombre;
                        worksheet.Cell(lastRowUsed, 3).Value = item.Prestados;
                    }
                    workbook.Save();
                }
            }
        }
    }
}
