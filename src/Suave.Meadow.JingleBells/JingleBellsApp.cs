using System;
using System.Linq;
using System.Threading;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Audio;
using Meadow.Hardware;

namespace Suave.Meadow.JingleBells
{
    public class JingleBellsApp : App<F7Micro, JingleBellsApp>
    {
        readonly PiezoSpeaker piezoSpeaker;

        const int LENGTH = 26;
        const string NOTES = "eeeeeeegcde fffffeeeeddedg";
        readonly int[] BEATS = { 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2 };
        const int TEMPO = 300;

        public JingleBellsApp()
        {
            piezoSpeaker = new PiezoSpeaker(Device.CreatePwmPort(Device.Pins.D05));

            PlayJingleBells();
        }

        protected void PlayJingleBells()
        {
            while (true)
            {
                Console.WriteLine("Playing jingle bells!");
                for (int i = 0; i < LENGTH; i++)
                {
                    if (NOTES[i] == ' ')
                    {
                        Thread.Sleep(BEATS[i] * TEMPO); // rest
                    }
                    else
                    {
                        PlayNote(NOTES[i], BEATS[i] * TEMPO);
                    }

                    // pause between notes
                    Thread.Sleep(TEMPO / 2);
                }
                Console.WriteLine("From the top!");
            }
        }
        void PlayNote(char note, int duration)
        {
            char[] names = { 'c', 'd', 'e', 'f', 'g', 'a', 'b', 'C' };
            int[] tones = { 1915, 1700, 1519, 1432, 1275, 1136, 1014, 956 };

            // play the tone corresponding to the note name
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == note)
                {
                    piezoSpeaker.PlayTone(tones[i], duration);
                }
            }
        }
    }
}
