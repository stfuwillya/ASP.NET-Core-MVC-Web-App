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
    [Route("")]
    [Route("Department")]
    public class DepartmentController : Controller
    {
        private readonly IRepository<Department> _repository;
        public DepartmentController(IRepository<Department> repository)
        {
            _repository = repository;
        }
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Details")]
        public IActionResult Details() 
        {
            var details = _repository.GetAll();
            List<DepartmentViewModel> viewModels = new List<DepartmentViewModel>();
            foreach(var v in details)
            {
                viewModels.Add(new DepartmentViewModel() { DId = v.Id, DName = v.DepName });
            }
            return View(viewModels);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromForm] DepartmentViewModel departmentViewModel)
        {
            Department department = new Department();
            department.DepName = departmentViewModel.DName;
            _repository.Add(department);
            return RedirectToAction("Details");
        }

        [HttpGet]
        [Route("Update/{id}")]
        public IActionResult Update(int id)
        {
            var value = _repository.GetById(id);
            if (value != null)
            {
                DepartmentViewModel departmentViewModel = new DepartmentViewModel();
                departmentViewModel.DId = value.Id;
                departmentViewModel.DName = value.DepName;
                return View(departmentViewModel);
            }
            else
            {
                ViewBag.result = "not found";
                return View("Index");
            }
                
        }

        [HttpPost]
        [Route("Update/{id}")]
        public IActionResult Update([FromForm] DepartmentViewModel departmentViewModel)
        {
            Department department = new Department();
            department.Id = departmentViewModel.DId;
            department.DepName = departmentViewModel.DName;
            _repository.Update(department);
            return RedirectToAction("Details");
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var value = _repository.GetById(id);
            if (value != null)
            {
                _repository.Delete(id);
                return RedirectToAction("Details");
            }
            else
            {
                ViewBag.result = "not found";
                return View("Index");
            }
        }
    }
}