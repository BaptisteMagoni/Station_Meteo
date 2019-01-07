using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using De.TorstenMandelkow.MetroChart;

namespace Station_Meteo
{
    class ProgressBarCircle
    {

        private InstanceProgress ipro;
        private RadialGaugeChart obj;

        public ProgressBarCircle(RadialGaugeChart progressbar)
        {
            obj = progressbar;
            ipro = new InstanceProgress(0);
            obj.DataContext = new InstanceProgressViewModel(ipro);
        }

        public void MAJ_Progress(int value)
        {
            ipro = new InstanceProgress(value);
            obj.DataContext = new InstanceProgressViewModel(ipro);
        }
    }

    internal class InstanceProgressViewModel
    {
        public List<InstanceProgress> Progress { get; set; }

        public InstanceProgressViewModel(InstanceProgress ipro)
        {
            Progress = new List<InstanceProgress>();
            Progress.Add(ipro);
        }
    }

    internal class InstanceProgress
    {
        public string Title { get; set; }

        public int Percentage { get; set; }

        public InstanceProgress(int percentage)
        {
            Title = "";
            Percentage = percentage;
        }
    }
}
