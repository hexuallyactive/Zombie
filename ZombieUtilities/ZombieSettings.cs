﻿using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZombieUtilities;

namespace Zombie.Utilities
{
    public class ZombieSettings : INotifyPropertyChanged
    {
        /// <summary>
        /// Since we want to serialize certain properties only when user requests so (for security reasons)
        /// this propert can be set to True right before Serialize call to include all hidden properties.
        /// </summary>
        public bool ShouldSerialize { get; set; }

        /// <summary>
        /// If user fires up an app with no Arg1 set we create a new Setting and will store it locally.
        /// This is an internal flag should not be serialized.
        /// </summary>
        [JsonIgnore]
        public bool StoreSettings { get; set; }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value.TrimLastCharacter("/"); RaisePropertyChanged("Address"); }
        }

        private string _accessToken;
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; RaisePropertyChanged("AccessToken"); }
        }

        /// <summary>
        /// AccessToken will be skipped when this returns False.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeAccessToken()
        {
            return ShouldSerialize;
        }

        private string _settingsLocation;
        public string SettingsLocation
        {
            get { return _settingsLocation; }
            set { _settingsLocation = value; RaisePropertyChanged("SettingsLocation"); }
        }

        /// <summary>
        /// SettingsLocation will be skipped when this returns False.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeSettingsLocation()
        {
            return ShouldSerialize;
        }

        private Frequency _frequency = Frequency.h2;
        [JsonConverter(typeof(StringEnumConverter))]
        public Frequency Frequency
        {
            get { return _frequency; }
            set { _frequency = value; RaisePropertyChanged("Frequency"); }
        }

        private List<AssetObject> _sourceAssets = new List<AssetObject>();
        public List<AssetObject> SourceAssets
        {
            get { return _sourceAssets; }
            set { _sourceAssets = value; RaisePropertyChanged("SourceAssets"); }
        }

        private List<Location> _destinationAssets = new List<Location>();
        public List<Location> DestinationAssets
        {
            get { return _destinationAssets; }
            set { _destinationAssets = value; RaisePropertyChanged("DestinationAssets"); }
        }

        private ReleaseObject _latestRelease;
        public ReleaseObject LatestRelease
        {
            get { return _latestRelease; }
            set { _latestRelease = value; RaisePropertyChanged("LatestRelease"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }

    public class Location : INotifyPropertyChanged
    {
        private LocationType _locationType = LocationType.Folder;
        [JsonConverter(typeof(StringEnumConverter))]
        public LocationType LocationType
        {
            get { return _locationType; }
            set { _locationType = value; RaisePropertyChanged("LocationType"); }
        }

        private string _directoryPath = string.Empty;
        public string DirectoryPath
        {
            get { return _directoryPath; }
            set { _directoryPath = value; RaisePropertyChanged("DirectoryPath"); }
        }

        private List<AssetObject> _assets = new List<AssetObject>();
        public List<AssetObject> Assets
        {
            get { return _assets; }
            set { _assets = value; RaisePropertyChanged("Assets"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LocationType
    {
        Folder,
        Source,
        Trash
    }

    /// <summary>
    /// 
    /// </summary>
    public enum Frequency
    {
        [Description("1min")]
        min1,
        [Description("15min")]
        min15,
        [Description("30min")]
        min30,
        [Description("1h")]
        h1,
        [Description("2h")]
        h2,
        [Description("3h")]
        h3,
        [Description("6h")]
        h6,
        [Description("12h")]
        h12,
        [Description("24h")]
        h24
    }
}
