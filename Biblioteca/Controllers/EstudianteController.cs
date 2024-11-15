using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPIWeb.Controllers
{
    public class EstudianteController : Controller
    {
        // GET: EstudianteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EstudianteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstudianteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstudianteController/Create
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

        // GET: EstudianteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstudianteController/Edit/5
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

        // GET: EstudianteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstudianteController/Delete/5
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
    }
}
