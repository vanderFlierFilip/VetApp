using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Numerics;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;

namespace VDMJasminka.WebApi.Core.Filters
{
    public class ValidCheckupTypeFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("CheckupType", StringComparison.InvariantCultureIgnoreCase))
            {
                var checkupCategoryList = new List<string> { "Vakcinacija", "Lečenje", "Čipovanje", "Hirurgija" };
                parameter.Schema.Enum = checkupCategoryList.Select(p => new OpenApiString(p)).ToList<IOpenApiAny>();
            }
        }
    }

    public class SetOperationIdOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return;
            }

            var attribute = context.MethodInfo.GetCustomAttribute<SwaggerOperationAttribute>();
            var actionName = attribute?.OperationId ?? controllerActionDescriptor.ActionName.Replace("Async", string.Empty);

            operation.OperationId = $"{controllerActionDescriptor.ControllerName}_{actionName}";
        }
    }
}