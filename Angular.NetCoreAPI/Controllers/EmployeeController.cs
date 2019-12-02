using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular.NetCoreAPI.Common;
using API.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angular.NetCoreAPI.Controllers
{
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private ApiService<Employee> apiService;

        public EmployeeController ()
        {
            apiService = new ApiService<Employee>();
        }
      
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
           var headers = Request.Headers["Authorization"];
           var result = await apiService.GetRecord("https://localhost:44343/Employee/getEmployee");
           return Ok(result);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}