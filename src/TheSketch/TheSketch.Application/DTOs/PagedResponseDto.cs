namespace TheSketch.Application.DTOs;

public class PagedResponseDto<T>
{
    public IEnumerable<T> Items { get; set; } = Array.Empty<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PagedResponseDto(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItems = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}