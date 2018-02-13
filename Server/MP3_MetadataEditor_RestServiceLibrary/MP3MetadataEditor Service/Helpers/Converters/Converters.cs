using System.Drawing;

namespace MP3_MetadataEditor_RestServiceLibrary.MP3MetadataEditor_Service.Helpers.Converters
{
    public static class Converters
    {
        public static byte[] ConvertToByteArray(string path)
        {
            return (byte[])new ImageConverter().ConvertTo(System.Drawing.Image.FromFile(path), typeof(byte[]));
        }
    }
}
