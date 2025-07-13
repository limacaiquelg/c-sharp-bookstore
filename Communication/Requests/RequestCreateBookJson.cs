namespace Bookstore.Communication.Requests;

public class RequestCreateBookJson
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public string Genre { get; set; }
    public double? Price { get; set; }
    public uint? Quantity { get; set; }
}
