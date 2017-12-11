using CodingDojo2.ViewModel;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simulation
{
    public class Simulator
    {
        private static Random rand = new Random(5);
        public List<ItemVm> Items { get; set; }


        /// <summary>
        /// Generates Demo Data (Sensors and Actuators and Starts manipulating the Values every 3 Secs.
        /// Light is only changed if Mode=auto!
        /// </summary>
        public Simulator(List<ItemVm> items)
        {
            this.Items = items;
            GenerateDemoData();
            ThreadPool.QueueUserWorkItem(StartGeneratingDemoData);
        }



        private void GenerateDemoData()
        {
            //Items = new List<ItemBase>();
            //Sensors
            //Items.Add(new ItemVm((new DoorContact("0.04", "Haustüre", "Garderobe", 4) as ISensor)));
            Items.Add(new ItemVm(new Switch("0.01", "TA Wohnzimmer", "WZ", 1)));
            Items.Add(new ItemVm(new Switch("0.02", "TA Wohnzimmer", "WZ", 2)));
            Items.Add(new ItemVm(new Switch("0.03", "TA Badezimmer", "Bad", 3)));
            Items.Add(new ItemVm(new MotionDedector("1.100", "IR Haustüre", "Garderobe", 5)));
            Items.Add(new ItemVm(new MotionDedector("1.101", "IR Wohnzimmer", "WZ", 6)));
            Items.Add(new ItemVm(new Temperature("0.10", "Temp Badezimmer", "Bad", 7)));
            Items.Add(new ItemVm(new Temperature("0.11", "Temp Wohnimmer", "WZ", 8)));
            Items.Add(new ItemVm(new Temperature("0.12", "Temp Aussen Nord", "Garten", 9)));
            Items.Add(new ItemVm(new TwilightSwitch("0.20", "Dämmerungssensor Nord", "Garten", 10)));
            //Actors
            Items.Add(new ItemVm(new Light("2.00", "Licht Wohnzimmer", "WZ", 100)));
            Items.Add(new ItemVm(new Light("2.01", "Licht Aussen", "Garten", 101)));
            Items.Add(new ItemVm(new Light("2.02", "Licht Badezimmer", "Bad", 102)));
            Items.Add(new ItemVm(new PowerJack("2.03", "Dose Badezimmer", "Bad", 103)));
            Items.Add(new ItemVm(new PowerJack("2.04", "Dose Wohnzimmer", "WZ", 104)));
            Items.Add(new ItemVm(new PowerJack("2.05", "Dose Wohnzimmer", "WZ", 105)));
        }

        private void StartGeneratingDemoData(object o)
        {
            while (true)
            {
                try
                {
                    foreach (var item in Items)
                    {
                        if (item.ValueType != null && (item.Mode.Equals("Auto")|| item.Mode.Equals("Enabled")))
                        {
                            if (item.ValueType.Equals("Boolean"))
                            {
                                item.Value = RandNo();
                            }
                            else if (item.ValueType.Equals("Byte"))
                            {
                                item.Value = rand.Next(0, 3);
                            }
                            else if (item.ValueType.Equals("Single"))
                            {
                                item.Value = Math.Round((rand.Next(0, 110) - 50) / 1.2, 1);
                            }
                            else if (item.ValueType.Contains("Int"))
                            {
                                item.Value = (rand.Next(0, 1000));
                            }
                        }
                     
                    }


                }
                catch (Exception e)
                {
                    var m = e.Message;
                }

                Thread.Sleep(3000);
            }
        }
        private int RandNo()
        {
            if (rand.Next(1, 100) > 50) return 1;
            else return 0;

        }
    }
}
