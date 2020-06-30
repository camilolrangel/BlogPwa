using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels.AdminPostagem;
using PWABlog.ViewModels.Admin;
using static PWABlog.ViewModels.Admin.AdminPostagensListarViewModel;

namespace PWABlog.Controllers.Admin
{
    public class AdminPostagemController : Controller
    {
       
            private readonly PostagemOrmService _postagemOrmService;
            private readonly CategoriaOrmService _categoriaOrmService;
            private readonly AutorOrmService _autoresOrmService;

        public AdminPostagemController(

                PostagemOrmService postagemOrmService,
                CategoriaOrmService categoriaOrmService,
                AutorOrmService autoresOrmService

            )
            {

                _postagemOrmService = postagemOrmService;
                _categoriaOrmService = categoriaOrmService;
                _autoresOrmService = autoresOrmService;

        }

            [HttpGet]

            public IActionResult Listar()
            {
                
            AdminPostagensListarViewModel model = new AdminPostagensListarViewModel();

            //Obter as Postagens
            var listarPostagens = _postagemOrmService.ObterPostagens();

            //Alimentar a model com as postagens que serão Listadas

            foreach (var postagemEntity in listarPostagens)
            {
                var postagemAdminPostagens = new PostagemAdminPostagens();
                postagemAdminPostagens.IdPostagem = postagemEntity.Id;
                postagemAdminPostagens.NomePostagem = postagemEntity.Descricao;
                postagemAdminPostagens.NomeAutor = postagemEntity.Autor.Nome;
                postagemAdminPostagens.NomeCategoria = postagemEntity.Categoria.Nome;

                model.Postagens.Add(postagemAdminPostagens);

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
            
            AdminPostagensCriarViewModel model = new AdminPostagensCriarViewModel();

            model.Erro = (string)TempData["erro-msg"];

            //Obter Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            //Alimentar o model com as categorias que serão colocadas no select

            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminPostagens = new CategoriaAdminPostagens();
                categoriaAdminPostagens.IdCategorias = categoriaEntity.Id;
                categoriaAdminPostagens.NomeCategorias = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminPostagens);
            }
            //Obter Autores
            var listaAutores = _autoresOrmService.ObterAutores();

            //Alimentar o model com os autores que serão colocadas no select

            foreach (var autorEntity in listaAutores)
            {
                var autorAdminPostagens = new AutorAdminPostagens();
                autorAdminPostagens.IdAutores = autorEntity.Id;
                autorAdminPostagens.NomeAutores = autorEntity.Nome;

                model.Autores.Add(autorAdminPostagens);
            }

            return View(model);
        }

            [HttpPost]
            public RedirectToActionResult Criar(AdminPostagensCriarRequestModel request)
            {
                var titulo = request.Titulo;
                var descricao = request.Descricao;
                var autor = request.Autor;
                var texto = request.Texto;
                var categoria = request.Categoria;
                var dataPostagem = DateTime.Parse(request.DataPostagem);

            try
                {
                    _postagemOrmService.CriarPostagem(titulo, descricao, autor, categoria, dataPostagem);
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
            AdminPostagensEditarViewModel model = new AdminPostagensEditarViewModel();

            // Obter etiqueta a editar
            var postagemEditar = _postagemOrmService.ObterPostagemPorId(id);
            if (postagemEditar == null)
            {
                return RedirectToAction("Listar");
            }

            // Definir possível erro de processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();

            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias)
            {
                var categoriaAdminPostagem = new CategoriaAdminPostagens();
                categoriaAdminPostagem.IdCategorias = categoriaEntity.Id;
                categoriaAdminPostagem.NomeCategorias = categoriaEntity.Nome;

                model.Categorias.Add(categoriaAdminPostagem);
            }
            //Obter Autores
            var listaAutores = _autoresOrmService.ObterAutores();

            //Alimentar o model com os autores que serão colocadas no select

            foreach (var autorEntity in listaAutores)
            {
                var autorAdminPostagens = new AutorAdminPostagens();
                autorAdminPostagens.IdAutores = autorEntity.Id;
                autorAdminPostagens.NomeAutores = autorEntity.Nome;

                model.Autores.Add(autorAdminPostagens);
            }

            // Alimentar o model com os dados da etiqueta a ser editada
            model.IdPostagem = postagemEditar.Id;
            model.NomePostagem = postagemEditar.Descricao;
            model.IdAutorPostagem = postagemEditar.Autor.Id;
            model.IdCategoriaPostagem = postagemEditar.Categoria.Id;
            model.TituloPagina += model.NomePostagem;

            return View(model);
        }

            [HttpPost]
            public RedirectToActionResult Editar(AdminPostagensEditarRequestModel request)
            {
                var id = request.Id;
                var titulo = request.Titulo;
                var descricao = request.Descricao;
                var autor = request.Autor;
                var categoria = request.Categoria;
                var dataPostagem = DateTime.Parse(request.DataPostagem);

            try
                {
                    _postagemOrmService.EditarPostagem(id, titulo, descricao, autor, categoria,dataPostagem);
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
                ViewBag.id = id;
                ViewBag.erro = TempData["erro-msg"];

                return View();
            }

            [HttpPost]
            public RedirectToActionResult Remover(AdminPostagensRemoverRequestModel request)
            {
            AdminPostagensRemoverViewModel model = new AdminPostagensRemoverViewModel();

            // Obter etiqueta a remover
            var postagemRemover = _postagemOrmService.ObterPostagemPorId(id);
            if (postagemRemover == null)
            {
                return RedirectToAction("Listar");
            }

            // Definir possível erro de processamento (vindo do post do criar)
            model.Erro = (string)TempData["erro-msg"];

            // Alimentar o model com os dados da etiqueta a ser editada
            model.IdPostagem = postagemRemover.Id;
            model.NomePostagem = postagemRemover.Descricao;
            model.TituloPagina += model.NomePostagem;

            return View(model);

        }
    }
    }