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
	bool debug;
	vector<vector<Point> > _contours;
	vector<Vec4i> _hierarchy;

public:
	Mat _binFrame;

	ContourProcessor(bool debug = false);
	ContourProcessor(Mat binFrame);
	void UpdateCont(Mat binFrame);
	int LargestConts(int len = 3);
	int FindLargestCont(vector<vector<Point>> tempVec);

	~ContourProcessor();
};

