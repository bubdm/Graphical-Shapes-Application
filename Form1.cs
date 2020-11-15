using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ASEDemo
{
    public partial class Form1 : Form
    {
        public static readonly Bitmap OutputBitmap = new Bitmap(444, 388);   //readonly (const) ensure the size never changes during runtime (determines size of image being drawn)
        readonly CanvasCommands Canvas;

        public Form1()
        {
            InitializeComponent();
            Canvas = new CanvasCommands(Graphics.FromImage(OutputBitmap));  //passes bitmap thru the graphics area (on the OutputArea below)
        }

        private void OutputArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(OutputBitmap, 0, 0);
        }

        public void InvalidInput()     //displays mssg when invalid command/parameter inputted
        {
            var image = new Bitmap(this.OutputArea.Width, this.OutputArea.Height);
            var font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString("Invalid command/parameter entered", font, Brushes.Black, new Point(0, 0));
            this.OutputArea.Image = image;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void CommandLineInterface_KeyDown(object sender, KeyEventArgs e)
        {
            string[] commandLines = CommandLineInterface.Lines;     // Get each line from the CommandLineInterface and separate them.
            string[] allLines = ProgramArea.Lines;         // Get each line from the ProgramArea too.
            var parser = new CommandParser();       // Uses the CommandParser() to parse input.


            if (e.KeyCode == Keys.Enter || commandLines.Contains("run"))
            {
                // Do something for each line.
                foreach (var line in allLines.Union(commandLines))
                {
                    try
                    {
                        // If it's the "draw to" line...
                        if (line.Trim().ToLower().Contains("draw to"))
                        {
                            // Get the parts past the command.
                            var tokens = parser.TokenizeCommand(line, "draw to");
                            // If they are both numbers...
                            if (int.TryParse(tokens[0].Trim(), out int x) &&
                                int.TryParse(tokens[1].Trim(), out int y))
                            {
                                // Draw a line to the indicated position.
                                Canvas.DrawTo(x, y);
                            }
                        }
                        else if (line.Trim().ToLower().Contains("rect"))
                        {
                            // Get the parts past the command.
                            var tokens = parser.TokenizeCommand(line, "rect");
                            // If they are both numbers...
                            if (int.TryParse(tokens[0].Trim(), out int t) &&
                                int.TryParse(tokens[1].Trim(), out int v))
                            {
                                // Draw to the indicated position.
                                Canvas.DrawRectangle(t, v);
                            }
                        }
                        else if (line.Trim().ToLower().Contains("circle"))
                        {
                            // Get the parts past the command.
                            var tokens = parser.TokenizeCommand(line, "circle");
                            // If they are both numbers...
                            if (float.TryParse(tokens[0].Trim(), out float R))
                            {
                                // Draw to the indicated position.
                                Canvas.DrawCircle(R);
                            }
                        }
                        else if (line.Trim().ToLower().Contains("triangle"))
                        {
                            // Get the parts past the command.
                            var tokens = parser.TokenizeCommand(line, "triangle");

                            // If they are both numbers...
                            if (int.TryParse(tokens[0].Trim(), out int T) &&
                                int.TryParse(tokens[1].Trim(), out int T2))
                            {
                                // Draw to the indicated position.
                                Canvas.DrawTriangle(T, T2);
                            }
                        }
                        else if (line.Trim().ToLower().Contains("move to"))
                        {
                            // Get the parts past the command.
                            var tokens = parser.TokenizeCommand(line, "move to");
                            // If they are both numbers...
                            if (int.TryParse(tokens[0].Trim(), out int move) &&
                                int.TryParse(tokens[1].Trim(), out int move2))
                            {
                                // Draw to the indicated position.
                                Canvas.MovePen(move, move2);
                            }
                        }
                        else if (line.Trim().ToLower().Contains("pen colour"))
                        {
                            var tokens = parser.TokenizeCommand(line, "pen colour");
                            // to ensure its a string...
                            if (tokens.Length > 0)
                            {
                                var colour = tokens[0].Trim();
                                Canvas.PenColour(colour);
                            }
                        }
                        else if (line.Trim().ToLower().Contains("fill pen"))
                        {
                            var tokens = parser.TokenizeCommand(line, "fill pen");
                            // to ensure its a string...
                            if (tokens.Length > 0)
                            {
                                var mode = tokens[0].Trim();
                                Canvas.FillPen(mode);
                            }
                        }
                        else if (line.Trim().ToLower().Equals("clear"))
                        {
                            Canvas.Clear();
                            break;
                        }

                        else if (line.Trim().ToLower().Equals("reset"))
                        {
                            Canvas.Reset();
                            break;
                        }
                        else if (line.Equals("run") || line.Equals(""))     // "" is when return is pressed
                        {
                            Console.WriteLine("no error to display...");    // Both are correct inputs
                        }
                        else
                        {
                            InvalidInput();
                        }
                    }
                    catch (IndexOutOfRangeException)    // When wrong input entered 
                    {
                        InvalidInput();
                    }
                }
                CommandLineInterface.Text = ""; // Clears the CLI
                Refresh();  // Ensures the user input is generated stryt after
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramArea.AppendText(ProgramArea.Text);
            System.IO.File.WriteAllText(@"C:\Users\Mhasa\Desktop\ASEDemo.txt", ProgramArea.Text.Replace("\n", Environment.NewLine));
        }                                                       //saves as ASEDemo

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opentext = new OpenFileDialog();
            if (opentext.ShowDialog() == DialogResult.OK)
            {
                ProgramArea.Text = File.ReadAllText(opentext.FileName);
            }
        }

        private void ProgramArea_TextChanged(object sender, EventArgs e)
        {
        }

        private void MenuBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}