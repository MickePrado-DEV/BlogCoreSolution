using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class Article
    {
        public Article()
        {
            CreationDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre del articulo es obligatorio")]
        [Display(Name="Nombre del Articulo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripcion del articulo es obligatorio")]
        [Display(Name = "Descripcion del Articulo")]
        public string Description { get; set; }
        [Display(Name = "Fecha de creacion del Articulo")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string? UrlImage { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La categoria es obligatoria")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
