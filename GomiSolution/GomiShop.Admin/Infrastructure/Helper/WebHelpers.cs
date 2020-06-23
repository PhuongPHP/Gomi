using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using GomiShop.Common.Configuration;
using GomiShop.Common.Extensions;

namespace Web.Admin.Infrastructure.Helper
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString IsActive(this HtmlHelper htmlHelper, string actions, string controller, string activeClass, string inActiveClass = "")
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();
            var listActions = actions.Split(',').Select(s => s.TrimEmpty()).ToList();
            var returnActive = (controller == routeController && listActions.Contains(routeAction));

            return new MvcHtmlString(returnActive ? activeClass : inActiveClass);
        }
    }

    public static class RenderHelper
    {
        public static string PartialView(Controller controller, string viewName, object model)
        {
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, new ViewDataDictionary(model), new TempDataDictionary(), sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
    }

    public static class EntitySelectList
    {
        public static List<SelectListItem> DeliverySttList(DeliveryStt[] removeObjects = null, object selected = null)
        {
            var items = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(DeliveryStt)))
            {
                var text = "";
                var value = "";
                switch ((DeliveryStt)item)
                {
                    case DeliveryStt.New:
                        text = "Chưa giao";
                        value = ((int)(DeliveryStt)item).ToString();
                        break;
                    case DeliveryStt.Delivered:
                        text = "Đã giao";
                        value = ((int)(DeliveryStt)item).ToString();
                        break;
                }
                if (!text.IsEmpty() && !value.IsEmpty())
                {
                    items.Add(new SelectListItem
                    {
                        Text = text,
                        Value = value,
                        Selected = selected != null && value == ((int)(DeliveryStt)selected).ToString()
                    });
                }
            }
            if (removeObjects != null && removeObjects.Any())
            {
                var valuesRemove = removeObjects.Select(x => ((int)x).ToString()).ToList();
                items = items.Where(x => !valuesRemove.Contains(x.Value)).ToList();
            }
            return items;
        }

        public static List<SelectListItem> TransactionSttList(TransactionStt[] removeObjects = null, object selected = null)
        {
            var items = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(TransactionStt)))
            {
                var text = "";
                var value = "";
                switch ((TransactionStt)item)
                {
                    case TransactionStt.New:
                        text = "Chưa thanh toán";
                        value = ((int)(DeliveryStt)item).ToString();
                        break;
                    case TransactionStt.Paid:
                        text = "Đã thanh toán";
                        value = ((int)(DeliveryStt)item).ToString();
                        break;
                }
                if (!text.IsEmpty() && !value.IsEmpty())
                {
                    items.Add(new SelectListItem
                    {
                        Text = text,
                        Value = value,
                        Selected = selected != null && value == ((int)(TransactionStt)selected).ToString()
                    });
                }
            }
            if (removeObjects != null && removeObjects.Any())
            {
                var valuesRemove = removeObjects.Select(x => ((int)x).ToString()).ToList();
                items = items.Where(x => !valuesRemove.Contains(x.Value)).ToList();
            }
            return items;
        }

        public static List<SelectListItem> CODSttList(CODStt[] removeObjects = null, object selected = null)
        {
            var items = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(CODStt)))
            {
                var text = "";
                var value = "";
                switch ((CODStt)item)
                {
                    case CODStt.New:
                        text = "Chưa nhận";
                        value = ((int)(DeliveryStt)item).ToString();
                        break;
                    case CODStt.Received:
                        text = "Đã nhận";
                        value = ((int)(DeliveryStt)item).ToString();
                        break;
                }
                if (!text.IsEmpty() && !value.IsEmpty())
                {
                    items.Add(new SelectListItem
                    {
                        Text = text,
                        Value = value,
                        Selected = selected != null && value == ((int)(CODStt)selected).ToString()
                    });
                }
            }
            if (removeObjects != null && removeObjects.Any())
            {
                var valuesRemove = removeObjects.Select(x => ((int)x).ToString()).ToList();
                items = items.Where(x => !valuesRemove.Contains(x.Value)).ToList();
            }
            return items;
        }
    }

    public static class EnumExtensions
    {
        public static string GetNamev2(this DeliveryStt value)
        {
            var name = string.Empty;

            switch (value)
            {
                case DeliveryStt.New:
                    name = "Chưa giao";
                    break;
                case DeliveryStt.Delivered:
                    name = "Đã giao";
                    break;
            }
            return name;
        }

        public static string GetName(this DeliveryStt value)
        {
            return EntitySelectList.DeliverySttList().FirstOrDefault(x => x.Value == ((int)value).ToString())?.Text + "";
        }

        public static string GetName(this TransactionStt value)
        {
            return EntitySelectList.TransactionSttList().FirstOrDefault(x => x.Value == ((int)value).ToString())?.Text + "";
        }

        public static string GetName(this CODStt value)
        {
            return EntitySelectList.CODSttList().FirstOrDefault(x => x.Value == ((int)value).ToString())?.Text + "";
        }
    }
}