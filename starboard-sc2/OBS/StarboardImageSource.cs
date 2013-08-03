using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLROBS;
using System.Threading;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Starboard.View;
using Starboard.ViewModel;
using System.Windows;

namespace Starboard
{
    public class StarboardImageSource : AbstractImageSource, IDisposable
    {
        private Object textureLock = new Object();
        private Texture texture = null;
        private XElement config;

        private MainWindowView starboardView;

        private ScoreboardControl scoreboardControl;

        public StarboardImageSource(XElement config)
        {
            this.config = config;
            UpdateSettings();
        }
                
        override public void UpdateSettings()
        {
            if (starboardView == null)
            {
                starboardView = new MainWindowView(this);
                starboardView.Show();
            }

            if (scoreboardControl != null)
            {
                Size.X = (float)scoreboardControl.Width;
                Size.Y = (float)scoreboardControl.Height;

                if (texture != null)
                {
                    texture.Dispose();
                    texture = null;
                }

                texture = GS.CreateTexture((UInt32)scoreboardControl.Width, (UInt32)scoreboardControl.Height, GSColorFormat.GS_BGRA, null, false, false);
            }
            else
            {
                Size.X = 100;
                Size.Y = 100;
            }
        }


        private delegate void TickFunctionDelegate();
        private void renderBitmap()
        {
            if (scoreboardControl != null)
            {
                int width = (int)scoreboardControl.Width;
                int height = (int)scoreboardControl.Height;
                RenderTargetBitmap scoreboardBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

                VisualBrush elementBrush = new VisualBrush(scoreboardControl);
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext dc = drawingVisual.RenderOpen();

                dc.DrawRectangle(elementBrush, null, new Rect(0, 0, width, height));
                dc.Close();

                scoreboardBitmap.Render(scoreboardControl);
                WriteableBitmap wb = new WriteableBitmap(scoreboardBitmap);

                lock (texture)
                {
                    if (texture != null)
                    {
                        texture.SetImage(wb.BackBuffer, GSImageFormat.GS_IMAGEFORMAT_BGRA, (UInt32)(wb.PixelWidth * 4));
                    }
                }
            }
        }
        public override void Tick(float fSeconds)
        {
            Application.Current.Dispatcher.Invoke(new TickFunctionDelegate(renderBitmap));
        }

        
        override public void Render(float x, float y, float width, float height)
        {
            
            
            lock (textureLock)
            {
                if (texture != null)
                {
                    GS.DrawSprite(texture, 0xFFFFFFFF, x, y, x + width, y + height);
                }
            }
        }

        public void Dispose()
        {
            lock (textureLock)
            {
                if (texture != null)
                {
                    texture.Dispose();
                    texture = null;
                }
            }
        }
                
        public ScoreboardControl Visual {
            get {
                return scoreboardControl;
            }
            set {
                scoreboardControl = value;
            }
        }
    }
  
}
