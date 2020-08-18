using System;
using Api.DataAccess.Provider;
using Api.Errorhandling;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenericControllerNameAttribute : Attribute, IControllerModelConvention
    {
        private readonly TypeToControllerNameProvider _typeToControllerNameProvider;

        public GenericControllerNameAttribute() : this(new TypeToControllerNameProvider())
        {
        }

        private GenericControllerNameAttribute(TypeToControllerNameProvider typeToControllerNameProvider)
        {
            _typeToControllerNameProvider = typeToControllerNameProvider;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.HasCustomAttribute<GenericControllerNameAttribute>())
            {
                var entityType = controller.ControllerType.GenericTypeArguments[0];
                var controllerName = _typeToControllerNameProvider.GetNameFor(entityType);
                if (controllerName.IsNull())
                {
                    throw new ProblemDetailsException(500, "Not possible to create a new controller for a specific entity.", $"Could not find a controller for the entity type: {entityType.Name}");
                }

                controller.ControllerName = controllerName;
            }
        }
    }
}