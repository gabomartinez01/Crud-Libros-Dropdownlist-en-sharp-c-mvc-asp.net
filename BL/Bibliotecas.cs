using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Bibliotecas
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var Bibliotecas = context.GetBibliotecas().ToList();
                    result.Objects = new List<object>();
                    if(Bibliotecas != null)
                    {
                        foreach(var item in Bibliotecas)
                        {
                            ML.Biblioteca biblioteca = new ML.Biblioteca();
                            biblioteca.IdBiblioteca = item.IdBiblioteca;
                            biblioteca.Nombre = item.Nombre;
                            result.Objects.Add(biblioteca);
                        }
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
    
        public static ML.Result GetBiblioteca(int IdBiblioteca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.LibreriaEntities context = new DL.LibreriaEntities())
                {
                    var biblioteca = (from l in context.GetBibliotecas().ToList()
                                      where l.IdBiblioteca == IdBiblioteca
                                      select l).FirstOrDefault();
                    result.Objects = new List<object>();
                    if (biblioteca != null)
                    {
                        ML.Biblioteca biblio = new ML.Biblioteca();
                        biblio.IdBiblioteca= Convert.ToInt32(biblioteca.IdBiblioteca);
                        biblio.Nombre = biblioteca.Nombre;
                        result.Objects.Add(biblio);
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
    }
}
