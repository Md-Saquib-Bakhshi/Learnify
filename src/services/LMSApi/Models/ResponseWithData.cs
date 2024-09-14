namespace LMSApi.Models
{
    public class ResponseWithData<T> : Response
    {
        public T? Data { get; set; }
    }
}
