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
    public class ViewModelMp3 : INotifyPropertyChanged
    {
        #region Interface Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Interface Implementations

        #region Properties

        private ModelMp3 _modelMp3;
        private ICommand _loadMp3Command;
        private ICommand _browseAlbumArtCommand;
        private ICommand _saveMp3Command;
        private ICommand _clearAlbumArtCommand;
        private ICommand _downloadAlbumArtCommand;
        private IModifyMp3Metadata _modifyMp3Metadata;

        public ModelMp3 ModelMp3
        {
            get => _modelMp3;
            set
            {
                _modelMp3 = value;
                OnPropertyChanged(nameof(ModelMp3));
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


        public ICommand SaveMp3Command
        {
            get => _saveMp3Command;
            set
            {
                _saveMp3Command = value;
                OnPropertyChanged(nameof(SaveMp3Command));
            }
        }

        public ICommand LoadMp3Command
        {
            get => _loadMp3Command;
            set
            {
                _loadMp3Command = value;
                OnPropertyChanged(nameof(LoadMp3Command));
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

        public ViewModelMp3()
        {
            InitializeCommands();
            GetMp3MetadataEditor();
        }

        #endregion Constructor

        #region Initialization

        private void InitializeCommands()
        {
            LoadMp3Command = new RelayCommand(LoadMP3Command_Execute, LoadMP3Command_CanExecute);
            BrowseAlbumArtCommand = new RelayCommand(LoadAlbumArtCommand_Execute, LoadAlbumArtCommand_CanExecute);
            SaveMp3Command = new RelayCommand(SaveMP3Command_Execute, SaveMP3Command_CanExecute);
            ClearAlbumArtCommand = new RelayCommand(ClearAlbumArtCommand_Execute, ClearAlbumArtCommand_CanExecute);
            DownloadAlbumArtCommand = new RelayCommand(DownloadAlbumArtCommand_Execute, DownloadAlbumArtCommand_CanExecute);
        }

        private void GetMp3MetadataEditor()
        {
            _modifyMp3Metadata = Mp3MetadataEditorFactory.Instance.GetMp3MetadataEditor(MP3MetadataReaderTypes.Mp3MetadataReaders.Taglib);
        }

        #endregion Initialization

        #region Command Methods

        public bool DownloadAlbumArtCommand_CanExecute()
        {
            return ModelMp3 != null && (ModelMp3.AlbumArt == null || ModelMp3.AlbumArt.Length < 1) && !ModelMp3.IsBusyDownloadingAlbumArt && !ModelMp3.IsBusySavingMp3;
        }

        public void DownloadAlbumArtCommand_Execute()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DownloadAlbumArt_DoGetAlbumArt;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_DownloadAlbumArt_Completed_AssignAlbumArtToVM;

            backgroundWorker.RunWorkerAsync();
            ModelMp3.IsBusyDownloadingAlbumArt = true;
        }

        public bool ClearAlbumArtCommand_CanExecute()
        {
            return ModelMp3 != null && ModelMp3.AlbumArt != null && ModelMp3.AlbumArt.Length >= 1 && !ModelMp3.IsBusySavingMp3;
        }

        public void ClearAlbumArtCommand_Execute()
        {
            ClearAlbumArt();
        }

        public bool SaveMP3Command_CanExecute()
        {
            return ModelMp3 != null && ModelMp3.FullMp3Path != string.Empty && !ModelMp3.HasError && !ModelMp3.IsBusyDownloadingAlbumArt && !ModelMp3.IsBusySavingMp3;
        }

        public void SaveMP3Command_Execute()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_SaveMP3_DoSaveMP3;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_SaveMP3_Completed_UpdateUI_InformUser;

            backgroundWorker.RunWorkerAsync();
            ModelMp3.IsBusySavingMp3 = true;
        }

        public bool LoadAlbumArtCommand_CanExecute()
        {
            return ModelMp3 != null && ModelMp3.FullMp3Path != string.Empty && !ModelMp3.IsBusyDownloadingAlbumArt && !ModelMp3.IsBusySavingMp3;
        }

        public void LoadAlbumArtCommand_Execute()
        {
            LoadAlbumArt();
        }

        public bool LoadMP3Command_CanExecute()
        {
            bool canExecute = false;

            if (ModelMp3 == null) //If null, model instance is empty, allow user to select mp3
                canExecute = true;

            if (ModelMp3 != null && !ModelMp3.IsBusyDownloadingAlbumArt && !ModelMp3.IsBusySavingMp3) //If model instance is not empty, do not allow user to load new mp3 while current one is being saved asynchronously
                canExecute = true;

            return canExecute;
        }

        public void LoadMP3Command_Execute()
        {
            LoadMp3();
        }

        #endregion Command Methods

        #region BackgroundWorker Events

        private void BackgroundWorker_DownloadAlbumArt_DoGetAlbumArt(object sender, DoWorkEventArgs e)
        {
            try
            {
                var proxy = new Mp3MetadataEditorServiceProxy();

                var response = proxy.GetAlbumArt(ModelMp3.Artist, ModelMp3.SongTitle);

                string imageFilePath = response.DataValue.Replace('"', ' ').Replace(Path.DirectorySeparatorChar, ' ');
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
                ModelMp3.AlbumArt = albumArt;
            else
                System.Windows.Forms.MessageBox.Show($"Unable to locate album art for {ModelMp3.Artist} - {ModelMp3.SongTitle}.\nPlease verify that MP3 MP3 Editor Windows Service is running.", "No Album Art Found", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);

            ModelMp3.IsBusyDownloadingAlbumArt = false;

            CommandManager.InvalidateRequerySuggested(); //Used to force 'canExecute' methods to execute again, to update state of model instance on ViewModel
        }

        private void BackgroundWorker_SaveMP3_DoSaveMP3(object sender, DoWorkEventArgs e)
        {
            var dialogMessage = "";

            SaveMP3(ref dialogMessage);
            AddMp3ToDatabase(ref dialogMessage);

            e.Result = dialogMessage;
        }

        private void BackgroundWorker_SaveMP3_Completed_UpdateUI_InformUser(object sender, RunWorkerCompletedEventArgs e)
        {
            string dialogMessage = e.Result.ToString();

            ModelMp3.IsBusySavingMp3 = false;
            CommandManager.InvalidateRequerySuggested(); // Used to force 'canExecute' methods to execute again, to update state of model instance on ViewModel

            System.Windows.Forms.MessageBox.Show(dialogMessage, "Update Successful", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        #endregion BackgroundWorker Events

        #region Logic Methods - Interaction and manipulation of Model Instance within ViewModel

        private void ClearAll()
        {
            ModelMp3 = null;
        }

        private void ClearAlbumArt()
        {
            ModelMp3.AlbumArt = new byte[0];
        }

        private void SaveMP3(ref string dialogMessage)
        {
            try
            {
                _modifyMp3Metadata.SaveMP3(ModelMp3);
                dialogMessage += "MP3 file has been successfully updated.";
            }
            catch (IOException)
            {
                System.Windows.Forms.MessageBox.Show("This file is being accessed by another process, please kill that process and try again.", "Cannot access file", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                _modifyMp3Metadata.Dispose();
            }
        }

        private void AddMp3ToDatabase(ref string dialogMessage)
        {
            try
            {
                var proxy = new Mp3MetadataEditorServiceProxy();

                var request = new AddMp3Request();

                request.Body = new Mp3RequestIntermediaryObjectBody()
                {
                    Album = ModelMp3.Album,
                    AlbumArt = Convert.ToBase64String(ModelMp3.AlbumArt),
                    Artist = ModelMp3.Artist,
                    Comments = ModelMp3.Comments,
                    Composer = ModelMp3.Composer,
                    Genre = ModelMp3.Genre,
                    Lyrics = ModelMp3.Lyrics,
                    SongTitle = ModelMp3.SongTitle,
                    TrackNumber = ModelMp3.TrackNumber,
                    Year = ModelMp3.Year
                };

                var response = proxy.AddMP3(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    dialogMessage += "\nMP3 details has been successfully added to database.";
                }
            }
            catch (WebException e)
            {
                dialogMessage += "\nMP3 details was not saved to database. MP3 Editor Windows Service is not running.";
                dialogMessage += "\n\n" + e.Message;
            }
        }

        private void LoadMp3()
        {
            var chooseMp3 = new OpenFileDialog();

            chooseMp3.Title = "Choose MP3";
            chooseMp3.DefaultExt = ".mp3";
            chooseMp3.Filter = "MP3 Format|*.mp3";
            chooseMp3.Multiselect = false;

            bool? result = chooseMp3.ShowDialog();

            if (result ?? true)
                ModelMp3 = _modifyMp3Metadata.GetMP3Metadata(chooseMp3.FileName);
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
                ModelMp3.AlbumArt = BinaryImageConverter.ConvertToByteArray(chooseAlbumArt.FileName);
        }

        #endregion Logic Methods - Interaction and manipulation of Model Instance within ViewModel
    }
}
