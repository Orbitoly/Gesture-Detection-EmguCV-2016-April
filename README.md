# Gesture-Detection-EmguCV-2016-April
<img width="989" alt="image" src="https://user-images.githubusercontent.com/17669444/232743626-bb1f2aa4-9838-4026-805d-fa93b0a350cb.png">

That was my "Bagrut" project at 12th grade through the National Cybereducation Program <a href="https://www.magshimim.cyber.org.il/"><b>"Magshimim"</b></a> . My goal was to build a C# DLL which handles hand gestures recording and detection, using basic web camera. <br/>
I had no knowledge in Computer Vision prior to this project, and the algorithm I've implemented was based on trial and error, while researching the web <br> 
<p><small>(and even asking in stack overflow ;) , see <br/>
https://stackoverflow.com/questions/35061734/opencv-background-substraction-using-absdiff
https://stackoverflow.com/questions/35456207/opencv-background-substraction-skin-thresh-to-detect-hand-c
</small></p>

During my research, I've found and used <a href="https://www.researchgate.net/publication/262371199_Explicit_image_detection_using_YCbCr_space_color_model_as_skin_detection">this article </a> which noted that YCbCr image format is great for human skin detection, and also specified a threshold range (which I've used in my code)
<p align="center">

<img src="https://github.com/Orbitoly/Gesture-Detection-EmguCV-2016-April/blob/master/Logo.jpg" alt="alt text" width="400px" height="whatever">
</p>


<h2>Algorithm</h2>
<p align="center">

<img src="https://github.com/Orbitoly/Gesture-Detection-EmguCV-2016-April/blob/master/Algorithm.png" style="background-color:white;" alt="alt text" width="700px" height="whatever">

</p>

<h2>Detection examples</h2>

<p align="center">

<img src="https://github.com/Orbitoly/Gesture-Detection-EmguCV-2016-April/blob/master/detection.jpg" style="background-color:white;" alt="alt text" width="400px" height="whatever">


<img src="https://github.com/Orbitoly/Gesture-Detection-EmguCV-2016-April/blob/master/detection2.jpg" style="background-color:white;" alt="alt text" width="400px" height="whatever">

</p>
You can see the combining absdiff with color detection, helped me to remove objects such as the guitar from the contours that might be my hand. (because altough it is in the range of skin colors, but it does not move)


For demo day I've built a C# code which uses my DLL, to record and identify ↑ ↓ → ← hand movements, which controlled a car driving on screen

https://github.com/Orbitoly/Gesture-Detection-EmguCV-2016-April/assets/17669444/024bd724-22aa-4677-b95c-4f2012576afe

