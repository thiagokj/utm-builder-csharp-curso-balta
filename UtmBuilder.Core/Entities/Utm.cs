using UtmBuilder.Core.Extensions;
using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Entities;

public class Utm
{
    /// <summary>
    /// Utm default constructor
    /// </summary>
    /// <param name="url">URL (Website Link)</param>
    /// <param name="campaign">Campaign details</param>
    public Utm(Url url, Campaign campaign)
    {
        Url = url;
        Campaign = campaign;
    }

    /// <summary>
    /// URL (Website Link)
    /// </summary>
    public Url Url { get; }

    /// <summary>
    /// Campaign details
    /// </summary>
    public Campaign Campaign { get; }

    /// <summary>
    /// Convert the Url to Utm
    /// </summary>
    /// <param name="link"></param>
    public static implicit operator Utm(string link)
    {
        if (string.IsNullOrEmpty(link))
            throw new InvalidUrlException();

        var url = new Url(link);

        var segments = url.Address.Split("?");

        if (segments.Length == 1)
            throw new InvalidUrlException("Nenhum segmento foi fornecido");

        var pars = segments[1].Split("&");

        var source = pars
            .Where(x => x.StartsWith("utm_source"))
            .FirstOrDefault("")
            .Split("=")[1];

        var medium = pars
            .Where(x => x.StartsWith("utm_medium"))
            .FirstOrDefault("")
            .Split("=")[1];

        var name = pars
            .Where(x => x.StartsWith("utm_campaign"))
            .FirstOrDefault("")
            .Split("=")[1];

        var id = pars
            .Where(x => x.StartsWith("utm_id"))
            .FirstOrDefault("")
            .Split("=")[1];

        var term = pars
            .Where(x => x.StartsWith("utm_term"))
            .FirstOrDefault("")
            .Split("=")[1];

        var content = pars
            .Where(x => x.StartsWith("utm_content"))
            .FirstOrDefault("")
            .Split("=")[1];

        var utm = new Utm(
            new Url(segments[0]),
            new Campaign(source, medium, name, id, term, content));

        return utm;
    }

    /// <summary>
    /// Convert Utm to string
    /// </summary>
    /// <param name="utm"></param>
    public static implicit operator string(Utm utm) => utm.ToString();

    public override string ToString()
    {
        var segments = new List<string>();

        segments.AddIfNotNull("utm_source", Campaign.Source);
        segments.AddIfNotNull("utm_medium", Campaign.Medium);
        segments.AddIfNotNull("utm_campaign", Campaign.Name);
        segments.AddIfNotNull("utm_id", Campaign.Id);
        segments.AddIfNotNull("utm_term", Campaign.Term);
        segments.AddIfNotNull("utm_content", Campaign.Content);

        // Url de exemplo https://plataforma.io/pagina-promo?utm_source=YouTube&utm_campaign=segments
        return $"{Url.Address}?{string.Join("&", segments)}";
    }

    public class Teste
    {
        public void Create()
        {
            var url = "https://balta.io/?utm_source&utm_medium=med&utm_campaign=nme&utm_id=id&utm_term=ter&utm_content=ctn;";

            string utm = (Utm)url;
        }
    }
}
