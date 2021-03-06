﻿using System;
namespace Tollminder.Core.Services
{
    public interface IProgressDialogManager
    {
        void ShowProgressDialog(string title, string message);
        void CloseProgressDialog();
        void CloseAndShowMessage(string title, string message);
        void ShowMessage(string title, string message);
    }
}
