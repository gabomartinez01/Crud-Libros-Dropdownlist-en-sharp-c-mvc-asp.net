using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.Controllers
{
    public class BibliotecasController : Controller
    {
        // GET: Bibliotecas
        public ActionResult GetAllBibliotecas()
        {
            ML.Result result = BL.Bibliotecas.GetAll();
            ML.Biblioteca biblioteca = new ML.Biblioteca();
            biblioteca.BibliotecasList = result.Objects;
            return View(biblioteca);
        }
        [HttpPost]
        public ActionResult GetAllBibliotecas(ML.Biblioteca Biblioteca)
        {
            if (Biblioteca.IdBiblioteca != null && Biblioteca.IdBiblioteca != 0)
            {
                ML.Result result = BL.Bibliotecas.GetBiblioteca(Biblioteca.IdBiblioteca);
                ML.Biblioteca biblioteca = new ML.Biblioteca();
                biblioteca.BibliotecasList = result.Objects;
                return View(biblioteca);
            }
            if(Biblioteca.IdBiblioteca == null)
            {
                return RedirectToAction("GetAllBibliotecas");
            }
            return RedirectToAction("GetAllBibliotecas");
        }
    }
}