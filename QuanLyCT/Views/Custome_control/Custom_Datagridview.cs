using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtanComponent
{
    public class ArtanPannel: Panel
    {
        // Field 
        private int borderRadius = 30;
        private float gradientAngle = 90F;
        private Color gradientTopcolor = Color.DodgerBlue;
        private Color gradientBttomColor = Color.CadetBlue; 

        //Constructor 
        public ArtanPannel()
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new Size(450, 200);         
        }


        // Properties
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; this.Invalidate(); }
        }
        public float GradientAngle
        {
            get => gradientAngle;
            set { gradientAngle = value; this.Invalidate(); }
        }
        public Color GradientTopcolor
        {
            get => gradientTopcolor;
            set { gradientTopcolor = value; this.Invalidate(); }
        }
        public Color GradientBttomColor
        {
            get => gradientBttomColor;
            set { gradientBttomColor = value; this.Invalidate(); }
        }
       
        //Methods 
        private GraphicsPath GetArtanPath(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Height - radius, radius, radius, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height - radius, radius, radius, 90, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        //Overridden Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Gradient 
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush brushArtan = new LinearGradientBrush(this.ClientRectangle, this.GradientTopcolor, this.GradientBttomColor, this.GradientAngle);
            Graphics graphicsArtan = e.Graphics;
            graphicsArtan.FillRectangle(brushArtan, ClientRectangle);

            //BorderRadius 
            RectangleF rectangleF = new RectangleF(0, 0, this.Width, this.Height);
            if (borderRadius > 2)
            {
                using (GraphicsPath graphicsPath = GetArtanPath(rectangleF, borderRadius))
                using (Pen pen = new Pen(this.Parent.BackColor, 2))
                {
                    this.Region = new Region(graphicsPath);
                    e.Graphics.DrawPath(pen, graphicsPath);

                }
            }
            else this.Region = new Region(rectangleF); 

        }
    }
}
