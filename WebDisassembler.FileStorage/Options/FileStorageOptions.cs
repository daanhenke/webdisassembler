namespace WebDisassembler.FileStorage.Options;

public class FileStorageOptions
{
    public BackendType Type { get; set; }
    public string ConnectionString { get; set; } = "";
}