namespace EnterpriseTemplate
{
    using EnterpriseTemplate.Instrumentation.Logging;
    using System;
    using System.Diagnostics.Tracing;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        private bool inProcessLogging;

        private EventListener dataListener;

        private EventListener fileListener;

        private EventListener rollingfileListener;

        public override void Dispose()
        {
            if (this.inProcessLogging)
            {
                this.fileListener.DisableEvents(EnterpriseTemplateEvents.Log);
                this.fileListener.Dispose();
                this.rollingfileListener.DisableEvents(EnterpriseTemplateEvents.Log);
                this.rollingfileListener.Dispose();
                this.dataListener.DisableEvents(EnterpriseTemplateEvents.Log);
                this.dataListener.Dispose();
            }

            base.Dispose();
        }

        protected void Application_Start()
        {
            this.SetupEventTracing();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // Global error log 
            var ex = this.Server.GetLastError();
            EnterpriseTemplateEvents.Log.ApplicationError(ex.Message, ex.GetType().FullName);

            // IUnityContainer container = Application.GetContainer();
            // var exceptionManager = container.Resolve<ExceptionManager>();
            // if (exceptionManager != null)
            // {
            // exceptionManager.HandleException(ex, Constants.GlobalPolicy);
            // }
        }

        private void SetupEventTracing()
        {
            bool.TryParse(WebConfigurationManager.AppSettings["InProcessLogging"], out this.inProcessLogging);

            if (this.inProcessLogging)
            {
                // Log to file all DataAccess events
                ////this.fileListener = FlatFileLog.CreateListener(
                ////    "EnterpriseTemplate.DataAccess.log",
                ////    new XmlEventTextFormatter(EventTextFormatting.Indented),
                ////    true);
                ////this.fileListener.EnableEvents(
                ////    EnterpriseTemplateEvents.Log,
                ////    EventLevel.LogAlways,
                ////    EnterpriseTemplateEvents.Keywords.DataAccess);

                ////// Log to Rolling file informational UI events only
                ////this.rollingfileListener = RollingFlatFileLog.CreateListener(
                ////    "EnterpriseTemplate.UserInterface.log",
                ////    10,
                ////    "yyyy",
                ////    RollFileExistsBehavior.Increment,
                ////    RollInterval.Day,
                ////    new JsonEventTextFormatter(EventTextFormatting.Indented),
                ////    isAsync: true);
                ////this.rollingfileListener.EnableEvents(
                ////    EnterpriseTemplateEvents.Log,
                ////    EventLevel.Informational,
                ////    EnterpriseTemplateEvents.Keywords.UserInterface);

                ////// Log all events to DB 
                ////this.dataListener = SqlDatabaseLog.CreateListener(
                ////    "EnterpriseTemplate",
                ////    WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                ////    bufferingInterval: TimeSpan.FromSeconds(3),
                ////    bufferingCount: 10);
                ////this.dataListener.EnableEvents(EnterpriseTemplateEvents.Log, EventLevel.LogAlways, Keywords.All);
            }
        }
    }
}