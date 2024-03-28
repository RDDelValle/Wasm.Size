namespace Wasm.Size;

public interface ISizeConfig
{
    string LocalStorageKey { get; }
    string SizeDefault { get; }
    string SizeSm { get; }
    string SizeLg { get; }
    string SizeXl { get; }
}