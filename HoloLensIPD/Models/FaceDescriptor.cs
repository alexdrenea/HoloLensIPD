using HoloLensIPD.Helpers;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace HoloLensIPD.Models
{
    public class FaceDescriptor
    {
        public Point LeftEye { get; set; }
        public Point RightEye { get; set; }

        public double EyeDistance { get { return LeftEye.DistanceTo(RightEye); } }

        public Rect FaceBounds { get; set; }

        public double IPD { get; set; }
    }
}
