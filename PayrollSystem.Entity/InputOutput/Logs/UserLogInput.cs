using Microsoft.AspNetCore.Mvc;

namespace PayrollSystem.Entity.InputOutput.Logs
{
    public class UserLogInput
    {
        [FromForm] public Int64 UserId { get; set; } = 0;

        [FromForm] public String BrowswerUsed { get; set; }

        [FromForm] public String IdAddress { get; set; }

        [FromForm] public Int64 Flag { get; set; }
    }
}
