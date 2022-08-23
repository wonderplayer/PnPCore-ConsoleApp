using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;

namespace PnPCore{
    [TestClass]
    public class FenceTests
    {
        [TestMethod]
        public async Task GetWeb()
        {
            const string title = "TestingTitle";
            const string uri = "https://contoso.sharepoint.com";
            var web = new Mock<IWeb>();
            web.Setup(w => w.Title).Returns(title);
            var factory = new Mock<IPnPContextFactory>();
            // var ctx = new Mock<PnPContext>();
            var ctx = PnPContext.NewPnPContextMock(new Uri(uri));
            // ctx.Setup(c => c.Web.GetAsync()).ReturnsAsync(web.Object);
            var fence = new Fence();
            fence.SetUp(ctx);

            var result = await fence.GetWeb();

            Assert.AreEqual(title, result.Title);
        }
    }
}