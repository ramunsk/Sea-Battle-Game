#include "stdafx.h"
#include "Rect.h"

using namespace System::Drawing;

Rect::Rect(int x, int y, int width, int height)
{
    this->X = x;
    this->Y = y;
    this->Width = width;
    this->Height = height;
};

int Rect::Right::get()
{
    return this->X + this->Width - 1;
};

int Rect::Bottom::get()
{
   return this->Y + this->Height - 1;
};

void Rect::Inflate(int width, int height)
{
    this->X -= width;
    this->Y -= height;
    this->Width += width * 2;
    this->Height += height * 2;
};

bool Rect::Contains(Rect^ rect)
{
    return (this->X <= rect->X 
            && 
            this->Y <= rect->Y
            && 
            this->Right >= rect->Right 
            && 
            this->Bottom >= rect->Bottom);
};

bool Rect::Contains(Point point)
{
    return (point.X >= this->X 
            && 
            point.X <= this->Right 
            && 
            point.Y >= this->Y 
            && 
            point.Y <= this->Bottom);
};

bool Rect::IntersectsWith(Rect^ rect)
{
    return !(this->X > rect->Right
             || 
             this->Right < rect->X
             || 
             this->Y > rect->Bottom
             || 
             this->Bottom < rect->Y);
};

void Rect::MoveTo(int x, int y)
{
    X = x;
    Y = y;
};

IList<Point>^ Rect::GetPoints()
{
    List<Point>^ points = gcnew List<Point>();

    for (int x = X; x <= Right; x++)
    {
        for (int y = Y; y <= Bottom; y++)
        {
            points->Add(Point(x, y));
        }
    }

    return points;
};