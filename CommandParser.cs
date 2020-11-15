namespace ASEDemo
{
    public class CommandParser  //made public for unit tests
    {
        public string[] TokenizeCommand(string input, string commandText)   //parser method
        {
            var trimmed = input.Trim();
            var pastCommand = trimmed.Substring(commandText.Length);

            // Now pastCommand be something like " 100,100".
            // I can split that
            var tokens = pastCommand.Split(',');
            return tokens;
        }
    }
}
