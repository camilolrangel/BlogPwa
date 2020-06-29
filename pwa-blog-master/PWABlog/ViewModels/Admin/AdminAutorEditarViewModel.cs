using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminAutorEditarViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Erro { get; set; }

        public AdminAutorEditarViewModel()
        {
            TituloPagina = "Editar Autor";

        }
    }
}
