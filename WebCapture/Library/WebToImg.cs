using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WebCapture.Library
{
    public class WebToImg
    {
        private Bitmap result;
        public Bitmap Capture(String url, int width = 1024, int height = 768)
        {
            Thread cptThread = new Thread(() => DoCapture(url,width,height));
            cptThread.SetApartmentState(ApartmentState.STA);
            cptThread.Start();
            cptThread.Join();
            return result;
        }
        public void DoCapture(String url, int width = 1024, int height = 768)
        {
            try
            {
               
                WebBrowser browser = new WebBrowser();
                browser.ScrollBarsEnabled = false;
                browser.ScriptErrorsSuppressed = true;
                browser.Navigate(url);
                while (browser.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }

                // Set the size of the WebBrowser control
                browser.Width = width;
                browser.Height = height;

                if (width == -1)
                {
                    // get image with full width
                    browser.Width = browser.Document.Body.ScrollRectangle.Width;
                }

                if (height == -1)
                {
                    //get image with full height
                    browser.Height = browser.Document.Body.ScrollRectangle.Height;
                }

               
                Bitmap bitmap = new Bitmap(browser.Width, browser.Height);
                browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
                browser.Dispose();
                result= bitmap;
            }
            catch (Exception ex)
            {
                result= null;

            }
          
        }
         
    }
}