using ImageMagick;
using System;
using System.IO;
using System.Linq;

namespace Utils.Helpers
{
#nullable enable

    /// <summary>
    /// Helper class to handle Images
    /// </summary>
    public class ImageUtils
    {
        /// <summary>
        /// Convert to base 64 string? a given bytes array
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string? ToBase64(byte[] bytes)
        {
            try
            {
                if (bytes is null || bytes.Length == 0) return null;

                return Convert.ToBase64String(bytes);
            }
            catch (Exception e)
            {
                new LogData().Error(e);
            }

            return null;
        }

        /// <summary>
        /// Convert to bytes array a given base 64 string?
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public byte[]? ToBytes(string? base64)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(base64)) return null;

                var bytes = Convert.FromBase64String(base64);
                return bytes;
            }
            catch (Exception e)
            {
                new LogData().Error(e);
            }
            return null;
        }


        /// <summary>
        /// Compress a given bytes array
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="isLossless"></param>
        /// <returns></returns>
        public byte[]? Compress(byte[] bytes, bool isLossless = false)
        {
            try
            {
                if (bytes is null || bytes.Length == 0) return null;

                var imageStream = new MemoryStream();

                using (var image = new MagickImage(bytes))
                {

                    //var mimetype = image.;

                    //if (string.IsNullOrWhiteSpace(mimetype) || !"image".Equals(mimetype.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()))
                    //    throw new IOException("Wrong file format!;");

                    image.Quality=25;
                    image.Write(imageStream);
                    imageStream.Position = 0;

                    if (isLossless)
                        new ImageOptimizer().LosslessCompress(imageStream);
                    else
                        new ImageOptimizer().Compress(imageStream);
                }
                imageStream.Dispose();
                return imageStream.ToArray();
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception e)
            {
                new LogData().Error(e);
            }

            return null;
        }

        /// <summary>
        /// Compress a given base 64 string?
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="isLossless"></param>
        /// <returns></returns>
        public byte[]? Compress(string? base64, bool isLossless = false)
        {
            try
            {
                var bytes = ToBytes(base64);
                return Compress(bytes!, isLossless);
            }
            catch (Exception e)
            {
                new LogData().Error(e);
            }

            return null;
        }
    }
}
