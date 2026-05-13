public class PaginatedList<T> : List<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public string SearchString { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string searchString)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        SearchString = searchString;

        AddRange(items);
    }

    public bool HasPreviousPage => (PageIndex > 1);
    public bool HasNextPage => (PageIndex < TotalPages);
}