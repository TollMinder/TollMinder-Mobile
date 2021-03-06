﻿using System;
using System.ComponentModel;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using Tollminder.Touch.Extensions;
using UIKit;

namespace Tollminder.Touch.Controls
{
    [Register("TextFieldValidationWithImage"), DesignTimeVisible(true)]
    public class TextFieldValidationWithImage : UIView
    {
        UITapGestureRecognizer _clickAction;
        public UIImageView LeftImageView { get; private set; }
        public TextFieldWithValidator TextFieldWithValidator { get; private set; }

        public TextFieldValidationWithImage() : base()
        {
            InitObjects();
        }

        public TextFieldValidationWithImage(IntPtr handle) : base(handle)
        {
            InitObjects();
        }

        public TextFieldValidationWithImage(NSCoder coder) : base(coder)
        {
            InitObjects();
        }

        public TextFieldValidationWithImage(CGRect rect) : base(rect)
        {
            InitObjects();
        }

        public void SetClickAction(UITapGestureRecognizer action)
        {
            _clickAction = action;
            TextFieldWithValidator.AddGestureRecognizer(_clickAction);
        }

        void InitObjects()
        {
            LeftImageView = new UIImageView() { ContentMode = UIViewContentMode.ScaleAspectFit };
            TextFieldWithValidator = new TextFieldWithValidator();

            this.AddIfNotNull(LeftImageView);
            this.AddIfNotNull(TextFieldWithValidator);
            this.AddConstraints(
                LeftImageView.AtLeftOf(this),
                LeftImageView.Height().EqualTo(20),
                LeftImageView.Width().EqualTo(20),
                LeftImageView.WithSameCenterY(this),

                TextFieldWithValidator.ToRightOf(LeftImageView),
                TextFieldWithValidator.AtRightOf(this),
                TextFieldWithValidator.AtTopOf(this, 8),
                TextFieldWithValidator.AtBottomOf(this, 8)
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LeftImageView?.Dispose();
                LeftImageView = null;

                TextFieldWithValidator?.Dispose();
                TextFieldWithValidator = null;

                TextFieldWithValidator?.RemoveGestureRecognizer(_clickAction);
            }

            base.Dispose(disposing);
        }
    }
}