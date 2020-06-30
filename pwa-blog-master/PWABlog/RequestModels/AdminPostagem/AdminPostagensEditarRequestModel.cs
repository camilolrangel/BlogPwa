using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.RequestModels.AdminPostagem
{
    public class AdminPostagensEditarRequestModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Autor { get; set; }
        public string Categoria { get; set; }

        public string DataPostagem { get; set; }
        public string Texto { get; set; }
    }
}
