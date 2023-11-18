using System;

namespace Port_Morski.Models
{
    public class Operacje_Log
    {
        public int LogID { get; set; }
        public string? OldOperation { get; set; }
        public string? NewOperation { get; set; }
        public int? OldShipId { get; set; }
        public int? NewShipId { get; set; }
        public int? OldDockId { get; set; }
        public int? NewDockId { get; set; }
        public bool? OldApproved { get; set; }
        public bool? NewApproved { get; set; }
        public DateTime? OldDate { get; set; }
        public DateTime? NewDate { get; set; }
        public string OperationTypeOnTable { get; set; }
        public DateTime LogDate { get; set; }
    }
}