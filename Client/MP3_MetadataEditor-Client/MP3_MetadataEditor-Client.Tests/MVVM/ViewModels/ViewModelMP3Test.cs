using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.Logic.Interface_Implementations.Factory;
using MP3_MetadataEditor_Client.MVVM.ViewModels;
using MP3_MetadataEditor_Client.MVVM.Models;

namespace MP3_MetadataEditor_Client.Tests.MVVM.ViewModels
{
    [TestClass]
    public class ViewModelMP3Test
    {
        [TestMethod]
        public void MP3MetadataReader_Returned_Via_Factory_Should_Not_Be_Null()
        {
            IMP3MetadataReader _mp3MetadataReader;
            _mp3MetadataReader =  MP3metadataReaderFactory.Instance.GetSelectedMP3metadataReader();

            Assert.IsNotNull(_mp3MetadataReader);
        }

        [TestMethod]
        public void MP3MetadataReader_Returned_Via_Factory_Should_Be_Instance_Of_IMP3metadataReader()
        {
            IMP3MetadataReader _mp3MetadataReader;
            _mp3MetadataReader = MP3metadataReaderFactory.Instance.GetSelectedMP3metadataReader();

            Assert.IsInstanceOfType(_mp3MetadataReader, typeof(IMP3MetadataReader));
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Not_Clickable_When_Model_Instance_Is_Null()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            bool clickable = viewModel.DownloadAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Not_Clickable_When_AlbumArt_Length_Is_Smaller_Than_One()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { AlbumArt = new byte[1] };

            bool clickable = viewModel.DownloadAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Not_Clickable_When_IsBusyDownloadingAlbumArt_Property_Is_Set()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { IsBusyDownloadingAlbumArt = true };

            bool clickable = viewModel.DownloadAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Setting_IsBusyDownloadingAlbumArt_Property_To_True_When_Executed()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { IsBusyDownloadingAlbumArt = false };

            viewModel.DownloadAlbumArtCommand_Execute();

            Assert.AreEqual(true, viewModel.ModelMP3.IsBusyDownloadingAlbumArt);
        }

        [TestMethod]
        public void ClearAlbumArt_Button_Not_Clickable_When_Model_Instance_Is_Null()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            bool clickable = viewModel.ClearAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void ClearAlbumArt_Button_Is_Clickable_When_AlbumArt_Property_Length_Is_One_Or_Larger()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { AlbumArt = new byte[1] };

            bool clickable = viewModel.ClearAlbumArtCommand_CanExecute();

            Assert.AreEqual(true, clickable);
        }

        [TestMethod]
        public void ClearAlbumArt_Button_Clears_AlbumArt_Property_In_Model_Instance_But_Not_To_Null()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { AlbumArt = new byte[1] { 0x20 } };

            viewModel.ClearAlbumArtCommand_Execute();

            Assert.IsNotNull(viewModel.ModelMP3.AlbumArt);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_Model_Instance_Is_Null()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_FullMP3Path_Property_Is_Empty()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { FullMP3Path = string.Empty, IsBusyDownloadingAlbumArt = false };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_IsBusyDownloadingAlbumArt_Property_Is_True()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { FullMP3Path = "test", IsBusyDownloadingAlbumArt = true };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Is_Clickable_When_IsBusyDownloadingAlbumArt_Property_Is_False_And_FullMP3Path_Property_Is_Set()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { FullMP3Path = "test", IsBusyDownloadingAlbumArt = false };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(true, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_IsBusySavingMP3_Property_Is_True()
        {
            ViewModelMP3 viewModel = new ViewModelMP3();

            viewModel.ModelMP3 = new ModelMP3() { IsBusySavingMP3 = true };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

    }
}
