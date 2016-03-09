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
//vector<vector<Point>> ContourProcessor::LargestConts(int len)
int ContourProcessor::LargestConts(int len)
{
	OutputArrayOfArrays outA();
	OutputArray outB();
	//findContours(_binFrame, outA, outB, CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
	findContours(_binFrame, _contours, _hierarchy, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
	vector<int> largests;
	vector<vector<Point>> tempCont= _contours;
	vector<Vec4i> largetsH;
	int temp;
	Rect bounding_rect;

	for (int i = 0; i < 3; i++)
	{
		temp = FindLargestCont(tempCont);
		if (temp != -1)
		{
			largests.push_back(temp);
			//largests.push_back(_contours[temp]);
			//largetsH.push_back(_hierarchy[temp]);
			tempCont.erase(tempCont.begin()+temp);
			//_contours.erase(_contours.begin() + temp);
			//_hierarchy.erase(_hierarchy.begin() + temp);
		}
	}
	Mat drawing = Mat::zeros(_binFrame.size(), CV_8UC3);
	Scalar color(250, 250, 0);
	for (int i = 0; i < largests.size(); i++)
	{
		drawContours(drawing, _contours, largests[ i], color, CV_FILLED, 8, _hierarchy); // Draw the largest contour using previously stored index.
		rectangle(drawing, bounding_rect, Scalar(150, 255, 0), 1, 8, 0);

	}
	imshow("contoClass", drawing);
	return 0;
}
int ContourProcessor::FindLargestCont(vector<vector<Point>> tempVec)
{
	int largest_area = 0;
	int largest_contour_index = 0;
	Rect bounding_rect;

	/// Draw contours
	//Mat drawing = Mat::zeros(_binFrame.size(), CV_8UC3);

	if (tempVec.size() == 0)
		return -1;
	for (int i = 0; i< tempVec.size(); i++) // iterate through each contour. 
	{
		double a = contourArea(tempVec[i], false);  //  Find the area of contour
		if (a>largest_area) {
			largest_area = a;
			largest_contour_index = i;                //Store the index of largest contour
			bounding_rect = boundingRect(tempVec[i]); // Find the bounding rectangle for biggest contour
		}

	}

	//Scalar color(250, 250, 0);
	//drawContours(drawing, _contours, largest_contour_index, color, CV_FILLED, 8, _hierarchy); // Draw the largest contour using previously stored index.
	//rectangle(drawing, bounding_rect, Scalar(0, 255, 0), 1, 8, 0);
	//imshow("contoClass", drawing);
	
	return largest_contour_index;
	
}
