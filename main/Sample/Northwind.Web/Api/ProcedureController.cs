#region

using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.Service;

#endregion

namespace Northwind.Web.Api
{
    public class ProcedureController : ApiController
    {
        private readonly IStoredProcedureService _storedProcedureService;

        public ProcedureController(IStoredProcedureService storedProcedureService)
        {
            _storedProcedureService = storedProcedureService;
        }

        [HttpGet]
        public HttpResponseMessage CustomerOrderHistory(string customerID)
        {
            var customerOrderHistory = _storedProcedureService.CustomerOrderHistory(customerID);
            return Request.CreateResponse(HttpStatusCode.OK, customerOrderHistory);
        }

        [HttpGet]
        public HttpResponseMessage CustomerOrderDetail(string customerId)
        {
            var customerOrderDetails = _storedProcedureService.CustomerOrderDetail(customerId);
            return Request.CreateResponse(HttpStatusCode.OK, customerOrderDetails);
        }
    }
}