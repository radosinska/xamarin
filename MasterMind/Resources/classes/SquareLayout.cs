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


namespace MasterMind
{

    public class SquareLayout : LinearLayout
    {

        public SquareLayout(Context context) : base(context) { }

        public SquareLayout(Context context, Android.Util.IAttributeSet attrs) : base(context, attrs) { }

        public SquareLayout(Context context, Android.Util.IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { }


        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
            int width = MeasureSpec.GetSize(widthMeasureSpec);
            int height = MeasureSpec.GetSize(heightMeasureSpec);
            int size = width > height ? height : width;
            SetMeasuredDimension(size, size);
        }
    }
}