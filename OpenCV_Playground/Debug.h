//#pragma once
//#include <opencv2\imgproc\imgproc.hpp>
//#include <opencv2/core/core.hpp>
//#include <opencv2/highgui/highgui.hpp>
//#include <opencv2/video/background_segm.hpp>
//#include <iostream>
//#include <vector>
//using namespace std;
//using namespace cv;
//class Debug
//{
//public:
//	Debug();
//	~Debug();
//
//	//functions prototypes
//	void on_trackbar(int, void*);
//	void createTrackbars();
//	void showimgcontours(Mat &threshedimg, Mat &original);
//	void toggle(int key);
//	void morphit(Mat &img);
//	void blurthresh(Mat &img);
//
//	//function prototypes ends here
//
//	//boolean toggles
//
//	bool domorph = false;
//	bool doblurthresh = false;
//	bool showchangedframe = false;
//	bool showcontours = false;
//	bool showhull = false;
//
//	//boolean toggles end
//
//
//	int H_MIN = 0;
//	int H_MAX = 255;
//	int S_MIN = 0;
//	int S_MAX = 255;
//	int V_MIN = 0;
//	int V_MAX = 255;
//
//	int kerode = 1;
//	int kdilate = 1;
//	int kblur = 1;
//	int threshval = 0;
//
//
//	//void on_trackbar(int, void*);
//	static void createTrackbars();
//
//	//void morphit(Mat &img);
//	//void blurthresh(Mat &img);
//	//void toggle(int key);
//
//	//void showimgcontours(Mat &threshedimg, Mat &original);
//	void loop(Mat frame);
//};
//
