namespace CleanArchitecture.Maui.MobileUi.Mobile.Options;

public class OidcClientSettings
{
    public string? Authority { get; set; }
    public string? ClientId { get; set; }
    public IEnumerable<string>? Scope { get; set; }
    public string? CallBackSchema { get; set; }
    public string? ClientSecret { get; set; }

}