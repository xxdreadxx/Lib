using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Dao.DaoAdmin;
using Models.EF;

namespace LibDemo.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        DonViDao dv = new DonViDao();
        NhanVienDao nv = new NhanVienDao();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(string username, string pass)
        {
            string host = System.Web.HttpContext.Current.Request.Url.Host;
            if (host != null)
            {
                var donvi = dv.getDataByHost(host);
                if (donvi != null)
                {
                    Session["IDDonVi"] = donvi.ID;
                    var tk = nv.getDataByUsername(username, pass, donvi.ID);
                    if (tk == -1)
                    {
                        return Json(new
                        {
                            status = false,
                            message = "Tên đăng nhập không tồn tại trong hệ thống hoặc không thuộc đơn vị này, đăng nhập thất bại!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (tk == -2)
                    {
                        return Json(new
                        {
                            status = false,
                            message = "Sai mật khẩu, đăng nhập thất bại!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (tk == -3)
                    {
                        return Json(new
                        {
                            status = false,
                            message = "Tài khoản bị khóa, đăng nhập thất bại!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (tk > 0)
                    {
                        Session["IDNV"] = tk;
                        return Json(new
                        {
                            status = true,
                            message = "Đăng nhập thành công!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            status = false,
                            message = "Đăng nhập thất công!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        message = "Xảy ra lỗi khi duyệt đơn vị, đăng nhập thất bại!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Xảy ra lỗi khi duyệt đơn vị, đăng nhập thất bại!"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LogOut()
        {
            Session["IDNV"] = null;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}