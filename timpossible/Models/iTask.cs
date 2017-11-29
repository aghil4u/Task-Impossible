using System;
using System.ComponentModel.DataAnnotations;

namespace timpossible.Models
{
    public class iTask
    {
        [Required]
        public int Id { get; set; }

        public string Owner { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public double Radius { get; set; }

        public string NegotiationMarker { get; set; }

        public string Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime TargetDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public double Renumeration { get; set; }

        public string PaymentTerms { get; set; }

        public string Currency { get; set; }

        public string CoverPhoto { get; set; }
    }
}