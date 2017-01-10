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
    public class ProfileView : BaseViewController<ProfileViewModel>, ISignInUIDelegate, ICleanBackStack
    {
        UIButton backHomeView;

        TextFieldValidationWithImage firstNameTextField;
        TextFieldValidationWithImage lastNameTextField;
        TextFieldValidationWithImage emailTextField;
        TextFieldValidationWithImage addressTextField;
        TextFieldValidationWithImage cityTextField;
        TextFieldValidationWithImage stateTextField;
        TextFieldValidationWithImage zipCodeTextField;

        ProfileButton addLicenseButton;
        ProfileButton addCreditCardButton;
        
        public ProfileView()
        {
        }

        public ProfileView(IntPtr handle) : base(handle)
        {
        }

        public ProfileView(string nibName, Foundation.NSBundle bundle) : base(nibName, bundle)
        {
        }

        protected override void InitializeObjects()
        {
            base.InitializeObjects();

            var topView = new UIView();
            var scrollView = new UIScrollView();
            var topTextRowView = new UIView();
            var centerTextRowView = new UIView();
            var bottomTextRowView = new UIView();
            var bottomView = new UIView();
            backHomeView = UIButton.FromType(UIButtonType.Custom);
            backHomeView.SetImage(UIImage.FromFile(@"Images/ic_back.png"), UIControlState.Normal);
            var profileNavigationBarBackground = new UIImageView(UIImage.FromBundle(@"Images/navigation_bar_background.png"));

            // Hide navigation bar
            NavigationController.SetNavigationBarHidden(true, false);
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile(@"Images/tab_background.png").Scale(View.Frame.Size));//EnvironmentInfo.CheckDevice().Scale(View.Frame.Size));
            profileNavigationBarBackground.Frame = new CoreGraphics.CGRect(10, 10, profileNavigationBarBackground.Image.CGImage.Width, profileNavigationBarBackground.Image.CGImage.Height);

            topView.AddIfNotNull(profileNavigationBarBackground, backHomeView);
            topView.AddConstraints(
                profileNavigationBarBackground.WithSameWidth(topView),
                profileNavigationBarBackground.WithSameHeight(topView),
                profileNavigationBarBackground.AtTopOf(topView),

                backHomeView.WithSameCenterY(topView),
                backHomeView.AtLeftOf(topView, 20),
                backHomeView.WithRelativeWidth(topView,0.1f),
                backHomeView.WithRelativeHeight(topView, 0.2f)
            );

            firstNameTextField = TextFieldInitializer("First Name");
            lastNameTextField = TextFieldInitializer("Last Name");
            emailTextField = TextFieldInitializer("Email");
            addressTextField = TextFieldInitializer("Address");
            cityTextField = TextFieldInitializer("City");
            stateTextField = TextFieldInitializer("State");
            zipCodeTextField = TextFieldInitializer("Zip Code");
            
            addLicenseButton = ProfileButtonManager.ButtonInitiaziler("Add License Plate", UIImage.FromFile(@"Images/profileView/ic_license.png"));
            addCreditCardButton = ProfileButtonManager.ButtonInitiaziler("Add Credit Card", UIImage.FromFile(@"Images/profileView/ic_card.png"));

            topTextRowView.AddIfNotNull(firstNameTextField, lastNameTextField);
            topTextRowView.AddConstraints(
                firstNameTextField.AtTopOf(topTextRowView),
                firstNameTextField.AtLeftOf(topTextRowView),
                firstNameTextField.WithRelativeWidth(topTextRowView, 0.475f),
                firstNameTextField.WithSameHeight(topTextRowView),

                lastNameTextField.AtTopOf(topTextRowView),
                lastNameTextField.AtRightOf(topTextRowView),
                lastNameTextField.WithRelativeWidth(topTextRowView, 0.475f),
                lastNameTextField.WithSameHeight(topTextRowView)
            );

            centerTextRowView.AddIfNotNull(emailTextField, addressTextField, cityTextField);
            centerTextRowView.AddConstraints(
                emailTextField.AtTopOf(centerTextRowView),
                emailTextField.WithSameCenterX(centerTextRowView),
                emailTextField.WithSameWidth(centerTextRowView),
                emailTextField.WithRelativeHeight(centerTextRowView, 0.3f),

                addressTextField.Below(emailTextField, 10),
                addressTextField.WithSameCenterX(centerTextRowView),
                addressTextField.WithSameWidth(centerTextRowView),
                addressTextField.WithRelativeHeight(centerTextRowView, 0.3f),

                cityTextField.Below(addressTextField, 10),
                cityTextField.WithSameCenterX(centerTextRowView),
                cityTextField.WithSameWidth(centerTextRowView),
                cityTextField.WithRelativeHeight(centerTextRowView, 0.3f)
            );

            bottomTextRowView.AddIfNotNull(stateTextField, zipCodeTextField);
            bottomTextRowView.AddConstraints(
                stateTextField.AtTopOf(bottomTextRowView),
                stateTextField.AtLeftOf(bottomTextRowView),
                stateTextField.WithRelativeWidth(bottomTextRowView, 0.475f),
                stateTextField.WithSameHeight(bottomTextRowView),

                zipCodeTextField.AtTopOf(bottomTextRowView),
                zipCodeTextField.AtRightOf(bottomTextRowView),
                zipCodeTextField.WithRelativeWidth(bottomTextRowView, 0.475f),
                zipCodeTextField.WithSameHeight(bottomTextRowView)
            );

            bottomView.AddIfNotNull(addLicenseButton, addCreditCardButton);
            bottomView.AddConstraints(
                addLicenseButton.AtTopOf(bottomView),
                addLicenseButton.WithSameCenterX(bottomView),
                addLicenseButton.WithSameWidth(bottomView),
                addLicenseButton.WithRelativeHeight(bottomView, 0.4f), 

                addCreditCardButton.Below(addLicenseButton, 10),
                addCreditCardButton.WithSameCenterX(bottomView),
                addCreditCardButton.WithSameWidth(bottomView),
                addCreditCardButton.WithRelativeHeight(bottomView, 0.4f)
            );

            scrollView.AddIfNotNull(topTextRowView, centerTextRowView, bottomTextRowView, bottomView);
            scrollView.AddConstraints(
                topTextRowView.AtTopOf(scrollView, 30),
                topTextRowView.WithSameWidth(scrollView),
                topTextRowView.WithSameCenterX(scrollView),
                topTextRowView.WithRelativeHeight(scrollView, 0.12f),

                centerTextRowView.Below(topTextRowView, 10),
                centerTextRowView.WithSameWidth(scrollView),
                centerTextRowView.AtLeftOf(scrollView),
                centerTextRowView.AtRightOf(scrollView),
                centerTextRowView.WithRelativeHeight(scrollView, 0.4f),

                bottomTextRowView.Below(centerTextRowView, 10),
                bottomTextRowView.WithSameWidth(scrollView),
                bottomTextRowView.WithSameCenterX(scrollView),
                bottomTextRowView.WithRelativeHeight(scrollView, 0.12f),

                bottomView.Below(bottomTextRowView, 10),
                bottomView.WithSameWidth(scrollView),
                bottomView.AtLeftOf(scrollView),
                bottomView.AtRightOf(scrollView),
                bottomView.AtBottomOf(scrollView, 100),
                bottomView.WithRelativeHeight(scrollView, 0.27f)
            );

            View.AddIfNotNull(topView, scrollView);
            View.AddConstraints(
                topView.AtTopOf(View),
                topView.WithSameWidth(View),
                topView.WithRelativeHeight(View, 0.2f),

                scrollView.Below(topView, 30),
                scrollView.AtLeftOf(View, 30),
                scrollView.AtRightOf(View, 30),
                scrollView.WithRelativeHeight(View, 0.8f)
            );

            SignIn.SharedInstance.UIDelegate = this;
            EnableNextKeyForTextFields(firstNameTextField.TextFieldWithValidator.TextField, lastNameTextField.TextFieldWithValidator.TextField, emailTextField.TextFieldWithValidator.TextField,
                                       addressTextField.TextFieldWithValidator.TextField, cityTextField.TextFieldWithValidator.TextField, stateTextField.TextFieldWithValidator.TextField,
                                       zipCodeTextField.TextFieldWithValidator.TextField);
        }

        private TextFieldValidationWithImage TextFieldInitializer(string placeholder)
        {
            TextFieldValidationWithImage textField = new TextFieldValidationWithImage();
            textField.TextFieldWithValidator.TextField.Placeholder = placeholder;
            textField.BackgroundColor = UIColor.White;
            textField.TextFieldWithValidator.TextField.TextColor = UIColor.LightGray;
            textField.TextFieldWithValidator.SeparatorView.BackgroundColor = UIColor.Clear;
            textField.Layer.CornerRadius = 10;
            textField.TextFieldWithValidator.TopLabelColor = UIColor.Black;
            textField.TextFieldWithValidator.TextField.KeyboardType = UIKeyboardType.Default;

            return textField;
        }

        protected override void InitializeBindings()
        {
             base.InitializeBindings();

            var set = this.CreateBindingSet<ProfileView, ProfileViewModel>();
            set.Bind(backHomeView).To(vm => vm.BackHomeCommand);
            set.Bind(addLicenseButton).To(vm => vm.AddLicenseCommand);
            //set.Bind(_trackingButton.ButtonText).To(vm => vm.TrackingText);
            //set.Bind(_trackingButton).For(x => x.ButtonImage).To(vm => vm.IsBound).
            //   WithConversion("GetPathToImage");
            //set.Bind(_profileButton).To(vm => vm.ProfileCommand);
            //set.Bind(_callCentergButton.ButtonText).To(vm => vm.SupportText);
            set.Apply();
        }
    }
}
