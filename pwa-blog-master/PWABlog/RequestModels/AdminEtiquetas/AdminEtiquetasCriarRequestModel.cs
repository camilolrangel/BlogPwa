using PWABlog.Models.Blog.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.RequestModels.AdminEtiquetas
{
    public class AdminEtiquetasCriarRequestModel
    {
        public string Nome { get; set; }
        public CategoriaEntity Categoria { get; set; }
    }
}
