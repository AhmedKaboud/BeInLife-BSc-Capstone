using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace BNLife
{
    class SpeechControl
    {
        public SpeechRecognitionEngine recognitionEngine;

        //Constructor to Start Speech Recognition
        public void IntializeSpeech(string[] choice)
        {
            // Create a new SpeechRecognitionEngine instance.
            recognitionEngine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            recognitionEngine.SetInputToDefaultAudioDevice();

            //Call Create Grammer to Add the List Command to it
            Grammar cg = CreateSampleGrammar(choice);

            recognitionEngine.LoadGrammar(cg);
        }

        //Create the Grammer with List of Commands
        private Grammar CreateSampleGrammar(string[] choice)
        {
            Choices commandChoices = new Choices();
            commandChoices.Add(choice);

            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(commandChoices);

            // Create the Grammar instance.
            Grammar g = new Grammar(grammarBuilder);

            return g;
        }

        //start Recognition with single command then close the thread
        public void StartSingleAsync()
        {
            // Start recognition.
            recognitionEngine.RecognizeAsync(RecognizeMode.Single);
        }

        //start Recognition with Multi command then close the thread
        public void StartMultiAsync()
        {
            // Start recognition.
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

    }
}
