using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace FunFlacts.Converters
{
	/*
       The IValueConverter interface defines a contract a single binding can use to coerce or convert a property value from a model, 
       into something usable by the UI property it is data bound to. The same interface is used in the WPF/UWP world for the same purpose.

       For simple, textual conversions, you can use the Binding.StringFormat property - this performs a String.Format on the source value 
       prior to applying it to the target. It does not perform any conversion going the other direction.
     */

	public class EmbeddedImageConverter : IValueConverter
    {
		/*
           Since the image is in a different assembly, we need to tell the ImageSource.FromResource method where to find it. 
           You can either hard-code this (like it is in the GetImageSource implementation), or create a public property on 
           the value converter and pass a Type in to load the image data. We'll use this latter approach since it's more flexible - 
           you can load images from any assembly.
         */

		// Optional type located in the assembly we want to get the resource from - 
	    // if not supplied, the API assumes the resource is located in this assembly.
		 
		public Type ResolvingAssemblyType { get; set; }

		// The Convert method should should turn the inbound value into a string and assume it's the embedded resource ID - 
		// use the code located in the FlagExtensions.GetImageSource method to load the resource.

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var imageUrl = (value ?? "").ToString();

			if (string.IsNullOrEmpty(imageUrl))
				return null;

			return ImageSource.FromResource(imageUrl, ResolvingAssemblyType?.GetTypeInfo().Assembly);
		}

		// The ConvertBack method is only used in two-way bindings; in this case our image URL will never be changed
	    // by the UI and so it can throw a NotSupportedException to indicate that it is not available.

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException(
			  $"{nameof(EmbeddedImageConverter)} cannot be used on two-way bindings.");
		}
    }
}
