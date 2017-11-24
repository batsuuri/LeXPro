using LeXPro.Models;
using LeXPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeXPro.Controllers
{
        [Authorize]
    public class TerminalController : Controller
    {
        #region SYSCONFIG
        public ActionResult SysConfigIndex()
        {
            Result res = TerminalContext.SysConfigList();
            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            return View((SysConfigViewModel)res.Data);

        }

        public ActionResult SysConfigEdit(string id)
        {
            if (Session["Message"] != null)
            {
                ViewBag.Result = Session["Message"];
                Session["Message"] = null;
            }
            SysConfigViewModel model = new SysConfigViewModel();
            Result res = TerminalContext.SysConfigList();
            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            else
            {
                model = (SysConfigViewModel)res.Data;
                model.SetCurrent(id);
                model.DisplayMode = "EditOnly";
            }
            return View("SysConfigIndex", model);
        }

        // POST: Admin/Food/Edit/5
        [HttpPost]
        public ActionResult SysConfigUpdate(LeXPro.Models.SysConfig CurrentSysConfig)
        {
            SysConfigViewModel model = new SysConfigViewModel();

            Result res = TerminalContext.SysConfigUpdate(CurrentSysConfig);

            if (res.Succeed)
            {
                model = (SysConfigViewModel)res.Data;
                model.SetCurrent(CurrentSysConfig.config_key);
                model.DisplayMode = "EditOnly";
                ViewBag.Result = "Successfully updated";
            }
            else
            {
                ViewBag.Result = res.Desc;
            }
            return View("SysConfigIndex", model);
        }

        [HttpGet]
        public ActionResult RefreshParamIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RefreshParamIndex(string id)
        {
            AppConfig.InitDic();
            ViewBag.Result = "Параметер шинэчлэгдлээ.";
            return View();
        }
        #endregion

        #region Customer
        [HttpGet]
        public ActionResult CustSearch()
        {
            CustSearchViewModel model = new CustSearchViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CustSearch(CustSearchViewModel model)
        {
            Result res = TerminalContext.CustSearch(model);

            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            return View(res.Data);
        }
        public ActionResult CustNew()
        {
            CustViewModel model = new CustViewModel();
            model.DisplayMode = "NewOnly";
            return View("CustIndex", model);
        }
        public ActionResult CustIndex(string id)
        {
            Result res = TerminalContext.CustGet(id);

            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            return View(res.Data);
        }
        [HttpPost]
        public ActionResult CustIndex(CustViewModel model)
        {
            //Result res = TerminalContext.CustGet(id);

            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            return View(res.Data);
        }
        public ActionResult Contract(string id)
        {
            Result res = TerminalContext.CustGet(id);

            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            return View();
            //return View(res.Data);
        }
        public ActionResult Schedule(string id)
        {
            Result res = TerminalContext.CustGet(id);

            if (!res.Succeed)
            {
                ViewBag.Result = res.Desc;
            }
            return View();
            //return View(res.Data);
        }
        #endregion
    }
}