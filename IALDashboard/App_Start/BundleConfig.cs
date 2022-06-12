using System.Web.Optimization;

namespace IALDashboard
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/js/adminlte.min.js",
                      "~/Scripts/js/demo.js"));



            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/Datatable/datatables/jquery.dataTables.min.js",
                        "~/Scripts/Datatable/datatables-bs4/js/dataTables.bootstrap4.min.js",
                        "~/Scripts/Datatable/datatables-responsive/js/dataTables.responsive.min.js",
                        "~/Scripts/Datatable/datatables-responsive/js/responsive.bootstrap4.min.js",
                        "~/Scripts/Datatable/datatables-buttons/js/dataTables.buttons.min.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.bootstrap4.min.js",
                        "~/Scripts/Datatable/jszip/jszip.min.js",
                        "~/Scripts/Datatable/pdfmake/pdfmake.min.js",
                        "~/Scripts/Datatable/pdfmake/vfs_fonts.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.html5.min.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.print.min.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.colVis.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Content/dist/css/adminlte.min.css"));

            bundles.Add(new StyleBundle("~/Content/dtcss").Include(
                    "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                    "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                    "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/Content/stickytable").Include(
                    "~/Content/plugins/stickytable/jquery.stickytable.min"));


            bundles.Add(new ScriptBundle("~/bundles/stickytable").Include(
                      "~/Scripts/stickytable/jquery.stickytable.js"));

        }
    }
}
