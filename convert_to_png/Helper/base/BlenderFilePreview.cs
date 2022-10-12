using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace convert_to_png
{
    public enum ColorMode
    {
        ColorMode_None,
        ColorMode_BGRA8
    }

    public class BlenderFilePreview
    {
        private BlenderFilePreview()
        {
            this.ColorMode = ColorMode.ColorMode_None;
        }

        public static BlenderFilePreview CreatePreviewFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Cannot read preview for " + fileName + ". File doesn't exist!");
            }
            BlenderFilePreview preview = new BlenderFilePreview();
            Stream? destination = null;
            FileStream? stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                destination = stream;
                byte[] buffer = new byte[4];
                stream.Read(buffer, 0, 3);
                if (((buffer[0] == 0x1f) && (buffer[1] == 0x8b)) && (buffer[2] == 8))
                {
                    stream.Seek(0L, SeekOrigin.Begin);
                    using (GZipStream stream3 = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        destination = new MemoryStream();
                        stream3.CopyTo(destination);
                        stream.Close();
                    }
                }
                destination.Seek(9L, SeekOrigin.Begin);
                destination.Read(buffer, 0, 3);
                if (Convert.ToInt32(Encoding.UTF8.GetString(buffer)) >= 250)
                {
                    int num6;
                    destination.Seek(7L, SeekOrigin.Begin);
                    destination.Read(buffer, 0, 1);
                    int num2 = (buffer[0] == 0x5f) ? 4 : 8;
                    int num3 = num2 + 0x10;
                    destination.Seek(12L, SeekOrigin.Begin);
                    int num4 = 12;
                    while (true)
                    {
                        if (destination.Read(buffer, 0, 4) == 0)
                        {
                            return preview;
                        }
                        string str = Encoding.UTF8.GetString(buffer);
                        destination.Read(buffer, 0, 4);
                        num6 = BitConverter.ToInt32(buffer, 0);
                        if (str == "TEST")
                        {
                            break;
                        }
                        num4 += num3 + num6;
                        destination.Seek((long) num4, SeekOrigin.Begin);
                    }
                    destination.Seek((long) (num4 + num3), SeekOrigin.Begin);
                    destination.Read(buffer, 0, 4);
                    int num7 = BitConverter.ToInt32(buffer, 0);
                    destination.Read(buffer, 0, 4);
                    int num8 = BitConverter.ToInt32(buffer, 0);
                    num6 -= 8;
                    buffer = new byte[num6];
                    destination.Read(buffer, 0, num6);
                    preview.Pixels = new byte[num6];
                    for (int i = 0; i < num8; i++)
                    {
                        for (int j = 0; j < num7; j++)
                        {
                            int index = (j + (i * num7)) * 4;
                            int num12 = (j + (((num8 - i) - 1) * num7)) * 4;
                            preview.Pixels[index] = buffer[num12 + 2];
                            preview.Pixels[index + 1] = buffer[num12 + 1];
                            preview.Pixels[index + 2] = buffer[num12];
                            preview.Pixels[index + 3] = 0xff;
                        }
                    }
                    preview.ColorMode = ColorMode.ColorMode_BGRA8;
                    preview.BitsPerPixel = 0x20;
                    preview.BytesPerPixel = 4;
                    preview.Width = num7;
                    preview.Height = num8;
                }
                return preview;
            }
            catch (Exception exception)
            {
                if (exception.IsFileSystemException())
                {
                    throw;
                }
                throw new BlenderException("Error while reading preview for " + fileName + "!", exception);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    destination!.Close();
                }
            }
        }

        public byte[] GetPixelColor(int x, int y)
        {
            if (this.IsEmpty)
            {
                return new byte[0];
            }
            int index = (x + (y * this.Width)) * 4;
            if (index >= this.Pixels!.Length)
            {
                return new byte[0];
            }
            return new byte[] { this.Pixels[index], this.Pixels[index + 1], this.Pixels[index + 2], this.Pixels[index + 3] };
        }

        public bool IsEmpty
        {
            get
            {
                if (((this.Width != 0) && (this.Height != 0)) && (this.Pixels != null))
                {
                    return (this.Pixels.Length == 0);
                }
                return true;
            }
        }

        public ColorMode ColorMode { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int BitsPerPixel { get; private set; }

        public int BytesPerPixel { get; private set; }

        public int PixelsSizeInBytes
        {
            get
            {
                if (this.Pixels != null)
                {
                    return this.Pixels.Length;
                }
                return 0;
            }
        }

        public int Stride =>
            (this.Width * this.BytesPerPixel);

        public byte[]? Pixels { get; private set; }
    }
}

