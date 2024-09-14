namespace AuthenticationAPI.Models
{
    public class ResponseWithData<T> : Response
    {
        public T? Data { get; set; }
    }
}
