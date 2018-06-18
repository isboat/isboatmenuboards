namespace MenuBoards.Services
{
    public sealed class IoC
    {
        private static readonly SlideService instance = new SlideService();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static IoC()
        {
        }

        private IoC()
        {
        }

        public static SlideService Instance
        {
            get
            {
                return instance;
            }
        }
    }

    public sealed class DisplayIoC
    {
        private static readonly DisplayService instance = new DisplayService(IoC.Instance);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DisplayIoC()
        {
        }

        private DisplayIoC()
        {
        }

        public static DisplayService Instance
        {
            get
            {
                return instance;
            }
        }
    }
}