using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> SlidersList {  get; set; } 
        public IEnumerable<Article> ArticleList {  get; set; }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }
    
}
