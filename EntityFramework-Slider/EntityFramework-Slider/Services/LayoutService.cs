using EntityFramework_Slider.Data;
using EntityFramework_Slider.Services.Interfaces;
using EntityFramework_Slider.ViewModels;
using Newtonsoft.Json;

namespace EntityFramework_Slider.Services
{
    public class LayoutService: ILayoutService   
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;  
        private readonly IBasketService _basketService;

        public LayoutService(AppDbContext context ,
                             IHttpContextAccessor httpContextAccessor,
                             IBasketService basketService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
        }


         public LayoutVM GetSettingDatas()  
         {
            Dictionary<string,string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
           

            List<BasketVM> basketDatas = _basketService.GetBasketDatas();


            //int count =  basketDatas.Sum(b => b.Count);   
            //LayoutVM model = new()
            //{
            //    Settings = settings,
            //    BasketCount = count         
            //};

            return new LayoutVM { Settings = settings, BasketCount = basketDatas.Sum(b => b.Count) };

         }



    }
}
