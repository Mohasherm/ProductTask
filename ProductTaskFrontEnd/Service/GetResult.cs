namespace ProductTaskFrontEnd.Service;

public class GetResult<TEntity>
{
    public TEntity Data { get; set; }
    public string ErrorMessage { get; set; }
}

public class GetErrorMessage
{
    public string ErrorMessage { get; set; }
}