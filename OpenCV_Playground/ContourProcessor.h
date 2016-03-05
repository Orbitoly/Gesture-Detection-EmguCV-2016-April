#pragma once
#include <opencv2\imgproc\imgproc.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/video/background_segm.hpp>
#include "features2d.hpp"
#include <iostream>
#include <vector>
#include <numeric>
using namespace std;
using namespace cv;
class ContourProcessor
{
private:
public:
	Mat _binFrame;

	ContourProcessor();
	ContourProcessor(Mat binFrame);
	void UpdateCont(Mat binFrame);
	void FindLargestCont();
	~ContourProcessor();
};

