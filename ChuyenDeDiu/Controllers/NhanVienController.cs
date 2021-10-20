using ChuyenDeDiu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChuyenDeDiu.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly QLCayCanhEntities db;
        public NhanVienController()
        {
            WebDbContext webDbContext = new WebDbContext();
            db = webDbContext.GetDBContext();
        }
        [CustomAuthen]
        public ActionResult Index()
        {
            ViewBag.title = "Danh sách tài khoản";
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = db.NhanViens.Where(x =>  (x.TenDangNhap.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name))).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(NhanVien inputModel)
        {
           
            db.NhanViens.Add(inputModel);
            db.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int ma)
        {
            var entity = db.NhanViens.Find(ma);
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(NhanVien inputModel)
        {


            var entity = db.NhanViens.Where(x => x.Ma == inputModel.Ma).FirstOrDefault();
            if (entity == null) 
            {
                return Json(new { result = false,message = "Thêm thất bại" });
            }
           
            db.Entry(entity).CurrentValues.SetValues(inputModel);
            db.SaveChanges();
            return Json(new { result = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {


            var entity = db.NhanViens.Find(id);

            if (entity == null)
            {
                return Json(new { result = false });
            }
           
            db.Entry(entity).CurrentValues.SetValues(entity);
            db.SaveChanges();
            return Json(new { result = true }); ;
        }
    }
}