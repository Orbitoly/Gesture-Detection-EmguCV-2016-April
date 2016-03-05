#pragma once
#include <opencv2\imgproc\imgproc.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/video/background_segm.hpp>
#include <iostream>
using namespace cv;
class MyThreshold
{
private:
	Mat changeColorFormat(Mat src);
	Mat skinColorThresh(Mat src);
	Mat _frame;
	Mat _background;
public:

	MyThreshold(Mat background);
	MyThreshold();
	      
	~MyThreshold();
	void SetFrame(Mat src);
	void SetBackground(Mat back);
	void UpdateFrame(Mat src);
	Mat Thresh();
	Mat ColorThresh();
	Mat DiffThresh();
};

