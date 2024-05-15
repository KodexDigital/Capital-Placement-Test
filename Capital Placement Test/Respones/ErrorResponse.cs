namespace cosmo_db_test.Response
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