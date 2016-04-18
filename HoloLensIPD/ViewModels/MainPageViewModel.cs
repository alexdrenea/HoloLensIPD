using HoloLensIPD.Controls;
using HoloLensIPD.Helpers;
using HoloLensIPD.Models;
using HoloLensIPD.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HoloLensIPD.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        private StorageFile _photo;
        private Size _photoSize;
        private double _controlIPD;
        private string _controlIPDString;
        private string _faceAPIKey;
        private bool _isBusy;
        private AnnotationCanvasUpdateEvent _annotationCanvasUpdateEvent;

        public MainPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CaptureImageCommand = DelegateCommand.FromAsyncHandler(CaptureImage, ()=> !IsBusy);
            ProcessImageCommand = DelegateCommand.FromAsyncHandler(ProcessImage, () => _controlIPD > 0 && !IsBusy);

            _controlIPD = ApplicationDataHelpers.GetLocalValue<double>("controlIPD");
            ControlIPD = _controlIPD.ToString();
            FaceApiKey = ApplicationDataHelpers.GetLocalValue<string>("apiKey") ?? "";

            _annotationCanvasUpdateEvent = _eventAggregator.GetEvent<AnnotationCanvasUpdateEvent>();
        }


        public string FaceApiKey
        {
            get { return _faceAPIKey; }
            set
            {
                _faceAPIKey = value;
                ApplicationDataHelpers.SetLocalValue("apiKey", value);
                OnPropertyChanged();
            }
        }

        public string ControlIPD
        {
            get
            {
                return _controlIPDString;
            }
            set
            {
                _controlIPDString = value;
                if (!double.TryParse(value, out _controlIPD))
                {
                    _controlIPD = 0;
                    ProcessImageCommandEnabled = false;
                }
                else
                {
                    ApplicationDataHelpers.SetLocalValue("controlIPD", _controlIPD);
                    ProcessImageCommandEnabled = true;
                }
                OnPropertyChanged();
                OnPropertyChanged(() => ProcessImageCommandEnabled);
            }
        }

        public ImageSource ImageSource { get; set; }

        public Size ImageRealSize { get; set; }

        public List<FaceDescriptor> Faces { get; set; }

        public ICommand CaptureImageCommand { get; private set; }
        public ICommand ProcessImageCommand { get; private set; }

        public bool ProcessImageCommandEnabled { get; set; }

        public bool IsBusy { get { return _isBusy; } set { _isBusy = value; OnPropertyChanged(); } }

        private async Task CaptureImage()
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;
            _photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);


            if (_photo == null)
            {
                ImageSource = null;
                OnPropertyChanged(() => ImageSource);
                // User cancelled photo capture
                return;
            }
            else
            {
                IsBusy = true;

                var bitmap = await _photo.ToBitmapAsync();
                ImageRealSize = new Size(bitmap.PixelWidth, bitmap.PixelHeight);
                OnPropertyChanged(() => ImageRealSize);

                var bitmapSource = new SoftwareBitmapSource();
                await bitmapSource.SetBitmapAsync(bitmap);

                ImageSource = bitmapSource;
                OnPropertyChanged(() => ImageSource);

                IsBusy = false;

                //update annotations
            }
        }

        private async Task ProcessImage()
        {
            if (_photo == null)
                return;

            IsBusy = true;

            var stream = await _photo.OpenAsync(FileAccessMode.Read);
            var res = await FaceDetectionService.ProcessImageAsync(stream.AsStream(), FaceApiKey);
            Faces = res.ToList();
            OnPropertyChanged(() => Faces);

            FaceDetectionService.CalculateIPDAsync(Faces, _controlIPD);

            IsBusy = false;

            _annotationCanvasUpdateEvent.Publish(true);
        }

    }
}
