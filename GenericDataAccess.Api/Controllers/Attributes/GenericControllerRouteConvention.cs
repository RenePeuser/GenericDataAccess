using System;
using System.Linq;
using Api.DataAccess.Provider;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Controllers.Attributes
{
    internal class GenericControllerRouteConvention : IControllerModelConvention
    {
        private readonly TypeSpecificRouteProvider _typeSpecificRouteProvider;

        internal GenericControllerRouteConvention() : this(new TypeSpecificRouteProvider())
        {
        }

        private GenericControllerRouteConvention(TypeSpecificRouteProvider typeSpecificRouteProvider)
        {
            _typeSpecificRouteProvider = typeSpecificRouteProvider;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType.IsNot())
            {
                return;
            }

            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var customNameAttribute = controller.ControllerType.GetCustomAttribute<RouteAttribute>();
            if (customNameAttribute.IsNotNull())
            {
                return;
            }

            var attributeRouteModel = controller.Selectors.First().AttributeRouteModel;
            if (attributeRouteModel.IsNull())
            {
                controller.Selectors.First().AttributeRouteModel = GetRoutAttribute(controller, genericType);
            }
            else
            {
                controller.Selectors.Add(new SelectorModel {AttributeRouteModel = GetRoutAttribute(controller, genericType) });
            }
        }

        private AttributeRouteModel GetRoutAttribute(ControllerModel controllerModel, Type genericType)
        {
            var route = _typeSpecificRouteProvider.GetRouteFor(genericType);
            // ToDo: refactor this.
            var apiVersion = controllerModel.Attributes.OfType<ApiVersionAttribute>().FirstOrDefault().Versions.FirstOrDefault();
            var concreteRoute = route.Replace("{version:apiVersion}", $"{apiVersion.MajorVersion}.{apiVersion.MinorVersion}");
            
            return new AttributeRouteModel(new RouteAttribute(concreteRoute));
        }
    }
}