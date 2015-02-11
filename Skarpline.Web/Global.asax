<%@ Application Language="C#" %>
<%@ Import Namespace="SkarplineChat.Web" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>
<%@ Import Namespace="System.Web.Mvc" %>


<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        //BundleConfig.RegisterBundles(BundleTable.Bundles);
        var container = UnityConfig.GetConfiguredContainer();
    }

</script>