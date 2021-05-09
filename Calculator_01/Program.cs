using System;

namespace Tuner
{
    public class Program
    {
        public static void Main()
        {
            Sound sound = new Sound();

            sound.StartDetect(sound.SelectInputDevice());
        }
    }
}
