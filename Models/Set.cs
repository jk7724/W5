using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    public class Set
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public string Name { get; set; }
        [Required]
        public string NativeLanguage { get; set; }
        [Required]
        public string LearnLanguage { get; set; }
        [Required]

        [Column(TypeName = "date")] //Convert DataTime to Date
        public DateTime CreateDate { get; set; }

        public List<Vocabulary> Vocabulary { get; set; }
        public List<RepetitionEvent> RepetitionEvent { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
