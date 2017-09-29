using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.visual
{
    
    class rectangleVisual:DrawVisual
    {
         //set line color
        private LineColor lineCol;
        public LineColor LineCol
        {
            set
            {
                switch(value)
                {
                    case LineColor.black:
                        this.drawbrush = Brushes.Black;
                        break;
                    case LineColor.blue:
                        this.drawbrush = Brushes.Blue;
                        break;
                    case LineColor.white:
                        this.drawbrush = Brushes.White;
                        break;
                    case LineColor.yellow:
                        this.drawbrush = Brushes.Yellow;
                        break;
                    case LineColor.red:
                        this.drawbrush = Brushes.Red;
                        break;
                    case LineColor.green:
                        this.drawbrush = Brushes.Green;
                        break;
                    default:
                        this.drawbrush = Brushes.Black;
                        break;
                }
            }
        }

        //set line width
        private LineWidth lineWid;
        public LineWidth LineWid
        {
            set
            {
                switch(value)
                {
                    case LineWidth.width2p:
                        this.lineWidth = 5f;
                        break;
                    case LineWidth.width4p:
                        this.lineWidth = 10f;
                        break;
                    case LineWidth.width6p:
                        this.lineWidth = 15f;
                        break;
                    default:
                        this.lineWidth = 5f;
                        break;
                }
            }
        }

        private ShapeType rectype;
        public ShapeType RectTy
        {
            set
            {
                if (value != ShapeType.RoundRect && value != ShapeType.SquareRect)
                {
                    this.rectype = ShapeType.SquareRect;
                }
                else
                {
                    this.rectype = value;
                }
            }
        }

        public Brush drawbrush;
        private float lineWidth;
        public Point startpoint;
        public  Point endpoint;
        private System.Drawing.Graphics g;

        public Point StartPoint
        {
            set { this.startpoint = value; }
        }

        public Point EndPoint
        {
            set
            {
                if (value.Equals(startpoint))
                {
                    endpoint = Point.Empty;
                }
                else
                {
                    endpoint = value;
                    //(this as DrawVisual).render();
                }
            }
        }

    
   

        public rectangleVisual()
        {
            
        }

        ~rectangleVisual()
        {
            if (this.g != null)
            {
                this.g.Dispose();
            }
        }

       void DrawVisual.render()
       {
           //g.Clear(Color.Transparent);
           if (!endpoint.Equals(Point.Empty))
           {
               Pen pen = new Pen(this.drawbrush);
               pen.Width = this.lineWidth;
               Rectangle drawRectangle = new Rectangle(this.startpoint, new Size(this.endpoint.X - this.startpoint.X, this.endpoint.Y - this.startpoint.Y));

               if (this.rectype == ShapeType.RoundRect)
               {
                   GraphicsPath graphicspath = RoundedRect(drawRectangle, 20);
                   g.DrawPath(pen, graphicspath);
               }
               else
               {
                   g.DrawRectangle(pen, drawRectangle);
               }
           }
       }


       public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
       {
           int diameter = radius * 2;
           Size size = new Size(diameter, diameter);
           Rectangle arc = new Rectangle(bounds.Location, size);
           GraphicsPath path = new GraphicsPath();

           if (radius == 0)
           {
               path.AddRectangle(bounds);
               return path;
           }

           // top left arc  
           path.AddArc(arc, 180, 90);

           // top right arc  
           arc.X = bounds.Right - diameter;
           path.AddArc(arc, 270, 90);

           // bottom right arc  
           arc.Y = bounds.Bottom - diameter;
           path.AddArc(arc, 0, 90);

           // bottom left arc 
           arc.X = bounds.Left;
           path.AddArc(arc, 90, 90);

           path.CloseFigure();
           return path;
       }

       void DrawVisual.setGraphics(Graphics g)
       {
           this.g = g;
       }
    }
}
