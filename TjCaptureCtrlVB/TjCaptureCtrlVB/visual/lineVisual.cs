using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.visual
{
    // the color of  line
    public enum LineColor
    {
        white,
        black,
        yellow,
        red,
        blue,
        green
    }

    //the width of line(pounds)
    public enum LineWidth
    {
        width2p,
        width4p,
        width6p
    }

    class lineVisual:DrawVisual
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

        private ShapeType linetype;
        public ShapeType LineTy
        {
            set
            {
                if (value != ShapeType.line && value != ShapeType.arrow)
                {
                    this.linetype = ShapeType.line;
                }
                else
                {
                    this.linetype = value;
                }
            }
        }

        public Brush drawbrush;
        private float lineWidth;
        public Point startpoint;
        public Point endpoint;
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


       
        public lineVisual()
        {
            
        }

       ~lineVisual()
        {
            if (this.g != null)
            {
                this.g.Dispose();
            }
        }

        void DrawVisual.render()
       {
           if (!endpoint.Equals(Point.Empty))
           {
               Pen pen = new Pen(this.drawbrush);
               pen.Width = this.lineWidth;

               if (this.linetype == ShapeType.arrow)
               {
                   //Specify the EndCap, because we're drawing a right-facing arrow
                   pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
               }

               g.DrawLine(pen, this.startpoint, this.endpoint);
           }
       }

        void DrawVisual.setGraphics(Graphics g)
        {
            this.g = g;
        }
    }
}
