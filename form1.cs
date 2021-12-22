/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Name                                                                                                        //
// by KentenCpu | humanoid capable of self replicating GIF images;                                             //
//                                                                                                             //
// find this project on **** please do not copy material I create,,                                            //
// thus see MS documentation; and dont do drugs and dont go to school, or do both, or do one or the other;     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace thx_wincursor_info
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
            start();
        }
        // a thread used to keep mouse information up to date, such as updating movement coordinates
        Thread thr = new Thread(() => { });
        // the mouse positions and display rectangles, such as location and width and height
        Point mousecPos = new Point(0, 0);
        Rectangle displayBounds = new Rectangle(0, 0, 0, 0);
        // when captured the captured point will be kept in this variable for the text to display even if the mouse is moving.
        Rectangle caputredRec = new Rectangle(0, 0, 0, 0);

        // to have the mouse coordinates updated while off the main form, create an invisble form the size of the display (dont later)
        Form formc = new Form();

        // the startup method, create a thread to run starthread() function
        void start()
        {
            formc.Bounds = formc.DesktopBounds;
           
            // create and start the update thread
            thr = new Thread(starthread);
            thr.IsBackground = true;
            thr.Start();
        }
       
        // the main thread function with the loop
        void starthread()
        {
            // set some startup info for the captured mouse pos and display bounds, this will be for textBox1
            caputredRec = new Rectangle(0, 0, formc.DesktopBounds.Width, formc.DesktopBounds.Width);
            // set topmost and invisible
            formc.Visible = false;
            formc.TopMost = true;  // topmost or toplevel, or both should keep it topmost without updates
            formc.TopLevel = true;
            // add an event handler for the invisble formc
            formc.MouseMove += new System.Windows.Forms.MouseEventHandler(formcMouseMove);
            // debug, set some text though it should be replaced instatntly
            textBox1.Text += "formc bounds are" + formc.Bounds.X.ToString() + formc.Bounds.Y.ToString();

            // note while in threads, event handlers in c# seem to not work unless a workaround which there may be a better way
            // though creating an action using methodinvoker from any control on the main form seems to do the trick
            while (true)
            {
                Thread.Sleep(50);
                displayBounds = Screen.GetBounds(formc);
                textBox1.Invoke(new MethodInvoker(() =>
                {
                    textBox1.Text = String.Format(@"");
                }));
                textBox1.Invoke(new MethodInvoker(() =>
                {
                    textBox1.Text += String.Format(@"Mouse Coords: {0}, {1}", MousePosition.X, MousePosition.Y);
                }));
                textBox1.Invoke(new MethodInvoker(() => { textBox1.Text += Environment.NewLine; }));
                textBox1.Invoke(new MethodInvoker(() =>
                {
                    textBox1.Text += String.Format(@"Display Dim: W,H,X,Y:{0}, {1}, {2}, {3}", displayBounds.Width, displayBounds.Height, MousePosition.X, MousePosition.Y);
                }));
                textBox1.Invoke(new MethodInvoker(() => { textBox1.Text += Environment.NewLine; }));
                textBox1.Invoke(new MethodInvoker(() =>
                {
                    textBox1.Text += String.Format(@"Caputred Bounds: W,H,X,Y:{0}, {1}, {2}, {3}", caputredRec.Width, caputredRec.Height, caputredRec.X, caputredRec.Y);
                }));
                textBox1.Invoke(new MethodInvoker(() => { textBox1.Text += Environment.NewLine; }));
            }
        }

        // event should do stuff on mouse movemnt, such as update the invisble form to a variable
        void formcMouseMove(object sender, MouseEventArgs e)
        {
            formc.Bounds = formc.DesktopBounds;
            displayBounds = Screen.GetBounds(this);
        }




        // anythin below is notes or unsued
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            

            // mouseCurrent = 
        }
    }
}
