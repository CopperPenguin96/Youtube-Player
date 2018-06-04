using System;
using System.Windows.Forms;
using Gecko.Events;
using System.Text;

namespace YoutubePlayer
{
    public partial class YoutubePlayer : UserControl
    {
        string id;
        public string VideoID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                geckoWebBrowser1.LoadHtml(GetHtml());
            }
        }

        private string GetUrl()
        {
            return $"https://youtube.com/v/{id}";
        }
        public YoutubePlayer()
        {
            InitializeComponent();
            Console.WriteLine("Initializing Xulrunner...");
            Gecko.Xpcom.Initialize("Firefox/");
            Gecko.CertOverrideService.GetService().ValidityOverride += geckoWebBrowser1_ValidityOverride;
        }

        private void geckoWebBrowser1_ValidityOverride(object sender, CertOverrideEventArgs e)
        {
            e.OverrideResult = Gecko.CertOverride.Mismatch | Gecko.CertOverride.Time | Gecko.CertOverride.Untrusted;
            e.Temporary = true;
            e.Handled = true;
        }

        private string GetHtml()
        {
            return string.Format(@"
<html>
    <head>
        <style type='text/css\'>
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
        <div id = content >
            < iframe id='ytplayer' type='text/html' width='100%' height='100%' frameborder='0'
  src='http://www.youtube.com/embed/{0}?autoplay=0&version=3'
  frameborder='0'></iframe>
        </div>
    </body>
</html>", VideoID);
        }
    }
}
