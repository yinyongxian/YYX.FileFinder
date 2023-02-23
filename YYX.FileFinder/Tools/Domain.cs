namespace YYX.FileFinder.Tools
{
    public static class Domain
    {
        public static string Value
        {
            get { return "http://localhost:" + Port; }
        }

        public static int Port
        {
            get
            {
                return 12321;
            }
        }
    }
}
