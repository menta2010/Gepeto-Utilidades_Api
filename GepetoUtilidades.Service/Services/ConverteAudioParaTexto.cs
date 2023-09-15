    using GepetoUtilidades.Service.Config;
    using GepetoUtilidades.Service.Contract;
    using GepetoUtilidades.Service.Interfaces;
    using Google.Cloud.Speech.V1;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Options;
using NAudio.Wave;
using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace GepetoUtilidades.Service.Services
{
    public class ConverteAudioParaTexto : IConsultaAudioParaTexto
    {
        public async Task<string> ConverterAudioParaTexto(string caminhoEntrada)
        {
            ConverterParaWav(caminhoEntrada);

            var speechConfig = SpeechConfig.FromSubscription("2ff06bd5df9f4ba9a589ce5cb79633bd", "brazilsouth");
            var audioConfig = AudioConfig.FromWavFileInput(caminhoEntrada);
            StringBuilder textoCompleto = new StringBuilder();

            using (var recognizer = new SpeechRecognizer(speechConfig, "pt-BR", audioConfig))
            {
                var tcs = new TaskCompletionSource<int>();

                recognizer.Recognizing += (s, e) =>
                {
                    if (e.Result.Reason == ResultReason.RecognizingSpeech)
                    {
                        textoCompleto.Append(e.Result.Text + " ");
                    }
                };

                recognizer.Recognized += (s, e) =>
                {
                    if (e.Result.Reason == ResultReason.RecognizedSpeech)
                    {
                        Console.WriteLine($"Texto reconhecido: {e.Result.Text}");
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

                await recognizer.StartContinuousRecognitionAsync();

                await tcs.Task;

                await recognizer.StopContinuousRecognitionAsync();

                return textoCompleto.ToString();
            }
        }

        private void ConverterParaWav(string inputPath)
        {
            using (var reader = new MediaFoundationReader(inputPath))
            {
                WaveFileWriter.CreateWaveFile(inputPath, reader);
            }
        }
    }
}

