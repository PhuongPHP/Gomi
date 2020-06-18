using GomiShop.Common.Configuration;
using System;

namespace GomiShop.Core.Model
{
    public class BaseProduct
    {
        public int Id { get; set; }
        public string Name_vi { get; set; }
        public string Name_en { get; set; }
        public bool IsDefault { get; set; }
        public Status Status { get; set; }
    }
    public class Variant : BaseProduct
    {
        public int? VariantId { get; set; }
    }
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductStatus Status { get; set; }
        public int TotalRows { get; set; }
    }
    public partial class Product
    {
        public string MediaPath { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MarketPrice { get; set; }
        public int DiscountPercent { get; set; }
        public string BrandName { get; set; }
        public int DetailId { get; set; }
        public int Comments { get; set; }
        public decimal AvgRating { get; set; }
        public string StarPercent { get; set; }
        public string WebAddress { get; set; }
        public Guid FavoriteId { get; set; }
    }

    public partial class Product
    {
        public string Thumbnail { get; set; }
        public int CountVariants { get; set; }
        public string MegaCategoryName_vi { get; set; }

    }

    public class ProductDetails
    {
        public int Id { get; set; }
        public Guid ProductVariantsId { get; set; } // Cấp 1 của sản phẩm
        public int ValueId { get; set; } // Cấp 2 của sản phẩm
        public string Title { get; set; }
        public string SKU { get; set; }
        public string BarCode { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MarketPrice { get; set; }
        public int SellerPercent { get; set; }
        public Status Status { get; set; }
        public int PriceListId { get; set; }
        public int OptionGroup1_Id { get; set; }
        public string OptionGroup1_Name_vi { get; set; }
        public string OptionGroup1_Name_en { get; set; }
        public int Option1_Id { get; set; }
        public string Option1_Name_vi { get; set; }
        public string Option1_Name_en { get; set; }
        public int OptionGroup2_Id { get; set; }
        public string OptionGroup2_Name_vi { get; set; }
        public string OptionGroup2_Name_en { get; set; }
        public int Option2_Id { get; set; }
        public string Option2_Name_vi { get; set; }
        public string Option2_Name_en { get; set; }
        public bool OptionDefault { get; set; }
        public int Index { get; set; }
    }

    public class ProductCategory
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

    }

    public class ProductCollection
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public int CollectionId { get; set; }
    }

    public class ProductAttribute
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public int AttributeId { get; set; }
        public string Name_vi { get; set; }
        public string Name_en { get; set; }
        public string Content_vi { get; set; }
        public string Content_en { get; set; }

    }

    public class PriceList
    {
        public int Id { get; set; }
        public int ProductDetailId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MarketPrice { get; set; }

        public int SellerPercent { get; set; }/*Số phần trăm cho seller*/
        public Guid CreatedBy { get; set; }
        public Status Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ProductMedia
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public string MediaPath { get; set; }
        public string Thumbnail { get; set; }
        public int Index { get; set; }
        public ProductMediaType Type { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int VariantValueId { get; set; }
    }

    public class ProductFilter
    {
        public string Keyword { get; set; }
        public Guid ShopId { get; set; }
        public int MegaCateId { get; set; }
        public int BrandId { get; set; }
        public MediaType MediaType { get; set; }
        public ProductStatus Status { get; set; }
        public int BeginRow { get; set; }
        public int NumRows { get; set; }

    }
}
