namespace PayrollSystem.Entity.InputOutput.Common
{
    public class ResponseModel
    {
        public StatusCodes ObjectStatusCode { get;set; }
        public Object Data { get; set; }   
        public string Message { get; set; }

    }
}
