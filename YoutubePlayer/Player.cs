using System;
using System.ComponentModel;
using System.Windows.Forms;
using Gecko.Events;
using System.Drawing;

namespace YoutubePlayer
{
    public partial class Player : UserControl
    {
        string id = "NpEaa2P7qZI";

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string VideoID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                if (!designMode) geckoWebBrowser1.LoadHtml(GetHtml());
            }
        }

        private string GetUrl()
        {
            return $"https://youtube.com/v/{id}?autoplay=0&version=3";
        }
        
        private bool designMode;
        public Player()
        {
            InitializeComponent();
            designMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            try
            {
                if (designMode) // If in design mode disables browser and puts a dummy cover
                {
                    Label label1 = new Label()
                    {
                        Width = this.Width,
                        Height = this.Height,
                        Location = new System.Drawing.Point(0, 0),
                        Text = "YoutubePlayer Control",
                        Font = new System.Drawing.Font("Times New Roman", 23),
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                        AutoSize = false,
                        BorderStyle = BorderStyle.Fixed3D,
                        BackColor = Color.Maroon
                    };
                    this.Controls.Add(label1);
                }
                else
                {
                    Console.WriteLine("Initializing Xulrunner...");
                    Gecko.Xpcom.Initialize("firefox\\");
                    Gecko.CertOverrideService.GetService().ValidityOverride += geckoWebBrowser1_ValidityOverride;
                    geckoWebBrowser1.Enabled = true;
                    geckoWebBrowser1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine("Unable to proceed: " + ex.ToString());
            }
        }

        private void Player_Load(object sender, EventArgs e)
        {
            Console.WriteLine($"Youtube Player Loaded ({Name})");
        }
        private void geckoWebBrowser1_ValidityOverride(object sender, CertOverrideEventArgs e)
        {
            e.OverrideResult = Gecko.CertOverride.Mismatch | Gecko.CertOverride.Time | Gecko.CertOverride.Untrusted;
            e.Temporary = true;
            e.Handled = true;
        }

        private string GetHtml()
        {
            return @"
<html>
    <head>
        <style type='text/css'>
            body, html
            {
                margin: 0; padding: 0; height: 100%; overflow: hidden;
            }

            #content
            {
                position:absolute; left: 0; right: 0; bottom: 0; top: 0px; 
            }
        </style>
    </head>
    <body>
        <div id='content'>
            <iframe id='ytplayer' type='text/html' width='100%' height='100%' frameborder='0'
  src='http://www.youtube.com/embed/" + VideoID + @"?autoplay=0&version=3'
  frameborder='0'></iframe>
        </div>
    </body>
</html>";
        }

        

    }
	public class ControlNotEnabledException : Exception
        {
            public ControlNotEnabledException() : base()
            {

            }

            public ControlNotEnabledException(string message) : base(message)
            {

            }

            public ControlNotEnabledException(string message, Exception inner) : base(message, inner)
            {

            }

            public ControlNotEnabledException(Exception inner) : base("", inner)
            {

            }
        }
}
