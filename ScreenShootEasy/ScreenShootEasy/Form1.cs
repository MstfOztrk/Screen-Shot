using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenShootEasy
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //DLL for keyboard listen
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        //Loop start
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        Bitmap captureBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
        Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
        
        //Taking Screen Shot and Save image
        void GetScreenShot()
        {
            
            try
            {
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                captureBitmap.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\ScreenShot.Png", ImageFormat.Png);             
                captureGraphics.Dispose();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }   

        //Loop and keyboard listener
        private void timer1_Tick(object sender, EventArgs e)
        {   //Read ASCII
            for (byte i = 32; i < 127; i++)
            {   //Read Necessary code
                if ((int)GetAsyncKeyState(i) == 32768)
                {   //Listen PrtScSysRq Key only and translate ASCII
                    if((char)i==',')
                    {
                        GetScreenShot();
                    }
                   
                }
            }         
        }

        private void ScreenShot_DoubleClick(object sender, EventArgs e)
        {   //notify for close program
            Application.Exit();
        }
    }

  
}

