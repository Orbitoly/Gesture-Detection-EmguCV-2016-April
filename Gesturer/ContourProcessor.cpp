#include "ContourProcessor.h"


ContourProcessor::ContourProcessor(bool debug)
{
}


ContourProcessor::~ContourProcessor()
{
}
ContourProcessor::ContourProcessor(Mat binFrame)
{

}
void ContourProcessor::UpdateCont(Mat binFrame)
{
	binFrame.copyTo(_binFrame);
}
//Finds the x biggest conts
vector<vector<Point>> ContourProcessor::LargestConts(int len)
{
	OutputArrayOfArrays outA();
	OutputArray outB();
	//findContours(_binFrame, outA, outB, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
	findContours(_binFrame, _contours, _hierarchy, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
	vector<vector<Point>> largests;
	vector<Point> temp;
	Rect bounding_rect;

	for (int i = 0; i < len; i++)
	{
		temp = FindLargestCont();
		if (temp.size() != 0)
		{
			largests.push_back(temp);
			_contours.erase(std::find(_contours.begin(), _contours.end(), temp));
		}
	}
	Mat drawing = Mat::zeros(_binFrame.size(), CV_8UC3);
	Scalar color(250, 250, 0);
	for (int i = 0; i < largests.size(); i++)
	{
		drawContours(drawing, _contours, i, color, CV_FILLED, 8, _hierarchy); // Draw the largest contour using previously stored index.
		rectangle(drawing, bounding_rect, Scalar(0, 255, 0), 1, 8, 0);

	}
	//imshow("contoClass", drawing);
	return largests;
}
vector<Point> ContourProcessor::FindLargestCont()
{
	int largest_area = 0;
	int largest_contour_index = 0;
	Rect bounding_rect;

	/// Draw contours
	//Mat drawing = Mat::zeros(_binFrame.size(), CV_8UC3);

	if (_contours.size() == 0)
		return vector<Point>();
	for (int i = 0; i< _contours.size(); i++) // iterate through each contour. 
	{
		double a = contourArea(_contours[i], false);  //  Find the area of contour
		if (a>largest_area) {
			largest_area = a;
			largest_contour_index = i;                //Store the index of largest contour
			bounding_rect = boundingRect(_contours[i]); // Find the bounding rectangle for biggest contour
		}

	}

	//Scalar color(250, 250, 0);
	//drawContours(drawing, _contours, largest_contour_index, color, CV_FILLED, 8, _hierarchy); // Draw the largest contour using previously stored index.
	//rectangle(drawing, bounding_rect, Scalar(0, 255, 0), 1, 8, 0);
	//imshow("contoClass", drawing);
	
	return _contours[largest_contour_index];
	
}
