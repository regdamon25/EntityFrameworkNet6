using EntityFrameworkNet6.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace EntityFrameworkNet6.Data
{
    internal class AuditEntry
    {
        public AuditEntry(EntityEntry entityEntry)
        {
            EntityEntry = entityEntry;
        }

        public EntityEntry EntityEntry { get; }

        public string Action { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; set; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()// This is building out our Audit Object
        {
            var audit = new Audit
            {
                DateTime = System.DateTime.Now,
                TableName = TableName,
                KeyValues = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues),
                Action = Action
            };
            return audit;
        }
    }
}