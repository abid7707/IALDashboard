using System.Web.Optimization;

namespace IALDashboard
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/plugins/moment/moment.min.js",
                      "~/Content/plugins/inputmask/jquery.inputmask.min.js"));

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



            /*bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/Datatable/datatables/jquery.dataTables.min.js",
                        "~/Scripts/Datatable/datatables-bs4/js/dataTables.bootstrap4.min.js",
                        "~/Scripts/Datatable/datatables-fixedheader/js/dataTables.fixedHeader.min.js",
                        "~/Scripts/Datatable/pdfmake/vfs_fonts.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.html5.min.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.print.min.js",
                        "~/Scripts/Datatable/datatables-buttons/js/buttons.colVis.min.js")); */
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                       "~/Content/plugins/select2/js/select2.min.js",
                       "~/Scripts/Datatable/datatables/jquery.dataTables.min.js",
                       "~/Scripts/Datatable/datatables-fixedcolumns/js/dataTables.fixedColumns.js",
                        "~/Scripts/Datatable/datatables-fixedheader/js/dataTables.fixedHeader.min.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Content/plugins/daterangepicker/daterangepicker.css",
                      "~/Content/plugins/select2/css/select2.min.css",
                      "~/Content/dist/css/adminlte.min.css"));

            bundles.Add(new StyleBundle("~/Content/dtcss").Include(
                    "~/Content/datatables/jquery.dataTables.min.css",
                    "~/Content/datatables/fixedcolumns.css",
                    "~/Content/datatables/fixedheaders.css"));

            bundles.Add(new StyleBundle("~/Content/stickytable").Include(
                    "~/Content/plugins/stickytable/jquery.stickytable.min"));


            bundles.Add(new ScriptBundle("~/bundles/stickytable").Include(
                      "~/Scripts/stickytable/jquery.stickytable.js"));

        }
    }
}
