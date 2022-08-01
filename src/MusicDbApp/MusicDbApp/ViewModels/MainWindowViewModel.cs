using Microsoft.EntityFrameworkCore;
using MusicDbApp.Commands;
using MusicDbApp.DbContexts;
using MusicDbApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicDbApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Контекст базы днных
        private readonly MusicDbContext _dbContext;

        // Выбранная музыкальная группа
        private Group _selectedGroup;
        // Выбранный альбом
        private Album _selectedAlbum;

        // Удалить трек
        private CommandBase? _deleteSongCommand;

        /// <summary>
        /// Список музыкальных групп
        /// При каждом обращении к этому свойству дёргается контекст БД
        /// </summary>
        public List<Group> Groups
        {
            get
            {
                return _dbContext.Groups?.Include(g => g.Albums)?.ThenInclude(a => a.Songs)?.ToList();
            }
        }

        /// <summary>
        /// Список альбомов
        /// </summary>
        public List<Album> Albums
        {
            get
            {
                return _selectedGroup?.Albums?.ToList();
            }
        }

        /// <summary>
        /// Список треков
        /// </summary>
        public List<Song> Songs
        {
            get
            {
                return _selectedAlbum?.Songs?.ToList();
            }
        }

        /// <summary>
        /// Выбранная группа
        /// </summary>
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;

                base.OnPropertyChanged(nameof(Albums));
                base.OnPropertyChanged(nameof(Songs));
            }
        }

        /// <summary>
        /// Выбранный альбом
        /// </summary>
        public Album SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;

                base.OnPropertyChanged(nameof(Songs));
            }
        }

        #region Команды
        /// <summary>
        /// Удалить трек
        /// </summary>
        public CommandBase DeleteSongCommand
        {
            get
            {
                return _deleteSongCommand ?? (_deleteSongCommand =
                    new RelayCommand
                    (
                        execute: DeleteSongCommand_Execute,
                        canExecute: (p) => { return true; }
                    ));
            }
        }

        #endregion

        public MainWindowViewModel(MusicDbContext dbContext)
        {
            _dbContext = dbContext;

            _selectedGroup = new Group()
            {
                Id = -1
            };
            _selectedAlbum = new Album()
            {
                Id = -1
            };
        }

        private void DeleteSongCommand_Execute(object parameter)
        {
            if (parameter == null)
                return;

            if (parameter is not Song)
                return;

            Song songToRemove = (Song)parameter;

            _dbContext.Songs.Remove(songToRemove);

            OnPropertyChanged(nameof(Songs));
        }
    }
}
