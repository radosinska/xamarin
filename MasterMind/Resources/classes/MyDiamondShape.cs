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
    public class MyDiamondShape
    {
        //Klasa definuj¹ca kszta³t przeci¹ganego 'diamentu'
        public MyDiamondShape(Canvas canvas, string color)
        {
            int width = canvas.Width;
            int height = canvas.Height;

            var paintColor = new Paint();
            var paintGrey = new Paint();

            //Wybór koloru wype³nienia w zale¿noœci od chwyconego 'diamentu'
            switch (color)
            {
                case "1":
                    paintColor.Color = Color.ParseColor("#ff4500");                    
                    break;
                case "2":
                    paintColor.Color = Color.ParseColor("#ffd700");
                    break;
                case "3":
                    paintColor.Color = Color.ParseColor("#9acd32");
                    break;
                case "4":
                    paintColor.Color = Color.ParseColor("#4682b4");
                    break;
                case "5":
                    paintColor.Color = Color.ParseColor("#db7093");
                    break;
                default:
                    paintColor.Color = Color.White;
                    break;
            }

            paintGrey.Color = Color.ParseColor("#eee9e9");

            var dr = canvas.Width / 26;
            
            canvas.DrawCircle((width / 2), (height / 2), (width / 2), paintColor);
            canvas.DrawCircle((width / 2), (height / 2), (width / 2) - dr, paintGrey);
            canvas.DrawCircle((width / 2), (height / 2), (width / 2) - (3*dr), paintColor);

        }
    }
}