using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;

namespace JBoyerLibaray
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) { return; }

            try
            {
                var configSetting = ConfigurationManager.AppSettings["ErrorLogPath"];
                if (!String.IsNullOrEmpty(configSetting))
                {
                    var errorLogPath = Path.Combine(
                        configSetting,
                        String.Format(
                            "ErrorLog {0:MM-dd-yyyy}.txt",
                            DateTime.Now
                        )
                    );

                    if (!Directory.Exists(configSetting))
                    {
                        // We cannot log things on the build server, so builds fail when integration tests are run.
                        DirectoryInfo di = Directory.GetParent(configSetting);
                        if (di.Exists)
                        {
                            Directory.CreateDirectory(configSetting);
                        }
                        else
                        {
                            // This will be caught below
                            throw new DirectoryNotFoundException(String.Format(
                                "The parent folder for the path '{0}' does not exist.",
                                configSetting
                            ));
                        }
                    }

                    var errorLog = new ErrorLog(errorLogPath);

                    errorLog.Write(filterContext.Exception);
                }
                else
                {
                    Trace.WriteLine(filterContext.Exception.Message);                    
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Reporting Error: " + filterContext.Exception.Message);

                Trace.WriteLine("Error Log Error: " + e.Message);
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult
                {
                    Data = filterContext.Exception.ToString().Trim(),
                    ContentType = "application/json",
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}