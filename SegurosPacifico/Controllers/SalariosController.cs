using Microsoft.AspNetCore.Mvc;

using SegurosPacifico.Models;
using System.Drawing.Printing;
using System.Security.Principal;

namespace SegurosPacifico.Controllers
{
    public class SalariosController : Controller
    {
        private DbContextSalarios dbContext = null;


        public SalariosController(DbContextSalarios pContext)
        {
            dbContext = pContext;
        }


        public IActionResult Index()
        {
            return View(dbContext.empleados.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Salarios pSal)
        {

            pSal.SalarioBruto = (pSal.HorasNormales * 1800) + (pSal.HorasExtra * (1.5m * 1800));

            if (pSal.SalarioBruto <= 250000)
            {
                pSal.Deducciones = "9%";
                pSal.SalarioNeto = pSal.SalarioBruto * 0.91m;
            }
            else if (pSal.SalarioBruto <= 380000)
            {
                pSal.Deducciones = "12%";
                pSal.SalarioNeto = pSal.SalarioBruto * 0.88m;
            }
            else
            {
                pSal.Deducciones = "15%";
                pSal.SalarioNeto = pSal.SalarioBruto * 0.85m;
            }

            if (pSal == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.empleados.Add(pSal);

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            Salarios temp = null;

            temp = dbContext.empleados.FirstOrDefault(r => r.ID == Id);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }

        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var temp = dbContext.empleados.FirstOrDefault(x => x.ID == Id);

            if (temp == null)
            {
                return NotFound();
            }
            {
                return View(temp);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind] Salarios pSal)
        {
            if (pSal == null)
            {
                return NotFound();
            }

            var temp = dbContext.empleados.FirstOrDefault(j => j.ID == pSal.ID);


            if (temp == null)
            {
                return NotFound();
            }
            else
            {

                dbContext.empleados.Remove(temp);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var temp = dbContext.empleados.FirstOrDefault(x => x.ID == Id);


            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind] Salarios pSal)
        {
            if (pSal == null)
            {
                return NotFound();
            }

            if (Id != pSal.ID)
            {
                return NotFound();
            }

            var temp = dbContext.empleados.FirstOrDefault((x) => x.ID == Id);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                temp.Nombre = pSal.Nombre;
                temp.HorasNormales = pSal.HorasNormales;
                temp.HorasExtra = pSal.HorasExtra;

                pSal.SalarioBruto = (pSal.HorasNormales * 1800) + (pSal.HorasExtra * (1.5m * 1800));

                if (pSal.SalarioBruto <= 250000)
                {
                    pSal.Deducciones = "9%";
                    pSal.SalarioNeto = pSal.SalarioBruto * 0.91m;
                }
                else if (pSal.SalarioBruto <= 380000)
                {
                    pSal.Deducciones = "12%";
                    pSal.SalarioNeto = pSal.SalarioBruto * 0.88m;
                }
                else
                {
                    pSal.Deducciones = "15%";
                    pSal.SalarioNeto = pSal.SalarioBruto * 0.85m;
                }

                temp.SalarioBruto = pSal.SalarioBruto;
                temp.Deducciones = pSal.Deducciones;
                temp.SalarioNeto = pSal.SalarioNeto;

                dbContext.empleados.Update(temp);

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
