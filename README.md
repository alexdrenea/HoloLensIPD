# HoloLens IPD
Windows Universal application to find your interpupillary distance (IPD) recommended for when using a HoloLens.

#What is interpupillary distance

Interpupillary distance (IPD) is the distance between the center of the pupils of the two eyes. While other VR headsets have a fixed preset value for this parameter, HoloLens allows adjusting it. A netural value (63mm) will probably fit the majority of people but for best experience it's recommended to set the value as closely as possible to the real value of each individual.

#How can we measure it
IPD can be measured with basic optometrist devices and HoloLens does come with a calibration software that can determine it. However,  the built-in tool takes 2-3 minutes to calculate and not everyone has a pupilometer available so often times the only option is to just set the neutral value and hope for the best.
With the HoloLens IPD, you can determine a person's IPD within seconds. It will come with a larger margin of error than the pupilometer, and potentially a bit less accurate than the built-in tool. 
Based on my current experiments, I found it to be accurate within 2mm which is better than nothing.


#How does the app work

The HoloLens IPD application will use a photo of you to determine the IPD value. 

Theoretically it is possible to measure the distance between two points of an photo if you know a few basic parameters such as distance to the sensor, focal length and other parameters of the lens.
That would obviously be way too complicated, both from a design perspective and from a user experience perspective (having to know and enter all parameters of your camera).

The obvious next solution was to use an object with a known size (like a ruler). Have the person hold the ruler **in the same plane** as their eyes and based on that, measure the distance between the eyes, and measure the same distance on the ruler in the image to get the actual value in mm or inches.
The disadvantages of this approach is that it requires a lot of manual intervention from the user to measure distance between eyes and match it to a same distance on the ruler in the image.

To solve the first problem, of measuring the pixel distance between the eyes, in the image, the HoloLens IPD app uses Microsoft's Face API (part of Microsoft Cognitive Services- https://www.microsoft.com/cognitive-services/en-us/face-api). The service returns very precise landmarks for faces it detects in images. Using the "eyeRightTop" and "eyeLeftTop" landmarks we can get a pretty accurate position and distance between person's pupils.

However, to transform this value in a real measurement, we still need a reference measurement - the ruler. Luckily, to fully automate the process, we can extend the use of the Face API and replace the ruler with another face, for which we know the interpupillary distance. We can use the distance in pixels and the real measurement to calculate the "millimeter to pixel" ratio and use that to identify the needed measurement.


#How to use the app
1. Register for Face API. Enter the key in the app (it will be saved so no need to re-enter)
2. Print an image of a face that will become the control image (yours, or some generic face - http://scottwesterfeld.com/blog/2005/10/pretties-week-begins/) 
3. Measure the distance between eyes in the printed image. be as exact as possible - enter the value in the app (unit of measure is not relevant, but keep in mind that the result will be in the same unit of measure. HoloLens accepts distance in millimeters only)
4. Use the app to **capture image** of the user, while holding the printed image, in the same plane as their eyes.
5. Once the image is displayed, use the **process image** button to get the app to process the image and get you the results.

*Note: In its current state the HoloLens IPD app will automatically pick the control image based on the smallest distance between eyes, so ensure that the printed control image smaller in size than a regular face.
