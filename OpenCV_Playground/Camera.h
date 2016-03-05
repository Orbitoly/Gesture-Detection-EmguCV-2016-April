#pragma once

#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <iostream>
using namespace cv;
using namespace std;

class Camera
{
private:
	Mat _frame;
	VideoCapture _capture;
public:
	Camera();
	Camera(int camNum, bool debug = false);
	Mat TakeShot();
	void MirrorImage();
	~Camera();
	void Set_FPS(int fps);
	Mat getFrame();
};

