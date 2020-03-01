using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WP.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }
        [Required]
        [Display(Name = "Wedder One")]
        [MinLength(2, ErrorMessage = "Field has to contain at least 2 characters and more")]
        public string WedderOne { get; set; }
        [Required]
        [Display(Name = "Wedder Two")]
        [MinLength(2, ErrorMessage = "Field has to contain at least 2 characters and more")]
        public string WedderTwo { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string Address { get; set; }
        public User Creator {get;set;}
        public int CreatorId {get;set;}
        public List<RSVP> Guests {get;set;}
        

    }
}