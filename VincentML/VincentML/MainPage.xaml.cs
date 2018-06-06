using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace VincentML
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ChooseImage(object sender, RoutedEventArgs e)
        {

            StorageFile modelDile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/Vincent.onnx"));
            Model model = await Model.CreateModel(modelDile);

            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".bmp");
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                BitmapImage src = new BitmapImage();
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await src.SetSourceAsync(stream);
                    stream.Dispose();
                };
                image.Source = src;

                ModelInput modelInput = new ModelInput();
                modelInput.data = await GetVideoFrame(file);

                ModelOutput modelOutput = await model.EvaluateAsync(modelInput);
                var topCategory = modelOutput.loss.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key + ", Confidence: " + modelOutput.loss.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Value.ToString("P4");
                tbResult.Text = topCategory;
            }
        }

        private async Task<VideoFrame> GetVideoFrame(StorageFile file)
        {
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream 
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file in BGRA8 format
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

                return VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);
            }
        }
    }
}
