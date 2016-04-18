using HoloLensIPD.Models;
using Microsoft.ProjectOxford.Face.Contract;
using Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Prism.Unity.Windows;

namespace HoloLensIPD.Controls
{
    public class AnnotationCanvas : Canvas
    {
        public AnnotationCanvas()
        {
            SizeChanged += OnSizeChanged;
            App.EventAggregator.GetEvent<AnnotationCanvasUpdateEvent>().Subscribe(UpdateAnnotations);
        }

        public List<FaceDescriptor> Faces
        {
            get { return (List<FaceDescriptor>)GetValue(FacesProperty); }
            set { SetValue(FacesProperty, value); }
        }

        public static readonly DependencyProperty FacesProperty = DependencyProperty.Register("Faces", typeof(List<FaceDescriptor>), typeof(AnnotationCanvas), new PropertyMetadata(null));


        public Size ImageRealSize
        {
            get { return (Size)GetValue(ImageRealSizeProperty); }
            set { SetValue(ImageRealSizeProperty, value); }
        }

        public static readonly DependencyProperty ImageRealSizeProperty = DependencyProperty.Register("ImageRealSize", typeof(Size), typeof(AnnotationCanvas), new PropertyMetadata(null));



        public void UpdateAnnotations(bool unused)
        {
            if (Faces == null || ImageRealSize == null)
                return;

            Children.Clear();
            var ar = ImageRealSize.Height / this.ActualHeight;
            var replacedFaces = Faces.Select(f => new Rect(f.FaceBounds.Left / ar, f.FaceBounds.Top / ar, f.FaceBounds.Width / ar, f.FaceBounds.Height / ar)).ToArray();


            var faceFrames = replacedFaces.Select(x => {
                var n = new Rectangle() { Width = x.Width, Height = x.Height };
                n.SetValue(Canvas.LeftProperty, x.Left);
                n.SetValue(Canvas.TopProperty, x.Top);
                n.Stroke = new SolidColorBrush(Colors.Red);
                n.StrokeThickness = 3;
                return n;
            });

            var IPds = Faces.Where(f => f.IPD > 0).Select(f =>
            {
                var n = new TextBox()
                {
                    Text = f.IPD.ToString("0.##"),
                    IsReadOnly = true,
                };
                n.SetValue(Canvas.LeftProperty, f.FaceBounds.Left / ar);
                n.SetValue(Canvas.TopProperty, f.FaceBounds.Top / ar - 25);
                return n;
            });

            foreach (var ipd in IPds)
                Children.Add(ipd);

            foreach (var faceFrame in faceFrames)
                Children.Add(faceFrame);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateAnnotations(false);
        }
    
    }

    public class AnnotationCanvasUpdateEvent : PubSubEvent<bool>
    { }

}
