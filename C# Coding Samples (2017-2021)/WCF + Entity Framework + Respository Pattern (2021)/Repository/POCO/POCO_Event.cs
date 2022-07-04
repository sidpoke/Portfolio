using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMOCore.Repository.Models
{
    /// <summary>
    /// This class represents an Event, which is a POCO class that holds information about
    /// an event inside of a gamesession
    /// </summary>

    [Table("Game Events")]
    public class POCO_Event
    {
        public POCO_Event() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        //public int LastId { get; set;}

        [Required]
        public string Name { get; set; }
        
        [Required]
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public POCO_Session Session { get; set; }
    }
}
