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

        [Column(TypeName = "date")]
        public DateTime Repeat1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime Repeat2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime Repeat3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime Repeat4 { get; set; }
        [Column(TypeName = "date")]
        public DateTime Repeat5 { get; set; }
       
        public bool Repeat1Flag { get; set; }
       
        public bool Repeat2Flag { get; set; }
        
        public bool Repeat3Flag { get; set; }
       
        public bool Repeat4Flag { get; set; }
      
        public bool Repeat5Flag { get; set; }

        public List<Vocabulary> Vocabulary { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
