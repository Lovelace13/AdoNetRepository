using Infrastructure.Entidades;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoIndusur.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IVehiculoServicio _vehiculoTest;

        public VehiculoController(IVehiculoServicio vehiculoTest)
        {
            _vehiculoTest = vehiculoTest;
        }

        public IActionResult Index()
        {
            string baseDir = AppContext.BaseDirectory;
            _vehiculoTest.GetAll();

            return PhysicalFile($"{baseDir}/Views/Vehiculo/Index.html", "text/html");
        }


        // GET: VehiculoController
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok( _vehiculoTest.GetAll() );
        }

        [HttpGet]
        public JsonResult GetData()
        {
            IEnumerable<VehiculoEntity> data = _vehiculoTest.GetAll();
            return new JsonResult(data);
        }

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculoController/Create
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

        // GET: VehiculoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehiculoController/Edit/5
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

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehiculoController/Delete/5
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
