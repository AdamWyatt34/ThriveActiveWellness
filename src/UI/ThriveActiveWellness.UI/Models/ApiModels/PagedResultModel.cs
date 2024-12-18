namespace ThriveActiveWellness.UI.Models.ApiModels;

public class PagedResultModel<T>
{
    public IEnumerable<T> Records { get; set; }
    public long TotalRecordCount { get; set; }

    public PagedResultModel()
    {
    }

    public PagedResultModel(IEnumerable<T> records, long totalRecordCount)
    {
        Records = records;
        TotalRecordCount = totalRecordCount;
    }
}

