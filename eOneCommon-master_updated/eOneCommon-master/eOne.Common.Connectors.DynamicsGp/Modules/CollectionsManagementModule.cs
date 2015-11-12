using System.Collections.Generic;

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

        public ConnectorEntity GetCollectionsNoteEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(CollectionsManagementSmartListNotes), "Notes", ParentConnector);
            
            var cn00100 = entity.AddTable("CN00100");
            
            AddCollectionsNoteFields(cn00100);
            
            return entity;
        }
        public void AddCollectionsNoteFields(ConnectorTable cn00100)
        {
            cn00100.AddField("CUSTNMBR", "Customer number", Connector.FieldTypeIdString, true);
            cn00100.AddField("Contact_date", "Contact date", Connector.FieldTypeIdDate, true);
            cn00100.AddField("Action_Promised", "Action promised", Connector.FieldTypeIdString, true);
            var actionType = cn00100.AddField("ActionType", "Action type", Connector.FieldTypeIdEnum, true);
            actionType.AddListItems(1, new List<string> { "None", "Dispute", "Promise to pay", "Special" });
            cn00100.AddField("Action_date", "Action date", Connector.FieldTypeIdDate, true);
            cn00100.AddField("Action_Assigned_To", "Action assigned to", Connector.FieldTypeIdString, true);
            var noteStatus = cn00100.AddField("NoteStatus", "Note status", Connector.FieldTypeIdEnum, true);
            noteStatus.AddListItems(1, new List<string> { "Open", "Completed", "Cancelled", "Cancelled by plan" });
            cn00100.AddField("Amount_Promised", "Amount promised", Connector.FieldTypeIdCurrency, true);
            cn00100.AddField("USERID", "Collector", Connector.FieldTypeIdString, true);
            cn00100.AddField("Note_Display_String", "Note display string", Connector.FieldTypeIdString, true);
            cn00100.AddField("CPRCSTNM", "Corporate customer number", Connector.FieldTypeIdString);
            cn00100.AddField("DATE1", "Date", Connector.FieldTypeIdDate);
            cn00100.AddField("TIME1", "Time", Connector.FieldTypeIdTime);
            cn00100.AddField("Contact_Time", "Contact time", Connector.FieldTypeIdTime);
            cn00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            cn00100.AddField("RevisionNumber", "CN revision number", Connector.FieldTypeIdInteger);
            cn00100.AddField("CN_Group_Note", "CN group note", Connector.FieldTypeIdYesNo);
            cn00100.AddField("Caller_ID_String", "Caller ID string", Connector.FieldTypeIdString);
            cn00100.AddField("Action_Completed", "Action completed", Connector.FieldTypeIdYesNo);
            cn00100.AddField("ACTCMDSP", "Action completed sort by", Connector.FieldTypeIdYesNo);
            cn00100.AddField("Action_Completed_date", "Action completed date", Connector.FieldTypeIdDate);
            cn00100.AddField("Action_Completed_Time", "Action completed time", Connector.FieldTypeIdTime);
            cn00100.AddField("Amount_Received", "Amount received", Connector.FieldTypeIdCurrency);
            cn00100.AddField("CNTCPRSN", "Contact person", Connector.FieldTypeIdString);
            cn00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            cn00100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            cn00100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            cn00100.AddField("USRDAT01", "User defined date 1", Connector.FieldTypeIdDate);
            var priority = cn00100.AddField("PRIORT", "Note priority", Connector.FieldTypeIdEnum);
            priority.AddListItems(1, new List<string> { "Low", "Normal", "High" });
            cn00100.AddField("NOTECAT", "Note category", Connector.FieldTypeIdString);
            cn00100.AddField("Action_Cancelled_By", "Action cancelled by", Connector.FieldTypeIdString);
            cn00100.AddField("Action_Cancelled_date", "Action cancelled date", Connector.FieldTypeIdDate);
        }

    }
}
