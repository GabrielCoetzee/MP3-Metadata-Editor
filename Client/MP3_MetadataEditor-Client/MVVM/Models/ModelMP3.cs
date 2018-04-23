using System.Collections.Generic;
using System.ComponentModel;

namespace MP3_MetadataEditor_Client.MVVM.Models
{
    public class ModelMP3 : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Interface Implemenations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string property]
        {
            get
            {
                string error = string.Empty;

                switch (property)
                {
                    case nameof(Year):
                        if (string.IsNullOrEmpty(_year.ToString()) || string.IsNullOrWhiteSpace(_year.ToString()) || _year.ToString().Length != 4)
                            error = Helpers.Constants.ValidationErrors.Year;
                        break;
                    case nameof(TrackNumber):
                        if (string.IsNullOrEmpty(_trackNumber.ToString()) || string.IsNullOrWhiteSpace(_trackNumber.ToString()) || _trackNumber == 0)
                            error = Helpers.Constants.ValidationErrors.TrackNumber;
                        break;
                    case nameof(Artist):
                        if (string.IsNullOrEmpty(_artist))
                            error = Helpers.Constants.ValidationErrors.Artist;
                        break;
                    case nameof(SongTitle):
                        if (string.IsNullOrEmpty(_songTitle))
                            error = Helpers.Constants.ValidationErrors.SongTitle;
                        break;
                    default:
                        error = string.Empty;
                        break;
                }

                if (!string.IsNullOrEmpty(error) && !_errorList.ContainsKey(property))
                    _errorList.Add(property, error);
                if (string.IsNullOrEmpty(error) && _errorList.ContainsKey(property))
                    _errorList.Remove(property);

                return error;
            }
        }

        #endregion Interface Implemenations

        #region Properties

        private byte[] _albumArt;
        private string _album;
        private string _artist;
        private string _genre;
        private string _comments;
        private uint? _trackNumber;
        private uint? _year;
        private string _lyrics;
        private string _composer;
        private string _songTitle;
        private string _fullMP3Path;
        private string _displayMP3Path;
        private bool _isBusyDownloadingAlbumArt;
        private bool _isBusySavingMP3;

        private Dictionary<string, string> _errorList = new Dictionary<string, string>() { };

        public bool HasError
        {
            get
            {
                return _errorList.Count > 0;
            }
        }

        public bool IsBusySavingMP3
        {
            get { return _isBusySavingMP3; }
            set
            {
                _isBusySavingMP3 = value;
                OnPropertyChanged(nameof(IsBusySavingMP3));
            }
        }


        public bool IsBusyDownloadingAlbumArt
        {
            get { return _isBusyDownloadingAlbumArt; }
            set
            {
                _isBusyDownloadingAlbumArt = value;
                OnPropertyChanged(nameof(IsBusyDownloadingAlbumArt));
            }
        }

        public string DisplayMP3Path
        {
            get { return _displayMP3Path; }
            set
            {
                _displayMP3Path = value;
                OnPropertyChanged(nameof(DisplayMP3Path));
            }
        }

        public string FullMP3Path
        {
            get { return _fullMP3Path; }
            set
            {
                _fullMP3Path = value;
                OnPropertyChanged(nameof(FullMP3Path));
            }
        }

        public byte[] AlbumArt
        {
            get { return _albumArt; }
            set
            {
                _albumArt = value;
                OnPropertyChanged(nameof(AlbumArt));
            }
        }

        public string Album
        {
            get { return _album; }
            set
            {
                _album = value;
                OnPropertyChanged(nameof(Album));
            }
        }
        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                OnPropertyChanged(nameof(Artist));
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public uint? TrackNumber
        {
            get { return _trackNumber; }
            set
            {
                _trackNumber = value;
                OnPropertyChanged(nameof(TrackNumber));
            }
        }

        public uint? Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        public string Lyrics
        {
            get { return _lyrics; }
            set
            {
                _lyrics = value;
                OnPropertyChanged(nameof(Lyrics));
            }
        }

        public string Composer
        {
            get { return _composer; }
            set
            {
                _composer = value;
                OnPropertyChanged(nameof(Composer));
            }
        }

        public string SongTitle
        {
            get { return _songTitle; }
            set
            {
                _songTitle = value;
                OnPropertyChanged(nameof(SongTitle));
            }
        }

        #endregion Properties
    }
}
