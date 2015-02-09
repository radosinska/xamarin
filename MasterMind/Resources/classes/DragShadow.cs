using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Graphics.Drawables;
using Android.Graphics;

namespace MasterMind
{
    public class DragShadow : View.DragShadowBuilder
    {
        //Klasa obs³uguj¹ca rysowanie 'shadow' w trakcie przeci¹gania
        string color;

        public DragShadow(View baseView, string color)
            : base(baseView)
        {
            this.color = color;
        }

        public override void OnDrawShadow(Canvas canvas)
        {

            MyDiamondShape s1 = new MyDiamondShape(canvas, color);


        }
        public override void OnProvideShadowMetrics(Point shadowSize, Point shadowTouchPoint)
        {
            int width = (int)View.Width;
            int height = (int)View.Height;

            shadowSize.Set(width, height);

            shadowTouchPoint.Set((int)width / 2, (int)height / 2);

        }
    }
}