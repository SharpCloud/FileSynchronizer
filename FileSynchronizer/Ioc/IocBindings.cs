using FileSynchronizer.Interfaces;
using FileSynchronizer.Services;
using Ninject.Modules;

namespace FileSynchronizer.Ioc
{
    internal class IocBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IFileListDiffService>().To<FileListDiffService>();
            Kernel.Bind<IFileHashService>().To<FileHashService>();
        }
    }
}
