using OAuthXFDemo.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace OAuthXFDemo.ViewModels
{
	public class SchedulerPageViewModel : BindableBase
	{
        List<Color> colorCollection;
        List<string> meetingDescriptions;

        public SchedulerPageViewModel()
        {
            Meetings = new ObservableCollection<Meeting>();
            AddAppointmentDetails();
            AddRandomMeetings();
        }

        public ObservableCollection<Meeting> Meetings { get; set; }

        private void AddAppointmentDetails()
        {
            meetingDescriptions = new List<string>();
            meetingDescriptions.Add("General Meeting");
            meetingDescriptions.Add("Plan Execution");
            meetingDescriptions.Add("Project Plan");
            meetingDescriptions.Add("Consulting");
            meetingDescriptions.Add("Support");
            meetingDescriptions.Add("Development Meeting");
            meetingDescriptions.Add("Scrum");
            meetingDescriptions.Add("Project Completion");
            meetingDescriptions.Add("Release updates");
            meetingDescriptions.Add("Performance Check");

            colorCollection = new List<Color>();
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FFF09609"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }

        private void AddRandomMeetings()
        {
            var today = DateTime.Now.Date;
            var random = new Random();
            for (int month = -1; month < 2; month++)
            {
                for (int day = -5; day < 5; day++)
                {
                    for (int hour = 9; hour < 18; hour += 5)
                    {
                        var meeting = new Meeting()
                        {
                            From = today.AddMonths(month).AddDays(day).AddHours(hour),
                            To = today.AddMonths(month).AddDays(day).AddHours(hour + 2),
                            EventName = meetingDescriptions[random.Next(7)],
                            color = colorCollection[random.Next(10)]
                        };
                        Meetings.Add(meeting);
                    }
                }
            }
        }

    }
}
