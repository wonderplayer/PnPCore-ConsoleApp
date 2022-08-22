using PnP.Core.Services;

namespace PnPCore
{
    public class SharePoint{
        const string Url = "";
        Fence Fence;

        public SharePoint(IPnPContextFactory factory, Fence fence){
            var ctx = factory.Create(new Uri(Url));
            Fence = fence;
            Fence.SetUp(ctx);
        }

        public async Task<string> GetWebTitle(){
            var web = await Fence.GetWeb();
            Console.WriteLine(web.Title);
            return web.Title;
        }
    }
}