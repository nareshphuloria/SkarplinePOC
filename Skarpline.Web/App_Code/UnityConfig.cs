using Microsoft.Practices.Unity;
using Skarpline.CommonLayer.Mapper;
using Skarpline.CommonLayer.Unity;
using System;

public class UnityConfig
{
    #region Unity Container
    private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
        var container = new UnityContainer();

        RegisterApplicationDBContext(container);
        SkarplineUnityContainer.RegisterTypes(container);

        InitializeObjectMapper(container);

        Container = container;
        return container;

    });

    public static IUnityContainer Container { get; set; }

    /// <summary>
    /// Gets the configured Unity container.
    /// </summary>
    public static IUnityContainer GetConfiguredContainer()
    {
        return container.Value;
    }

    private static void RegisterApplicationDBContext(IUnityContainer container)
    {

    }

    private static void InitializeObjectMapper(IUnityContainer container)
    {
        container.Resolve<IObjectMapper>().CreateMap();
    }

    #endregion
}