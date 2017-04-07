using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using JBoyerLibaray.FileSystem;

namespace JBoyerLibaray
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private IFileSystemHelper _fileSystemHelper;

        public CustomHandleErrorAttribute() : this(new FileSystemHelper()) { }

        public CustomHandleErrorAttribute(IFileSystemHelper fileSystemHelper)
        {
            _fileSystemHelper = fileSystemHelper;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                return;
            }

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

                    if (!_fileSystemHelper.Directory.Exists(configSetting))
                    {
                        // We cannot log things on the build server, so builds fail when integration tests are run.
                        var parentPath = _fileSystemHelper.Directory.GetParentPath(configSetting);
                        
                        if (_fileSystemHelper.Directory.Exists(parentPath))
                        {
                            _fileSystemHelper.Directory.CreateDirectory(configSetting);
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

                    errorLog.Write(filterContext.RequestContext.HttpContext.User, filterContext.Exception);
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

                if (!filterContext.ExceptionHandled && filterContext.RequestContext.HttpContext.IsCustomErrorEnabled)
                {
                    filterContext.ExceptionHandled = true;

                    filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary(filterContext.Controller.ViewData)
                        {
                            Model = new HandleErrorInfo(
                                filterContext.Exception,
                                filterContext.RequestContext.RouteData.Values["controller"].ToString(),
                                filterContext.RequestContext.RouteData.Values["action"].ToString()
                            )
                        }
                    };
                }
            }
        }
    }
}