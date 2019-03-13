using Ninject;

namespace FileSynchronizer.Ioc
{
    public class NinjectKernelFactory
    {
        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel(
                new IocBindings());

            return kernel;
        }
    }
}
