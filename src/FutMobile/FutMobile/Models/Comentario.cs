using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FutMobile.Models
{
    public class Comentario
    {
        [Key]
        public int CodigoCom { get; set; }
        public int CodigoDoPost { get; set; }
        public string UserLoginCom { get; set; }
        public DateTime DataCom { get; set; }
        public string MsgCom { get; set; }
        public string NomeCom { get; set; }
        [Display(Name = "Numero de Likes")]
        public string LikesCom { get; set; }
    }
}