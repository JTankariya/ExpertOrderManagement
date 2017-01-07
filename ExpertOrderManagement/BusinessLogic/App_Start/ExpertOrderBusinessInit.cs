namespace BusinessLogic.App_Start
{
    using System;
    using System.Web;
    using Ninject;

    public static class ExpertOrderBusinessInit
    {
        public static IKernel kernel { get; private set; }
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static void Initialize()
        {
            try
            {
                kernel = new StandardKernel();
                RegisterServices(kernel);
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }


        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<IManagerFactory<User,IUserManager>>().To<ManagerFactory<User,IUserManager>>();
        }
    }
}
