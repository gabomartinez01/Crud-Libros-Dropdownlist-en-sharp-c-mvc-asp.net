using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Libros
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var Libros = context.GetLibros().ToList();
                    result.Objects = new List<object>();
                    if(result != null) { 
                    foreach(var item in Libros)
                    {
                        ML.Libros libros = new ML.Libros();
                        libros.IdLibro = item.IdLibro;
                        libros.Nombre = item.NombreLibro;
                        libros.Biblioteca = new ML.Biblioteca();
                        libros.Biblioteca.Nombre = item.NombreBiblioteca;
                        result.Objects.Add(libros);
                    }
                        result.Correct = true;
                    }
                    else{
                        result.Correct = false;
                    }
                }
            }catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }

        public static ML.Result GetBusquedaAbierda(string text)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var libros = (from l in context.GetLibros().ToList()
                                      where l.NombreLibro == text.ToString()
                                      select l).FirstOrDefault();
                    result.Objects = new List<object>();
                    if (libros != null)
                    {
                        ML.Libros li = new ML.Libros();
                        li.IdLibro = Convert.ToInt32(libros.IdLibro);
                        li.Nombre = libros.NombreLibro;
                        li.Biblioteca = new ML.Biblioteca();
                        li.Biblioteca.Nombre = libros.NombreBiblioteca;
                        result.Objects.Add(li);
                    }
                    result.Correct = true;

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }


        public static ML.Result AddET(ML.Libros libros)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var query = context.AddLibro(libros.Nombre, libros.Biblioteca.IdBiblioteca);
                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e; 
            }
            return result;
        }
    
    
        public static ML.Result GetByIdEF(int ? IdLibro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var query = (from l in context.GetLibros().ToList()
                                 where l.IdLibro == IdLibro
                                 select l).FirstOrDefault();

                    if(query != null)
                    {
                        ML.Libros libros = new ML.Libros();
                        libros.IdLibro = Convert.ToInt32(query.IdLibro);
                        libros.Nombre = query.NombreLibro;
                        libros.Biblioteca = new ML.Biblioteca();
                        libros.Biblioteca.IdBiblioteca = query.IdBiblioteca;
                        libros.Biblioteca.Nombre = query.NombreBiblioteca;
                        result.Object = libros;
                    }
                    result.Correct = true;
                }
            }catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }
   

        public static ML.Result UpdateEF(ML.Libros libros)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var query = context.UpdateLibro(libros.Nombre, libros.Biblioteca.IdBiblioteca, libros.IdLibro);
                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }
        
        public static ML.Result DeleteEF(int ? IdLibro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var query = context.DeleteLibro(IdLibro);
                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }catch(Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = "Error en " + e;
            }
            return result;
        }
    }
}
