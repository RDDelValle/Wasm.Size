namespace Wasm.Size;

public class SizeConfig(string sizeDefault = "16px", string sizeSm = "14px", string sizeLg = "18px", string sizeXl = "20px", string localStorageKey = "Preferences.Size") : ISizeConfig
{
    public string LocalStorageKey { get; } = localStorageKey;
    public string SizeDefault { get; } = sizeDefault;
    public string SizeSm { get; } = sizeSm;
    public string SizeLg { get; } = sizeLg;
    public string SizeXl { get; } = sizeXl;
}