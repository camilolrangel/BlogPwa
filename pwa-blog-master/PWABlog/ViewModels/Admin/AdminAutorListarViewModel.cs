using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWABlog.ViewModels.Admin
{
    public class AdminAutorListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutoresAdminAutores> Autores { get; set; }
        public AdminAutorListarViewModel()
        {
            TituloPagina = "Autores - Administrador";
        }
    }

    public class AutoresAdminAutores
    {
        public int Id { get; set; }
        public string Nome { get; set; }

    }
}
