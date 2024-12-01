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
            services.AddSingleton<IBussEmployeeServices, BussEmployeeServices>();
            services.AddSingleton<IEmployeeServices, EmployeeServices>();
            services.AddSingleton<IBussHrServices,BussHRServices>();
            services.AddSingleton<IHrServices,HRServices>();
            services.AddSingleton<IBussLogServices,BussLogServices>();
            services.AddSingleton<ILogServices,LogServices>();
            services.AddSingleton<IBussCommonServices, BussEmployeeServices>();
            services.AddSingleton<ICommonServices,EmployeeServices>();
            services.AddSingleton<IBussCommonServices, BussHRServices>();
            services.AddSingleton<ICommonServices, HRServices>();
        }
    }
}
