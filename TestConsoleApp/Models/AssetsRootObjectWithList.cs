namespace CryptoInfo.Models;

internal class AssetsRootObjectWithList
{
    public List<Cryptocurrency> data { get; set; }
    public long timestamp { get; set; }
}