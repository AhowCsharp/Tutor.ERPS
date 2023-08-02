using TUTOR.ERPS.API.Infa.Attribute;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TUTOR.ERPS.API.Infa.Swagger
{
    public class AddRequireHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            if (context.ApiDescription.CustomAttributes().Any(x => x.GetType() == typeof(ApTokenAuth)))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
#if (DEBUG)
                    Example = new Microsoft.OpenApi.Any.OpenApiString("DEBUG"),
#endif
                    Name = "X-Ap-Token",
                    Description = "Token",
                    In = ParameterLocation.Header,
                    Required = true
                });
            }
            //if (context.ApiDescription.CustomAttributes().Any(x => x.GetType() == typeof(CountryCodeAuth)))
            //{
            //    operation.Parameters.Add(new OpenApiParameter
            //    {
            //        Example = new Microsoft.OpenApi.Any.OpenApiString("TW"),
            //        Name = "X-Country-Code",
            //        Description = "國家代碼",
            //        In = ParameterLocation.Header,
            //        Required = true
            //    });
            //}
            //if (context.ApiDescription.CustomAttributes().Any(x => x.GetType() == typeof(LanguageCodeAuth)))
            //{
            //    operation.Parameters.Add(new OpenApiParameter
            //    {
            //        Example = new Microsoft.OpenApi.Any.OpenApiString("zh-tw"),
            //        Name = "X-Language-Code",
            //        Description = "語系代碼",
            //        In = ParameterLocation.Header,
            //        Required = true
            //    });
            //}
        }
    }
}
