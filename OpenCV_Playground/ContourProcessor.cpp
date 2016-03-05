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
void ContourProcessor::FindLargestCont()
{
	int largest_area = 0;
	int largest_contour_index = 0;
	Rect bounding_rect;
	findContours(_binFrame, _contours, _hierarchy, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));

	/// Draw contours
	Mat drawing = Mat::zeros(_binFrame.size(), CV_8UC3);
	for (int i = 0; i< _contours.size(); i++) // iterate through each contour. 
	{
		double a = contourArea(_contours[i], false);  //  Find the area of contour
		if (a>largest_area) {
			largest_area = a;
			largest_contour_index = i;                //Store the index of largest contour
			bounding_rect = boundingRect(_contours[i]); // Find the bounding rectangle for biggest contour
		}

	}

	Scalar color(250, 250, 0);
	drawContours(drawing, _contours, largest_contour_index, color, CV_FILLED, 8, _hierarchy); // Draw the largest contour using previously stored index.
	rectangle(drawing, bounding_rect, Scalar(0, 255, 0), 1, 8, 0);
	imshow("contoClass", drawing);

	/// Draw contours
	//for (int i = 0; i< contours.size(); i++) // iterate through each contour. 
	//{
	//	double a = contourArea(contours[i], false);  //  Find the area of contour
	//	if (a>largest_area) {
	//		largest_area = a;
	//		largest_contour_index = i;                //Store the index of largest contour
	//		bounding_rect = boundingRect(contours[i]); // Find the bounding rectangle for biggest contour
	//	}

	//}

	//Scalar color(255, 255, 255);

	//drawContours(_hand, contours, largest_contour_index, color, CV_FILLED, 8, hierarchy); // Draw the largest contour using previously stored index.
	//imshow("hand", _hand);
	//rectangle(_hand, bounding_rect, Scalar(0, 255, 0), 1, 8, 0);
	
}
