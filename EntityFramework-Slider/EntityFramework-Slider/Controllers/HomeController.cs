using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.Services.Interfaces;
using EntityFramework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EntityFramework_Slider.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;




        public HomeController(AppDbContext context,
                              IBasketService basketService,
                              IProductService productService )
        {
            _context = context;
            _basketService = basketService;
            _productService = productService;
            
        }

       







        public async Task<IActionResult> Index()
        {




            
            List<Slider> sliders = await _context.Sliders.ToListAsync();

            

            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();

           
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => m.SoftDelete == false).ToListAsync();

            
            IEnumerable<Category> categories = await _context.Categories.Where(m => m.SoftDelete == false).ToListAsync();


            
            IEnumerable<Product> products = await _productService.GetAll();   

            About abouts = await _context.Abouts.Include(m => m.Adventages).FirstOrDefaultAsync();

            IEnumerable<Experts> experts = await _context.Experts.Where(m => m.SoftDelete == false).ToListAsync();

            ExpertsHeader expertsHeader = await _context.ExpertsHeaders.FirstOrDefaultAsync();

            Subscribe subscribe = await _context.Subscribes.FirstOrDefaultAsync();

            OurBlog ourBlog = await _context.OurBlogs.FirstOrDefaultAsync();

            IEnumerable<Say> says = await _context.Says.Where(m => m.SoftDelete == false).ToListAsync();

            IEnumerable<Instagram> instagrams = await _context.Instagrams.Where(m => m.SoftDelete == false).ToListAsync();





            HomeVM model = new()
            {
                Sliders = sliders,
                SliderInfo = sliderInfo,
                Blogs = blogs,
                Categories = categories,
                Products = products,
                Abouts = abouts,
                Experts = experts,
                ExpertsHeader = expertsHeader,
                Subscribe = subscribe,
                OurBlog = ourBlog,
                Says = says,
                Instagrams = instagrams,
            };

            return View(model);
        }



      






        

        [HttpPost] 

        
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return BadRequest();

            Product dbproduct = await _productService.GetById((int)id);     

            if (dbproduct == null) return NotFound();

            List<BasketVM> basket = _basketService.GetBasketDatas(); 

            BasketVM? existProduct = basket?.FirstOrDefault(m => m.Id == dbproduct.Id);

            _basketService.AddProductToBasket(existProduct, dbproduct, basket);  


            int basketCount = basket.Sum(m => m.Count);  

            return Ok(basketCount);
        }


    }

    
}