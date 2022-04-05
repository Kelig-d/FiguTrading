using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FiguTrading.Droid;
using FiguTrading.Interfaces;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(MyImageCompressor_Android))]
namespace FiguTrading.Droid
{
    class MyImageCompressor_Android : IMyImageCompressor
    {
        public MyImageCompressor_Android() { }

        public string ImageCompressor(byte[] bitmapBytes)
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapBytes, 0, bitmapBytes.Length);

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(bitmap, 300, 300, false);
            var stream = new System.IO.MemoryStream();

            resizedImage.Compress(Bitmap.CompressFormat.Webp, 100, stream);
            byte[] imageBytes = stream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}