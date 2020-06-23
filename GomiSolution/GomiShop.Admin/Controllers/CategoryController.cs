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
    [CustomAuthorize(new[] { AccountRole.Admin })]
    public class CategoryController : BasicController
    {
        private readonly IMegaCategoryService _megaCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public CategoryController(IMegaCategoryService megaCategoryService, ICategoryService categoryService, ISubCategoryService subCategoryService)
        {
            _megaCategoryService = megaCategoryService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }


        public ActionResult Index()
        {
            return View();
        }

        #region--===== Mega Category
        /// <summary>
        /// Lấy Html Form thêm, sửa Mega Category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/mega/get-form/{id}", Name = "GetFormMegaCate")]
        public async Task<JsonResult> Ajax_GetFrom_Mega(int id)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var modelVM = new UpdateMegaCategoryViewModel();

                // Nếu request là update
                if (id != 0)
                {
                    var megaCate = await _megaCategoryService.FindById(id);
                    if (megaCate == null)
                        return Json(new { status = false, message = StrErrorCode });

                    modelVM.FromModel(megaCate);
                    modelVM.AvatarPath = AppSettings.ImageHosting + AppSettings.CategoryIconPath;
                    modelVM.CoverPath = AppSettings.ImageHosting + AppSettings.CategoryIconPath;
                }

                return Json(new
                {
                    status = true,
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\MegaCate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_GetFrom_Mega() Method", ex.Message);
                return Json(new { status = false, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Submit Form Mega Category(Add, Update)
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns>Json()</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/mega/submit-form", Name = "SubmitFormMegaCate")]
        public async Task<JsonResult> Ajax_SubmitForm_MegaCate(UpdateMegaCategoryViewModel modelVM)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                if (modelVM.Id != 0)
                    return await UpdateMegaCate(modelVM);
                else
                    return await AddMegaCate(modelVM);
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_SubmitForm_MegaCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Lấy danh sách Mega Categories
        /// </summary>
        /// <param name="pageNo"></param>
        /// <returns>Json</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/get-mega-cate")]
        public async Task<JsonResult> Ajax_GetMegaCate(int pageNo, int id)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var modelVM = new List<MegaCategoryViewModel>();

                if (id == 0) // Get List
                {
                    modelVM = await GetListMegaCateByFilter(string.Empty, pageNo);
                }
                else
                {
                    modelVM.Add(await GetMegaCateById(id));
                }

                return Json(new
                {
                    status = true,
                    totalPage = modelVM.Any() ? modelVM.ElementAt(0).TotalPage : 0,
                    totalRows = modelVM.Any() ? modelVM.ElementAt(0).TotalRows : 0,
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\MegaCate\_List.cshtml", modelVM)
                });

            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_GetMegaCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Cập nhật Status Mega Category
        /// </summary>
        /// <param name="id">Mega Category</param>
        /// <param name="status"></param>
        /// <returns>Json</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/update-status-mega-cate/{id}/{status}", Name = "UpdateStatusMegaCate")]
        public async Task<JsonResult> Ajax_UpdateStatus_MegaCate(int id, Status status)
        {
            try
            {
                // get Mega Cate update
                var megaCate = await _megaCategoryService.FindById(id);
                if (megaCate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                megaCate.Status = status; // update Status

                // update into Database
                var update = await _megaCategoryService.Update(User.Identity.GetUserId().ToGuid(), megaCate);
                if (update == -1)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrUpdateSuccess, megaCate.Name_vi)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_UpdateStatus_MegaCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Search trong Select của Mega Category
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/mega/select-search")]
        public async Task<JsonResult> Ajax_MegaSelectSearch(string keyword, int pageNo)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, items = new List<MegaCategoryViewModel>(), error = true, message = StrErrorCode });

                var modelVM = await GetListMegaCateByFilter(keyword, pageNo);

                return Json(new
                {
                    status = true,
                    itemPerPage = AppSettings.ItemPerPage,
                    totalPage = modelVM.Any() ? modelVM.ElementAt(0).TotalPage : 0,
                    totalRows = modelVM.Any() ? modelVM.ElementAt(0).TotalRows : 0,
                    items = modelVM
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_MegaSelectSearch() Method", ex.Message);
                return Json(new { status = false, items = new List<MegaCategoryViewModel>(), error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Lấy danh sách Mega Category
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="pageNo">Số phân trang</param>
        /// <returns>List<MegaCategoryViewModel>()</returns>
        private async Task<List<MegaCategoryViewModel>> GetListMegaCateByFilter(string keyword, int pageNo)
        {
            var modelVM = new List<MegaCategoryViewModel>();

            try
            {
                var beginRow = Calculator.BeginRow(pageNo);
                var numRows = Calculator.NumRows(pageNo);

                var result = await _megaCategoryService.FindBy(keyword ?? "", Status.Undefined, beginRow, numRows);
                foreach (var item in result)
                {
                    var itemVM = new MegaCategoryViewModel();
                    itemVM.FromModel(item);
                    itemVM.Avatar = itemVM.Avatar.ToFilePath(AppSettings.CategoryIconPath);
                    itemVM.Cover = itemVM.Cover.ToFilePath(AppSettings.CategoryBannerPath);
                    itemVM.TotalRows = item.TotalRows;
                    itemVM.TotalPage = Calculator.TotalPage(item.TotalRows);
                    modelVM.Add(itemVM);
                }
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at GetListMegaCateByFilter() Method", ex.Message);
            }

            return modelVM;
        }


        /// <summary>
        /// Lấy Mega Category theo Id
        /// </summary>
        /// <param name="id">Id Mega Category</param>
        /// <returns>MegaCategoryViewModel</returns>
        private async Task<MegaCategoryViewModel> GetMegaCateById(int id)
        {
            var modelVM = new MegaCategoryViewModel();

            try
            {
                var result = await _megaCategoryService.FindById(id);
                modelVM.FromModel(result);
                modelVM.Avatar = modelVM.Avatar.ToFilePath(AppSettings.CategoryIconPath);
                modelVM.Cover = modelVM.Cover.ToFilePath(AppSettings.CategoryBannerPath);
                modelVM.TotalRows = 1;
                modelVM.TotalPage = 1;
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at GetMegaCateById() Method", ex.Message);
            }

            return modelVM;
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

                // if upload has image Cover
                if (!modelVM.Cover.IsEmpty())
                {
                    newMegaCate.Cover = FileHelpers.MoveFile("", modelVM.Cover, HttpContext.Server.MapPath("~/" + TempPath), AppSettings.CategoryBannerPath, "banner_" + modelVM.Name_vi.RemoveUnicodeUrl());
                }

                // if upload has image Avatar
                if (!modelVM.Avatar.IsEmpty())
                {
                    newMegaCate.Avatar = FileHelpers.MoveFile("", modelVM.Avatar, HttpContext.Server.MapPath("~/" + TempPath), AppSettings.CategoryIconPath, "ic_" + modelVM.Name_vi.RemoveUnicodeUrl());
                }

                // Insert new Mega Category
                var insert = await _megaCategoryService.Insert(User.Identity.GetUserId().ToGuid(), newMegaCate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrAddSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\MegaCate\_Form.cshtml", new UpdateMegaCategoryViewModel())
                });

            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at AddMegaCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Cập nhật Mega Category
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns></returns>
        private async Task<JsonResult> UpdateMegaCate(UpdateMegaCategoryViewModel modelVM)
        {
            try
            {
                var megaCate = await _megaCategoryService.FindById(modelVM.Id);
                if (megaCate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                if (!ModelState.IsValid)
                {
                    // Nếu đã update ảnh thì path Temp
                    modelVM.CoverPath = modelVM.Cover != megaCate.Cover ? "/" + TempPath : AppSettings.ImageHosting + AppSettings.CategoryIconPath;
                    modelVM.AvatarPath = modelVM.Avatar != megaCate.Avatar ? "/" + TempPath : AppSettings.ImageHosting + AppSettings.CategoryIconPath;

                    return Json(new { status = false, view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\MegaCate\_Form.cshtml", modelVM) });
                }

                // Mapping View Model To Model
                megaCate.FromViewModel(modelVM);

                // if upload has image Cover
                if (!modelVM.Cover.IsEmpty() && modelVM.Cover != megaCate.Cover)
                {
                    megaCate.Cover = FileHelpers.MoveFile("", modelVM.Cover, HttpContext.Server.MapPath("~/" + TempPath), AppSettings.CategoryBannerPath, "banner_" + modelVM.Name_vi.RemoveUnicodeUrl());
                }

                // if upload has image Avatar
                if (!modelVM.Avatar.IsEmpty() && modelVM.Avatar != megaCate.Avatar)
                {
                    megaCate.Avatar = FileHelpers.MoveFile("", modelVM.Avatar, HttpContext.Server.MapPath("~/" + TempPath), AppSettings.CategoryIconPath, "ic_" + modelVM.Name_vi.RemoveUnicodeUrl());
                }

                // Update Mega Category
                var insert = await _megaCategoryService.Update(User.Identity.GetUserId().ToGuid(), megaCate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });


                // Success and update info after save database
                modelVM.Avatar = megaCate.Avatar;
                modelVM.Cover = megaCate.Cover;
                modelVM.AvatarPath = AppSettings.ImageHosting + AppSettings.CategoryIconPath;
                modelVM.CoverPath = AppSettings.ImageHosting + AppSettings.CategoryBannerPath;
                return Json(new
                {
                    status = true,
                    message = string.Format(StrUpdateSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\MegaCate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at UpdateMegaCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }

        #endregion



        #region --====== Category
        /// <summary>
        /// Lấy Html Form thêm, sửa Category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/cate/get-form/{id}", Name = "GetFormCate")]
        public async Task<JsonResult> Ajax_GetForm_Cate(int id, int megaId)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });


                // Lấy thông tin danh mục cha
                var mega = await _megaCategoryService.FindById(megaId);
                if (mega == null)
                    return Json(new { status = false, message = StrErrorCode });

                var modelVM = new UpdateCategoryViewModel();
                modelVM.MegaName_vi = mega.Name_vi;
                modelVM.MegaName_en = mega.Name_en.IsEmpty() ? mega.Name_vi : mega.Name_en;
                modelVM.MegaId = megaId;

                // Nếu request là update thì 
                if (id != 0)
                {
                    // Lấy thông tin category muốn update
                    var cate = await _categoryService.FindById(id);
                    if (cate == null)
                        return Json(new { status = false, message = StrErrorCode });

                    modelVM.FromModel(cate);
                }

                return Json(new
                {
                    status = true,
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\Cate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_GetForm_Cate() Method", ex.Message);
                return Json(new { status = false, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Submit Form Category(Add, Update)
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns>Json()</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/cate/submit-form", Name = "SubmitFormCate")]
        public async Task<JsonResult> Ajax_SubmitForm_Cate(UpdateCategoryViewModel modelVM)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Check if Add or Update
                if (modelVM.Id != 0)
                    return await UpdateCate(modelVM);
                else
                    return await AddCate(modelVM);
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_SubmitForm_Cate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Cập nhật Status Category
        /// </summary>
        /// <param name="id">Category</param>
        /// <param name="status"></param>
        /// <returns>Json</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/update-status-mega-cate/{id}/{megaId}/{status}", Name = "UpdateStatusCate")]
        public async Task<JsonResult> Ajax_UpdateStatus_Cate(int id, int megaId, Status status)
        {
            try
            {
                // get Mega Cate update
                var megaCate = await _megaCategoryService.FindById(megaId);
                if (megaCate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var cate = await _categoryService.FindById(id);
                if (cate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                cate.Status = status;

                // update into Database
                var update = await _categoryService.Update(User.Identity.GetUserId().ToGuid(), cate);
                if (update == -1)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrUpdateSuccess, cate.Name_vi)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_UpdateStatus_Cate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Lấy danh sách Categories
        /// </summary>
        /// <param name="pageNo">Số phân trang</param>
        /// <param name="id">Id danh mục được chọn</param>
        /// <param name="megaId">Id danh mục cha</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/get-cate")]
        public async Task<JsonResult> Ajax_GetCategories(int pageNo, int id, int parentId)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var modelVM = new List<CategoryViewModel>();

                // Khi id = 0 và parent = 0 tức danh mục cha đã bỏ chọn nên không có danh mục nào sẽ được lấy.
                // return Count() = 0
                if (id == 0 && parentId == 0)
                    return Json(new { status = true, totalPage = 0, totalRows = 0, view = "" });

                if (id == 0) // Get List
                {
                    modelVM = await GetListCateByFilter(string.Empty, pageNo, parentId);
                }
                else
                {
                    modelVM.Add(await GetCateById(id));
                }

                return Json(new
                {
                    status = true,
                    totalPage = modelVM.Any() ? modelVM.ElementAt(0).TotalPage : 0,
                    totalRows = modelVM.Any() ? modelVM.ElementAt(0).TotalRows : 0,
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\Cate\_List.cshtml", modelVM)
                });

            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_GetCategories() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Search trong Select của Category
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageNo">Số phân trang</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/cate/select-search")]
        public async Task<JsonResult> Ajax_CateSelectSearch(string keyword, int pageNo, int parentId)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, items = new List<CategoryViewModel>(), error = true, message = StrErrorCode });

                var modelVM = await GetListCateByFilter(keyword, pageNo, parentId);

                return Json(new
                {
                    status = true,
                    itemPerPage = AppSettings.ItemPerPage,
                    totalPage = modelVM.Any() ? modelVM.ElementAt(0).TotalPage : 0,
                    totalRows = modelVM.Any() ? modelVM.ElementAt(0).TotalRows : 0,
                    items = modelVM
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_CateSelectSearch() Method", ex.Message);
                return Json(new { status = false, items = new List<CategoryViewModel>(), error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Lấy danh sách Category
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageNo">Số phân trang</param>
        /// <param name="megaId">Danh mục cha</param>
        /// <returns>List<CategoryViewModel>()</returns>
        private async Task<List<CategoryViewModel>> GetListCateByFilter(string keyword, int pageNo, int megaId)
        {
            var modelVM = new List<CategoryViewModel>();

            try
            {
                var beginRow = Calculator.BeginRow(pageNo);
                var numRows = Calculator.NumRows(pageNo);

                var result = await _categoryService.FindBy(keyword ?? "", megaId, Status.Undefined, beginRow, numRows);
                foreach (var item in result)
                {
                    var itemVM = new CategoryViewModel();
                    itemVM.FromModel(item);
                    itemVM.TotalRows = item.TotalRows;
                    itemVM.TotalPage = Calculator.TotalPage(item.TotalRows);
                    modelVM.Add(itemVM);
                }
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at GetListCateByFilter() Method", ex.Message);
            }

            return modelVM;
        }


        /// <summary>
        /// Lấy Category theo Id
        /// </summary>
        /// <param name="id">Id Category</param>
        /// <returns>CategoryViewModel</returns>
        private async Task<CategoryViewModel> GetCateById(int id)
        {
            var modelVM = new CategoryViewModel();

            try
            {
                var result = await _categoryService.FindById(id);
                modelVM.FromModel(result);
                modelVM.TotalRows = 1;
                modelVM.TotalPage = 1;
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at GetCateById() Method", ex.Message);
            }

            return modelVM;
        }


        /// <summary>
        /// Thêm Category
        /// </summary>
        /// <param name="modelVM">UpdateCategoryViewModel</param>
        /// <returns></returns>
        private async Task<JsonResult> AddCate(UpdateCategoryViewModel modelVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { status = false, error = true, message = StrErrorCode });


                var megaCate = await _megaCategoryService.FindById(modelVM.MegaId);
                if (megaCate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var newCate = new Category();
                newCate.FromViewModel(modelVM);

                // Insert new Category
                var insert = await _categoryService.Insert(User.Identity.GetUserId().ToGuid(), newCate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // New View Model after add new item
                modelVM = new UpdateCategoryViewModel();
                modelVM.MegaId = megaCate.Id;
                modelVM.MegaName_vi = megaCate.Name_vi;
                modelVM.MegaName_en = megaCate.Name_en.IsEmpty() ? megaCate.Name_vi : megaCate.Name_en;

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrAddSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\Cate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at AddCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Cập nhật Category
        /// </summary>
        /// <param name="modelVM">UpdateCategoryViewModel</param>
        /// <returns> Json
        /// Error: View Error
        /// Success: Trả về thông báo và thông tin đã update
        /// </returns>
        private async Task<JsonResult> UpdateCate(UpdateCategoryViewModel modelVM)
        {
            try
            {
                var megaCate = await _megaCategoryService.FindById(modelVM.MegaId);
                if (megaCate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var cate = await _categoryService.FindById(modelVM.Id);
                if (cate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Add Value to View Model
                modelVM.MegaName_vi = megaCate.Name_vi;
                modelVM.MegaName_en = megaCate.Name_en.IsEmpty() ? megaCate.Name_vi : megaCate.Name_en;

                // Check Valid Model
                if (!ModelState.IsValid)
                    return Json(new { status = false, view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\Cate\_Form.cshtml", modelVM) });

                // Mapping View Model To Model
                cate.FromViewModel(modelVM);

                // Update Category
                var insert = await _categoryService.Update(User.Identity.GetUserId().ToGuid(), cate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrUpdateSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\Cate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at UpdateCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        #endregion



        #region --====== SubCategory
        /// <summary>
        /// Lấy Html Form thêm, sửa SubCategory
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/subcate/get-form/{id}", Name = "GetFormSubCate")]
        public async Task<JsonResult> Ajax_GetFormSubCate(int id, int cateId)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });


                // Lấy thông tin danh mục cha
                var cate = await _categoryService.FindById(cateId);
                if (cate == null)
                    return Json(new { status = false, message = StrErrorCode });

                var modelVM = new UpdateSubCategoryViewModel();
                modelVM.CateName_vi = cate.Name_vi;
                modelVM.CateName_en = cate.Name_en.IsEmpty() ? cate.Name_vi : cate.Name_en;
                modelVM.CateId = cateId;

                // Nếu request là update thì 
                if (id != 0)
                {
                    // Lấy thông tin category muốn update
                    var subcate = await _subCategoryService.FindById(id);
                    if (subcate == null)
                        return Json(new { status = false, message = StrErrorCode });

                    modelVM.FromModel(subcate);
                }

                return Json(new
                {
                    status = true,
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\SubCate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_GetFormSubCate() Method", ex.Message);
                return Json(new { status = false, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Submit Form SubCategory(Add, Update)
        /// </summary>
        /// <param name="modelVM"></param>
        /// <returns>Json()</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/sub-cate/submit-form", Name = "SubmitFormSubCate")]
        public async Task<JsonResult> Ajax_SubmitFormSubCate(UpdateSubCategoryViewModel modelVM)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Check if Add or Update
                if (modelVM.Id != 0)
                    return await UpdateSubCate(modelVM);
                else
                    return await AddSubCate(modelVM);
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at SubmitFormSubCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Cập nhật Status SubCategory
        /// </summary>
        /// <param name="id">Sub Category</param>
        /// <param name="status"></param>
        /// <returns>Json</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/update-status-sub-cate/{id}/{cateId}/{status}", Name = "UpdateStatus_SubCate")]
        public async Task<JsonResult> Ajax_UpdateStatus_SubCate(int id, int cateId, Status status)
        {
            try
            {
                // get Mega Cate update
                var cate = await _categoryService.FindById(cateId);
                if (cate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var subcate = await _subCategoryService.FindById(id);
                if (subcate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                subcate.Status = status;

                // update into Database
                var update = await _subCategoryService.Update(User.Identity.GetUserId().ToGuid(), subcate);
                if (update == -1)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrUpdateSuccess, subcate.Name_vi)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_UpdateStatus_SubCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Lấy danh sách Sub Categories
        /// </summary>
        /// <param name="pageNo">Số phân trang</param>
        /// <param name="id">Id danh mục được chọn</param>
        /// <param name="megaId">Id danh mục cha</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/get-sub-cate")]
        public async Task<JsonResult> Ajax_GetSubCategories(int pageNo, int id, int parentId)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var modelVM = new List<SubCategoryViewModel>();

                // Khi id = 0 và parent = 0 tức danh mục cha đã bỏ chọn nên không có danh mục nào sẽ được lấy.
                // return Count() = 0
                if (id == 0 && parentId == 0)
                    return Json(new { status = true, totalPage = 0, totalRows = 0, view = "" });

                if (id == 0) // Get List
                {
                    modelVM = await GetListSubCateByFilter(string.Empty, pageNo, parentId);
                }
                else
                {
                    modelVM.Add(await GetSubCateById(id));
                }

                return Json(new
                {
                    status = true,
                    totalPage = modelVM.Any() ? modelVM.ElementAt(0).TotalPage : 0,
                    totalRows = modelVM.Any() ? modelVM.ElementAt(0).TotalRows : 0,
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\SubCate\_List.cshtml", modelVM)
                });

            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_GetSubCategories() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Search trong Select của SubCategory
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageNo">Số phân trang</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/category/sub-cate/select-search")]
        public async Task<JsonResult> Ajax_SubCateSelectSearch(string keyword, int pageNo, int parentId)
        {
            try
            {
                if (!Request.IsAjaxRequest())
                    return Json(new { status = false, items = new List<SubCategoryViewModel>(), error = true, message = StrErrorCode });

                var modelVM = await GetListSubCateByFilter(keyword, pageNo, parentId);

                return Json(new
                {
                    status = true,
                    itemPerPage = AppSettings.ItemPerPage,
                    totalPage = modelVM.Any() ? modelVM.ElementAt(0).TotalPage : 0,
                    totalRows = modelVM.Any() ? modelVM.ElementAt(0).TotalRows : 0,
                    items = modelVM
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at Ajax_SubCateSelectSearch() Method", ex.Message);
                return Json(new { status = false, items = new List<SubCategoryViewModel>(), error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Lấy danh sách Category
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="pageNo">Số phân trang</param>
        /// <param name="megaId">Danh mục cha</param>
        /// <returns>List<SubCategoryViewModel>()</returns>
        private async Task<List<SubCategoryViewModel>> GetListSubCateByFilter(string keyword, int pageNo, int cateId)
        {
            var modelVM = new List<SubCategoryViewModel>();

            try
            {
                var beginRow = Calculator.BeginRow(pageNo);
                var numRows = Calculator.NumRows(pageNo);

                var result = await _subCategoryService.FindBy(keyword ?? "", cateId, Status.Undefined, beginRow, numRows);
                foreach (var item in result)
                {
                    var itemVM = new SubCategoryViewModel();
                    itemVM.FromModel(item);
                    itemVM.TotalRows = item.TotalRows;
                    itemVM.TotalPage = Calculator.TotalPage(item.TotalRows);
                    modelVM.Add(itemVM);
                }
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at GetListSubCateByFilter() Method", ex.Message);
            }

            return modelVM;
        }


        /// <summary>
        /// Lấy Category theo Id
        /// </summary>
        /// <param name="id">Id SubCategory</param>
        /// <returns>SubCategoryViewModel</returns>
        private async Task<SubCategoryViewModel> GetSubCateById(int id)
        {
            var modelVM = new SubCategoryViewModel();

            try
            {
                var result = await _subCategoryService.FindById(id);
                modelVM.FromModel(result);
                modelVM.TotalRows = 1;
                modelVM.TotalPage = 1;
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at GetSubCateById() Method", ex.Message);
            }

            return modelVM;
        }


        /// <summary>
        /// Thêm Category
        /// </summary>
        /// <param name="modelVM">UpdateSubCategoryViewModel</param>
        /// <returns></returns>
        private async Task<JsonResult> AddSubCate(UpdateSubCategoryViewModel modelVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { status = false, error = true, message = StrErrorCode });


                var cate = await _categoryService.FindById(modelVM.CateId);
                if (cate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var newSubCate = new SubCategory();
                newSubCate.FromViewModel(modelVM);

                // Insert new Sub Category
                var insert = await _subCategoryService.Insert(User.Identity.GetUserId().ToGuid(), newSubCate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // New View Model after add new item
                modelVM = new UpdateSubCategoryViewModel();
                modelVM.CateId = cate.Id;
                modelVM.CateName_vi = cate.Name_vi;
                modelVM.CateName_en = cate.Name_en.IsEmpty() ? cate.Name_vi : cate.Name_en;

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrAddSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\SubCate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at AddSubCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        /// <summary>
        /// Cập nhật Category
        /// </summary>
        /// <param name="modelVM">UpdateSubCategoryViewModel</param>
        /// <returns> Json
        /// Error: View Error
        /// Success: Trả về thông báo và thông tin đã update
        /// </returns>
        private async Task<JsonResult> UpdateSubCate(UpdateSubCategoryViewModel modelVM)
        {
            try
            {
                var cate = await _categoryService.FindById(modelVM.CateId);
                if (cate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                var subCate = await _subCategoryService.FindById(modelVM.Id);
                if (subCate == null)
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Add Value to View Model
                modelVM.CateName_en = cate.Name_vi;
                modelVM.CateName_vi = cate.Name_en.IsEmpty() ? cate.Name_vi : cate.Name_en;

                // Check Valid Model
                if (!ModelState.IsValid)
                    return Json(new { status = false, view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\SubCate\_Form.cshtml", modelVM) });

                // Mapping View Model To Model
                subCate.FromViewModel(modelVM);

                // Update SubCategory
                var insert = await _subCategoryService.Update(User.Identity.GetUserId().ToGuid(), subCate);
                if (insert == -1)// Error
                    return Json(new { status = false, error = true, message = StrErrorCode });

                // Success
                return Json(new
                {
                    status = true,
                    message = string.Format(StrUpdateSuccess, modelVM.Name_vi),
                    view = RenderHelper.PartialView(this, @"~\Views\Category\Partial\SubCate\_Form.cshtml", modelVM)
                });
            }
            catch (Exception ex)
            {
                _megaCategoryService.WriteError("Error in CategoryController at UpdateSubCate() Method", ex.Message);
                return Json(new { status = false, error = true, message = StrErrorCode });
            }
        }


        #endregion
    }
}