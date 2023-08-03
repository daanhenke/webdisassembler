namespace WebDisassembler.DataStorage.Options;

public class DataStorageOptions
{
    public BackendType Type { get; set; }

    public string ConnectionString { get; set; } = "";
}