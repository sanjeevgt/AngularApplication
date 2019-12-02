using API.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IEmployeeService
    {
        int Delete(int EmployeeId);
        Employee GetByEmployeeId(int EmployeeId);
        string Update(Employee employee);
        int Create(Employee EmployeeDetails);
        List<Employee> ListAll();
    }
}
