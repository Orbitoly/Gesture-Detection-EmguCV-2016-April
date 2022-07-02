# Gesture-Detection-EmguCV-2016-April
That is my "Bagrut" project through the national cybereducation program <a href="https://www.magshimim.cyber.org.il/"><b>"Magshimim"</b></a> . The goal was to build a DLL which handles hand gestures recording and detection, using basic web camera. <br/>
I had no knowledge in Computer Vision prior to this project, and the algorithm I've implemented was based on trial and error, while researching the web <br> 
<p><small>(and even asking in stack overflow ;) , see <br/>
https://stackoverflow.com/questions/35061734/opencv-background-substraction-using-absdiff
https://stackoverflow.com/questions/35456207/opencv-background-substraction-skin-thresh-to-detect-hand-c
</small></p>

Also, I've found <a href="https://www.researchgate.net/publication/262371199_Explicit_image_detection_using_YCbCr_space_color_model_as_skin_detection">this article </a> which noted that YCbCr image format is great for human skin detection, and also specified a threshold range (which I've used in my code)
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
