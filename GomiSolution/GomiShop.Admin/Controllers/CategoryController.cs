using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Gomi.Common.Helper;
using GomiShop.Admin.Infrastructure.Filters;
using GomiShop.Common.Configuration;
using GomiShop.Common.Extensions;
using GomiShop.Common.Helper;
using GomiShop.Core.Interface.Service;
using GomiShop.Core.Model;
using GomiShop.Core.ViewModel;
using Microsoft.AspNet.Identity;
using Web.Admin.Controllers;
using Web.Admin.Infrastructure.Helper;

namespace GomiShop.Admin.Controllers
{
    public class CategoryController : BasicController
    {
        private readonly IMegaCategoryService _megaCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Thêm Mega Category
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns></returns>
        private async Task<JsonResult> AddMegaCate(UpdateMegaCategoryViewModel modelVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    modelVM.AvatarPath = "/" + TempPath;
                    modelVM.CoverPath = "/" + TempPath;
                    return Json(new { status = false, error = true, message = StrErrorCode });
                }

                var newMegaCate = new MegaCategory();
                newMegaCate.FromViewModel(modelVM);
                newMegaCate.Avatar = "";
                newMegaCate.Cover = "";

                //if upload has image Cover
                if (!modelVM.Cover.IsEmpty())
                {
                    newMegaCate.Cover = FileHelpers.MoveFile("", modelVM.Cover, HttpContext.Server.MapPath("~/" + TempPath), AppSettings.CategoryBannerPath, "banner_" + modelVM.Name_vi.RemoveUnicodeUrl());
                }

                //if upload has image Avatar
                if (!modelVM.Avatar.IsEmpty())
                {
                    newMegaCate.Avatar = FileHelpers.MoveFile("", modelVM.Avatar, HttpContext.Server.MapPath("~/" + TempPath), AppSettings.CategoryIconPath, "ic_" + modelVM.Name_vi.RemoveUnicodeUrl());
                }

                //Insert new Mega Category
                var insert = await _megaCategoryService.Insert(User.Identity.GetUserId().ToGuid(), newMegaCate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });

                //Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrAddSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\_AddCategoryMega.cshtml", new UpdateMegaCategoryViewModel())
                });

            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at AddMegaCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
            //return null;
        }




    }
}