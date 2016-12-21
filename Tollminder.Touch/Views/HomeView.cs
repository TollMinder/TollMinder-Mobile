﻿using System;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Google.SignIn;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using Tollminder.Core;
using Tollminder.Core.Converters;
using Tollminder.Core.Helpers;
using Tollminder.Core.ViewModels;
using Tollminder.Touch.Controllers;
using Tollminder.Touch.Controls;
using Tollminder.Touch.Extensions;
using Tollminder.Touch.Helpers;
using Tollminder.Touch.Interfaces;
using UIKit;
using Foundation;
using Tollminder.Touch.Converters;
using System.Diagnostics;

namespace Tollminder.Touch.Views
{
    public class HomeView : BaseViewController<HomeViewModel>, ISignInUIDelegate, ICleanBackStack
    {
        RoundedButton _trackingButton;
        RoundedButton _profileButton;
        RoundedButton _payButton;
        RoundedButton _payHistoryButton;
        RoundedButton _callCentergButton;
        //RoundedButtonManager RoundedButtonManager;
        public new HomeViewModel ViewModel { get { return base.ViewModel as HomeViewModel; } }
        
        public HomeView()
        {
        }

        public HomeView(IntPtr handle) : base(handle)
        {
        }

        public HomeView(string nibName, Foundation.NSBundle bundle) : base(nibName, bundle)
        {
        }

        protected override void InitializeObjects()
        {
            base.InitializeObjects();

            var topView = new UIView();
            var centerView = new UIView();
            var bottomView = new UIView();
            var applicationLogo = new UIImageView(UIImage.FromBundle(@"Images/home_logo.png"));
            var callCenterLabel = new UILabel();
            var applicationBoard = new UIImageView(UIImage.FromBundle(@"Images/home_board.png"));
            //_roundedButtonManager = new RoundedButtonManager();

            // Hide navigation bar
            NavigationController.SetNavigationBarHidden(true, false);
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile(@"Images/home_background.png").Scale(View.Frame.Size));//EnvironmentInfo.CheckDevice().Scale(View.Frame.Size));
            applicationLogo.Frame = new CoreGraphics.CGRect(10, 10, applicationLogo.Image.CGImage.Width, applicationLogo.Image.CGImage.Height);
            topView.AddIfNotNull(applicationLogo);
            topView.AddConstraints(
                applicationLogo.WithRelativeWidth(topView, 0.5f),
                applicationLogo.WithRelativeHeight(topView, 0.18f),
                applicationLogo.WithSameCenterX(topView),
                applicationLogo.WithSameCenterY(topView)
            );
            //topView.BackgroundColor = UIColor.LightGray;

            _profileButton = RoundedButtonManager.ButtonInitiaziler("PROFILE", UIImage.FromFile(@"Images/ic_home_profile.png"));
            _payButton = RoundedButtonManager.ButtonInitiaziler("PAY", UIImage.FromFile(@"Images/ic_home_pay.png"));
            _payHistoryButton = RoundedButtonManager.ButtonInitiaziler("PAY HISTORY", UIImage.FromFile(@"Images/ic_home_pay_history.png"));
            _trackingButton = RoundedButtonManager.ButtonInitiaziler(EnvironmentInfo.GetTrackingButtonDistanceBetweenTextAndImage);

            this.AddLinqBinding(ViewModel, vm => vm.TrackingCommand, (value) =>
            {
                _trackingButton.BackgroundColor = UIColor.White;
                _trackingButton.Alpha = 0.7f;
                _trackingButton.ButtonTextColor = UIColor.FromRGB(3, 117, 27);
                //var blur = new UIVisualEffectView(UIBlurEffect.FromStyle(UIBlurEffectStyle.Light));
                //blur.Frame = _trackingButton.Bounds;
                //blur.UserInteractionEnabled = true;
                //_trackingButton.InsertSubview(blur, 0);
            });
            //_callCentergButton = ButtonInitiaziler(null, UIImage.FromFile(@"Images/ic_home_support.png"));
            //_callCentergButton.ButtonText.TextColor = UIColor.LightGray;
            //_callCentergButton.ButtonBackgroundColor = null;
            //callCenterLabel.Text = "+(1)305 335 85 08";
            //callCenterLabel.TextColor = UIColor.LightGray;

            applicationBoard.Frame = new CoreGraphics.CGRect(10, 10, applicationBoard.Image.CGImage.Width, applicationBoard.Image.CGImage.Height);
            //centerView.BackgroundColor = UIColor.Magenta;//UIColor.FromPatternImage(UIImage.FromFile(@"Images/home_board.png"));
            centerView.AddIfNotNull(applicationBoard, _profileButton, _payButton, _payHistoryButton);
            centerView.AddConstraints(
                applicationBoard.WithSameHeight(centerView),
                applicationBoard.WithSameWidth(centerView),
                applicationBoard.WithSameCenterX(centerView),
                applicationBoard.WithSameCenterY(centerView),

                _profileButton.AtTopOf(centerView, 10),
                _profileButton.AtLeftOf(centerView, 8),
                _profileButton.WithRelativeWidth(centerView, 0.4f),
                _profileButton.WithRelativeHeight(centerView, 0.47f),

                _payHistoryButton.AtTopOf(centerView, 10),
                _payHistoryButton.WithSameCenterX(centerView),
                _payHistoryButton.WithRelativeWidth(centerView, 0.4f),
                _payHistoryButton.WithRelativeHeight(centerView, 0.47f),

                _payButton.AtTopOf(centerView, 10),
                _payButton.AtRightOf(centerView, 8),
                _payButton.WithRelativeWidth(centerView, 0.4f),
                _payButton.WithRelativeHeight(centerView, 0.47f)
            );

            bottomView.AddIfNotNull(_trackingButton);
            bottomView.AddConstraints(
                _trackingButton.AtTopOf(bottomView),
                _trackingButton.AtLeftOf(bottomView, 20),
                _trackingButton.AtRightOf(bottomView, 20),
                _trackingButton.WithRelativeHeight(bottomView, EnvironmentInfo.GetTrackingButtonHeight),
                _trackingButton.WithRelativeWidth(bottomView, EnvironmentInfo.GetTrackingButtonWidth)
            );

            View.AddIfNotNull(topView, centerView, bottomView);
            View.AddConstraints(
                topView.AtTopOf(View),
                topView.AtLeftOf(View),
                topView.AtRightOf(View),
                topView.WithRelativeHeight(View, 0.2f),

                centerView.Below(topView),
                centerView.AtLeftOf(View, 15),
                centerView.AtRightOf(View, 15),
                centerView.WithRelativeHeight(View, 0.43f),

                bottomView.Below(centerView),
                bottomView.WithSameCenterX(topView),
                bottomView.WithRelativeHeight(View, 0.27f),
                bottomView.AtBottomOf(View, 30)
            );

            SignIn.SharedInstance.UIDelegate = this;
        }

        protected override void InitializeBindings()
        {
             base.InitializeBindings();

            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(_trackingButton).To(vm => vm.TrackingCommand);
            set.Bind(_trackingButton.ButtonText).To(vm => vm.TrackingText);
            set.Bind(_trackingButton).For(x => x.ButtonImage).To(vm => vm.IsBound).
               WithConversion("GetPathToImage");
            set.Bind(_profileButton).To(vm => vm.ProfileCommand);
            //set.Bind(_callCentergButton.ButtonText).To(vm => vm.SupportText);

            //this.AddLinqBinding(ViewModel, v=>v.IsBound, (isBound)=>{
            //    string imagePath = string.Format(@"Images/ic_home_tracking{0}", isBound ? "_active.png" : "_default.png");
            //    Debug.WriteLine(imagePath);
            //    _trackingButton.ButtonTextColor = isBound ? UIColor.FromRGB(3, 117, 27) : UIColor.Red;
            //    Debug.WriteLine(_trackingButton.ButtonTextColor);
            //    _trackingButton.ButtonText.Text = ViewModel.TrackingText;
            //    Debug.WriteLine(_trackingButton.ButtonText.Text);
            //        _trackingButton.ButtonImage = UIImage.FromFile(imagePath);
            //});
         set.Apply();
        }
    }
}
