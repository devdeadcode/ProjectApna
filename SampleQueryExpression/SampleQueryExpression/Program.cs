using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleQueryExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entering:RetrieveMultipleWithRelatedEntityColumns");
            ////Create multiple accounts with primary contacts
            //Entity contact = new Entity("contact");
            //contact.Attributes["firstname"] = "ContactFirstName";
            //contact.Attributes["lastname"] = "ContactLastName";
            //Guid contactId = _orgService.Create(contact, null);

            //Entity account = new Entity("account");
            //account["name"] = "Test Account1";
            //EntityReference primaryContactId = new EntityReference("contact", contactId);
            //account["primarycontactid"] = primaryContactId;

            //Guid accountId1 = _orgService.Create(account, null);
            //account["name"] = "Test Account2";
            //Guid accountId2 = _orgService.Create(account, null);
            //account["name"] = "Test Account3";
            //Guid accountId3 = _orgService.Create(account, null);

            ////Create a query expression specifying the link entity alias and the columns of the link entity that you want to return
            //QueryExpression qe = new QueryExpression();
            //qe.EntityName = "account";
            //qe.ColumnSet = new ColumnSet();
            //qe.ColumnSet.Columns.Add("name");

            //qe.LinkEntities.Add(new LinkEntity("account", "contact", "primarycontactid", "contactid", JoinOperator.Inner));
            //qe.LinkEntities[0].Columns.AddColumns("firstname", "lastname");
            //qe.LinkEntities[0].EntityAlias = "primarycontact";

            //EntityCollection ec = _orgService.RetrieveMultiple(qe);

            //Console.WriteLine("Retrieved {0} entities", ec.Entities.Count);
            //foreach (Entity act in ec.Entities)
            //{
            //    Console.WriteLine("account name:" + act["name"]);
            //    Console.WriteLine("primary contact first name:" + act["primarycontact.firstname"]);
            //    Console.WriteLine("primary contact last name:" + act["primarycontact.lastname"]);
            //}
        }
    }
}
