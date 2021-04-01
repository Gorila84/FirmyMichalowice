using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FirmyMichalowice.Helpers
{
    public class LogLastActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var repository = resultContext.HttpContext.RequestServices.GetService<ICompanyRepository>();

            var companyId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var company = await repository.GetCompany(companyId);

            company.Modify = DateTime.Now;

            await repository.SaveAll();

        }
    }
}
