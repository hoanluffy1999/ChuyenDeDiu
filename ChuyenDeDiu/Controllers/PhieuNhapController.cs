using ChuyenDeDiu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChuyenDeDiu.Controllers
{
    public class PhieuNhapController : Controller
    {
        private readonly QLCayCanhEntities db;
        public PhieuNhapController()
        {
            WebDbContext webDbContext = new WebDbContext();
            db = webDbContext.GetDBContext();
        }
        [CustomAuthen]
        public ActionResult Index()
        {
            ViewBag.title = "Danh sách nhà phiếu nhập";
            return View();
        }
        [HttpGet]
        public ActionResult GetList(string name)
        {
            var data = db.PhieuNhaps.Where(x =>/* x.Ma.ToLower().Contains(name.ToLower()) ||*//* string.IsNullOrEmpty(name)*/ true).ToList();
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["NhanVien"] = db.NhanViens.Where(x => true).ToList();
            ViewData["NhaCungCap"] = db.NhaCungCaps.Where(x => true).ToList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(PhieuNhap inputModel)
        {

            var nhanvien = db.NhanViens.Where(x => x.Ma == inputModel.MaNV).FirstOrDefault();
            var nhaCungCap = db.NhaCungCaps.Where(x => x.Ma == inputModel.MaNCC).FirstOrDefault();
            inputModel.NhanVien = nhanvien;
            inputModel.NhaCungCap = nhaCungCap;
            inputModel.NgayNhap = DateTime.Now;
            var data = db.PhieuNhaps.Add(inputModel);
            db.SaveChanges();
            PhieuNhapViewModel viewModel = new PhieuNhapViewModel()
            {
                Ma = data.Ma,
                MaNCC = data.MaNCC,
                MaNV = data.MaNV
            };
            return Json(new { data = viewModel, result = true }); ;
        }
        [HttpGet]
        public ActionResult Update(int Ma)
        {
            ViewData["NhanVien"] = db.NhanViens.Where(x => true).ToList();
            ViewData["NhaCungCap"] = db.NhaCungCaps.Where(x => true).ToList();
            var entity = db.PhieuNhaps.Find(Ma);
            return PartialView(entity);
        }
        [HttpPost]
        public ActionResult Update(PhieuNhap inputModel)
        {


            var entity = db.PhieuNhaps.Find(inputModel.Ma);
            if (entity == null)
            {
                return Json(new { result = false });
            }
            var nhanvien = db.NhanViens.Where(x => x.Ma == inputModel.MaNV).FirstOrDefault();
            var nhaCungCap = db.NhaCungCaps.Where(x => x.Ma == inputModel.MaNCC).FirstOrDefault();
            inputModel.NhanVien = nhanvien;
            inputModel.NhaCungCap = nhaCungCap;
            db.Entry(entity).CurrentValues.SetValues(inputModel);
            db.SaveChanges();
            return Json(new { result = true });
        }
        [HttpPost]
        public ActionResult Delete(int Ma)
        {


            var entity = db.PhieuNhaps.Find(Ma);

            if (entity == null)
            {
                return Json(new { result = false });
            }
            db.PhieuNhaps.Remove(entity);

            db.SaveChanges();
            return Json(new { result = true }); ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ma"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChiTiet(int ma)
        {
            var data = db.PhieuNhaps.Where(x => x.Ma == ma).FirstOrDefault();
            return PartialView(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateChiTiet()
        {
            ViewData["SanPham"] = db.SanPhams.Where(x => true).ToList();

            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateChiTiet(ChiTiepPhieuNhap inputModel)
        {
            var data = db.ChiTiepPhieuNhaps.Add(inputModel);
            db.SaveChanges();
            return Json(new { data = data, result = true }); ;
        }
        [HttpGet]
        public ActionResult UpdateChiTiet(int Ma)
        {
            ViewData["SanPham"] = db.SanPhams.Where(x => true).ToList();
            var data = db.PhieuNhaps.Where(x => x.Ma == Ma).FirstOrDefault();
            
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult UpdateChiTiet(ChiTiepPhieuNhap inputModel)
        {


            var entity = db.ChiTiepPhieuNhaps.Find(inputModel.Ma);
            if (entity == null)
            {
                return Json(new { result = false });
            }
            db.Entry(entity).CurrentValues.SetValues(inputModel);
            db.SaveChanges();
            return Json(new { result = true });
        }
        [HttpGet]
        public ActionResult GetListChiTiet(int maPhieu)
        {
            var data = db.ChiTiepPhieuNhaps.Where(x =>x.MaPhieuNhap == maPhieu).ToList();
            return PartialView("ListChiTiet", data);
        }
        [HttpPost]
        public ActionResult DeleteChiTiet(int Ma)
        {


            var entity = db.ChiTiepPhieuNhaps.Find(Ma);

            if (entity == null)
            {
                return Json(new { result = false });
            }
            db.ChiTiepPhieuNhaps.Remove(entity);

            db.SaveChanges();
            return Json(new { result = true }); ;
        }
    }
}