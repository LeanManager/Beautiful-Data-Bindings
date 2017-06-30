using FlagData;
using Xamarin.Forms;
using FunFlacts.Extensions;
using System;
using System.Collections;

namespace FunFlacts
{
	/* Bindings can be created in code or markup (XAML) and require three pieces of information to function:

       1) A binding source, which is any .NET object you want to render.
       2) A property path, which identifies the specific public property of the .NET object to display in the target.
       3) A binding target, which is a BindableProperty of a BindableObject, this is typically some UI control such as a Entry.
    */
	public partial class MainPage : ContentPage
	{
		FlagRepository repository;
        int currentFlag;

		// --------------------------------------------------------------------------------------------------------------------------- //

		public MainPage()
		{
			InitializeComponent();

			// Load our data
			repository = new FlagRepository();

			// Setup the view
			InitializeData();
		}

        // --------------------------------------------------------------------------------------------------------------------------- //

        public Flag CurrentFlag
        {
            get 
            {
                return repository.Flags[currentFlag];
            }
        }

		// --------------------------------------------------------------------------------------------------------------------------- //

		private void InitializeData()
		{
			/* 
               Set the BindingContext which our XAML bindings will pull from. 
               We could set it on every single element here in code, but remember 
               from the lecture that BindingContext is inherited when it is set on a parent element. 
               So, assign the BindingContext on the page itself - make it the Flag object we want to 
               get all our data from. Do this at the end of the InitializeData method - in particular 
               make sure it's set after the Picker.ItemSource property is set. 
            */
			// this.BindingContext = CurrentFlag;

			// Set the binding context to an anonymous type containing both the countries
			// and the current flag. Note: this could also be a real type (like a ViewModel).
			this.BindingContext = new { Countries = repository.Countries, CurrentFlag };
		}

		// --------------------------------------------------------------------------------------------------------------------------- //

		private async void OnShow(object sender, EventArgs e)
		{
			// Must assign the property since DateTime is immutable.
			CurrentFlag.DateAdopted = CurrentFlag.DateAdopted.AddYears(1);

            await DisplayAlert(CurrentFlag.Country,
				$"{CurrentFlag.DateAdopted:D} - {CurrentFlag.IncludesShield}: {CurrentFlag.MoreInformationUrl}", 
				"OK");
		}

		// --------------------------------------------------------------------------------------------------------------------------- //

		private void OnMoreInformation(object sender, TappedEventArgs e)
        {
            Device.OpenUri(CurrentFlag.MoreInformationUrl);
        }

		// --------------------------------------------------------------------------------------------------------------------------- //

		private void OnPrevious(object sender, EventArgs e)
        {
            currentFlag--;
            if (currentFlag < 0)
                currentFlag = 0;
            InitializeData();
        }

		// --------------------------------------------------------------------------------------------------------------------------- //

		private void OnNext(object sender, EventArgs e)
        {
            currentFlag++;
            if (currentFlag >= repository.Flags.Count)
                currentFlag = repository.Flags.Count-1;
            
            InitializeData();
        }

		// --------------------------------------------------------------------------------------------------------------------------- //
	}
}
