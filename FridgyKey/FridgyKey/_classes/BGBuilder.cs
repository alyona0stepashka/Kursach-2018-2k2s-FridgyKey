using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FridgyKey 
{
    abstract class BGBuilder
    {
        public abstract void BuildImage();
        public abstract void BuildColor();
        public abstract void BuildStandarTheme();
        public abstract void SetBG(Canvas canv);
        public abstract BG GetResult();
    }
    public class BG//product
    {
        public SolidColorBrush Mcolor;
        public SolidColorBrush Mcolor3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4B4A43")); 
        public ImageBrush brush = new ImageBrush();
        public int r;
        //0 - standart
        //1 - image
        //2 - color
        public void AddStandartColor()
        {
            Mcolor = Mcolor3; 
        }
        public void AddColor()
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            var result = cd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Mcolor = new SolidColorBrush(Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B));
            } 
        }
        public void AddImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images |*.png;*.jpg";
            var s = dialog.SafeFileName;
            if (dialog.ShowDialog().Value)
            {
                brush.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
            } 
        }
    }
    class StandartBuilder : BGBuilder
    {
        BG bg = new BG();
        public override void BuildImage()
        { }
        public override void BuildColor()
        { }
        public override void BuildStandarTheme()
        {
            bg.AddStandartColor();
            bg.r = 0;
        }
        public override BG GetResult()
        {
            return bg;
        }
        public override void SetBG(Canvas canv)
        {  
            canv.Background = bg.Mcolor3; 
        }
    }
    class ColorBuilder : BGBuilder
    {
        BG bg = new BG();
        public override void BuildImage()
        { }
        public override void BuildColor()
        {
            bg.AddColor();
            bg.r = 2;
        }
        public override void BuildStandarTheme()
        { }
        public override BG GetResult()
        {
            return bg;
        }
        public override void SetBG(Canvas canv)
        { 
            canv.Background = bg.Mcolor; 
        }
    }
    class ImageBuilder:BGBuilder
    {
        BG bg = new BG();
        public override void BuildImage()
        {
            bg.AddImage();
            bg.r=1; 
        }
        public override void BuildColor()
        { }
        public override void BuildStandarTheme()
        { }
        public override BG GetResult()
        {
            return bg;
        }
        public override void SetBG(Canvas canv)
        {
            canv.Background = bg.brush;
        }
    }      
}
