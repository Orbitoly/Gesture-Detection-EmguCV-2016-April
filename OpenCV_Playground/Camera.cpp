#include "Camera.h"



Camera::Camera()
{
} 


Camera::~Camera()
{

}
Camera::Camera(int camNum)
{
	VideoCapture cap(camNum);
	_capture = cap;
	cap.set(CV_CAP_PROP_AUTO_EXPOSURE, 0);
}
void Camera::TakeShot()
{
	_capture >> _frame;
	MirrorImage();
}
void Camera::Set_FPS(int fps)
{
	if (fps > 25)
		fps = 25;
	else if (fps < 1)
		fps = 1;
	_capture.set(CV_CAP_PROP_FPS, fps);
}
Mat Camera::getFrame()
{
	return _frame;
}
void Camera::MirrorImage()
{
	flip(_frame, _frame, 1);
}