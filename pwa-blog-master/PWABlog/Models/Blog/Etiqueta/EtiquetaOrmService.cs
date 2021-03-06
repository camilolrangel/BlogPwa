﻿using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public EtiquetaOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<EtiquetaEntity> ObterEtiqueta()
        {
            // INÍCIO DOS EXEMPLOS

            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/

            // First = Obter a primeira categoria retornada pela consulta
            //var primeiraCategoria = _databaseContext.Categorias.First();

            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            var primeiraEtiquetaOuNulo = _databaseContext.Etiquetas.FirstOrDefault();

            // Single = Obter um único registro do banco de dados
            // var algumaCategoriaEspecifica = _databaseContext.Categorias.Single(c => c.Id == 3);

            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            var algumaEtiquetaOuNulo = _databaseContext.Etiquetas.SingleOrDefault(c => c.Id == 3);

            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            var encontrarEtiqueta = _databaseContext.Etiquetas.Find(3);


            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/

            // ToList
            var todasEtiquetas = _databaseContext.Etiquetas.ToList();


            /***********/
            /* FILTROS */
            /***********/

            var etiquetasComALetraG = _databaseContext.Etiquetas.Where(c => c.Nome.StartsWith("G")).ToList();
            var etiquetasComALetraMouL = _databaseContext.Etiquetas
                .Where(c => c.Nome.StartsWith("M") || c.Nome.StartsWith("L"))
                .ToList();



            /*************/
            /* ORDENAÇÃO */
            /*************/

            var etiquetasEmOrdemAlfabetica = _databaseContext.Etiquetas.OrderBy(c => c.Nome).ToList();
            var etiquetasEmOrdemAlfabeticaInversa = _databaseContext.Etiquetas.OrderByDescending(c => c.Nome).ToList();


            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            var etiquetasESuaCategoria = _databaseContext.Etiquetas
                .Include(c => c.Categoria);
            // FIM DOS EXEMPLOS



            // Código de fato necessário para o método
            return _databaseContext.Etiquetas
                .Include(c => c.Categoria)
                .ToList();
        }

        public EtiquetaEntity ObterEtiquetaPorId(int idEtiqueta)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(idEtiqueta);

            return etiqueta;
        }

        public List<EtiquetaEntity> PesquisarEtiquetaPorNome(string nomeEtiqueta)
        {
            return _databaseContext.Etiquetas.Where(c => c.Nome.Contains(nomeEtiqueta)).ToList();

        }


        public EtiquetaEntity CriarEtiqueta(string nome, CategoriaEntity idCategoria)
        {
            // Verificar se um nome foi passado
            if (nome == null)
            {
                throw new Exception("A Etiqueta precisa de um nome!");
            }

            // Verificar existência da Categoria da Etiqueta
            var categoria = _databaseContext.Categorias.Find(idCategoria);
            if (categoria == null)
            {
                throw new Exception("A Categoria informada para a Etiqueta não foi encontrada!");
            }

            // Criar nova Etiqueta
            var novaEtiqueta = new EtiquetaEntity { Nome = nome, Categoria = categoria };
            _databaseContext.Etiquetas.Add(novaEtiqueta);
            _databaseContext.SaveChanges();

            return novaEtiqueta;
        }

        public EtiquetaEntity EditarEtiqueta(int id, string nome, CategoriaEntity idCategoria)
        {
            // Obter Etiqueta a Editar
            var etiqueta = _databaseContext.Etiquetas.Find(id);
            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada!");
            }

            // Verificar existência da Categoria da Etiqueta
            var categoria = _databaseContext.Categorias.Find(idCategoria);
            if (categoria == null)
            {
                throw new Exception("A Categoria informada para a Etiqueta não foi encontrada!");
            }

            // Atualizar dados da Etiqueta
            etiqueta.Nome = nome;
            etiqueta.Categoria = categoria;
            _databaseContext.SaveChanges();

            return etiqueta;
        }

        public bool RemoverEtiqueta(int id)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(id);

            if (etiqueta == null)
            {
                throw new Exception("Etiqueta não encontrada!");
            }

            _databaseContext.Etiquetas.Remove(etiqueta);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}
