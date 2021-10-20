using ChuyenDeDiu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChuyenDeDiu.Controllers
{
    public class CayController : Controller
    {
        private readonly QLCayCanhEntities db;
        public CayController()
        {
            WebDbContext webDbContext = new WebDbContext();
            db = webDbContext.GetDBContext();
        }
        [CustomAuthen]
        public ActionResult Index()
        {
            ViewBag.title = "Danh sách cây ";
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = db.SanPhams.Where(x => x.Ten.ToLower().Contains(name.ToLower()) || string.IsNullOrEmpty(name)).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["PhanLoai"] = db.PhanLoais.Where(x => true).ToList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(SanPham inputModel)
        {
            inputModel.SoLuong = 0;
            inputModel.PhanLoai = db.PhanLoais.Where(x => x.Ma == inputModel.MaPhanLoai).FirstOrDefault();
            db.SanPhams.Add(inputModel);
            db.SaveChanges();
            return Json(new { result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewData["PhanLoai"] = db.PhanLoais.Where(x => true).ToList();
            var entity = db.SanPhams.Find(id);
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(SanPham inputModel)
        {


            var entity = db.SanPhams.Find(inputModel.Ma);
            if (entity == null)
            {
                return Json(new { result = false });
            }
            db.Entry(entity).CurrentValues.SetValues(inputModel);
            db.SaveChanges();
            return Json(new { result = true,message="Thêm mới thành công"});
        }
        [HttpPost]
        public ActionResult Delete(int Ma)
        {


            var entity = db.SanPhams.Find(Ma);

            if (entity == null)
            {
                return Json(new { result = false });
            }
            db.SanPhams.Remove(entity);

            db.SaveChanges();
            return Json(new { result = true }); ;
        }
    }
}