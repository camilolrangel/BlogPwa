using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly DatabaseContext _databaseContext;
        //private readonly RevisaoOrmService _revisaoOrmService;

        public PostagemOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            //_revisaoOrmService = revisaoOrmService;
        }

        public List<PostagemEntity> ObterPostagens()
        {
            return _databaseContext.Postagens
                .Include(p => p.Categoria)
                .Include(p => p.Revisoes)
                .Include(p => p.Comentarios)
                
                .ToList();
        }

        public List<PostagemEntity> ObterPostagensPopulares()
        {
            //return _databaseContext.Postagens.Include(p => p.Categoria).ToList();
            return _databaseContext.Postagens
               .Include(p => p.Autor)
               .OrderByDescending(c => c.Comentarios.Count)
               .Take(4)
               .ToList();
        }

        public PostagemEntity ObterPostagemPorId(int idPostagem)
        {
            var postagem = _databaseContext.Postagens.Find(idPostagem);

            return postagem;
        }


        public PostagemEntity CriarPostagem(string titulo, string descricao, AutorEntity autor, CategoriaEntity categoria, DateTime dataPostagem)
        {
            var novaPostagem = new PostagemEntity { Titulo = titulo, Descricao = descricao, Autor = autor, Categoria = categoria, DataPostagem = dataPostagem };
            _databaseContext.Postagens.Add(novaPostagem);
            _databaseContext.SaveChanges();

            // Criar a Revisão para a Postagem
            //_revisaoOrmService.CriarRevisao(novaPostagem.Id, texto);

            return novaPostagem;
        }

        public PostagemEntity EditarPostagem(int id, string titulo, string descricao, AutorEntity autor, CategoriaEntity categoria, DateTime dataPostagem)
        {
            var postagem = _databaseContext.Postagens.Find(id);

            if (postagem == null)
            {
                throw new Exception("Postagem não encontrada!");
            }

                postagem.Titulo = titulo;
                postagem.Descricao = descricao;
                postagem.Autor = autor;
                postagem.Categoria = categoria;
                postagem.DataPostagem = dataPostagem;

            _databaseContext.SaveChanges();

            // Criar nova Revisão para a Postagem
            //_revisaoOrmService.CriarRevisao(postagem.Id, texto);

            return postagem;
        }

        public bool RemoverPostagem(int id)
        {
            var postagem = _databaseContext.Postagens.Find(id);

            if (postagem == null)
            {
                throw new Exception("Postagem não encontrada!");
            }

            _databaseContext.Postagens.Remove(postagem);
            _databaseContext.SaveChanges();

            return true;
        }

    }
}