using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.MetadataReaders.Interface_Implementations;
using MP3_MetadataEditor_Client.MetadataReaders.Interface_Implementations.Factory;
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
            IModifyMp3Metadata modifyMp3Metadata =  Mp3MetadataEditorFactory.Instance.GetMp3MetadataEditor(MP3MetadataReaderTypes.Mp3MetadataReaders.Taglib);

            Assert.IsNotNull(modifyMp3Metadata);
        }

        [TestMethod]
        public void MP3MetadataReader_Returned_Via_Factory_Should_Be_Instance_Of_IMP3metadataReader()
        {
            IModifyMp3Metadata modifyMp3Metadata = Mp3MetadataEditorFactory.Instance.GetMp3MetadataEditor(MP3MetadataReaderTypes.Mp3MetadataReaders.Taglib);

            Assert.IsInstanceOfType(modifyMp3Metadata, typeof(IModifyMp3Metadata));
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Not_Clickable_When_Model_Instance_Is_Null()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            bool clickable = viewModel.DownloadAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Not_Clickable_When_AlbumArt_Length_Is_Smaller_Than_One()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { AlbumArt = new byte[1] };

            bool clickable = viewModel.DownloadAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Not_Clickable_When_IsBusyDownloadingAlbumArt_Property_Is_Set()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { IsBusyDownloadingAlbumArt = true };

            bool clickable = viewModel.DownloadAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void DownloadAlbumArt_Button_Setting_IsBusyDownloadingAlbumArt_Property_To_True_When_Executed()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { IsBusyDownloadingAlbumArt = false };

            viewModel.DownloadAlbumArtCommand_Execute();

            Assert.AreEqual(true, viewModel.ModelMp3.IsBusyDownloadingAlbumArt);
        }

        [TestMethod]
        public void ClearAlbumArt_Button_Not_Clickable_When_Model_Instance_Is_Null()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            bool clickable = viewModel.ClearAlbumArtCommand_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void ClearAlbumArt_Button_Is_Clickable_When_AlbumArt_Property_Length_Is_One_Or_Larger()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { AlbumArt = new byte[1] };

            bool clickable = viewModel.ClearAlbumArtCommand_CanExecute();

            Assert.AreEqual(true, clickable);
        }

        [TestMethod]
        public void ClearAlbumArt_Button_Clears_AlbumArt_Property_In_Model_Instance_But_Not_To_Null()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { AlbumArt = new byte[1] { 0x20 } };

            viewModel.ClearAlbumArtCommand_Execute();

            Assert.IsNotNull(viewModel.ModelMp3.AlbumArt);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_Model_Instance_Is_Null()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_FullMP3Path_Property_Is_Empty()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { FullMp3Path = string.Empty, IsBusyDownloadingAlbumArt = false };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_IsBusyDownloadingAlbumArt_Property_Is_True()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { FullMp3Path = "test", IsBusyDownloadingAlbumArt = true };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Is_Clickable_When_IsBusyDownloadingAlbumArt_Property_Is_False_And_FullMP3Path_Property_Is_Set()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { FullMp3Path = "test", IsBusyDownloadingAlbumArt = false };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(true, clickable);
        }

        [TestMethod]
        public void SaveMP3_Button_Not_Clickable_When_IsBusySavingMP3_Property_Is_True()
        {
            ViewModelMp3 viewModel = new ViewModelMp3();

            viewModel.ModelMp3 = new ModelMp3() { IsBusySavingMp3 = true };

            bool clickable = viewModel.SaveMP3Command_CanExecute();

            Assert.AreEqual(false, clickable);
        }

    }
}
