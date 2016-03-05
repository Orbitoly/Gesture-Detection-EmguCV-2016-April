#include <Windows.h>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <iostream>
#include "Camera.h"
#include "MyThreshold.h"
#include "Debug.cpp"
#include "ContourProcessor.h"
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/video/background_segm.hpp>
using namespace cv;
using namespace std;
Ptr<BackgroundSubtractor> pMOG2;
void drawRec(Mat frame, Point a);
int main() {
	//pMOG2 = createBackgroundSubtractorMOG2();
	//Ptr< BackgroundSubtractorGMG> pGMG; //MOG2 Background subtractor  
	const int LAPTOP_CAM = 0;
	const int LIFECAM = 1;
	const int MAX_FPS = 25;
	const int WAIT_TIME = 3000;
	Camera cam(LAPTOP_CAM);
	Mat frame, backGround;
	Mat blured;
	MyThreshold thr;

	ContourProcessor cont=ContourProcessor();
	
	Mat threshed;
	Sleep(WAIT_TIME); // waiting for the camera to adjust, before first picture
	cam.TakeShot(); // taking background shot, no hand should be in that image
	cam.TakeShot(); // taking background shot, no hand should be in that image
	cam.getFrame().copyTo(backGround); //Deep Copy
	thr = MyThreshold(backGround);
	Mat diff;
	Mat src_drawing;
	Mat conts;
	
	imshow("Background2", backGround);

	//Mat _hand = Mat(backGround.rows, backGround.cols, CV_8UC1, Scalar::all(0));
	//imshow("Hand", _hand);
	vector<vector<Point> > contours;
	Rect boundingrect;
	vector<Vec4i> hierarchy;
	int largest_area = 0;
	int largest_contour_index = 0;
	while (true) {
		cam.TakeShot();
		
		cam.getFrame().copyTo(frame);

		thr.UpdateFrame(frame);

	    //cont = ContourProcessor(thr.GetOutput());
		threshed = thr.Thresh();
		imshow("threshed", threshed);
		cont.UpdateCont(threshed);
		cont.FindLargestCont();



		
		//findContours(threshed, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE);
		//////this will find largest contour
		//double a;
		//largest_contour_index = -1; // something invalid

		//for (int i = 0; i< contours.size(); i++) // iterate through each contour. 
		//{
		//	//drawContours(frame, contours, i, CV_RGB(0, 255, 0), 2, 8, hierarchy);

		//	a = contourArea(contours[i], false);  //  Find the area of contour
		//	if (a>largest_area)
		//	{
		//		largest_area = a;
		//		largest_contour_index = i;                //Store the index of largest contour
		//	}

		//}
		////search for largest contour has end


		//if (largest_contour_index > -1)
		//{
		//	///Draw largest perimeter
		//	src_drawing = Mat::zeros(frame.size(), CV_8UC3);
		//	Scalar color = Scalar(40, 40, 40);
		//	drawContours(src_drawing, contours, largest_contour_index, color, 2, 8, hierarchy, 0, Point());
		//	Mat src_drawingclone = src_drawing.clone();

		//	///Bounding Rectangle
		//	boundingrect = boundingRect(contours[largest_contour_index]);
		//	rectangle(src_drawing, boundingrect, Scalar(255, 255, 0), 2);
		//	cout << "x:" << boundingrect.x << "  y:" << boundingrect.y << endl;
		//	cout << "h:" << boundingrect.height << "  w:" << boundingrect.width;
		//}
		//// iterate through each contour.
		//
		//imshow("Frame", src_drawing);

		cam.Set_FPS(MAX_FPS);
		if (waitKey(1) == 27)
			break;
	}
	
}
void drawRec(Mat frame, Point a)
{

	rectangle(frame, a, cv::Point(a.x+40, a.y+40), cv::Scalar(40, 40, 40));
}
//void CannyThreshold(int, void*)
//{
//	/// Reduce noise with a kernel 3x3
//	blur(src_gray, detected_edges, Size(3, 3));
//
//	/// Canny detector
//	Canny(detected_edges, detected_edges, lowThreshold, lowThreshold*ratio, kernel_size);
//
//	/// Using Canny's output as a mask, we display our result
//	dst = Scalar::all(0);
//
//	src.copyTo(dst, detected_edges);
//	imshow(window_name, dst);
//}

void saveBackground(Camera cam)
{
	cam.TakeShot();
	cam.MirrorImage();
	imwrite("Gray_Image.jpg", cam.getFrame());
}
