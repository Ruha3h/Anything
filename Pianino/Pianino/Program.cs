using System;
using System.Threading;
using NAudio.Wave;

namespace Pianino
{

    public class Buffer : object
    {
        public Buffer(ConsoleKey key)
        {
            this.key = key;
        }

        public ConsoleKey key;
    }

    static class Pianino
    {
        public static double[] Octaves ={
            261.6256,
            277.1826,
            293.6648,
            311.127,
            329.6276,
            349.2282,
            369.9944,
            391.9954,
            415.3047,
            440,
            466.1638,
            493.8833,
            523.2511,
            554.3653,
            587.3295,
            622.254,
            659.2551,
            698.4565,
            739.9888,
            783.9909,
            830.6094,
            880,
            932.3275,
            987.7666,
            1046.502,
            1108.731,
            1174.659,
            1244.508,
            1318.51,
            1396.913,
            1479.978,
            1567.982,
            1661.219,
            1760,
            1864.655,
            1975.533,
            2093.005,
            2217.461,
            2349.318,
            2489.016,
            2637.02,
            2793.826,
            2959.955,
            3135.963,
            3322.438,
            3520,
            3729.31,
            3951.066,
            4186.009,
            4434.922,
            4698.636,
            4978.032,
            5274.041,
            5587.652,
            5919.911,
            6271.927,
            6644.875,
            7040,
            7458.62,
            7902.133,
        };

        public static string[] OctavesPath = new string[] {
            "C4",
            "C#4",
            "D4",
            "D#4",
            "E4",
            "F4",
            "F#4",
            "G4",
            "G#4",
            "A4",
            "A#4",
            "B4",
            "C5",
            "C#5",
            "D5",
            "D#5",
            "E5",
            "F5",
            "F#5",
            "G5",
            "G#5",
            "A5",
            "A#5",
            "B5",
             "C6",
            "C#6",
            "D6",
            "D#6",
            "E6",
            "F6",
            "F#6",
            "G6",
            "G#6",
            "A6",
            "A#6",
            "B6",
        };
        public static int GetNote(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Z://Do0:
                    return 0;
                case ConsoleKey.S://DoD0:
                    return 1;
                case ConsoleKey.X://Re0:
                    return 2;
                case ConsoleKey.D://ReD0:
                    return 3;
                case ConsoleKey.C://Mi0:
                    return 4;
                case ConsoleKey.V://Fa0:
                    return 5;
                case ConsoleKey.G://FaD0:
                    return 6;
                case ConsoleKey.B://Sol0:
                    return 7;
                case ConsoleKey.H://SolD0:
                    return 8;
                case ConsoleKey.N://Lya0:
                    return 9;
                case ConsoleKey.J://LyaD0:
                    return 10;
                case ConsoleKey.M://Si0:
                    return 11;
                case ConsoleKey.OemComma://Do1:
                case ConsoleKey.Q://Do1:
                    return 12;
                case ConsoleKey.L://DoD1:
                case ConsoleKey.D2://DoD1:
                    return 13;
                case ConsoleKey.OemPeriod://Re1:
                case ConsoleKey.W://Re1:
                    return 14;
                case ConsoleKey.Oem1://ReD1:
                case ConsoleKey.D3://ReD1:
                    return 15;
                case ConsoleKey.Oem2://Mi1:
                case ConsoleKey.E://Mi1:
                    return 16;
                case ConsoleKey.R://Fa1:
                    return 17;
                case ConsoleKey.D5://FaD1:
                    return 18;
                case ConsoleKey.T://Sol1:
                    return 19;
                case ConsoleKey.D6://SolD1:
                    return 20;
                case ConsoleKey.Y://Lya1:
                    return 21;
                case ConsoleKey.D7://LyaD1:
                    return 22;
                case ConsoleKey.U://Si1:
                    return 23;
                case ConsoleKey.I://Do2:
                    return 24;
                case ConsoleKey.D9://DoD2:
                    return 25;
                case ConsoleKey.O://Re2:
                    return 26;
                case ConsoleKey.D0://ReD2:
                    return 27;
                case ConsoleKey.P://Mi2:
                    return 28;
                case ConsoleKey.Oem4://Fa2:
                    return 29;
                case ConsoleKey.OemPlus://FaD2:
                    return 30;
                case ConsoleKey.Oem6://Fa2:
                    return 31;
            }
            return -1;
        }

        public static void PlayNoteAsBeep(int Index)
        {
            if (Index != -1) Console.Beep(Convert.ToInt32(Octaves[Index]), 200);
        }

        public static void PlayNoteAsSound(object Index)
        {
            if (Convert.ToInt32(Index) != -1)
            {
                Console.WriteLine("Piano\\" + OctavesPath[Convert.ToInt32(Index)] + ".waw");
                using (var audioFile = new AudioFileReader(@"C:\Piano\" + OctavesPath[Convert.ToInt32(Index)] + ".wav"))
                {
                    Console.WriteLine("\tStep1");
                    using (var outputDevice = new WaveOutEvent())
                    {
                        Console.WriteLine("\t\tStep2");
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Console.WriteLine($"\t\t\t - {Index} - {Thread.CurrentThread.GetHashCode()} -");
                            Thread.Sleep(5000);
                        }
                    }
                }
            }
        }
    }

   

    class Program
    {
        static void Action(object keyBuffer)
        {
            Buffer temp = keyBuffer as Buffer;
            var NoteIndex = Pianino.GetNote(temp.key);

            Pianino.PlayNoteAsSound(NoteIndex);
        }
        static void Main(string[] args)
        {
            //Pianino.PlayNoteAsSound(10);

            while (true)
            {
                new Thread(Action).Start(new Buffer(Console.ReadKey(true).Key));
            }
        }
        
    }
}