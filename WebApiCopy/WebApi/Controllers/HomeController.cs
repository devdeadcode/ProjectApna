using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private const string CrmConnectionUrl = @"Url=https://eone.crm.dynamics.com; Username=lahiru.kariyawasamhe@eonesolutions.com; Password=y2mgyL@123;";

        public ActionResult Index()
        {
            var model = new IndexModel();
            var connection = CrmConnection.Parse(CrmConnectionUrl);
            var service = new OrganizationService(connection);


            var request = new RetrieveAllEntitiesRequest
            {
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true,

            };

            var response = (RetrieveAllEntitiesResponse)service.Execute(request);
            foreach (var curEntity in response.EntityMetadata.OrderBy(em => em.SchemaName))
            {
                var entity = new EntityInfo { EntityName = curEntity.SchemaName };

                model.Entities.Add(entity);
            }
            return View(model);
        }

        public ActionResult Entity(string id)
        {
            var model = new EntityInfo();
            var connection = CrmConnection.Parse(CrmConnectionUrl);
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
                model.Attributes.Add(new EntityObject() {AttributeName = curAttribute.LogicalName, AttributeValue = curAttribute.DisplayName.ToString() });
            }

            return View(model);
        }

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


    public class IndexModel
    {
        public List<EntityInfo> Entities { get; set; }

        public IndexModel()
        {
            Entities = new List<EntityInfo>();
        }
    }

    public class EntityInfo
    {
        public string EntityName { get; set; }
        public List<EntityObject> Attributes { get; set; }
        

        public EntityInfo()
        {
            Attributes = new List<EntityObject>();
            

        }
    }

    public class EntityObject
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}