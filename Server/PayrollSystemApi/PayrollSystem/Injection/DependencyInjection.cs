using PayrollSystem.Business.Common;
using PayrollSystem.Business.Employee;
using PayrollSystem.Business.HR;
using PayrollSystem.Business.Logs;
using PayrollSystem.Core.Common;
using PayrollSystem.Core.Employee;
using PayrollSystem.Core.HR;
using PayrollSystem.Core.Logs;

namespace PayrollSystem.Injection
{
    public class DependencyInjection
    {
        public static void Injectctor(IServiceCollection services)
        {
            services.AddScoped<IBussEmployeeServices, BussEmployeeServices>();
            services.AddScoped<IEmployeeServices, EmployeeServices>();
            services.AddScoped<IBussHrServices, BussHRServices>();
            services.AddScoped<IHrServices, HRServices>();
            services.AddScoped<IBussLogServices, BussLogServices>();
            services.AddSingleton<ILogServices, LogServices>();
            services.AddScoped<IBussCommonTaskServices, BussCommonTaskServices>();
            services.AddScoped<ICommonTaskServices, CommonTaskServices>();
        }
    }
}
