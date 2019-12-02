using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entites;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _jobManager;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService jobManager)
        {
            _logger = logger;
            _jobManager = jobManager;
        }

        [HttpGet("GetEmployee")]
        public IEnumerable<Employee> Get()
        {
            var data = _jobManager.ListAll();
            return data;
        }

        [HttpGet("GetEmployeeId")]
        public Employee GetEmployeeId(int id)
        {
            var data = _jobManager.GetByEmployeeId(id);
            return data;
        }

        [HttpPost("SaveEmployee")]
        public ActionResult SaveEmployee(Employee emp)
        {
            var data = _jobManager.Create(emp);
            return new JsonResult(data);
        }
    }
}
