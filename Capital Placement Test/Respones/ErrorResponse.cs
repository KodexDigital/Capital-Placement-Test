namespace Capital_Placement_Test.Response
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; set; }
    }
}