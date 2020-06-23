using GomiShop.Common.Configuration;
using GomiShop.Common.Extensions;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.ViewModel
{
    public class BaseCategoryViewModel
    {
        public int Id { get; set; }
        public string Name_vi { get; set; }
        public string Name_en { get; set; }
        public byte Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Status Status { get; set; }

        public bool Selected { get; set; }

        public double TotalPage { get; set; }
        public int TotalRows { get; set; }
    }

    public class MegaCategoryViewModel : BaseCategoryViewModel
    {
        public MegaCategoryViewModel()
        {
            Categories = new List<CategoryViewModel>();
        }

        public string Avatar { get; set; }
        public string Cover { get; set; }
        public CategoryType Type { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }

    public class CategoryViewModel : BaseCategoryViewModel
    {
        public CategoryViewModel()
        {
            SubCategories = new List<SubCategoryViewModel>();
        }
        public int MegaId { get; set; }
        public string MegaName_vi { get; set; }
        public string MegaName_en { get; set; }

        public List<SubCategoryViewModel> SubCategories { get; set; }
    }

    public class SubCategoryViewModel : BaseCategoryViewModel
    {
        public int CategoryId { get; set; }
    }


    public class AddBaseCategoryViewModel
    {
        public AddBaseCategoryViewModel()
        {
            IsEnabled = true;
        }
        public int Id { get; set; }

        [Display(Name = "Tên(tiếng Việt)")]
        [Required(ErrorMessage = " ")]
        [StringLength(100, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string Name_vi { get; set; }

        [Display(Name = "Tên(tiếng Anh)")]
        [StringLength(100, ErrorMessage = "{0} tối đa {1} ký tự.")]
        public string Name_en { get; set; }

        [Display(Name = "Hiện thị")]
        public bool IsEnabled { get; set; }

    }

    public class UpdateMegaCategoryViewModel : AddBaseCategoryViewModel
    {
        [Display(Name = "Icon")]
        public string Avatar { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Cover { get; set; }

        public string AvatarPath { get; set; }

        public string CoverPath { get; set; }
    }

    public class UpdateCategoryViewModel : AddBaseCategoryViewModel
    {
        [Required(ErrorMessage = " ")]
        public int MegaId { get; set; }
        public string MegaName_vi { get; set; }
        public string MegaName_en { get; set; }
    }

    public class UpdateSubCategoryViewModel : AddBaseCategoryViewModel
    {
        [Required(ErrorMessage = " ")]
        public int CateId { get; set; }

        public string CateName_vi { get; set; }
        public string CateName_en { get; set; }

    }


    public static partial class MapperHelper
    {

        public static void BaseFromModel(this BaseCategoryViewModel obj, MegaCategory model)
        {
            obj.Id = model.Id;
            obj.Name_vi = model.Name_vi;
            obj.Name_en = model.Name_en;
            obj.Position = model.Position;
            obj.CreatedDate = model.CreatedDate;
            obj.CreatedBy = model.CreatedBy;
            obj.Status = model.Status;
        }

        public static void FromModel(this MegaCategoryViewModel obj, MegaCategory model)
        {
            obj.BaseFromModel(model);// BaseCategoryViewModel

            obj.Avatar = model.Avatar;
            obj.Cover = model.Cover;
            obj.Type = model.Type;
        }

        public static void FromViewModel(this MegaCategory obj, UpdateMegaCategoryViewModel model)
        {
            obj.Name_vi = model.Name_vi.TrimEmpty();
            obj.Name_en = model.Name_en.TrimEmpty();
            obj.Status = model.IsEnabled ? Status.Activated : Status.Disabled;
        }

        public static void FromModel(this UpdateMegaCategoryViewModel obj, MegaCategory model)
        {
            obj.Id = model.Id;
            obj.Name_vi = model.Name_vi;
            obj.Name_en = model.Name_en;
            obj.Avatar = model.Avatar;
            obj.Cover = model.Cover;
            obj.IsEnabled = model.Status == Status.Activated;
        }

        //========= Category 
        public static void FromModel(this UpdateCategoryViewModel obj, Category model)
        {
            obj.Id = model.Id;
            obj.Name_vi = model.Name_vi;
            obj.Name_en = model.Name_en;
            obj.IsEnabled = model.Status == Status.Activated;
        }

        public static void FromViewModel(this Category obj, UpdateCategoryViewModel model)
        {
            obj.MegaId = model.MegaId;
            obj.Name_vi = model.Name_vi.TrimEmpty();
            obj.Name_en = model.Name_en.TrimEmpty();
            obj.Status = model.IsEnabled ? Status.Activated : Status.Disabled;
        }

        public static void FromModel(this CategoryViewModel obj, Category model)
        {
            obj.Id = model.Id;
            obj.MegaId = model.MegaId;
            obj.Name_vi = model.Name_vi.TrimEmpty();
            obj.Name_en = model.Name_en.TrimEmpty();
            obj.Position = model.Position;
            obj.CreatedDate = model.CreatedDate;
            obj.CreatedBy = model.CreatedBy;
            obj.Status = model.Status;
        }

        //========= SubCategory 
        public static void FromModel(this UpdateSubCategoryViewModel obj, SubCategory model)
        {
            obj.Id = model.Id;
            obj.Name_vi = model.Name_vi;
            obj.Name_en = model.Name_en;
            obj.IsEnabled = model.Status == Status.Activated;
        }

        public static void FromViewModel(this SubCategory obj, UpdateSubCategoryViewModel model)
        {
            obj.CategoryId = model.CateId;
            obj.Name_vi = model.Name_vi.TrimEmpty();
            obj.Name_en = model.Name_en.TrimEmpty();
            obj.Status = model.IsEnabled ? Status.Activated : Status.Disabled;
        }

        public static void FromModel(this SubCategoryViewModel obj, SubCategory model)
        {
            obj.Id = model.Id;
            obj.CategoryId = model.CategoryId;
            obj.Name_vi = model.Name_vi.TrimEmpty();
            obj.Name_en = model.Name_en.TrimEmpty();
            obj.Position = model.Position;
            obj.CreatedDate = model.CreatedDate;
            obj.CreatedBy = model.CreatedBy;
            obj.Status = model.Status;

        }
    }
}
