using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using TjCaptureCtrlVB.visual;

namespace TjCaptureCtrlVB
{
    class circleVisual:DrawVisual
    {
        //set line color
        private LineColor lineCol;
        public LineColor LineCol
        {
            set
            {
                switch (value)
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
                switch (value)
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

        private Brush drawbrush;
        private float lineWidth;
        private Point startpoint;
        private Point endpoint;
        private System.Drawing.Graphics g;


        private void render()
        {
            
        }

        void DrawVisual.render()
        {
            if (!endpoint.Equals(Point.Empty))
            {
                Pen pen = new Pen(this.drawbrush);
                pen.Width = this.lineWidth;
                Rectangle drawRectangle = new Rectangle(this.startpoint, new Size(this.endpoint.X - this.startpoint.X, this.endpoint.Y - this.startpoint.Y));
                GraphicsPath graphicspath = new GraphicsPath();
                graphicspath.AddEllipse(startpoint.X, this.startpoint.Y, endpoint.X - endpoint.Y, endpoint.X - endpoint.Y);
                g.DrawPath(pen, graphicspath);
            }
        }

        void DrawVisual.setGraphics(Graphics g)
        {
            this.g = g;
        }
        ~circleVisual()
        {
            if (this.g != null)
            {
                this.g.Dispose();
            }
        }
    }
}
