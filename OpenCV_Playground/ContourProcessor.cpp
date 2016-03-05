#include "ContourProcessor.h"


ContourProcessor::ContourProcessor()
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
