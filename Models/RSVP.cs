using System;
using System.ComponentModel.DataAnnotations;

namespace WP.Models
{
    public class RSVP{
        [Key]
        public int RSVPId {get;set;}
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        public Wedding wedding {get;set;}
        public User user {get;set;}
    }
}