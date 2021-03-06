﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Tollminder.Core.Helpers;
using Tollminder.Core.Models;
using Tollminder.Core.Services;
using Tollminder.Core.Utils;

namespace Tollminder.Droid.Services
{
    //TODO: Google Speech Recognition timeout http://stackoverflow.com/questions/38150312/google-speech-recognition-timeout
    public class DroidSpeechToTextService : Java.Lang.Object, ISpeechToTextService, IRecognitionListener
    {
        readonly IPlatform PlatformService;
        readonly ITextFromSpeechMappingService MappingService;
        readonly ITextToSpeechService TextToSpeechService;
        
        TaskCompletionSource<bool> _recognitionTask;

        SpeechRecognizer _speechRecognizer;
        Timer _timer;
        Handler _handler;
        AlertDialog _dialog;
        bool _firstInit = true;
        bool _dialogWasManuallyAnswered;
        bool _isMusicRunning;
        bool isTimeStarted;

        string _question;
        public string Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
            }
        }

        Intent _voiceIntent;
        public Intent VoiceIntent
        {
            get
            {
                if (_voiceIntent == null)
                {
                    _voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                    _voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                    _voiceIntent.PutExtra(RecognizerIntent.ExtraCallingPackage, "com.tollminder");
                    _voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, new Java.Lang.Long(10000));
                    _voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, new Java.Lang.Long(10000));
                    _voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, new Java.Lang.Long(25000));
                    _voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                    _voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, "en-US");
                }

                return _voiceIntent;
            }
        }

        public DroidSpeechToTextService()
        {
            PlatformService = Mvx.Resolve<IPlatform>();
            MappingService = Mvx.Resolve<ITextFromSpeechMappingService>();
            TextToSpeechService = Mvx.Resolve<ITextToSpeechService>();
        }

        public Task<bool> AskQuestion(string question)
        {
            _recognitionTask = new TaskCompletionSource<bool>();

            TimerManager(question);

            return _recognitionTask.Task;
        }

        async Task AskQuestionMethod(string question)
        {
            if (_speechRecognizer != null)
            {
                //StopRecognizer();
                CancelDialog();
            }

            _isMusicRunning = PlatformService.IsMusicRunning;

            if (_isMusicRunning)
                PlatformService.PauseMusic();

            _handler = new Handler(Application.Context.MainLooper);

            if (await TextToSpeechService.Speak(question, false))
            {
                if (await TextToSpeechService.Speak("Please, answer yes or no after the tone", false))
                {
                    if (isTimeStarted)
                    {
                        Question = question;
                        StartSpeechRecognition();
                    }
                }
            }
        }

        void StartSpeechRecognition()
        {
            _handler.Post(() =>
           {
               ShowDialog();
               StartRecognizer();
           });
        }

        void StartRecognizer()
        {
            _speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(Application.Context);

            PlatformService.SetAudioEnabled(_firstInit);

            _speechRecognizer.SetRecognitionListener(this);
            _speechRecognizer.StartListening(VoiceIntent);
        }

        void StopRecognizer()
        {
            _speechRecognizer?.StopListening();
            _speechRecognizer?.Cancel();
            _speechRecognizer?.Destroy();
            _speechRecognizer = null;
            CancelDialog();
        }

        private void TimerManager(string question)
        {
            StopRecognizer();
            if (!isTimeStarted)
            {
                isTimeStarted = true;
                _timer = new Core.Utils.Timer((s) => { AskQuestionMethod(question); }, question, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 15), true);
            }
            else
            {
                _timer.Cancel();
                isTimeStarted = false;
            }
        }

        void ShowDialog()
        {
            if (PlatformService.IsAppInForeground && _dialog == null)
            {
                try
                {
                    _dialogWasManuallyAnswered = false;
                    _dialog = new AlertDialog.Builder(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity)
                            .SetTitle(Question)
                            .SetMessage("Please, answer yes or no after the tone")
                            .SetCancelable(false)
                            .SetPositiveButton("Yes", (sender, e) => SetDialogAnswer(true))
                            .SetNegativeButton("No", (sender, e) => SetDialogAnswer(false))
                            .Show();
                }
                catch (Exception e)
                {
                    Mvx.Trace(e.Message + e.StackTrace);
                }
            }
        }

        void SetDialogAnswer(bool result)
        {
            TimerManager(Question);
            _dialogWasManuallyAnswered = true;
            //_speechRecognizer?.StopListening();
            //CancelDialog();
            _recognitionTask.TrySetResult(result);
        }

        void CancelDialog()
        {
            if (_dialog?.IsShowing ?? false)
                _dialog?.Cancel();
            _dialog = null;
        }

        public void OnBeginningOfSpeech()
        {
            Console.WriteLine("OnBeginningOfSpeech");
        }

        public void OnBufferReceived(byte[] buffer)
        {
            Console.WriteLine("OnBufferReceived");
        }

        public void OnEndOfSpeech()
        {
            Console.WriteLine("OnEndOfSpeech");
        }

        public void OnError([GeneratedEnum] SpeechRecognizerError error)
        {
            Log.LogMessage("SpeechRecognizerError = " + error);

            //if (!_dialogWasManuallyAnswered && (error == SpeechRecognizerError.NoMatch || error == SpeechRecognizerError.SpeechTimeout))
            //    StartSpeechRecognition();
        }

        public void OnEvent(int eventType, Bundle @params)
        {
            Console.WriteLine("OnEvent");
        }

        public void OnPartialResults(Bundle partialResults)
        {
            Console.WriteLine("OnPartialResults");
        }

        public void OnReadyForSpeech(Bundle @params)
        {
            Console.WriteLine("OnReadyForSpeech");
            PlatformService.SetAudioEnabled(true);
        }

        public void OnResults(Bundle results)
        {
            Console.WriteLine("OnResults");
            if (!_dialogWasManuallyAnswered)
            {
                TimerManager(Question);
                var res = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
                Log.LogMessage($"Speech recognition results = {string.Join(", ", res)}");
                var answer = MappingService.DetectAnswer(res);

                //_handler.Post(() =>
                //{
                //    if (answer != AnswerType.Unknown)
                //        CancelDialog();

                //    StopRecognizer();
                //});

                //_firstInit = true;

                if (answer != AnswerType.Unknown)
                {
                    TextToSpeechService.Speak($"Your answer is {answer.ToString()}", false).Wait();
                    if (_isMusicRunning)
                        PlatformService.PlayMusic();
                    _recognitionTask.TrySetResult(answer == AnswerType.Positive);
                }
                else
                    TimerManager(Question);
            }
        }

        public void OnRmsChanged(float rmsdB)
        {
            Console.WriteLine("OnRmsChanged");
        }
    }
}

