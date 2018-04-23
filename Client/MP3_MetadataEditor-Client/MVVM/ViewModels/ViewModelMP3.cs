using Microsoft.Win32;
using MP3_MetadataEditor_Client.Helpers.Command_Classes;
using MP3_MetadataEditor_Client.Helpers.Converters;
using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.MP3MetadataEditorService;
using MP3_MetadataEditor_Client.MVVM.Models;
using MP3_MetadataEditor_Client.Service_Communication;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Input;
using MP3_MetadataEditor_Client.MetadataReaders.Interface_Implementations;
using MP3_MetadataEditor_Client.MetadataReaders.Interface_Implementations.Factory;

namespace MP3_MetadataEditor_Client.MVVM.ViewModels
{
    public class ViewModelMP3 : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Interface Implementations

        #region Properties

        private ModelMP3 _modelMP3;
        private ICommand _loadMP3Command;
        private ICommand _browseAlbumArtCommand;
        private ICommand _saveMP3Command;
        private ICommand _clearAlbumArtCommand;
        private ICommand _downloadAlbumArtCommand;
        private IMP3MetadataReader _mp3MetadataReader;

        public ModelMP3 ModelMP3
        {
            get => _modelMP3;
            set
            {
                _modelMP3 = value;
                OnPropertyChanged(nameof(ModelMP3));
            }
        }

        public ICommand DownloadAlbumArtCommand
        {
            get => _downloadAlbumArtCommand;
            set
            {
                _downloadAlbumArtCommand = value;
                OnPropertyChanged(nameof(DownloadAlbumArtCommand));
            }
        }


        public ICommand SaveMP3Command
        {
            get => _saveMP3Command;
            set
            {
                _saveMP3Command = value;
                OnPropertyChanged(nameof(SaveMP3Command));
            }
        }

        public ICommand LoadMP3Command
        {
            get => _loadMP3Command;
            set
            {
                _loadMP3Command = value;
                OnPropertyChanged(nameof(LoadMP3Command));
            }
        }

        public ICommand BrowseAlbumArtCommand
        {
            get => _browseAlbumArtCommand;
            set
            {
                _browseAlbumArtCommand = value;
                OnPropertyChanged(nameof(BrowseAlbumArtCommand));
            }
        }

        public ICommand ClearAlbumArtCommand
        {
            get => _clearAlbumArtCommand;
            set
            {
                _clearAlbumArtCommand = value;
                OnPropertyChanged(nameof(ClearAlbumArtCommand));
            }
        }

        #endregion Properties

        #region Constructor

        public ViewModelMP3()
        {
            InitializeCommands();
            GetMP3MetadataReader();
        }

        #endregion Constructor

        #region Initialization

        private void InitializeCommands()
        {
            LoadMP3Command = new RelayCommand(LoadMP3Command_Execute, LoadMP3Command_CanExecute);
            BrowseAlbumArtCommand = new RelayCommand(LoadAlbumArtCommand_Execute, LoadAlbumArtCommand_CanExecute);
            SaveMP3Command = new RelayCommand(SaveMP3Command_Execute, SaveMP3Command_CanExecute);
            ClearAlbumArtCommand = new RelayCommand(ClearAlbumArtCommand_Execute, ClearAlbumArtCommand_CanExecute);
            DownloadAlbumArtCommand = new RelayCommand(DownloadAlbumArtCommand_Execute, DownloadAlbumArtCommand_CanExecute);
        }

        private void GetMP3MetadataReader()
        {
            _mp3MetadataReader = Mp3MetadataReaderFactory.Instance.GetMp3MetadataReader((int)MP3MetadataReaderTypes.Mp3MetadataReaders.Taglib);
        }

        #endregion Initialization

        #region Command Methods

        public bool DownloadAlbumArtCommand_CanExecute()
        {
            bool canExecute = ModelMP3 != null && (ModelMP3.AlbumArt == null || ModelMP3.AlbumArt.Length < 1) && !ModelMP3.IsBusyDownloadingAlbumArt && !ModelMP3.IsBusySavingMP3;

            return canExecute;
        }

        public void DownloadAlbumArtCommand_Execute()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DownloadAlbumArt_DoGetAlbumArt;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_DownloadAlbumArt_Completed_AssignAlbumArtToVM;

            backgroundWorker.RunWorkerAsync();
            ModelMP3.IsBusyDownloadingAlbumArt = true;
        }

        public bool ClearAlbumArtCommand_CanExecute()
        {
            bool canExecute = ModelMP3 != null && ModelMP3.AlbumArt != null && ModelMP3.AlbumArt.Length >= 1 && !ModelMP3.IsBusySavingMP3;

            return canExecute;
        }

        public void ClearAlbumArtCommand_Execute()
        {
            ClearAlbumArt();
        }

        public bool SaveMP3Command_CanExecute()
        {
            bool canExecute = ModelMP3 != null && ModelMP3.FullMP3Path != string.Empty && !ModelMP3.HasError && !ModelMP3.IsBusyDownloadingAlbumArt && !ModelMP3.IsBusySavingMP3;

            return canExecute;
        }

        public void SaveMP3Command_Execute()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_SaveMP3_DoSaveMP3;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_SaveMP3_Completed_UpdateUI_InformUser;

            backgroundWorker.RunWorkerAsync();
            ModelMP3.IsBusySavingMP3 = true;
        }

        public bool LoadAlbumArtCommand_CanExecute()
        {
            bool canExecute = ModelMP3 != null && ModelMP3.FullMP3Path != string.Empty && !ModelMP3.IsBusyDownloadingAlbumArt && !ModelMP3.IsBusySavingMP3;

            return canExecute;
        }

        public void LoadAlbumArtCommand_Execute()
        {
            LoadAlbumArt();
        }

        public bool LoadMP3Command_CanExecute()
        {
            bool canExecute = false;

            if (ModelMP3 == null) //If null, model instance is empty, allow user to select mp3
                canExecute = true;

            if (ModelMP3 != null && !ModelMP3.IsBusyDownloadingAlbumArt && !ModelMP3.IsBusySavingMP3) //If model instance is not empty, do not allow user to load new mp3 while current one is being saved asynchronously
                canExecute = true;

            return canExecute;
        }

        public void LoadMP3Command_Execute()
        {
            LoadMP3();
        }

        #endregion Command Methods

        #region BackgroundWorker Events

        private void BackgroundWorker_DownloadAlbumArt_DoGetAlbumArt(object sender, DoWorkEventArgs e)
        {
            try
            {
                var proxy = new Mp3MetadataEditorServiceProxy();
                string imageFilePath = proxy.GetAlbumArt(ModelMP3.Artist, ModelMP3.SongTitle).Replace('"', ' ').Replace(Path.DirectorySeparatorChar, ' ');
                byte[] image = Convert.FromBase64String(imageFilePath);
                e.Result = image;
            }
            catch (WebException)
            {
            }

        }

        private void BackgroundWorker_DownloadAlbumArt_Completed_AssignAlbumArtToVM(object sender, RunWorkerCompletedEventArgs e)
        {
            var albumArt = (byte[])e.Result;

            if (e.Error == null && albumArt?.Length > 1)
                ModelMP3.AlbumArt = albumArt;
            else
                System.Windows.Forms.MessageBox.Show(
                    $"Unable to locate album art for {ModelMP3.Artist} - {ModelMP3.SongTitle}.\nPlease verify that MP3 Metadata Editor Windows Service is running.", "No Album Art Found", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            ModelMP3.IsBusyDownloadingAlbumArt = false;

            CommandManager.InvalidateRequerySuggested(); //Used to force 'canExecute' methods to execute again, to update state of model instance on ViewModel
        }

        private void BackgroundWorker_SaveMP3_DoSaveMP3(object sender, DoWorkEventArgs e)
        {
            var dialogMessage = "";

            SaveMP3(ref dialogMessage);
            AddMP3ToDatabase(ref dialogMessage);

            e.Result = dialogMessage;
        }

        private void BackgroundWorker_SaveMP3_Completed_UpdateUI_InformUser(object sender, RunWorkerCompletedEventArgs e)
        {
            string dialogMessage = e.Result.ToString();

            ModelMP3.IsBusySavingMP3 = false;
            CommandManager.InvalidateRequerySuggested(); // Used to force 'canExecute' methods to execute again, to update state of model instance on ViewModel

            System.Windows.Forms.MessageBox.Show(dialogMessage, "Update Successful", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        #endregion BackgroundWorker Events

        #region Logic Methods - Interaction and manipulation of Model Instance within ViewModel

        private void ClearAll()
        {
            ModelMP3 = null;
        }

        private void ClearAlbumArt()
        {
            ModelMP3.AlbumArt = new byte[0];
        }

        private void SaveMP3(ref string dialogMessage)
        {
            try
            {
                _mp3MetadataReader.SaveMP3(ModelMP3);
                dialogMessage += "MP3 file has been successfully updated.";
            }
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show("This file is being accessed by another process, please kill that process and try again.", "Cannot access file", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                _mp3MetadataReader.Dispose();
            }
        }

        private void AddMP3ToDatabase(ref string dialogMessage)
        {
            try
            {
                var proxy = new Mp3MetadataEditorServiceProxy();
                var request = new Mp3MetadataEditorServiceRequest();
                request.Body = new Body()
                {
                    Album = ModelMP3.Album,
                    AlbumArt = Convert.ToBase64String(ModelMP3.AlbumArt),
                    Artist = ModelMP3.Artist,
                    Comments = ModelMP3.Comments,
                    Composer = ModelMP3.Composer,
                    Genre = ModelMP3.Genre,
                    Lyrics = ModelMP3.Lyrics,
                    SongTitle = ModelMP3.SongTitle,
                    TrackNumber = ModelMP3.TrackNumber,
                    Year = ModelMP3.Year
                };

                if (proxy.AddMp3(request).StatusCode == HttpStatusCode.OK)
                    dialogMessage += "\nMP3 details has been successfully added to database.";
            }
            catch (WebException)
            {
                //dialogMessage += "\nMP3 details was not saved to database. MP3 Metadata Editor Windows Service is not running.";
            }


        }

        private void LoadMP3()
        {
            var chooseMP3 = new OpenFileDialog();

            chooseMP3.Title = "Choose MP3";
            chooseMP3.DefaultExt = ".mp3";
            chooseMP3.Filter = "MP3 Format|*.mp3";
            chooseMP3.Multiselect = false;

            bool? result = chooseMP3.ShowDialog();

            if (result ?? true)
                ModelMP3 = _mp3MetadataReader.GetMP3Metadata(chooseMP3.FileName, chooseMP3.SafeFileName);
        }

        private void LoadAlbumArt()
        {
            var chooseAlbumArt = new OpenFileDialog();

            chooseAlbumArt.Title = "Choose Album Art";
            chooseAlbumArt.DefaultExt = ".jpg";
            chooseAlbumArt.Filter = "Jpeg Format|*.jpg";
            chooseAlbumArt.Multiselect = false;

            bool? result = chooseAlbumArt.ShowDialog();

            if (result ?? true)
                ModelMP3.AlbumArt = BinaryImageConverter.ConvertToByteArray(chooseAlbumArt.FileName);
        }

        #endregion Logic Methods - Interaction and manipulation of Model Instance within ViewModel
    }
}
