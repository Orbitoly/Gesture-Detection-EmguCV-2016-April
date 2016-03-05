#include "MyThreshold.h"


MyThreshold::MyThreshold(Mat back)
{
	back.copyTo(_background);
	cvtColor(_background, _background, CV_BGR2GRAY);

	//absdiff(changeColorFormat(skinColorThresh(src)), changeColorFormat(skinColorThresh(background)), thresh_mat);
	//
}
MyThreshold::MyThreshold()
{

}
MyThreshold::~MyThreshold()
{
}
Mat MyThreshold::changeColorFormat(Mat src)
{
	Mat colored;
	cvtColor(src, colored, COLOR_BGR2YCrCb);
	return colored;
}
Mat MyThreshold::skinColorThresh(Mat src)
{
	Scalar min = Scalar(0, 133, 77);
	Scalar max = Scalar(255, 173, 127);
	Mat skin;
	inRange(src, min, max, skin);
	return skin;
}
void MyThreshold::SetFrame(Mat src)
{
	src.copyTo(_frame);
}
void MyThreshold::SetBackground(Mat back)
{
	back.copyTo(_background);

}
void MyThreshold::UpdateFrame(Mat src)
{
	SetFrame(src);
}
Mat MyThreshold::Thresh()
{
	return ColorThresh()& DiffThresh();
}
Mat MyThreshold::DiffThresh()
{

	Mat element5(10, 10, CV_8U, cv::Scalar(1));
	Mat eroding;
	Mat dilating;


	Mat diffThresh;
	Mat grey;

	cvtColor(_frame, grey, CV_BGR2GRAY);
	absdiff(_background, grey, diffThresh);
	threshold(diffThresh, diffThresh, 50, 255, CV_THRESH_BINARY);

	



	//morphologyEx(diffThresh, eroding, cv::MORPH_ERODE, element5);
	//morphologyEx(eroding, dilating, cv::MORPH_DILATE, element5);


	imshow("diff", diffThresh);



	return diffThresh;
}
Mat MyThreshold::ColorThresh()
{
	Mat colorThresh;
	cv::cvtColor(_frame, colorThresh, COLOR_BGR2YCrCb);

	inRange(colorThresh, Scalar(0, 133, 77), Scalar(255, 173, 127), colorThresh);
	Mat element5(5, 5, CV_8U, cv::Scalar(1));
	Mat closed;
	Mat opened;

	//morphologyEx(colorThresh, closed, cv::MORPH_CLOSE, element5);
	//morphologyEx(closed, opened, cv::MORPH_OPEN, element5);

	imshow("skinColor", colorThresh);
	return colorThresh;
}

