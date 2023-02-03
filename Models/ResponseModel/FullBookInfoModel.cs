namespace LibraryWebApi.Models.ResponseModel;

internal sealed class FullBookInfoModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }
    public int BookletId { get; set; }
    public int PublisherId { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public int BookCount { get; set; }
    public double Price { get; set; }
    public string WikiLink { get; set; }
}