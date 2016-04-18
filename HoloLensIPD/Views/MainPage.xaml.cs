using System;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using HoloLensIPD.Helpers;
using HoloLensIPD.Services;
using System.IO;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Microsoft.ProjectOxford.Face.Contract;

namespace HoloLensIPD.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        //private StorageFile _photo;
        //private Size _photoSize;
        //private double _controlIPD;

        //public MainPage()
        //{
        //    InitializeComponent();
        //    InitiateIPD();
        //    FaceAPIKeyText.Text = LocalSettingsHelpers.GetValue<string>("apiKey") ?? "";
        //}


        //private async void CaptureButton_Click(object sender, RoutedEventArgs e)
        //{

        //    //var picker = new Windows.Storage.Pickers.FileOpenPicker();
        //    //picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
        //    //picker.SuggestedStartLocation =
        //    //    Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
        //    //picker.FileTypeFilter.Add(".jpg");
        //    //picker.FileTypeFilter.Add(".jpeg");
        //    //picker.FileTypeFilter.Add(".png");

        //    //_photo = await picker.PickSingleFileAsync();



        //    CameraCaptureUI captureUI = new CameraCaptureUI();
        //    captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
        //    captureUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;
        //    _photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);


        //    if (_photo == null)
        //    {
        //        // User cancelled photo capture
        //        return;
        //    }
        //    else
        //    {
        //        var bitmap = await _photo.ToBitmapAsync();
        //        _photoSize = new Size(bitmap.PixelWidth, bitmap.PixelHeight);

        //        var bitmapSource = new SoftwareBitmapSource();
        //        await bitmapSource.SetBitmapAsync(bitmap);
        //        CapturedImage.Source = bitmapSource;
        //    }

        //}

        //private async void ProcessButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_photo == null)
        //        return;

        //    var stream = await _photo.OpenAsync(FileAccessMode.Read);
        //    var res = await FaceDetectionService.ProcessImageAsync(stream.AsStream(), FaceAPIKeyText.Text);

        //    TheCanvas.Faces = res.ToList();
        //    TheCanvas.ImageRealSize = _photoSize;
        //    TheCanvas.UpdateAnnotations();

        //    FaceDetectionService.CalculateIPDAsync(TheCanvas.Faces, _controlIPD);
        //    TheCanvas.UpdateAnnotations();
        //}



        //private void InitiateIPD()
        //{
        //    _controlIPD = LocalSettingsHelpers.GetValue<double>("controlIPD");
        //    ControlIPDText.Text = _controlIPD.ToString();
        //}


        //private void ControlIPDText_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!double.TryParse(ControlIPDText.Text, out _controlIPD))
        //    {
        //        ControlIPDText.Foreground = new SolidColorBrush(Colors.Red);
        //        ProcessButton.IsEnabled = false;
        //    }
        //    else
        //    {
        //        ControlIPDText.Foreground = new SolidColorBrush(Colors.Green);
        //        ProcessButton.IsEnabled = true;
        //        LocalSettingsHelpers.SetValue("controlIPD", _controlIPD);
        //    }
        //}


        //private void FaceAPIKey_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    LocalSettingsHelpers.SetValue("apiKey", ((TextBox)sender).Text);
        //}
    }
}
