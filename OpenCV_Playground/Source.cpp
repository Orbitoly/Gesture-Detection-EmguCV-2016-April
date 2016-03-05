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
	Camera cam(LAPTOP_CAM,TRUE);
	Mat frame, backGround;
	Mat blured;
	MyThreshold thr;

	ContourProcessor cont=ContourProcessor();
	
	Mat threshed;
	Sleep(WAIT_TIME); // waiting for the camera to adjust, before first picture
	cam.TakeShot(); // taking background shot, no hand should be in that image
	cam.TakeShot().copyTo(backGround); // taking background shot, no hand should be in that image
	//cam.getFrame().copyTo(backGround); //Deep Copy

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
		cam.TakeShot().copyTo(frame);
		
		//cam.getFrame().copyTo(frame);

		thr.UpdateFrame(frame);

	    //cont = ContourProcessor(thr.GetOutput());
		threshed = thr.Thresh();


		imshow("threshed", threshed);
		cont.UpdateCont(threshed);
		cont.FindLargestCont();
		/*findContours(threshed, contours, hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));

		/// Draw contours
		Mat drawing = Mat::zeros(threshed.size(), CV_8UC3);
		for (int i = 0; i< contours.size(); i++)
		{
			Scalar color = Scalar(40,40,40);
			drawContours(drawing, contours, i, color, 2, 8, hierarchy, 0, Point());
		}


		imshow("cont", threshed);*/

		

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
