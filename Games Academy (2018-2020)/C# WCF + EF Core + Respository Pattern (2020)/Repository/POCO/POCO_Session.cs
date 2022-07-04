using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMOCore.Repository.Models
{
    /// <summary>
    /// This class represents a Session, which is a POCO class that holds nothing, but is referenced as 
    /// an event inside of a gamesession
    /// </summary>
   
    [Table("Game Sessions")]
    public class POCO_Session
    {
        public POCO_Session() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<POCO_Event> Events { get; set; } //Reference to events
    }
}
