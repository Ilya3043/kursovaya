using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсачик
{
    class Figure
    {
        public float S(float s1, float s2, float s3, float s4, float s5, float s6, float s7)
        {
            return s1 + s2 + s3 + s4 + s5 + s6 + s7;
        }
    }
    class Triangle : Figure
    {
        public float S(float ax, float ay, float bx, float by, float cx, float cy)
        {
            return ((bx - ax) * (cy - ay) - (cx - ax) * (by - ay)) / 2;
        }
    }
    class Square : Figure
    {

        public float S(float ax, float ay, float bx, float by)
        {
            return (float)Math.Pow((ax - bx), 2) + (float)Math.Pow((ay - by), 2);
        }
    }
    class Rectangle : Figure
    {

        public float S(float ax, float bx)
        {
            return (float)Math.Pow(ax, 0.5) * (float)Math.Pow(bx, 0.5);
        }
    }
    class Parallelogram : Figure
    {

        public float S(float ax, float bx, float cx, float ay, float by, float cy)
        {
            return (bx - ax) * (cy - ay) - (cx - ax) * (by - ay);
        }
    }
    class Rhomb : Figure
    {

        public float S(float ao, float bo)
        {
            return ao * bo * 2;
        }
    }
    class Trapezoid : Figure
    {

        public float S(float bc, float da, float preS1)
        {
            return (bc + da) / 2 * ((float)Math.Pow((preS1), 0.5));
        }
    }
    class Circle : Figure
    {

        public float S(float r)
        {
            return (float)3.14 * (float)(r * r);
        }
    }
    class SUPERSQUARE : Figure
    {

        public float S(float a)
        {
            return (float)Math.Pow(a, 2);
        }
    }
}
