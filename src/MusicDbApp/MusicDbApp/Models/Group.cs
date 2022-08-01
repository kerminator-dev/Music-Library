using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace MusicDbApp.Models
{
    public class Group
    {
        /// <summary>
        /// Идентефикатор группы
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Дата создания группы
        /// </summary>
        [Required]
        public DateOnly Created { get; set; }

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
        /// Список альбомов группы
        /// </summary>
        public virtual List<Album>? Albums { get; set; }
        #endregion
    }
}
