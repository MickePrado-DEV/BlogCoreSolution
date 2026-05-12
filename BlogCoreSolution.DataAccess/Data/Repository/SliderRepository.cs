using BlogCoreSolution.DataAccess.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(Slider slider)
        {
            var objToDb = _db.Sliders.FirstOrDefault(c => c.Id == slider.Id);
            objToDb.Name = slider.Name;
            objToDb.Status = slider.Status;
            objToDb.UrlImage = slider.UrlImage;


        }
    }

       
}
