using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BlogCoreSolution.Models.ViewModels
{
    public class ArticleVM
    {
        public Article Article { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem>? ListOfCategories { get; set; }
    }
}
