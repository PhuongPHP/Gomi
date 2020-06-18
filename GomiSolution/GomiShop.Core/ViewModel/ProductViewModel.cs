using GomiShop.Common.Configuration;
using GomiShop.Common.Extensions;
using GomiShop.Common.Helper;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.ViewModel
{
    public partial class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public decimal AvgRating { get; set; }
        public int Comments { get; set; }
        public string StarPercent { get; set; }
        public double TotalPage { get; set; }
        public int TotalRows { get; set; }
    }

    public partial class ProductViewModel
    {
        public ProductViewModel()
        {
            Details = new List<ProductDetailsViewModel>();
            Media = new List<ProductMediaViewModel>();
            Attributes = new List<ProductAttributeViewModel>();
        }

        public string BrandName { get; set; }
        public string BrandCountry { get; set; }

        public string MegaCateName { get; set; }

        public int CountVariants { get; set; }

        public string Thumbnail { get; set; }

        public bool IsProductBasic { get; set; }

        public bool IsTwoOptions { get; set; }

        public List<ProductDetailsViewModel> Details { get; set; }

        public List<ProductMediaViewModel> Media { get; set; }

        public List<ProductAttributeViewModel> Attributes { get; set; }

    }

    public partial class ProductDetailsViewModel
    {
        public ProductDetailsViewModel()
        {
            Option1 = new OptionViewModel();
            Option2 = new OptionViewModel();

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string SalePrice { get; set; }
        public string MarketPrice { get; set; }
        public string DiscountCurrency { get; set; }
        public int DiscountPercent { get; set; }
        public string SKU { get; set; }
        public string BarCode { get; set; }

        public OptionViewModel Option1 { get; set; }// giá trị cột ProductVariantsId bảng ProductDetails

        public OptionViewModel Option2 { get; set; }// giá trị cột ValueId bảng ProductDetails

        public bool Selected { get; set; }// Sản phẩm được chọn và hiện thị(màu, size)

        public Guid ProductVariantsId { get; set; } // Cấp 1 của sản phẩm
    }

    public class OptionViewModel
    {
        public string Group_Name_vi { get; set; }
        public string Group_Name_en { get; set; }
        public int Id { get; set; }
        public string Name_vi { get; set; }
        public string Name_en { get; set; }

    }

    public partial class ProductMediaViewModel
    {
        public int Id { get; set; } //VariantValueId
        public string MediaPath { get; set; }
        public int VariantValueId { get; set; }
    }


    public class ProductFavoriteViewModel
    {
        public Guid Id { get; set; }
        public Guid PrdId { get; set; }
        public string WebAddress { get; set; }
        public string Name { get; set; }
        public decimal AvgRating { get; set; }
        public string SalePrice { get; set; }
        public string MarketPrice { get; set; }
        public int DiscountPercent { get; set; }
        public string Thumbnail { get; set; }
    }


    public static partial class MapperHelper
    {
        public static void FromModel(this ProductViewModel obj, Product model)
        {
            obj.Id = model.Id;
            obj.Name = model.Name;
            obj.Summary = model.Summary;
            obj.Description = model.Description.Replace("http://localhost:1111/", AppSettings.ImageHosting);
            obj.BrandId = model.BrandId;
            obj.BrandName = model.BrandName;
            obj.Comments = model.Comments;
            obj.AvgRating = model.AvgRating;
            obj.StarPercent = model.StarPercent;
            obj.Thumbnail = model.Thumbnail.ToFilePath(AppSettings.ProductPath);
            obj.CountVariants = model.CountVariants;
            obj.MegaCateName = model.MegaCategoryName_vi;

        }

        public static void FromModel(this ProductDetailsViewModel obj, ProductDetails model)
        {
            obj.Id = model.Id;
            obj.ProductVariantsId = model.ProductVariantsId;
            obj.Title = model.Title;
            obj.SKU = model.SKU;
            obj.BarCode = model.BarCode;
            obj.SalePrice = StringHelper.FormatCurrency(model.SalePrice, AppSettings.CurrencyFormat);
            obj.MarketPrice = StringHelper.FormatCurrency(model.MarketPrice, AppSettings.CurrencyFormat);
            obj.DiscountCurrency = StringHelper.FormatCurrency(model.MarketPrice - model.SalePrice, AppSettings.CurrencyFormat);
            obj.DiscountPercent = Calculator.DiscountPercent(model.SalePrice, model.MarketPrice);

        }

        public static void FromModel(this ProductMediaViewModel obj, ProductMedia model)
        {
            obj.Id = model.VariantValueId;
            obj.MediaPath = model.MediaPath;
        }

        public static void FromModel(this ProductFavoriteViewModel obj, Product model)
        {
            obj.Id = model.FavoriteId;
            obj.PrdId = model.Id;
            obj.WebAddress = model.WebAddress;
            obj.Name = model.Name;
            obj.AvgRating = model.AvgRating;
            obj.Thumbnail = model.MediaPath.ToFilePath(AppSettings.ProductPath);
            obj.SalePrice = StringHelper.FormatCurrency(model.SalePrice, AppSettings.CurrencyFormat);
            obj.MarketPrice = StringHelper.FormatCurrency(model.MarketPrice, AppSettings.CurrencyFormat);
            obj.DiscountPercent = Calculator.DiscountPercent(model.SalePrice, model.MarketPrice);
        }
    }
}
