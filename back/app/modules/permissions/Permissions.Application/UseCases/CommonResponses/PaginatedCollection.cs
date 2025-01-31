namespace Permissions.Application.UseCases.CommonResponses;

public sealed record PaginatedCollection<T>(
    IEnumerable<T> Data,
    int TotalItems,
    int TotalPages)
{
    public static PaginatedCollection<T> Map(
        IEnumerable<T> data, 
        int totalItems, 
        int totalPages)
        => new(
            data, 
            totalItems, 
            totalPages);
}
