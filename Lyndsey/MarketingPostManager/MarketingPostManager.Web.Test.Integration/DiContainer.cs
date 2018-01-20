using Ninject;
using System.Reflection;

namespace MarketingPostManager.Web.Test.Integration
{
    public static class DiContainer
    {
        public static IKernel Container
        {
            get
            {
                var kernel = new StandardKernel();
                kernel.Load(Assembly.Load("MarketingPostManager.Web"));

                return kernel;
            }
        }
    }
}
