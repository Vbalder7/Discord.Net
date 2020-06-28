using System;

namespace Cs_Bot
{
    class Program
    {
        static void Main(string[] args)
           => new Bot().MainAsync().GetAwaiter().GetResult();
    }
}
