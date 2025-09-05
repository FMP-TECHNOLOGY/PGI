using System;
using System.Collections.Generic;
using Utils.Helpers;

namespace DataAccess.Entities;

public partial class Log
{
    public Log(string newData)
    {
        NewData = newData;
        Sha256 = Hasher.Hash(NewData, HashAlg.SHA256);
    }
    public long Id { get; set; }

    public string? IdDocumento { get; set; } 

    public int? Objtype { get; set; }

    public string?Action { get; set; }

    public string?OldData { get; set; }

    public string?NewData { get; set; }

    public string?Sha256 { get; set; }

    public string?CreatedBy { get; set; }

    public DateTime? Timestamp { get; set; }
}
