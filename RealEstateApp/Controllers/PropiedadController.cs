﻿using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Dtos.Account;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Helpers;

namespace RealEstateApp.Controllers
{
    public class PropiedadController : Controller
    {
        private readonly IPropiedadService _service;
        private readonly IMejoraService mejoraService;
        private readonly ITipoVentaService tipoVentaService;
        private readonly ITipoPropiedadService tipoPropiedadService;
        private readonly AuthenticationResponse userViewModel;
        private readonly IHttpContextAccessor _contextAccessor;
        public PropiedadController(IPropiedadService service, IMejoraService mejoraService, ITipoVentaService tipoVentaService, ITipoPropiedadService tipoPropiedadService, IHttpContextAccessor contextAccessor)
        {
            _service = service;
            this.mejoraService = mejoraService;
            this.tipoVentaService = tipoVentaService;
            this.tipoPropiedadService = tipoPropiedadService;
            _contextAccessor = contextAccessor;
            userViewModel = contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IActionResult> Index()
        {

            var propiedades = await _service.GetAllByAgente(userViewModel.Id);
            return View(propiedades);
        }

        public async Task<ActionResult> CreateView()
        {
            var tipos = await tipoPropiedadService.GetAllViewModel();
            var tipoventa = await tipoVentaService.GetAllViewModel();
            var mejoras = await mejoraService.GetAllViewModel();

            ViewBag.Tipo = tipos;
            ViewBag.TipoVenta = tipoventa;
            ViewBag.Mejoras = mejoras;

            return View(new SavePropiedadViewModel());
        }

        public async Task<IActionResult> Create(SavePropiedadViewModel svm, string selectedMejoras)
        {

            svm.Mejoras = selectedMejoras;
           
            

           
            if (svm.Files != null && svm.Files.Count > 0)
            {
                List<string> imagenes = new List<string>();
                foreach (var file in svm.Files)
                {
                    var imagePath = UploadFile(file, svm.Id);
                    imagenes.Add(imagePath);
                }
                svm.Imagenes = string.Join(";", imagenes); 
            }

            await _service.Add(svm);

            return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
        }
        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Propiedades/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            //Console.WriteLine($"{basePath}/{fileName}");

            return $"{basePath}/{fileName}";
        }

    }
}

