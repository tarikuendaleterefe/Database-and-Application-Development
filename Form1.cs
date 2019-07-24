using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
namespace SpeechRecognition
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
      
        public Form1()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            Choices clist = new Choices();
            clist.Add(new string[] {"Hello", "Ilaali", "Galatoomi", "Cufadhu" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                return;
            }
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("Speech Recognized!" + e.Result.Text.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pb.ClearContent();
            pb.AppendText(textBox1.Text);
            ss.Speak(pb);
        }
    }
}
