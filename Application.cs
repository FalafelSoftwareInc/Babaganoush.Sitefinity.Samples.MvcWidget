using Babaganoush.Sitefinity.Models.Abstracts;
using Babaganoush.Sitefinity.Mvc.Configuration;
using Babaganoush.Sitefinity.Mvc.Routes;
using Babaganoush.Sitefinity.Mvc.Web.Controllers;
using Babaganoush.Sitefinity.Utilities;
using Babaganoush.Sitefinity.Samples.MvcWidget;
using Babaganoush.Sitefinity.Samples.MvcWidget.Web.Controllers;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;

//Startup and Shutdown code into a web application. This gives a much cleaner
//solution than having to modify global.asax with the startup logic from many packages.
[assembly: PreApplicationStartMethod(typeof(Application), "PreInit")]
namespace Babaganoush.Sitefinity.Samples.MvcWidget
{
    /// <summary>
    /// Application_Start of the target site.
    /// </summary>
    public class Application : BaseApplication<Application>
    {
        /// <summary>
        /// Will run when the application is starting (same as Application_Start)
        /// Called by the assembly PreApplicationStartMethod attribute.
        /// </summary>
        public static void PreInit()
        {
            //CALL BASE REGISTERATION
            RegisterStartup();

            //SUBSCRIBE TO SITEFINITY BOOTSTRAP EVENTS
            Bootstrapper.Initializing += OnBootstrapperInitializing;
            Bootstrapper.Initialized += OnBootstrapperInitialized;
        }

        /// <summary>
        /// Handles the Initializing event of the Bootstrapper.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutingEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitializing(object sender, ExecutingEventArgs e)
        {
            if (e.CommandName == "RegisterRoutes")
            {
                //REGISTER CUSTOM VIRTUAL PATH FOR SITEFINITY TEMPLATES
                ConfigHelper.RegisterVirtualPath(Constants.VALUE_CUSTOM_VIRTUAL_ROOT_PATH + "/*", "Babaganoush.Sitefinity.Samples.MvcWidget");
            }
        }

        /// <summary>
        /// Handles the Initialized event of the Bootstrapper.
        /// </summary>
        ///
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedEventArgs"/> instance containing the event data.</param>
        protected static void OnBootstrapperInitialized(object sender, ExecutedEventArgs e)
        {
            //PERFORM LAST ACTIONS IN BOOTSTRAPPER
            if (e.CommandName == "Bootstrapped")
            {
                //REGISTER MVC WIDGETS
                ConfigHelper.RegisterToolboxWidget<SpeakersListController>(
                    title: "Speakers List",
                    description: "Displays list of speakers."
                );
            }
        }
    }
}
