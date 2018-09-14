using System.Web;
using System.Web.Optimization;

namespace PowerNew
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //-----打包和压缩js和css文件-------//
            //公用css包
            bundles.Add(new ScriptBundle("~/css/common").Include(
                         "~/Content/bootstrap.css",
                         "~/Content/style.css",
                         "~/Css/LayoutIndex.css"));

            //公用js包
            bundles.Add(new ScriptBundle("~/js/common").Include(
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/messages_zh.js",
                "~/Scripts/jquery.form.js"));

            //渲染包
            bundles.Add(new ScriptBundle("~/js/jsrender").Include(
                "~/Content/js/jsrender.js"));

            //JqueryUI
            bundles.Add(new ScriptBundle("~/js/jqueryui").Include(
                "~/Content/jquery-ui/jquery-ui.min.js",
                "~/Content/jquery-ui/i18n/jquery.ui.datepicker-zh-CN.min.js",
                "~/Content/jquery.fancytree/jquery.fancytree-all.js",
                "~/Content/jquery-ui/jquery.ui.widget.min.js"
                ));

            //fileinputcss
            bundles.Add(new StyleBundle("~/css/fileinput").Include(
                "~/Js/utf8-net/themes/default/css/ueditor.css",
                "~/Js/bootstrap-fileinput-master/css/fileinput.css"
                ));

            //fileinputjs
            bundles.Add(new ScriptBundle("~/js/fileinput").Include(
               "~/Js/bootstrap-fileinput-master/js/fileinput.js",
               "~/Js/bootstrap-fileinput-master/js/locales/zh.js",
               "~/Js/utf8-net/ueditor.config.js",
               "~/Js/utf8-net/ueditor.all.min.js",
               "~/Js/utf8-net/lang/zh-cn/zh-cn.js"
                ));
        }
    }
}
