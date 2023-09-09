using Microsoft.AspNetCore.Mvc;
using RedisTest.CacheManagement;
using RedisTest.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace RedisTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheService _cacheService;
        CacheService cacheService = new CacheService(); 
        public List<Member> Family;
        public HomeController(ILogger<HomeController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
            
        }

        public IActionResult Index()
        {
           var Family = cacheService.GetData<List<Member>>("Family");
            if (Family == null)
            {
                Member obj = new Member();
                Family = obj.GetFamily();


                cacheService.SetData<List<Member>>("Family", Family, DateTimeOffset.Now.AddMinutes(2));
            }
            return View(Family);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Edit(int id)
        {
            Member obj = new Member();

            Family = obj.GetFamily();
            var objq = Family.Where(a => a.Id == id).FirstOrDefault();
            return View(objq);
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member objMem)
        {
            try
            {
                Family = objMem.GetFamily();
                objMem.UpdateFamilyMember(Family, objMem);
                return View(nameof(Index),Family) ;
            }
            catch
            {
                return View();
            }
        }

        // GET: TestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}