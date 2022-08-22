using PnP.Core.Model.SharePoint;
using PnP.Core.Services;

namespace PnPCore
{
    public class Fence{
        private PnPContext Ctx;
        public virtual void SetUp(PnPContext ctx){
            Ctx = ctx;
        }

        public virtual async Task<IWeb> GetWeb(){
            return await Ctx.Web.GetAsync();
        }
    }
}