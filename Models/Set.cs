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
        public string NativeLanguage { get; set; }
        public string LearnLanguage { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime Repeat1 { get; set; }
        [Required]
        public DateTime Repeat2 { get; set; }
        [Required]
        public DateTime Repeat3 { get; set; }
        [Required]
        public DateTime Repeat4 { get; set; }
        [Required]
        public DateTime Repeat5 { get; set; }
        [Required]
        public bool Repeat1Flag { get; set; }
        [Required]
        public bool Repeat2Flag { get; set; }
        [Required]
        public bool Repeat3Flag { get; set; }
        [Required]
        public bool Repeat4Flag { get; set; }
        [Required]
        public bool Repeat5Flag { get; set; }

        public List<Vocabulary> Vocabulary { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
