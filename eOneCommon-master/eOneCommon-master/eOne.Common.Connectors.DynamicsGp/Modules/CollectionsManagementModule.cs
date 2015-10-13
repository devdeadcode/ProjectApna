using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class CollectionsManagementModule : DynamicsGpModule
    {

        private const short CollectionsManagementSmartListNotes = 1;
        
        public CollectionsManagementModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 1157;
            Name = "Collections management";
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            Entities.Add(GetCollectionsNoteEntity());
        }

        public DataConnectorEntity GetCollectionsNoteEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(CollectionsManagementSmartListNotes), "Notes", ParentConnector);
            
            var cn00100 = entity.AddTable("CN00100");
            
            AddCollectionsNoteFields(cn00100);
            
            return entity;
        }
        public void AddCollectionsNoteFields(DataConnectorTable cn00100)
        {
            cn00100.AddField("CUSTNMBR", "Customer Number", DataConnector.FieldTypeIdString, true);
            cn00100.AddField("Contact_Date", "Contact Date", DataConnector.FieldTypeIdDate, true);
            cn00100.AddField("Action_Promised", "Action Promised", DataConnector.FieldTypeIdString, true);
            var actionType = cn00100.AddField("ActionType", "Action Type", DataConnector.FieldTypeIdEnum, true);
            actionType.AddListItems(1, new List<string> { "None", "Dispute", "Promise To Pay", "Special" });
            cn00100.AddField("Action_Date", "Action Date", DataConnector.FieldTypeIdDate, true);
            cn00100.AddField("Action_Assigned_To", "Action Assigned To", DataConnector.FieldTypeIdString, true);
            var noteStatus = cn00100.AddField("NoteStatus", "NoteStatus", DataConnector.FieldTypeIdEnum, true);
            noteStatus.AddListItems(1, new List<string> { "Open", "Completed", "Cancelled", "Cancelled By Plan" });
            cn00100.AddField("Amount_Promised", "Amount Promised", DataConnector.FieldTypeIdCurrency, true);
            cn00100.AddField("USERID", "Collector", DataConnector.FieldTypeIdString, true);
            cn00100.AddField("Note_Display_String", "Note Display String", DataConnector.FieldTypeIdString, true);
            cn00100.AddField("CPRCSTNM", "Corporate Customer Number", DataConnector.FieldTypeIdString);
            cn00100.AddField("DATE1", "Date", DataConnector.FieldTypeIdDate);
            cn00100.AddField("TIME1", "Time", DataConnector.FieldTypeIdTime);
            cn00100.AddField("Contact_Time", "Contact Time", DataConnector.FieldTypeIdTime);
            cn00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            cn00100.AddField("RevisionNumber", "CN Revision Number", DataConnector.FieldTypeIdInteger);
            cn00100.AddField("CN_Group_Note", "CN Group Note", DataConnector.FieldTypeIdYesNo);
            cn00100.AddField("Caller_ID_String", "Caller ID String", DataConnector.FieldTypeIdString);
            cn00100.AddField("Action_Completed", "Action Completed", DataConnector.FieldTypeIdYesNo);
            cn00100.AddField("ACTCMDSP", "Action Completed Sort By", DataConnector.FieldTypeIdYesNo);
            cn00100.AddField("Action_Completed_Date", "Action Completed Date", DataConnector.FieldTypeIdDate);
            cn00100.AddField("Action_Completed_Time", "Action Completed Time", DataConnector.FieldTypeIdTime);
            cn00100.AddField("Amount_Received", "Amount Received", DataConnector.FieldTypeIdCurrency);
            cn00100.AddField("CNTCPRSN", "Contact Person", DataConnector.FieldTypeIdString);
            cn00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            cn00100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            cn00100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            cn00100.AddField("USRDAT01", "User Defined Date 1", DataConnector.FieldTypeIdDate);
            var priority = cn00100.AddField("PRIORT", "Note Priority", DataConnector.FieldTypeIdEnum);
            priority.AddListItems(1, new List<string> { "Low", "Normal", "High" });
            cn00100.AddField("NOTECAT", "Note Category", DataConnector.FieldTypeIdString);
            cn00100.AddField("Action_Cancelled_By", "Action Cancelled By", DataConnector.FieldTypeIdString);
            cn00100.AddField("Action_Cancelled_Date", "Action Cancelled Date", DataConnector.FieldTypeIdDate);
        }

    }
}
