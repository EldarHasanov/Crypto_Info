using System.Collections.Generic;

namespace CryptoInfo.Models;

internal class AssetsRootObjectWithList
{
    public List<Cryptocurrency> data { get; set; }
    public long Timestamp { get; set; }
}