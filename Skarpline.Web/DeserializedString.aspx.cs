#region Using directives

using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

#endregion

public partial class DeserializedString : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string endpoint = System.Configuration.ConfigurationManager.AppSettings["api"];
        string teststring = "{\"has_title\":true,\"title\":\"GoodLuck\",\"entries\":[[\"/getting started.pdf\",{\"thumb_exists\":false,\"path\":\"/Getting Started.pdf\",\"client_mtime\":\"Wed, 08 Jan 2014 18:00:54 +0000\",\"bytes\":249159}],[\"/task.jpg\",{\"thumb_exists\":true,\"path\":\"/Ta sk.jpg\",\"client_mtime\":\"Tue, 14 Jan 2014 05:53:57 +0000\",\"bytes\":207696}]]}";

        RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();

        var response = new RestResponse();
        response.Content = teststring;

        lblResponseString.Text = teststring;
        var result = deserial.Deserialize<RootObject>(response);
        lblConverstionResult.Text = (result != null).ToString();
    }

}
public class RootObject
{
    public bool has_title { get; set; }
    public string title { get; set; }
    public List<List<object>> entries { get; set; }
}