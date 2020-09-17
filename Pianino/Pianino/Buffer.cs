using System;

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
}