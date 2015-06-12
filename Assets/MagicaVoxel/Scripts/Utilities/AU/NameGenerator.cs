namespace AU
{

    public class NameGenerator
    {
        int currentIndex;

        public string prefix;
        public string postfix;

        public NameGenerator()
        {
            currentIndex = 0;
        }

        public NameGenerator(string _prefix, string _postfix)
        {
            currentIndex = 0;
            prefix = _prefix;
            postfix = _postfix;
        }

        public string next()
        {
            currentIndex += 1;
            return prefix + currentIndex.ToString() + postfix;
        }

        public void reset()
        {
            currentIndex = 0;
        }
    }

}