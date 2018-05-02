using System.IO;
using MP3_MetadataEditor_Client.Helpers.Converters;
using MP3_MetadataEditor_Client.Logic.Interfaces;
using MP3_MetadataEditor_Client.MVVM.Models;
using TagLib;
using File = TagLib.File;

namespace MP3_MetadataEditor_Client.Logic.Interface_Implementations
{
    public class TaglibMp3MetadataEditorWrapper : IModifyMp3Metadata
    {
        #region Properties

        private File _taglibMp3MetadataEditor;

        public ModelMP3 MP3 { get; set; } = new ModelMP3() { };

        #endregion

        #region Interface Implementations

        public void Dispose()
        {
            _taglibMp3MetadataEditor.Dispose();
        }

        public ModelMP3 GetMP3Metadata(string path)
        {
            _taglibMp3MetadataEditor = File.Create(path);

            MP3.AlbumArt = _taglibMp3MetadataEditor.Tag.Pictures.Length >= 1 ? _taglibMp3MetadataEditor.Tag.Pictures[0].Data.Data : null;
            MP3.Album = _taglibMp3MetadataEditor.Tag.Album;
            MP3.Artist = _taglibMp3MetadataEditor.Tag.FirstPerformer;
            MP3.Genre = _taglibMp3MetadataEditor.Tag.FirstGenre;
            MP3.Comments = _taglibMp3MetadataEditor.Tag.Comment;
            MP3.TrackNumber = _taglibMp3MetadataEditor.Tag.Track;
            MP3.Year = _taglibMp3MetadataEditor.Tag.Year;
            MP3.Lyrics = _taglibMp3MetadataEditor.Tag.Lyrics;
            MP3.Composer = _taglibMp3MetadataEditor.Tag.FirstComposer;
            MP3.SongTitle = _taglibMp3MetadataEditor.Tag.Title;
            MP3.FullMP3Path = path;
            MP3.DisplayMP3Path = Path.GetFileName(path);

            return MP3;
        }

        public void SaveMP3(ModelMP3 metadata)
        {
            SetMP3Metadata(metadata);
            _taglibMp3MetadataEditor.Save();
        }

        private void SetMP3Metadata(ModelMP3 metadata)
        {
            _taglibMp3MetadataEditor.Tag.AlbumArtists = new string[] { metadata.Artist != null ? metadata.Artist : "" };
            _taglibMp3MetadataEditor.Tag.Composers = new string[] { metadata.Composer != null ? metadata.Composer : "" };
            _taglibMp3MetadataEditor.Tag.Genres = new string[] { metadata.Genre != null ? metadata.Genre : "" };
            _taglibMp3MetadataEditor.Tag.Performers = new string[] { metadata.Artist != null ? metadata.Artist : "" };
            _taglibMp3MetadataEditor.Tag.Lyrics = metadata.Lyrics;
            _taglibMp3MetadataEditor.Tag.Pictures = new IPicture[] { metadata.AlbumArt != null ? BinaryImageConverter.ConvertToIPicture(metadata.AlbumArt) : new Picture { } };
            _taglibMp3MetadataEditor.Tag.Album = metadata.Album;
            _taglibMp3MetadataEditor.Tag.Comment = metadata.Comments;
            _taglibMp3MetadataEditor.Tag.Title = metadata.SongTitle;
            _taglibMp3MetadataEditor.Tag.Track = (uint)metadata.TrackNumber;
            _taglibMp3MetadataEditor.Tag.Year = (uint)metadata.Year;
        }

        #endregion
    }
}
