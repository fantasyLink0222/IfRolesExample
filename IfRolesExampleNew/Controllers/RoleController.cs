﻿using IfRolesExampleNew.Data;
using IfRolesExampleNew.Repositories;
using IfRolesExampleNew.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IfRolesExampleNew.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
      

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            var rolesList = roleRepo.GetAllRoles();
            return View(rolesList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                bool isSuccess = roleRepo.CreateRole(roleVM.RoleName);

                if (isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Role creation failed." +
                                             " The role may already exist.");
                }
            }
            return View(roleVM);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);

            if (role == null)
            {
                // Handle the case where the role doesn't exist. 
                // For example, redirect to an error page or show a not found message.
                return NotFound(); // or RedirectToAction("ErrorPage");
            }

            var roleVM = new RoleVM
            {
                RoleName = role.Name,
                Id = role.Id
            };

            return View(roleVM);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                // Assuming _roleRepo is injected via constructor and is of type IRoleRepository
                var (isSuccess, Message) = await roleRepo.DeleteRoleAsync(roleVM.Id);

                if (isSuccess)
                {
                    // Use TempData to pass success messages if they are to be displayed to the user
                    TempData["SuccessMessage"] = Message;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Use TempData to pass error messages if they are to be displayed to the user
                    TempData["ErrorMessage"] =Message;
                    return RedirectToAction(nameof(Index));
                }
            }

            // If model state is not valid, return to the view with the model to show validation errors
            return View(roleVM);
        }
    }
}
