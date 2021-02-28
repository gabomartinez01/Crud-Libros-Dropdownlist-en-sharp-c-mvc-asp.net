using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Libros.Controllers
{
    public class LibrosController : Controller
    {
        // GET: Libros
        [HttpGet]
        public ActionResult GetAllLibros()
        {
            ML.Result result = BL.Libros.GetAll();
            ML.Libros libros = new ML.Libros();
            libros.LibrosList = result.Objects;
            return View(libros);
        }

        [HttpPost]
        public ActionResult GetAllLibros(string text)
        {
            if(text == null || text == "") {
                return RedirectToAction("GetAllLibros");
            }
            else
            {
                ML.Libros libros = new ML.Libros();
                ML.Result result = BL.Libros.GetBusquedaAbierda(text);
                libros.LibrosList = result.Objects;
                return View(libros);
            }
        }


        [HttpGet]
        public ActionResult Form(int ? IdLibro)
        {
            ML.Libros libros = new ML.Libros();

            ML.Result resultBiliotecas = BL.Bibliotecas.GetAll();
            libros.Biblioteca = new ML.Biblioteca();
            libros.Biblioteca.BibliotecasList = resultBiliotecas.Objects;

            if(IdLibro == null)
            {
                //Agregar
                ViewBag.Title = "Nuevo Libro";
                return View(libros);
            }
            else
            {
                ViewBag.Title = "Editar Libro";

                //Buscar
                ML.Result result = BL.Libros.GetByIdEF(IdLibro.Value);
                if (result.Correct)
                {
                    libros.IdLibro = ((ML.Libros)result.Object).IdLibro;
                    libros.Nombre = ((ML.Libros)result.Object).Nombre;
                    libros.Biblioteca.IdBiblioteca = ((ML.Libros)result.Object).Biblioteca.IdBiblioteca;
                    libros.Biblioteca.Nombre = ((ML.Libros)result.Object).Biblioteca.Nombre;

                    return View(libros);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("validacion");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Libros libros)
        {
            ML.Result result = new ML.Result();
            if(libros.IdLibro == 0)
            {
                //Agrega
                result = BL.Libros.AddET(libros);
                ViewBag.Message = "El libro se agrego correctamente";
            }
            else
            {
                //Update
                result = BL.Libros.UpdateEF(libros);
                ViewBag.Message = "El libro se modifico correctamente";
            }

            //
            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar el departamento";
            }
            return PartialView("validacion");
        }

        public ActionResult Delete(int IdLibro)
        {
            BL.Libros.DeleteEF(IdLibro);
            return RedirectToAction("GetAllLibros");
        }
    }
}