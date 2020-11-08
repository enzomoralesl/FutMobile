using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FutMobile.Models
{
    public class Campeonato
    {
        [Key]
        public int CodigoCamp { get; set; }

        [Display(Name = "Nome do Campeonato")]
        [Required(ErrorMessage = "O campo Nome do Campeonato é requerido.")]
        public string NomeCamp { get; set; }

        [Display(Name = "Logo")]
        public string SimboloCampPath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Primeira Edição")]
        [Required(ErrorMessage = "O campo Primeira Edição é requerido.")]
        public int PrimeiraEdicao { get; set; }

        [Display(Name = "Resumo do Campeonato")]
        [Required(ErrorMessage = "O campo Resumo do campeonato é requerido.")]
        [Column(TypeName = "varchar(MAX)")]
        public string ResumoCamp { get; set; }

        [Display(Name = "Principais Artilheiros")]
        [Required(ErrorMessage = "O campo Principais Artilheiros é requerido.")]
        [Column(TypeName = "varchar(MAX)")]
        public string PrincipaisArtilheiros { get; set; }
    }
}