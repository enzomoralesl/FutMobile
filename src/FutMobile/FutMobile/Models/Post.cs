using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FutMobile.Models
{
    public class Post
    {
        [Key]
        public int Codigo { get; set; }
        [Display(Name = "Login")]
        public string UserLogin { get; set; }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
        public string Titulo { get; set; }
        [Display(Name = "Post")]
        public string Msg { get; set; }
        public string Nome { get; set; }

        // Teste -> irá guardar login dos usuarios que deram like
        [Display(Name = "Numero de Likes")]
        public string Likes { get; set; }
    }
}
 