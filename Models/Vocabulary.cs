using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vocabulary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NativeWord { get; set; }
        [Required]
        public string LearnWord { get; set; }
        public string NativeSentence { get; set; }
        public string LearnSentence { get; set; }

        public int SetId { get; set; }

        [ForeignKey("SetId")]
        public Set Set { get; set; }
    }
}
