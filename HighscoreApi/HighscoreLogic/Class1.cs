using System;
using System.ComponentModel.DataAnnotations;

namespace HighscoreLogic
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        [MaxLength(3)]
        public String PName { get; set; }
        [Required]
        public int Score { get; set; }


    }
}
