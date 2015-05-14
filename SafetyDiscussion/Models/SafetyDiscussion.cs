using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrototypeApp.Models
{
    public class SafetyDiscussion
    {
        [Key]
        public int Id { get; set; }
        public string Observer { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Colleague { get; set; }
        public string Subject { get; set; }
        public string Outcomes { get; set; }
    }
}