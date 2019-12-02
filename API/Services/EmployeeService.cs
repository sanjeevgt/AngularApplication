using API.Entites;
using API.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDapperHelper _dapperHelper;

        public EmployeeService(IDapperHelper dapperHelper)
        {
            this._dapperHelper = dapperHelper;
        }
        public int Create(Employee EmployeeDetails)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Name", EmployeeDetails.Name, DbType.String);
            dbPara.Add("LastName", EmployeeDetails.LastName, DbType.String);

       
            var data = _dapperHelper.Insert<int>("[dbo].[sp_saveEmployee]",
                            dbPara,
                            commandType: CommandType.StoredProcedure);
            return data;
        }

        public int Delete(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public Employee GetByEmployeeId(int EmployeeId)
        {
            #region using dapper  
            var data = _dapperHelper.Get<Employee>($"select * from Employee  where EmployeeId={EmployeeId}", null,
                    commandType: CommandType.Text);
            return data;
            #endregion
        }

        public List<Employee> ListAll()
        {
            var data = _dapperHelper.GetAll<Employee>("[dbo].[SP_Employee_List]", null, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public string Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
