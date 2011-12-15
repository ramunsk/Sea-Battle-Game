#pragma once

using namespace System::Collections::Generic;

ref class Rect
{
    private:
        int _width;
        int _height;
    public:
        Rect(int x, int y, int width, int height);
        property int X;
        property int Y;
        property int Width;
        property int Height;
        property int Right { int get(); }
        property int Bottom { int get(); }
        void Inflate(int width, int height);
        bool Contains(Rect^ rect);
        bool Contains(Point point);
        bool IntersectsWith(Rect^ rect);
        void MoveTo(int x, int y);
        IList<Point>^ GetPoints();
};
