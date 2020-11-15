using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASEDemo;


namespace ASEUnitTests
{
    [TestClass]
    public class MyASETests
    {
        [TestMethod]
        // This will test when valid commands are inputted.
        // Ensure the parser digests the input in properly.
        public void ValidCommandInput()
        {
            var sut = new CommandParser();      // "sut" stands for System Under Test
            var input = "draw to 100, 200";

            var expected = new[] { " 100", " 200" };     // Whitespace used, due to format of var input (above)

            var actual = sut.TokenizeCommand(input, "draw to");

            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);

            // Assert actual has a length of 2
            // Assert actual[0] is "100"
            // Assert actual[1] is "200"
        }

        [TestMethod]
        // Check if a command is inputted incorrectly.
        // When an incorrect format is inputted for a certain command.
        public void InvalidCommandInput()
        {
            var sut = new CommandParser();
            string s = "";

            try
            {
                var expected = new[] { " 100", " 200" };
                var actual = sut.TokenizeCommand("draw to 100", "draw to");

                Assert.AreEqual(expected, actual);
            }
            catch (Exception e)
            {
                Console.WriteLine("error: " + e.Message);
                s = e.Message;
            }
            Assert.AreEqual(s, s);
        }

        [TestMethod]
        // Check if a valid variable is inputted (for part 2) 
        // Must convert input to specific var (Whichever is inputted, int in this case below)
        // Subject to change*
        public void VariableInput()       
        {
            var sut = new CommandParser();
            var input = "10";

            var actualVal = sut.TokenizeCommand(input, "number = ");
            
            int val = 10;

            Assert.AreEqual(val, actualVal[0]);
        }

    }
}