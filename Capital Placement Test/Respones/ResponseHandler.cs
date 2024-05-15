namespace Capital_Placement_Test.Response
{
    public class ResponseHandler
    {
        public bool Status { get; set; } = true;
        public string? Message { get; set; } = "Request not completed";
    }

    public class ResponseHandler<T> : ResponseHandler
    {
        public T? Data { get; set; }
    }
}