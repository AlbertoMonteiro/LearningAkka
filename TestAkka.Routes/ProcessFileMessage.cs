namespace TestAkka.Routes
{
    public class ProcessFileMessage
    {
        public string FilePath { get; }

        public ProcessFileMessage(string filePath)
        {
            FilePath = filePath;
        }
    }
}