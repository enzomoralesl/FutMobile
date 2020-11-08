using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FutMobile.Models
{
    public class MotivoModel
    {
        [Key]
        public int IDMotivo { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Motivo da Remoção")]
        public string MotivoDaRemocao { get; set; }
    }
}