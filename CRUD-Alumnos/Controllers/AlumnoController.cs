using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                using (var db = new AlumnoContent())
            {
                //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList(); //Muestra solo los de la lista mayores de 18años
                return View(db.Alumno.ToList());
            }

            }
            catch (Exception)
            {

                throw;
            }

           // AlumnoContent db = new AlumnoContent();
            //Con esta lista pide desde la base de datos todos los alumnos mayores de 18años 
           


            //Devuelve los TODOS los Alumnos de la Lista (base de datos)
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //CREA UN TOKEN QUE VERIFICA QUE AL FORMULARIO QUE SE ESTÁ ENVIANDO ES AL QUE PERTENECE
        public ActionResult Add(Alumno a)
        {
            //Si el modelo no es Válido
            if (!ModelState.IsValid) return View();

            try
            {
                using (AlumnoContent db = new AlumnoContent())//al usar el using le digo que abra la conecxion y a la vez la cierre
                {
                    a.FechaRegistro = DateTime.Now;
                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("","Error al registrar Alumno - " +  ex.Message);
                return View();
            }
            
        }

        public ActionResult Editar(int ID)
        {
            try
            {
                using (var db = new AlumnoContent())
                {
                    //Alumno al = db.Alumno.Where(a => a.ID == ID).SingleOrDefault(); //Siempre lo puedo usar.
                    Alumno alu = db.Alumno.Find(ID); //solo lo voy a buscar cuando solo tenga 1 clave primaria.
                    return View(alu);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a)
        {
            try
            {
                using (var db = new AlumnoContent())
                {
                    Alumno al = db.Alumno.Find(a.ID);
                    al.Nombre = a.Nombre;
                    al.Apellido = a.Apellido;
                    al.Edad = a.Edad;
                    al.Sexo = a.Sexo;

                    //Guardar Cambios
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Detallesalumno(int ID)
        {
            try
            {
            using (var db = new AlumnoContent())
            {
                Alumno alu = db.Alumno.Find(ID); 
                return View(alu);
            }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EliminarAlumno(int ID)
        {
            try
            {
                using (var db = new AlumnoContent())
                {
                    Alumno alu = db.Alumno.Find(ID);
                    db.Alumno.Remove(alu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}