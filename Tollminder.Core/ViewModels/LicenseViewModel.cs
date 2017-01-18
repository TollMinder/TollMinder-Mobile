﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Tollminder.Core.Models;
using Tollminder.Core.Services;
using Xamarin.Forms;

namespace Tollminder.Core.ViewModels
{
    public class LicenseViewModel : BaseViewModel
    {
        readonly ILoadResourceData<StatesData> loadStatesData;
        readonly ILoadResourceData<string> loadVehicleData;
        readonly IProfileSettingService profileSettingService;
        readonly int firstElement = 0;
        
        public LicenseViewModel()
        {
            loadStatesData = Mvx.Resolve<ILoadResourceData<StatesData>>();
            profileSettingService = Mvx.Resolve<IProfileSettingService>();
            profile = new Profile();
            driverLicense = new DriverLicense();

            States = loadStatesData.GetData("Tollminder.Core.states.json");
            SelectedState = States[firstElement];
            
            loadVehicleData = Mvx.Resolve<ILoadResourceData<string>>();
            VehicleClasses = loadVehicleData.GetData();
            SelectedVehicleClass = VehicleClasses[firstElement];

            backToProfileCommand = new MvxCommand(() => {
                SaveUserBeforeYouLive();
                Close(this);
                //ShowViewModel<ProfileViewModel>(); 
            });
            statesWheelCommand = new MvxCommand(() => {
                if (IsVehicleClassWheelHidden)
                    IsVehicleClassWheelHidden = false;
                IsStateWheelHidden = IsStateWheelHidden ? false : true;
            });
            vehicleClassesWheelCommand = new MvxCommand(() => { 
                if (IsStateWheelHidden)
                    IsStateWheelHidden = false;
                IsVehicleClassWheelHidden = IsVehicleClassWheelHidden ? false : true; 
            });
        }

        public override void Start()
        {
            base.Start();
            Profile = profileSettingService.GetProfile();
            if(Profile.DriverLicense != null)
                DriverLicense = Profile.DriverLicense;
        }

        public override void OnPause()
        {
            SaveUserBeforeYouLive();
            base.OnPause();
        }

        public override void OnDestroy()
        {
            SaveUserBeforeYouLive();
            base.OnDestroy();
        }

        private void SaveUserBeforeYouLive()
        {
            Profile.DriverLicense = DriverLicense;
            profileSettingService.SaveProfile(Profile);
        }

        private MvxCommand backToProfileCommand;
        public ICommand BackToProfileCommand { get { return backToProfileCommand; } }
        private MvxCommand statesWheelCommand;
        public ICommand StatesWheelCommand { get { return statesWheelCommand; } }
        private MvxCommand vehicleClassesWheelCommand;
        public ICommand VehicleClassesWheelCommand { get { return vehicleClassesWheelCommand; } }

        private Profile profile;
        public Profile Profile
        {
            get { return profile; }
            set
            {
                SetProperty(ref profile, value);
            }
        }

        private DriverLicense driverLicense;
        public DriverLicense DriverLicense
        {
            get { return driverLicense; }
            set{
                SetProperty(ref driverLicense, value);
            }
        }

        // States
        private List<StatesData> states;
        public List<StatesData> States
        {
            get { return states; }
            set
            {
                SetProperty(ref states, value);
                RaisePropertyChanged(() => States);
            }
        }

        private StatesData selectedState;
        public StatesData SelectedState
        {
            get { return new StatesData() { Name = DriverLicense.State }; }
            set
            {
                DriverLicense.State = value.ToString();
                SetProperty(ref selectedState, value);
                RaisePropertyChanged(() => StateAbbreviation);
            }
        }

        public string StateAbbreviation
        {
            get { return SelectedState.Abbreviation; }
        }

        bool isStateWheelHidden;
        public bool IsStateWheelHidden
        {
            get { return isStateWheelHidden; }
            set
            {
                SetProperty(ref isStateWheelHidden, value);
                RaisePropertyChanged(() => IsStateWheelHidden);
            }
        }

        // Vehicle classes
        private List<string> vehicleClasses;
        public List<string> VehicleClasses
        {
            get { return vehicleClasses; }
            set
            {
                SetProperty(ref vehicleClasses, value);
                RaisePropertyChanged(() => VehicleClasses);
            }
        }

        private string selectedVehicleClass;
        public string SelectedVehicleClass
        {
            get { return DriverLicense.VehicleClass; }
            set
            {
                SetProperty(ref selectedVehicleClass, value);
                DriverLicense.VehicleClass = value;
                RaisePropertyChanged(() => SelectedVehicleClass);
            }
        }

        bool isVehicleClassWheelHidden;
        public bool IsVehicleClassWheelHidden
        {
            get { return isVehicleClassWheelHidden; }
            set
            {
                SetProperty(ref isVehicleClassWheelHidden, value);
                RaisePropertyChanged(() => IsVehicleClassWheelHidden);
            }
        }
    }
}
