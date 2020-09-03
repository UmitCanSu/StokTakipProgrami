using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace StokTakipUygulamasi.Class
{
    public class Bonus
    {

        public static void SesCal() 
        {
            MediaPlayer mediaplayer = new MediaPlayer();
            mediaplayer.Open(new Uri("audios/popupAudio.wav", UriKind.Relative));
            mediaplayer.Play();
        }


        public static void PopupShow(Popup popup)  // Popup açma kapatma
        {
            SesCal();

            popup.IsOpen = true;
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(4)
            };

            timer.Tick += delegate (object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                if (popup.IsOpen)
                {
                    popup.IsOpen = false;
                }
            };

            timer.Start();

        }
    }
}
