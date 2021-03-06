﻿using System;
using System.Threading.Tasks;

namespace Tollminder.Core.Services
{
    public interface ITextToSpeechService
    {
        Task<bool> Speak(string text, bool disableMusic = false);
		bool IsEnabled { get; set; }
    }
}