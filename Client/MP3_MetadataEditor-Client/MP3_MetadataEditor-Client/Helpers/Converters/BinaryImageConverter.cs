using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Drawing;
using TagLib;

namespace MP3_MetadataEditor_Client.Helpers.Converters
{
    public class BinaryImageConverter : IValueConverter
    {
        /// <summary>
        /// Converter used in XAML when showing Album Art
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is byte[])
            {
                byte[] bytes = value as byte[];

                if (bytes.Length > 1)
                {
                    MemoryStream stream = new MemoryStream(bytes);

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();

                    return image;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Part of Converter used in XAML to display Album Art
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(value, typeof(byte[]));
        }

        /// <summary>
        /// Used to convert to correct type before inserting album art into MP3
        /// </summary>
        public static IPicture ConvertToIPicture(byte[] file)
        {
            if (file != null)
            {
                return new Picture(new ByteVector(file));
            }
            return new Picture() { };
        }

        /// <summary>
        /// Used to convert the image a byte array when loading into the ViewModel
        /// </summary>
        public static byte[] ConvertToByteArray(string filePath)
        {
            return (byte[])new ImageConverter().ConvertTo(Image.FromFile(filePath), typeof(byte[]));
        }
    }
}
