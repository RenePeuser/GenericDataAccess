using System;
using System.Linq;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Api.Controllers
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
                controller.Selectors.First().AttributeRouteModel = GetRoutAttribute(genericType);
            }
            else
            {
                controller.Selectors.Add(new SelectorModel {AttributeRouteModel = GetRoutAttribute(genericType)});
            }
        }

        private AttributeRouteModel GetRoutAttribute(Type genericType)
        {
            return new AttributeRouteModel(new RouteAttribute(_typeSpecificRouteProvider.GetRouteFor(genericType)));
        }
    }
}