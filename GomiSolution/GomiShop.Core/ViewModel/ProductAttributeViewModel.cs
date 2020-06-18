using GomiShop.Common.Configuration;
using GomiShop.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GomiShop.Core.ViewModel
{
    public partial class ProductAttributeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public partial class ProductAttributeViewModel
    {
        public int AttrId { get; set; }
        public string Name_vi { get; set; }
        public string Name_en { get; set; }
        public string Content_vi { get; set; }
        public string Content_en { get; set; }
    }

    public static class ProductAttributeViewModelExtension
    {
        public static void FromProductAttribute(this ProductAttributeViewModel model, ProductAttribute obj, LanguageType language)
        {
            string name = string.Empty;
            string content = string.Empty;

            switch (language)
            {
                case LanguageType.Vietnamese:
                    name = obj.Name_vi;
                    content = obj.Content_vi;
                    break;

                case LanguageType.English:
                    name = obj.Name_en;
                    content = obj.Content_en;
                    break;
            }

            model.Id = obj.Id;
            model.Name = string.IsNullOrEmpty(name) ? obj.Name_vi : name;
            model.Content = string.IsNullOrEmpty(content) ? obj.Content_vi : content;
            model.AttrId = obj.AttributeId;
            model.Name_vi = obj.Name_vi ?? "";
            model.Name_en = obj.Name_en ?? "";
            model.Content_vi = obj.Content_vi ?? "";
            model.Content_en = obj.Content_en ?? "";

        }

        public static void FromProductAttribute(this ProductAttributeViewModel model, ProductAttribute obj)
        {

            model.Id = obj.Id;
            model.AttrId = obj.AttributeId;
            model.Name_vi = obj.Name_vi ?? "";
            model.Content_vi = obj.Content_vi ?? "";

        }
    }
}
