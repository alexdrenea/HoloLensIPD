using HoloLensIPD.Models;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace HoloLensIPD.Services
{
    //TODO - Make this a proper service, when more features are added
    public class FaceDetectionService
    {
        public static async Task<IEnumerable<FaceDescriptor>> ProcessImageAsync(Stream image, string apikey)
        {
            var faceServiceClient = new FaceServiceClient(apikey);

            var faces = await faceServiceClient.DetectAsync(image, false, true);

            return faces.Select(f => new FaceDescriptor
            {
                FaceBounds = new Rect(f.FaceRectangle.Left, f.FaceRectangle.Top, f.FaceRectangle.Width, f.FaceRectangle.Height),
                LeftEye = new Point(f.FaceLandmarks.EyeLeftTop.X, f.FaceLandmarks.EyeLeftTop.Y),
                RightEye = new Point(f.FaceLandmarks.EyeRightOuter.X, f.FaceLandmarks.EyeRightOuter.Y)
            });
        }

        /// <summary>
        /// Calculates the IPD distance for the given face descriptors
        /// </summary>
        /// <param name="faces">Collection of face descriptors to find the IPD. One of the images must be the control image, for which we already know the IPD</param>
        /// <param name="controlImageIPD">The IPD value of the control image. Unit of measure is irrelevant. The results will be in the same unit of measure. Since HoloLens works with mm this value is recommended to be in mm as well.</param>
        /// <param name="controlFaceIndex">Index in the faces collection of the control image. If -1 (default) then the algorithm will consider the smallest image to be the control image.</param>
        public static void CalculateIPDAsync(List<FaceDescriptor> faces, double controlImageIPD, int controlFaceIndex = -1)
        {
            if (controlFaceIndex == -1)
            {
                //if control image index is not provided, we'll assume it is the image with the lowest distance 
                var minDist = faces.Min(f => f.EyeDistance);
                var minDistElement = faces.First(f => f.EyeDistance == minDist);
                controlFaceIndex = faces.IndexOf(minDistElement);
            }
            var controlFaceDistance = faces[controlFaceIndex].EyeDistance;

            for (int i = 0; i < faces.Count(); i++)
            {
                if (i == controlFaceIndex)
                {
                    continue;
                }
                faces[i].IPD = faces[i].EyeDistance * controlImageIPD / controlFaceDistance;
            }
        }
    }
}
