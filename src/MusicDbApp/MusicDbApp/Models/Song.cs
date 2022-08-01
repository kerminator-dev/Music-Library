using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicDbApp.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AlbumId { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
    }
}
