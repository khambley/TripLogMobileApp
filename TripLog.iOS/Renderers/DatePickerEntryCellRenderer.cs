using System;
using Foundation;
using TripLog.Controls;
using TripLog.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

[assembly: ExportRenderer(typeof(DatePickerEntryCell), typeof(DatePickerEntryCellRenderer))]
namespace TripLog.iOS.Renderers
{
    public class DatePickerEntryCellRenderer : EntryCellRenderer
    {
        public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var datePickerCell = (DatePickerEntryCell)item;
            UITextField textField = null;

            if (cell != null)
            {
                textField = (UITextField)cell.ContentView.Subviews[0];
            }

            //Default datepicker display attributes
            var mode = UIDatePickerMode.Date;
            var displayFormat = "d";
            var date = NSDate.Now;
            var isLocalTime = false;

            //Update datepicker based on Cell's properties
            if (datePickerCell != null)
            {
                //Kind must be Universal or Local to case to NSDate
                if (datePickerCell.Date.Kind == DateTimeKind.Unspecified)
                {
                    var local = new DateTime(datePickerCell.Date.Ticks, DateTimeKind.Local);
                    date = (NSDate)local;
                }
                else
                {
                    date = (NSDate)datePickerCell.Date;
                }
                isLocalTime = datePickerCell.Date.Kind == DateTimeKind.Local || datePickerCell.Date.Kind == DateTimeKind.Unspecified;
            }

            //Create iOS Picker
            var datePicker = new UIDatePicker
            {
                Mode = mode,
                BackgroundColor = UIColor.White,
                Date = date,
                TimeZone = isLocalTime ? NSTimeZone.LocalTimeZone : new NSTimeZone("UTC")
            };

            // Create a toolbar with a done button that will
            // close the datepicker and set the selected value
            var done = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) =>
            {
                var pickedDate = (DateTime)datePicker.Date;

                if (isLocalTime)
                {
                    pickedDate = pickedDate.ToLocalTime();
                }
                //Update the value of the UITextfield within the Cell
                if (textField != null)
                {
                    textField.Text = pickedDate.ToString(displayFormat);
                    textField.ResignFirstResponder();
                }

                // Update the Date property on the Cell
                if (datePickerCell != null)
                {
                    datePickerCell.Date = pickedDate;
                    datePickerCell.SendCompleted();
                }
            });
            var toolbar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                Translucent = false
            };
            toolbar.SizeToFit();
            toolbar.SetItems(new[] { done }, true);
            // Set the input view, toolbar and initial value for the Cell's UITextField
            if (textField != null)
            {
                textField.InputView = datePicker;
                textField.InputAccessoryView = toolbar;
                if (datePickerCell != null)
                {
                    textField.Text = datePickerCell.Date.ToString(displayFormat);
                }
            }
            return cell;
        }
    }
}

