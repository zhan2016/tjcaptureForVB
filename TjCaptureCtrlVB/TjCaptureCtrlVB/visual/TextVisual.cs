using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TjCaptureCtrlVB.visual
{
    class TextVisual : DrawVisual
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
        private string drawText;


         void DrawVisual.render()
        {
            if (!endpoint.Equals(Point.Empty))
            {
                Pen pen = new Pen(this.drawbrush);
                pen.Width = this.lineWidth;
                g.DrawString(this.drawText, new Font("Arial", 12f), this.drawbrush, endpoint);
            }
        }

        public TextVisual(string text)
         {
             drawText = text;
         }

        void DrawVisual.setGraphics(Graphics g)
        {
            this.g = g;
        }
        ~TextVisual()
        {
            if (this.g != null)
            {
                this.g.Dispose();
            }
        }
    }
}
