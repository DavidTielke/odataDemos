using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using System.Web.Services.Protocols;
using ODataAttributeBasedRouting.Models;

namespace ODataAttributeBasedRouting.Controllers
{
    [ODataRoutePrefix("Persons")]
    public class PersonsController : ODataController
    {
        private List<Person> _persons;

        public PersonsController()
        {
            _persons = new List<Person>
            {
                new Person(1, "David", 32),
                new Person(2, "Lena", 30),
                new Person(3, "Maximilian", 4),
                new Person(4, "Teddy", 3),
            };
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute]
        public IQueryable<Person> Get()
        {
            return _persons.AsQueryable();
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute("({key})")]
        public SingleResult<Person> GetOne([FromODataUri] int key)
        {
            return SingleResult<Person>.Create(_persons.AsQueryable().Where(p => p.Id == key));
        }
        
        public string GetNameOfAFoo(int key)
        {
            return _persons.AsQueryable().Single(p => p.Id == key).Name;
        }

        [HttpPost]
        [ODataRoute("({key})/HS.Mark")]
        // http://localhost:15360/api/Persons(1)/HS.Mark
        public IHttpActionResult Foo([FromODataUri] int key, ODataActionParameters paras)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // ...

            return Ok();
        }

        [HttpGet]
        [ODataRoute("({key})/HS.ToLongString(prefix={prefix})")]
        public IHttpActionResult ToLongString([FromODataUri] int key, [FromODataUri] string prefix)
        {
            var foo = _persons.Single(p => p.Id == key);
            var bar = prefix + ": " + foo.Name + " " + foo.Age;
            return Ok(bar);
        }

        [HttpGet]
        [ODataRoute("HS.AllAdults")]
        public IQueryable<Person> AllAdults()
        {
            return _persons.Where(p => p.Age >= 18).AsQueryable();
        }

        [HttpGet]
        [ODataRoute("HS.AllChildren")]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Person> AllChildren()
        {
            return _persons.Where(p => p.Age < 18).AsQueryable();
        }
    }
}
