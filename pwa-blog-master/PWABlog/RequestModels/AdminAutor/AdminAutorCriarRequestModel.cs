using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.RequestModels.AdminAutor
{
    public class AdminAutorCriarRequestModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Erro { get; set; }

    }
}
