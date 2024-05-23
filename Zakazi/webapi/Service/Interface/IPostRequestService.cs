namespace webapi.Service.Interface
{
    public interface IPostRequestService
    {
        Task<IEnumerable<int>> GetPostIdsByRequestId(int requestId);
    }
}
