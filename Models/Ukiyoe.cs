using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ukiyoe
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        public int? PublishDate { get; set; }
        public DateTime Posted {  get; set; }
    }
}
