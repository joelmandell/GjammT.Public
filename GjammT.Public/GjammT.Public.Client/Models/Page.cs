namespace GjammT.Public.Client.Models;

public class Page
{
    public string id  { get; set; }
    public string Language { get; set; }
    public string Path { get; set; }
    public string Data { get; set; }
    public string PartitionKey { get; set; } = "Page";
}