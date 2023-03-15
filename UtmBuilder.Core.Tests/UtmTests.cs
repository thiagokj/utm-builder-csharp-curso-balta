using UtmBuilder.Core.Entities;
using UtmBuilder.Core.ValueObjects;

namespace UtmBuilder.Core.Tests;

[TestClass]
public class UtmTests
{
    private readonly Url _url = new("https://plataforma.io/");
    private readonly Campaign _campaign = new(
        "src",
        "med",
        "nme",
        "id",
        "trm",
        "ctn");
    private const string Result = "https://plataforma.io/"
            + "?utm_source=src"
            + "&utm_medium=med"
            + "&utm_campaign=nme"
            + "&utm_id=id"
            + "&utm_term=trm"
            + "&utm_content=ctn";

    [TestMethod]
    public void Deve_retornar_uma_url_de_um_utm()
    {
        var utm = new Utm(_url, _campaign);

        Assert.AreEqual(Result, utm.ToString());
        Assert.AreEqual(Result, (string)utm);
    }

    [TestMethod]
    public void Deve_retornar_um_utm_de_uma_url()
    {
        Utm utm = Result;

        Assert.AreEqual("https://plataforma.io/", utm.Url.Address);
        Assert.AreEqual("src", utm.Campaign.Source);
        Assert.AreEqual("med", utm.Campaign.Medium);
        Assert.AreEqual("nme", utm.Campaign.Name);
        Assert.AreEqual("id", utm.Campaign.Id);
        Assert.AreEqual("trm", utm.Campaign.Term);
        Assert.AreEqual("ctn", utm.Campaign.Content);
    }
}