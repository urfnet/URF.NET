using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;

namespace Northwind.Web.Areas.Spa.Extensions
{
    // This is a sample implementation of routing convention to support composit keys.
    // The implementation will fail if key value has ',' in it. Please implement your own convention to handle it.
    public class CompositeKeyRoutingConvention : EntityRoutingConvention
    {
        public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            string action = base.SelectAction(odataPath, controllerContext, actionMap);

            if (action != null)
            {
                var routeValues = controllerContext.RouteData.Values;

                if (routeValues.ContainsKey(ODataRouteConstants.Key))
                {
                    var keyRaw = routeValues[ODataRouteConstants.Key] as string;

                    if (keyRaw != null)
                    {
                        var compoundKeyPairs = keyRaw.Split(',');

                        if (!compoundKeyPairs.Any())
                        {
                            return action;
                        }

                        foreach (var compoundKeyPair in compoundKeyPairs)
                        {
                            string[] pair = compoundKeyPair.Split('=');

                            if (pair.Length != 2)
                            {
                                continue;
                            }

                            string keyName = pair[0].Trim();
                            string keyValue = pair[1].Trim();

                            routeValues.Add(keyName, keyValue);
                        }
                    }
                }
            }
            return action;
        }
    }
}