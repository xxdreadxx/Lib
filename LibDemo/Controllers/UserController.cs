﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.EF;
using Models.ModelsView;
using Models.Dao.DaoAdmin;

namespace LibDemo.Controllers
{
    public class UserController : Controller
    {
        PhieuMuon_Dao pm = new PhieuMuon_Dao();
        DonViDao dv = new DonViDao();
        BanDocDao nv = new BanDocDao();
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginP(string username, string pass)
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
                        Session["IDUser"] = tk;
                        Session["TenUser"] = username;
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

        public JsonResult Register(string username, string pass, string ten)
        {
            string host = System.Web.HttpContext.Current.Request.Url.Host;
            if (host != null)
            {
                var donvi = dv.getDataByHost(host);
                if (donvi != null)
                {
                    Session["IDDonVi"] = donvi.ID;
                    cBanDoc item = new cBanDoc();
                    item.HoTen = ten;
                    item.IDDonVi = donvi.ID;
                    item.IDDonVi = donvi.ID;
                    item.NgayHetHan = DateTime.Now.AddYears(1);
                    item.NgayTao = DateTime.Now;
                    item.Password = pass;
                    item.Username = username;
                    item.TrangThai = 1;
                    var kt = nv.Insert(item);
                    if (kt == 0)
                    {
                        return Json(new
                        {
                            status = false,
                            message = "Xảy ra lỗi, đăng ký tài khoản không thành công!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        Session["IDUser"] = kt;
                        return Json(new
                        {
                            status = true,
                            message = "Đăng ký thành công!"
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
            Session["IDUser"] = null;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TaoPhieu(long id)
        {
            long IDUser = long.Parse(Session["IDUser"].ToString());
            int IDDonVi = int.Parse(Session["IDDonVi"].ToString());
            cPhieuMuon item = new cPhieuMuon();
            item.IDBanDoc = IDUser;
            item.IDDonVi = IDDonVi;
            item.IDMCB = id;
            item.NgayMuon = DateTime.Now;
            item.NgayHenTra = DateTime.Now.AddDays(5);
            item.NgayTao = DateTime.Now;
            item.TrangThai = 2;
            bool kt = pm.Insert(item);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListPM()
        {
            long IDUser = long.Parse(Session["IDUser"].ToString());
            ViewBag.lstPM = pm.getListDataViewByIDBanDoc(IDUser);
            return View();
        }

        public JsonResult HuyPM(long id)
        {
            bool kt = pm.Delete(id, 0);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeStatusPM(long id, byte type)
        {
            bool kt = pm.ChangeStatus(id, type, 0);
            return Json(new
            {
                status = kt
            }, JsonRequestBehavior.AllowGet);
        }
    }
}