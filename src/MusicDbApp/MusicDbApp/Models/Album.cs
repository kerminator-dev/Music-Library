using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace MusicDbApp.Models
{
    public class Album
    {
        /// <summary>
        /// Идентефикатор альбома
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Дата релиза альбома
        /// </summary>
        [Required]
        public DateOnly ReleaseDate { get; set; }

        /// <summary>
        /// Относительный путь к изображению
        /// </summary>
        public virtual string ImagePath { get; set; }

        /// <summary>
        /// Абсолютный путь к изображению
        /// </summary>
        public virtual string FullImagePath => Directory.GetCurrentDirectory() + this.ImagePath;


        #region Виртуальные свойства EF
        /// <summary>
        /// Список треков альбома
        /// </summary>
        public virtual List<Song> Songs { get; set; }
        #endregion
    }
}
