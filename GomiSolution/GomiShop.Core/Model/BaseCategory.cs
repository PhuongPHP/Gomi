using GomiShop.Common.Configuration;
using System;

namespace GomiShop.Core.Model
{
    public class BaseCategory
    {
        public int Id { get; set; }
        public string Name_vi { get; set; }
        public string Name_en { get; set; }
        public byte Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Status Status { get; set; }

        public int TotalRows { get; set; }
    }

    public class MegaCategory : BaseCategory
    {
        public string Avatar { get; set; }
        public string Cover { get; set; }
        public CategoryType Type { get; set; }
    }

    public class Category : BaseCategory
    {
        public int MegaId { get; set; }
    }

    public class SubCategory : BaseCategory
    {
        public int CategoryId { get; set; }
    }
}
