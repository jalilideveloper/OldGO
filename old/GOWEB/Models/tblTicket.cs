//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GOWEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTicket
    {
        public int TicketID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string Title { get; set; }
        public string AskQuestion { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public string QuestionDatetime { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> TicketStatusID { get; set; }
        public string AnswerTicket { get; set; }
        public string AnswerDateTime { get; set; }
        public Nullable<int> ParrentID { get; set; }
        public Nullable<int> SupportID { get; set; }
    
        public virtual tblPriority tblPriority { get; set; }
        public virtual tblTicketStatu tblTicketStatu { get; set; }
        public virtual tblUnit tblUnit { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
    }
}
