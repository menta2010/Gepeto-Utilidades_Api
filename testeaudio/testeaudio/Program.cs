// See https://aka.ms/new-console-template for more information
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using NAudio.Wave;
using System.Text;

Console.WriteLine("Hello, World!");

var caminhoEntrada = "C:/Users/NatalinoEstevesRodri/Documents/EntradaAudio/teste2.mp3";
var caminhoSaida = "C:/Users/NatalinoEstevesRodri/Documents/SaidaAudio/saida2.wav";

ConverterParaWav(caminhoEntrada, caminhoSaida);

var speechConfig = SpeechConfig.FromSubscription("2ff06bd5df9f4ba9a589ce5cb79633bd", "brazilsouth");
var audioConfig = AudioConfig.FromWavFileInput(caminhoSaida);
StringBuilder textoCompleto = new StringBuilder();

using (var recognizer = new SpeechRecognizer(speechConfig, "pt-BR", audioConfig))
{
    var tcs = new TaskCompletionSource<int>();

    recognizer.Recognizing += (s, e) =>
    {
        if (e.Result.Reason == ResultReason.RecognizingSpeech)
        {
            textoCompleto.Append(e.Result.Text + " "); // Armazene o texto conforme ele é reconhecido
        }
    };

    recognizer.Recognized += (s, e) =>
    {
        if (e.Result.Reason == ResultReason.RecognizedSpeech)
        {
            Console.WriteLine($"Texto reconhecido: {e.Result.Text}"); // Isto é apenas para visualizar cada pedaço reconhecido
        }
    };

    recognizer.Canceled += (s, e) =>
    {
        Console.WriteLine($"Cancelado: Razão={e.Reason}");
        if (tcs.Task.Status != TaskStatus.RanToCompletion)
            tcs.SetResult(0);
    };

    recognizer.SessionStopped += (s, e) =>
    {
        Console.WriteLine("\nSessão parada.");
        if (tcs.Task.Status != TaskStatus.RanToCompletion)
            tcs.SetResult(0);
    };


    recognizer.StartContinuousRecognitionAsync().Wait();

    tcs.Task.Wait();

    recognizer.StopContinuousRecognitionAsync().Wait();
}

Console.WriteLine($"Texto completo reconhecido: {textoCompleto.ToString()}");


static void ConverterParaWav(string inputPath, string outputPath)
{
    using (var reader = new MediaFoundationReader(inputPath))
    {
        WaveFileWriter.CreateWaveFile(outputPath, reader);
    }
}