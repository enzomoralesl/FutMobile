using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FutMobile.Models
{
    public class Time
    {
        [Key]
        public int CodigoTime { get; set; }

        [Display(Name = "Nome do Time")]
        [Required(ErrorMessage = "O campo Nome do Time é requerido.")]
        public string NomeTime { get; set; }

        [Display(Name = "Escudo")]
        public string BrasaoPath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Fundação")]
        [Required(ErrorMessage = "O campo Fundação é requerido.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime Fundacao { get; set; }

        [Display(Name = "Resumo da história")]
        [Required(ErrorMessage = "O campo Resumo da história é requerido.")]
        [Column(TypeName = "varchar(MAX)")]
        public string Resumo { get; set; }

        [Display(Name = "Resumo dos títulos")]
        [Required(ErrorMessage = "O campo Resumo dos títulos é requerido.")]
        [Column(TypeName = "varchar(MAX)")]
        public string ResumoTitulos { get; set; }

        [Display(Name = "Número de Mundiais")]
        [Required(ErrorMessage = "O campo Mundial é requerido.")]
        public int Mundial { get; set; }

        [Display(Name = "Número de Libertadores")]
        [Required(ErrorMessage = "O campo Libertadores é requerido.")]
        public int Libertadores { get; set; }

        [Display(Name = "Número de Campeonatos Brasileiros")]
        [Required(ErrorMessage = "O campo Campeonatos Brasileiros é requerido.")]
        public int Brasileiro { get; set; }

        [Display(Name = "Número de Copa do Brasil")]
        [Required(ErrorMessage = "O campo Copa do Brasil é requerido.")]
        public int CopaDoBrasil { get; set; }

        [Display(Name = "Número de campeonatos estaduais")]
        [Required(ErrorMessage = "O campo Estadual é requerido.")]
        public int Estadual { get; set; }
    }
}