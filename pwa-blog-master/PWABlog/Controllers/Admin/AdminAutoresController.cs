using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.RequestModels.AdminAutor;
using PWABlog.ViewModels.Admin;

namespace PWABlog.Controllers.Admin
{
    public class AdminAutoresController : Controller
    {
        private readonly AutorOrmService _autorOrmService;
        public AdminAutoresController(

            AutorOrmService autorOrmService

        ){

            _autorOrmService = autorOrmService;

        }


        [HttpGet]

        public IActionResult Listar()
        {
            AdminAutorListarViewModel model = new AdminAutorListarViewModel();

            // Alimentar o model com os autores que serão listados

            // Obter as Etiquetas
            var listarAtores = _autorOrmService.ObterAutores();

            foreach (var AutorEntity in listarAtores)
            {
                var autoresAdminAutores = new AutoresAdminAutores();
                autoresAdminAutores.Id = AutorEntity.Id;
                autoresAdminAutores.Nome = AutorEntity.Nome;


                model.Autores.Add(autoresAdminAutores);
            }


            return View(model);
        }

        [HttpGet]

        public IActionResult Detalhar()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Criar()
        {
            AdminAutorCriarRequestModel model = new AdminAutorCriarRequestModel();

            // Definir possível erro de processamento (vindo do post do criar)
            ViewBag.erro = TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Criar(AdminAutorCriarRequestModel request)
        {
            var nome = request.Nome;

            try
            {
                _autorOrmService.CriarAutor(nome);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]

        public IActionResult Editar(int id)
        {
            AdminAutorCriarRequestModel model = new AdminAutorCriarRequestModel();

            // Obter etiqueta a editar
            var autorEditar = _autorOrmService.ObterAutores(id);
            if (autorEditar == null)
            {
                return RedirectToAction("Listar");
            }

            // Alimentar o model com os dados da etiqueta a ser editada
            model.Nome = autorEditar.Nome;
            model.Id = autorEditar.Id;

            return View(model);

        }

        [HttpPost]
        public RedirectToActionResult Editar(AdminAutorEditarRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try
            {
                _autorOrmService.EditarAutor(id, nome);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new { id = id });
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]

        public IActionResult Remover(int id)
        {
            AdminAutorCriarRequestModel model = new AdminAutorCriarRequestModel();

            // Obter etiqueta a editar
            var autorRemover = _autorOrmService.ObterAutores(id);
            if (autorRemover == null)
            {
                return RedirectToAction("Listar");
            }

            // Alimentar o model com os dados da etiqueta a ser editada
            model.Id = autorRemover.Id;
            model.Nome = autorRemover.Nome;

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Remover(AdminAutorRemoverRequestModel request)
        {
            var id = request.Id;

            try
            {
                _autorOrmService.RemoverAutor(id);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new { id = id });
            }

            return RedirectToAction("Listar");
        }

        //[HttpGet]
        //[Route(template:"admin/autores")]
        //[Route(template: "admin/autores/listar")]
        //public String Listar()
        //{
        //    return "Listar Autores";
        //}

        //[HttpPost]
        //[Route(template: "admin/autores/criar")]
        //public String Criar()
        //{
        //    return "Criar Autor";
        //}

        //[HttpPost]
        //[Route(template: "admin/autores/editar/{id}")]
        //public String Editar(int id)
        //{
        //    return "Editar Autor";
        //}

        //[HttpPost]
        //[Route(template: "admin/autores/remover/{id}")]
        //public String Remover(int id)
        //{
        //    return "Remover Autor";
        //}

    }
}