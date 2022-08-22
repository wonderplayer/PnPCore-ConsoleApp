using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;

namespace PnPCore{
    [TestClass]
    public class SharePointTests
    {
        [TestMethod]
        public async Task GetWebTitle()
        {
            const string title = "TestingTitle";
            var fence = new Mock<Fence>();
            var web = new Mock<IWeb>();
            web.Setup(w => w.Title).Returns(title);
            fence.Setup(x => x.GetWeb()).ReturnsAsync(web.Object);
            var factory = new Mock<IPnPContextFactory>();
            var sp = new SharePoint(factory.Object, fence.Object);
            
            var response = await sp.GetWebTitle();

            Assert.AreEqual(title, response);
        }
    }
}