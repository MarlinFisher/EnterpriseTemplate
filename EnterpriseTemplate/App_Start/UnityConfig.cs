namespace EnterpriseTemplate
{

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.AddExtension(new Interception());

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}