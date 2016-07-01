using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Personalization.Impl;
using Telerik.Sitefinity.Personalization.Impl.Configuration;
using Telerik.Sitefinity.Personalization.Impl.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow;

namespace DayOfWeekPersonalization
{
    public class Installer
    {
        /// <summary>
        /// This is the actual method that is called by ASP.NET even before application start.
        /// </summary>
        public static void PreApplicationStart()
        {
            // With this method we subscribe for the Sitefinity Bootstrapper_Initialized event, which is fired after initialization of the Sitefinity application.
            Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
        }

        /// <summary>
        /// Handles the Initialized event of the Bootstrapper control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedEventArgs"/> instance containing the event data.</param>
        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(CreateSampleWorker);
                SystemManager.RunWithElevatedPrivilege(worker);
            }
        }

        /// <summary>
        /// Creates the sample worker.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        private static void CreateSampleWorker(object[] parameters)
        {
            if (!IsPersonalizationModuleInstalled())
            {
                return;
            }

            AddVirtualPathToEmbeddedRes();

            CreateCriteria();

            RegisterCriteria();

            CreateSegment();

            CreateAndPersonalizePage();
        }

        /// <summary>
        /// Checks if the Personalization module is install in the Sitefinity application
        /// </summary>
        /// <returns></returns>
        private static bool IsPersonalizationModuleInstalled()
        {
            return SystemManager.ApplicationModules != null &&
                SystemManager.ApplicationModules.ContainsKey(PersonalizationModule.ModuleName) &&
                !(SystemManager.ApplicationModules[PersonalizationModule.ModuleName] is InactiveModule);
        }

        /// <summary>
        /// Adds the virtual path to embedded resource.
        /// </summary>
        private static void AddVirtualPathToEmbeddedRes()
        {
            //Register the resource file
            Res.RegisterResource<CustomPersonalizationResources>();

            var virtualPathConfig = Config.Get<VirtualPathSettingsConfig>();
            if (!virtualPathConfig.VirtualPaths.Contains(virtualPath + "*"))
            {
                var pathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
                {
                    VirtualPath = Installer.virtualPath + "*",
                    ResolverName = "EmbeddedResourceResolver",
                    ResourceLocation = "DayOfWeekPersonalization"
                };
                virtualPathConfig.VirtualPaths.Add(pathConfig);
                ConfigManager.GetManager().SaveSection(virtualPathConfig);
                SystemManager.RestartApplication("ConfigChange");
            }

        }

        /// <summary>
        /// Creates the criteria
        /// </summary>
        private static void CreateCriteria()
        {
            var personalizationConfig = Config.Get<PersonalizationConfig>();
            if (!personalizationConfig.Criteria.Contains(Res.Get<CustomPersonalizationResources>().Day))
            {
                CriterionElement ageCriterion = new CriterionElement(personalizationConfig.Criteria)
                {
                    Name = Res.Get<CustomPersonalizationResources>().Day,
                    Title = Res.Get<CustomPersonalizationResources>().Day,
                    ResourceClassId = typeof(CustomPersonalizationResources).Name,
                    CriterionEditorUrl = "DayOfWeekPersonalization.DayOfWeekEditor.ascx",
                    ConsoleCriterionEditorUrl = "DayOfWeekPersonalization.DayOfWeekEditor.ascx",
                    CriterionVirtualPathPrefix = Installer.virtualPath
                };
                personalizationConfig.Criteria.Add(ageCriterion);
            }
        }


        /// <summary>
        /// Register the evaluator for the criteria
        /// </summary>
        private static void RegisterCriteria()
        {
            ObjectFactory.Container.RegisterType(
                typeof(ICriterionEvaluator),
                typeof(DayOfWeekEvaluator),
                Res.Get<CustomPersonalizationResources>().Day,
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor());
        }

        /// <summary>
        /// Creates a segment with criteria
        /// </summary>
        public static void CreateSegment()
        {
            var personalizationManager = PersonalizationManager.GetManager();
            using (new ElevatedModeRegion(personalizationManager))
            {
                if (!personalizationManager.GetSegments().Any(s => s.Id == Installer.segmentId))
                {
                    var segment = personalizationManager.CreateSegment(Installer.segmentId);
                    segment.Name = "Weekend segment";
                    segment.Description = "Pages will be personalized according to the day of the week";
                    segment.IsActive = true;
                    CriteriaGroup group = new CriteriaGroup();

                    Criterion saturdayCriterion = new Criterion();
                    saturdayCriterion.CriterionValue = ((int)DayOfWeek.Saturday).ToString();
                    saturdayCriterion.CriterionDisplayValue = Res.Get<CustomPersonalizationResources>().Saturday;
                    saturdayCriterion.CriterionName = Res.Get<CustomPersonalizationResources>().Day;
                    saturdayCriterion.CriterionTitle = Res.Get<CustomPersonalizationResources>().Day;

                    Criterion sundayCriterion = new Criterion();
                    sundayCriterion.CriterionValue = ((int)DayOfWeek.Sunday).ToString();
                    sundayCriterion.CriterionDisplayValue = Res.Get<CustomPersonalizationResources>().Sunday;
                    sundayCriterion.CriterionName = Res.Get<CustomPersonalizationResources>().Day;
                    sundayCriterion.CriterionTitle = Res.Get<CustomPersonalizationResources>().Day;

                    group.Criteria.Add(saturdayCriterion);
                    group.Criteria.Add(sundayCriterion);
                    segment.CriteriaGroups.Add(group);
                    personalizationManager.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Creates and personalizes a Page
        /// </summary>
        private static void CreateAndPersonalizePage()
        {
            PageManager pageManager = PageManager.GetManager();
            if (!pageManager.GetPageNodes().Any(a => a.Name == Installer.pageName))
            {
                CreatePageNativeAPI(Installer.pageId, Installer.pageName, false, Guid.Empty);
                AddControlToPage();
                PersonalizePage();
            }
        }

        /// <summary>
        /// Creates a page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="isHomePage">if set to <c>true</c> sets the page as a home page.</param>
        public static void CreatePageNativeAPI(Guid pageId, string pageName, bool isHomePage, Guid parentPageNodeId)
        {
            PageManager manager = PageManager.GetManager();
            PageData pageData = null;
            PageNode pageNode = null;

            // Get the parent node Id
            if (parentPageNodeId == Guid.Empty)
            {
                parentPageNodeId = SiteInitializer.CurrentFrontendRootNodeId;
            }

            PageNode parent = manager.GetPageNode(parentPageNodeId);

            // Check whether exists
            var initialPageNode = manager.GetPageNodes().Where(n => n.Id == pageId).SingleOrDefault();

            if (initialPageNode != null)
            {
                return;
            }

            // Create the page
            pageNode = manager.CreatePage(parent, pageId, NodeType.Standard);

            //pageData.NavigationNode = pageNode;
            pageData = pageNode.GetPageData();
            pageData.Culture = Thread.CurrentThread.CurrentCulture.ToString();
            pageData.HtmlTitle = pageName;

            pageNode.Title = pageName;
            pageNode.Description = pageName;
            pageNode.Name = pageName;
            pageNode.UrlName = Regex.Replace(pageName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            pageNode.ShowInNavigation = true;
            pageNode.DateCreated = DateTime.UtcNow;
            pageNode.LastModified = DateTime.UtcNow;

            // Check whether home page
            if (isHomePage)
            {
                manager.SetHomePage(pageId);
            }

            manager.SaveChanges();

            // Publish
            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(PageNode).FullName);
            WorkflowManager.MessageWorkflow(pageId, typeof(PageNode), null, "Publish", false, bag);
        }

        /// <summary>
        /// Adds a control to page.
        /// </summary>
        public static void AddControlToPage()
        {
            //get a PageManager object
            PageManager pageManager = PageManager.GetManager();
            var pageData = pageManager.GetPageNode(Installer.pageId).GetPageData();

            var draftPage = pageManager.EditPage(pageData.Id);
            var contentBlock = new ContentBlock();
            contentBlock.Html = Installer.weekdayContent + Installer.instructionContent;

            var draftControl = pageManager.CreateControl<PageDraftControl>(contentBlock, "Body");
            draftControl.Caption = "Content block";
            draftPage.Controls.Add(draftControl);

            //Save the changes
            pageManager.PublishPageDraft(draftPage, CultureInfo.CurrentUICulture);
            pageManager.SaveChanges();
        }

        /// <summary>
        /// Creates a personalized page and modifies the existing control.
        /// </summary>
        public static void PersonalizePage()
        {
            PageManager pageManager = PageManager.GetManager();
            var pageNode = pageManager.GetPageNode(Installer.pageId);
            if (pageNode != null)
            {
                var personalizationManager = PersonalizationManager.GetManager();
                personalizationManager.CreatePersonalizedPage(pageNode.GetPageData().Id, Installer.segmentId);
                personalizationManager.SaveChanges();

                var personalizedPageData = pageManager.GetPageDataList().FirstOrDefault(p => p.PersonalizationSegmentId == Installer.segmentId);
                if (personalizedPageData != null)
                {
                    var draftPage = pageManager.EditPage(personalizedPageData.Id);

                    var contentItem = draftPage.Controls.FirstOrDefault();
                    var currentControl = pageManager.LoadControl(contentItem) as ContentBlock;
                    currentControl.Html = Installer.weekendContent + Installer.instructionContent;

                    // Copy the properties of the current Content Block item to the newly created
                    pageManager.ReadProperties(currentControl, contentItem);

                    //Save the changes
                    pageManager.PublishPageDraft(draftPage);
                    pageManager.SaveChanges();
                }
            }
        }

        #region Private members & constants

        private static readonly string pageName = "DayOfWeekPage";
        private static readonly string virtualPath = "~/SFCustomPersonalization/";
        private static readonly string weekdayContent =
                                    @"<h1>Weekday page.</h1>
                                    <p>This page is visible during weekdays.</p>";           
        private static readonly string weekendContent =
                                    @"<h1>Weekend page.</h1>
                                    <p>This page is visible during weekends.</p>";
        private static readonly string instructionContent =
                                    @"<p>To change when this page is visible</p>
                                    <ol>
                                        <li>Go to Sitefinity's Backend</li> 
                                        <li>Select <i>Marketing &gt; Personalization</i></li>
                                        <li>Select the <i>Weekend segment</i></li>
                                        <li>Modify the existing <i>characteristics</i></li>
                                        <li>Click <i>Save changes</i></li>
                                        <li>Refresh this page</li>
                                    </ol>
                                    </p>";
        private static readonly Guid segmentId = new Guid("2E68BB39-9D4E-4D8E-B53A-551B81D2FBA4");
        private static readonly Guid pageId = new Guid("5B9B0941-3B78-4F23-842D-95820FDC5D0C");
        public const string UrlNameCharsToReplace = @"[^\w\-\!\$\'\(\)\=\@\d_]+";
        public const string UrlNameReplaceString = "-";

        #endregion
    }
}
