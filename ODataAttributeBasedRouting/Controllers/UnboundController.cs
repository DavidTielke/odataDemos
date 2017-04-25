using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace ODataAttributeBasedRouting.Controllers
{
    public class UnboundController : ODataController
    {
        // Unbound Function
        [HttpGet]
        [ODataRoute("Add(z1={z1}, z2={z2})")]
        public IHttpActionResult Add([FromODataUri] int z1, [FromODataUri] int z2)
        {
            return Ok(z1 + z2);
        }

        // Unbound Action
        [HttpPost]
        [ODataRoute("ClearLoggingTable")]
        public IHttpActionResult Clear(ODataActionParameters paras)
        {
            return Ok();
        }
    }
}
