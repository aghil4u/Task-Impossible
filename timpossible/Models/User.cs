using System;
using System.ComponentModel.DataAnnotations;

namespace timpossible.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public String Gender { get; set; }

        public string Bio { get; set; }

        public string BillingAddress { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public bool Tasker { get; set; }

        public bool Verified { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Base { get; set; }

        public string Location { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public string Status { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime LastVisitedDate { get; set; }

        public string BaseCurrency { get; set; }

        public string Qualifications { get; set; }

        public string Capabilities { get; set; }

        public string DisplayImage { get; set; }

        public int OverallRating { get; set; }

        public int TasksCompleted { get; set; }

        public string Grade { get; set; }


        
    }
}