using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityLayer;
using InfraLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using NewGeneric.Models;

namespace NewGeneric.Controllers
{
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _empRep;
        private readonly IRepository<Department> _depRep;

        public EmployeeController(IRepository<Employee> empRep, IRepository<Department> depRep)
        {
            _empRep = empRep;
            _depRep = depRep;
        }

        [Route("Details")]
        public IActionResult Details()
        {
            var resultSet = (from empp in _empRep.GetAll()
                             join depp in _depRep.GetAll()
                             on empp.DepartmentId equals depp.Id
                             select new
                             {
                                 Id = empp.Id,
                                 Name = empp.Name,
                                 Salary = empp.Salary,
                                 DepName = depp.DepName
                             }).ToList();
            List<EmployeeViewModel> details = new List<EmployeeViewModel>();
            foreach(var v in resultSet)
            {
                details.Add(new EmployeeViewModel()
                {
                    EId = v.Id,
                    EName = v.Name,
                    ESalary = v.Salary,
                    EDepartmentName = v.DepName
                });
            }
            return View(details);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromForm] EmployeeViewModel employeeViewModel)
        {
            if(employeeViewModel != null)
            {
            Employee employee = new Employee();
            employee.Name = employeeViewModel.EName;
            employee.Salary = employeeViewModel.ESalary;
            employee.DepartmentId = employeeViewModel.EDepartmentId;
            _empRep.Add(employee);
            }
            return RedirectToAction("Details");
        }

        [HttpGet]
        [Route("Update/{id}")]
        public IActionResult Update(int id)
        {
            var result = _empRep.GetById(id);
            if(result!=null)
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();
                employeeViewModel.EId = result.Id;
                employeeViewModel.EName = result.Name;
                employeeViewModel.ESalary = result.Salary;
                employeeViewModel.EDepartmentId = result.DepartmentId;
                return View(employeeViewModel);
            }
            else
            {
                ViewBag.result = "not found";
                return View("Department/Index");
            }
        }

        [HttpPost]
        [Route("Update/{id}")]
        public IActionResult Update([FromForm] EmployeeViewModel employeeViewModel)
        {
            Employee employee = new Employee();
            employee.Id = employeeViewModel.EId;
            employee.Name = employeeViewModel.EName;
            employee.Salary = employeeViewModel.ESalary;
            employee.DepartmentId = employeeViewModel.EDepartmentId;
            _empRep.Update(employee);
            return RedirectToAction("Details");
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _empRep.GetById(id);
            if(result!=null)
            {
                _empRep.Delete(id);
                return RedirectToAction("Details");
            }
            else
            {
                ViewBag.result = "not found";
                return View("Department/Index");
            }
        }
    }
}