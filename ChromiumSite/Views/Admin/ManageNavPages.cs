using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ChromiumSite.Views.Admin
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string UserChanges => "UserChanges";

        public static string GalleryManage => "GalleryManage";

        public static string ManageAquaprint => "ManageAquaprint";

        public static string AquaProposition => "AquaProposition";

        public static string ChromeProposition => "ChromeProposition";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string UserChangesNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserChanges);

        public static string GalleryManageNavClass(ViewContext viewContext) => PageNavClass(viewContext, GalleryManage);

        public static string ManageAquaprintNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageAquaprint);

        public static string AquaPropositionNavClass(ViewContext viewContext) => PageNavClass(viewContext, AquaProposition);

        public static string ChromePropositionNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChromeProposition);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
