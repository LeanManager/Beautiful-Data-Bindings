using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlagData
{
    /// <summary>
    /// This model object represents a single flag
    /// </summary>
    public class Flag : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _dateAdopted;

        /// <summary>
        /// Name of the country that this flag belongs to
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Image URL for the flag (from resources)
        /// </summary>
        public string ImageUrl { get; set; }
		/// <summary>
		/// The date this flag was adopted
		/// </summary>
		public DateTime DateAdopted
		{
			get { return _dateAdopted; }
			set
			{
				if (_dateAdopted != value)
				{
					_dateAdopted = value;
					// Can pass the property name as a string,
					// -or- let the compiler do it because of the
					// CallerMemberNameAttribute on the RaisePropertyChanged method.
					RaisePropertyChanged();
				}
			}
		}
        /// <summary>
        /// Whether the flag includes an image/shield as part of the design
        /// </summary>
        public bool IncludesShield { get; set; }
        /// <summary>
        /// Some trivia or commentary about the flag
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A URL for more information
        /// </summary>
        public Uri MoreInformationUrl { get; set; }

		/*
           We must create a helper-method to raise the PropertyChanged event named RaisePropertyChanged. 
           The method will accept a string parameter which is the text name of the property that has changed.
           
           * .NET 4.5 includes an attribute CallerMemberNameAttribute in the System.Runtime.CompilerServices 
             namespace which you can use to get the compiler to identify the property that has been changed. 
             Alternatively, you can have the caller pass the name using the C# nameof() compiler feature.
             
           * Make sure to test the event for null before raising it. You can either use the built-in C# null check support, 
             or for earlier versions of C#, test the event for null, or assign the event to an empty delegate 
             (this is called the null pattern).
         */

		private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}
