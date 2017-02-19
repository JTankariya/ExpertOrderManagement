namespace BusinessLogic
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
            kernel.Bind<IPartyManager>().To<PartyManager>();
            kernel.Bind<IClientManager>().To<ClientManager>();
            kernel.Bind<IProductGroupManager>().To<ProductGroupManager>();
            kernel.Bind<IProductManager>().To<ProductManager>();
            kernel.Bind<IOrderManager>().To<OrderManager>();
            kernel.Bind<IManagerFactory<Party, IPartyManager>>().To<ManagerFactory<Party, IPartyManager>>();
            kernel.Bind<IManagerFactory<Client, IClientManager>>().To<ManagerFactory<Client, IClientManager>>();
            kernel.Bind<IManagerFactory<ClientUser, IUserManager>>().To<ManagerFactory<ClientUser, IUserManager>>();
            kernel.Bind<IManagerFactory<ProductGroup, IProductGroupManager>>().To<ManagerFactory<ProductGroup, IProductGroupManager>>();
            kernel.Bind<IManagerFactory<Product, IProductManager>>().To<ManagerFactory<Product, IProductManager>>();
            kernel.Bind<IManagerFactory<Order, IOrderManager>>().To<ManagerFactory<Order, IOrderManager>>();

            kernel.Bind<IProductGroupHelper>().To<ProductGroupHelper>();
            kernel.Bind<IProductHelper>().To<ProductHelper>();
            kernel.Bind<IPartyHelper>().To<PartyHelper>();
            kernel.Bind<IPartyGroupHelper>().To<PartyGroupHelper>();
            kernel.Bind<IClientHelper>().To<ClientHelper>();
            kernel.Bind<IOrderHelper>().To<OrderHelper>();
            kernel.Bind<IHelperFactory<string, int, IPartyHelper>>().To<HelperFactory<string, int, IPartyHelper>>();
            kernel.Bind<IHelperFactory<string, int, IPartyGroupHelper>>().To<HelperFactory<string, int, IPartyGroupHelper>>();
            kernel.Bind<IHelperFactory<string, int, IProductGroupHelper>>().To<HelperFactory<string, int, IProductGroupHelper>>();
            kernel.Bind<IHelperFactory<string, int, IProductHelper>>().To<HelperFactory<string, int, IProductHelper>>();
            kernel.Bind<IHelperFactory<string, int, IClientHelper>>().To<HelperFactory<string, int, IClientHelper>>();
            kernel.Bind<IHelperFactory<string, int, IOrderHelper>>().To<HelperFactory<string, int, IOrderHelper>>();
        }
    }
}
