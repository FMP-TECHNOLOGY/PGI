using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.Json;
using Utils.Helpers;

namespace DataAccess.Entities;

public partial class Log
{
    public Log() { }
    public Log(object newData)
    {
        SetNewData(newData);
    }

    public Log(object newData, object? oldData)
    {
        SetNewData(newData);
        SetOldData(oldData);
        //Objtype = newData?.GetType().Name;
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

    [NotMapped]
    private readonly JsonSerializerOptions JsonOptions = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    private void SetNewData(object newData)
    {
        NewData = ToJSON(newData);
        Sha256 = CryptoHelper.Hash(NewData, HashAlg.SHA256) ?? "";
    }

    private void SetOldData(object? oldData)
    {
        OldData = ToJSON(oldData);
    }

    private string? ToJSON(object? data) => data == null ? null : JsonSerializer.Serialize(data, JsonOptions);

}
