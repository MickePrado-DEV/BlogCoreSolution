using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El Nombre de categoria es requerido")]
        [MaxLength(60)]
        [Display(Name= "Nombre de Categoria")]
        public string Name { get; set; }
        [Display(Name = "Orden de visualizacion")]
        [Range(1,10, ErrorMessage = "El valor debe estasr entre 1 y 100")]
        public int  Order { get; set; }

        public DateTime DateCreate { get; set; }


    }
}
