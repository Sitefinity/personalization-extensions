using DayOfWeekPersonalization;
using MbUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Services;

namespace DayOfWeekPersonalizationIntegrationTests
{
    [TestFixture]
    [Description("Integration tests for the Pages module.")]
    public class PagesModuleTests
    {
        public static Guid NewPageNodeId = new Guid("5B9542F9-E0D6-4587-96D4-F91C12532235");

        [Test]
        [Category(TestCategories.Sdk)]
        [Description("A test that always passes.")]
        [Author("SDK")]
        public void TestThatAlwaysPasses()
        {
            Assert.IsTrue(true);
        }

        [Test]
        [Category(TestCategories.Sdk)]
        [Description("Verifies that a page can be created using the Native API.")]
        [Author("SDK")]
        public void CreatePageNativeAPITestDevGuide()
        {
            using (var fluent = App.WorkWith())
            {
                // set up
                PagesModuleTests.DeleteIfTestPageExists(fluent);
                int initialPagesCount = fluent.Pages().Get().Count();

                Guid newPageId = NewPageNodeId;
                Guid expectedParentPageId = SiteInitializer.CurrentFrontendRootNodeId;
                string expectedName = "Test name";
                bool expectedIsHomePage = true;

                Installer.CreatePageNativeAPI(PagesModuleTests.NewPageNodeId, expectedName, expectedIsHomePage, expectedParentPageId);
                int pagesCountAfterAddingNewPage = fluent.Pages().Get().Count();
                Assert.AreEqual(initialPagesCount + 1, pagesCountAfterAddingNewPage);

                var createdPageNode = fluent.Page(newPageId).Get();

                // Clears current site from the context because of changing the home page
                SystemManager.CurrentContext.InvalidateCache();

                var homePageId = SystemManager.CurrentContext.CurrentSite.HomePageId; //Config.Get<PagesConfig>().HomePageId;

                Assert.IsNotNull(createdPageNode);
                Assert.AreEqual(expectedName, (string)createdPageNode.Title);
                Assert.AreEqual(expectedName, (string)createdPageNode.GetPageData().HtmlTitle);
                Assert.AreEqual(expectedName, (string)createdPageNode.Title);
                Assert.AreEqual(expectedName, createdPageNode.Name);
                Assert.AreEqual(expectedParentPageId, createdPageNode.ParentId);
                Assert.AreEqual(createdPageNode.Id, homePageId);
                string expectedUrlName = Regex.Replace(expectedName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                Assert.AreEqual(expectedUrlName, (string)createdPageNode.UrlName);

                // tear down
                fluent.Page(newPageId).Delete().SaveChanges();
                int pagesCountAfterDeletion = fluent.Pages().Get().Count();
                Assert.AreEqual(initialPagesCount, pagesCountAfterDeletion);
            }
        }

        public static void DeleteIfTestPageExists(FluentSitefinity fluent)
        {
            var createdPageNode = fluent.Pages().Get().Where(p => p.Id == NewPageNodeId).FirstOrDefault();
            if (createdPageNode != null)
            {
                fluent.Page(NewPageNodeId).Delete().SaveChanges();
            }
        }
    }
}
