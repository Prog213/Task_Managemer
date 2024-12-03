using API.Models.Enums;

namespace API.Helpers;

public class TaskQueryParams
{
    private const int MaxPageSize = 10;
    private int pageSize = 5;
    public int PageSize
    {
        get => pageSize;
        set
        {
            if (value < 1)
                pageSize = 1;
            else if (value > MaxPageSize)
                pageSize = MaxPageSize;
            else
                pageSize = value;
        }
    }

    private int pageNumber = 1;
    public int PageNumber
    {
        get => pageNumber;
        set
        {
            if (value < 1)
                pageNumber = 1;
            else
                pageNumber = value;
        }
    }

    public AppTaskStatus? Status { get; set; }
    public AppTaskPriority? Priority { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
    public string? OrderBy { get; set; }
}
