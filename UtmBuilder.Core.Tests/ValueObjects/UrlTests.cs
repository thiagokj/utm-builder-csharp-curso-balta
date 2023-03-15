using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Tests.ValueObjects;

[TestClass]
public class UrlTests
{
    private const string InvalidUrl = "123qew";
    private const string ValidUrl = "https://youtube.com";

    [TestMethod]
    [TestCategory("Teste de URL")]
    [ExpectedException(typeof(InvalidUrlException))]
    public void Deve_retornar_excecao_quando_a_url_for_invalida()
    {
        new Url(InvalidUrl);
    }

    [TestMethod]
    [TestCategory("Teste de URL")]
    public void Nao_Deve_retornar_excecao_quando_a_url_for_valida()
    {
        new Url(ValidUrl);
        Assert.IsTrue(true);
    }

    [TestMethod]
    [TestCategory("Teste de URL")]
    [DataRow(" ", true)]
    [DataRow("http", true)]
    [DataRow("youtube", true)]
    [DataRow("https://youtube.com", false)]
    public void TesteUrl(string link, bool expectException)
    {
        if (expectException)
        {
            try
            {
                new Url(link);
                Assert.Fail();
            }
            catch (InvalidUrlException)
            {
                Assert.IsTrue(true);
            }
        }
        else
        {
            new Url(link);
            Assert.IsTrue(true);
        }
    }
}