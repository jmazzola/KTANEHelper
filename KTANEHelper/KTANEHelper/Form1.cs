using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;

// KTANE Helper - For people who have no one to play Keep Talking and Nobody Explodes with.
// Coded completely by Justin Mazzola


namespace KTANEHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SpeechRecognitionEngine speechEngine = null;
        SpeechSynthesizer speechTalk = null;

        GlobalInformation bombInfo = null;

        SimpleWires simpWires = null;

        bool DidSay(string[] phrases, string phrase, ref int retrievedIndex)
        {
            retrievedIndex = 0;

            foreach (string p in phrases)
            {
                if (p.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;

                retrievedIndex++;
            }

            return false;
        }

        void SanitizeColors(ref string str)
        {
            str = str.Replace("read", "red");
            str = str.Replace("blew", "blue");
            str = str.Replace(",", "");
        }

        void SetupSpeech()
        {
            speechEngine = new SpeechRecognitionEngine();
            speechTalk = new SpeechSynthesizer();

            speechEngine.LoadGrammarAsync(new DictationGrammar());
            speechEngine.SpeechRecognized += speechEngine_SpeechRecognized;

            speechEngine.SetInputToDefaultAudioDevice();
            speechEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        void speechEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Result.Text;

            int index = 0;

            string[] alternates = new string[e.Result.Alternates.Count];
            for (int i = 0; i < alternates.Count(); i++)
            {
                alternates[i] = e.Result.Alternates[i].Text;
                SanitizeColors(ref alternates[i]);
            }

            // -- BOMB INFORMATION --
            {
                // "serial digit ___ number (0-9)"
                if (DidSay(alternates, "serial digit", ref index))
                {
                    bombInfo.DetermineLastDigit(e.Result.Text);

                    if (bombInfo.LastSerialDigitOdd)
                        speechTalk.Speak("The last serial is odd");
                    else
                        speechTalk.Speak("The last serial is even");
                }

                // "has a vowel"
                if (DidSay(alternates, "has a vowel", ref index))
                {
                    bombInfo.SerialHasVowel = true;

                    if (bombInfo.SerialHasVowel)
                        speechTalk.Speak("The serial contains a vowel.");
                    else
                        speechTalk.Speak("The serial DOES NOT contain a vowel.");
                }

                // "doesn't have a vowel"
                if (DidSay(alternates, "doesn't have a vowel", ref index))
                {
                    bombInfo.SerialHasVowel = false;

                    if (bombInfo.SerialHasVowel)
                        speechTalk.Speak("The serial contains a vowel.");
                    else
                        speechTalk.Speak("The serial DOES NOT contain a vowel.");
                }
            }

            // -- SIMPLE WIRES --
            {
                // "easy wires [colors proceeded with commas]"
                if (DidSay(alternates, "easy wires", ref index))
                {
                    // Exclusive colors
                    GrammarBuilder grammarBuilder = new GrammarBuilder();
                    grammarBuilder.Append(new Choices(new string[] { "blue", "red", "yellow", "black", "white" }));

                    Grammar g = new Grammar(grammarBuilder);
                    g.Name = "colorRule";

                    speechEngine.LoadGrammar(g);

                    string correctSpeech = alternates[index];
                    string[] wireSequence = correctSpeech.Split(' ');
                    string[] input = new string[wireSequence.Length - 2];

                    for (int i = 2; i < wireSequence.Length; i++)
                        input[i - 2] = wireSequence[i];

                    speechTalk.SpeakAsync(simpWires.ParseSolution(input));
                }
            }
            // -- THE BUTTON -- 
            {
                // "button [color] [text]"
                if (DidSay(alternates, "button", ref index))
                {

                }
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Set up Speech Recognition and Synthesis
            SetupSpeech();

            // Set up the bomb information
            bombInfo = new GlobalInformation();

            // Set up SimpleWires module
            simpWires = new SimpleWires(ref bombInfo);

        }
    }
}
