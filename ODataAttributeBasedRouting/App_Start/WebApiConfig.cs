using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using ODataAttributeBasedRouting.Models;

namespace ODataAttributeBasedRouting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", "api", CreateModel());
        }

        private static IEdmModel CreateModel()
        {
            var builder = new ODataModelBuilder();
            builder.Namespace = "HS";

            var personType = builder.EntityType<Person>()
                .HasKey(p => p.Id);
            personType.Property(p => p.Name);
            personType.Property(p => p.Age);
            personType.OrderBy().Select().Filter().Expand().Count();

            personType.Action("Mark").Parameter<string>("comment");

            // Entity Function
            var function = personType
                .Function("ToLongString");
            function.Parameter<string>("prefix");
            function.Returns<string>();

            // Collection Functions
            var allAdultsFunction = personType
                .Collection
                .Function("AllAdults");
            allAdultsFunction.ReturnsCollectionFromEntitySet<Person>("Persons");

            var allChildrenFunction = personType
                .Collection
                .Function("AllChildren");
            allChildrenFunction.ReturnsCollectionFromEntitySet<Person>("Persons");

            // Unbound Function
            var addFunction = builder.Function("Add");
            addFunction.Parameter<int>("z1");
            addFunction.Parameter<int>("z2");
            addFunction.Returns<int>();

            // Unbound Action
            var clearAction = builder.Action("ClearLoggingTable");
            clearAction.Parameter<int>("days");

            builder.EntitySet<Person>("Persons");

            return builder.GetEdmModel();
        }
    }
}
