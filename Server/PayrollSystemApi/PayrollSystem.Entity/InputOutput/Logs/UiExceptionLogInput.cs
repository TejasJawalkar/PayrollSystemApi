using Microsoft.AspNetCore.Mvc;

namespace PayrollSystem.Entity.InputOutput.Logs
{
    public class UiExceptionLogInput
    {
        [FromForm] public string ClassName { get; set; }
        [FromForm] public string MethodName { get; set; }
        [FromForm] public string Exception { get; set; }
        [FromForm] public string SiteName { get; set; }
    }
}
