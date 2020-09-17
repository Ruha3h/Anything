using System;
using System.Threading;

namespace Pianino
{
    class Program
    {
        static void Action(object keyBuffer)
        {
            Buffer temp = keyBuffer as Buffer;
            var NoteIndex = Pianino.GetNote(temp.key);

            Pianino.PlayNoteAsSound(NoteIndex);
        }
        static void Main()
        {
            while (true)
            {
                new Thread(Action).Start(new Buffer(Console.ReadKey(true).Key));
            }
        }
    }
}