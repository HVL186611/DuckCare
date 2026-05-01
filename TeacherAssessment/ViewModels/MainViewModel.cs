using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TeacherAssessment.ViewModels
{


    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<string> Log { get; set; } = new();



        public void AddEvent(string message, ObservableCollection<string>? log)
        {
            log.Add(message);
        }


        //AddEvent("E");

        static void Main(String[] args)
        {
            //AddEvent("e", Log);
        }

    };


}
