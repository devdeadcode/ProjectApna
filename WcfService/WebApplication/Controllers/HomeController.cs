using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        #region Class level members
        private OrganizationServiceProxy proxy;
        private IOrganizationService service;
        private const string connectionUrl = "";

        private Guid accountId;
        #endregion Class level members

        #region Code Index
        public ActionResult Index()
        {   
            var connection = CrmConnection.Parse(connectionUrl);
            var service = new OrganizationService(connection);

            //Entity account = new Entity("account");

            var request = new RetrieveAllEntitiesRequest(){
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };

            var response = (RetrieveAllEntitiesResponse)service.Execute(request);
            var model = new Models.IndexModel();

            foreach (var curEntity in response.EntityMetadata.OrderBy(em => em.SchemaName))
            { 
                var entity = new Models.EntityInfo {EntityName = curEntity.SchemaName};
                model.Entities.Add(entity);
            }

        
            //ViewBag.Message = "testing";
            return View(model);
        }
        #endregion Code Index

        #region Code Entity
        public ActionResult Entity(string id)
        {
            var model = new Models.EntityInfo();
            var connection = CrmConnection.Parse(connectionUrl);
            var service = new OrganizationService(connection);
            var curEntity = new Entity(id.ToLower());
            var entity = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = curEntity.LogicalName
            };
            var response = (RetrieveEntityResponse)service.Execute(entity);

            model.EntityName = response.EntityMetadata.SchemaName;

            foreach (var curAttribute in response.EntityMetadata.Attributes.OrderBy(a => a.LogicalName))
            {
                model.AttributeNames.Add(curAttribute.LogicalName);
            }

            return View(model);
        }
        #endregion Code Entity



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}