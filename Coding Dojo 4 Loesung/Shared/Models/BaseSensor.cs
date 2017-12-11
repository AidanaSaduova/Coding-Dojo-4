using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public abstract class BaseSensor : ItemBase, ISensor
    {
        private Type sensorValueType;
        private SensorModeType sensorMode;
        private object sensorValue;

        public Type SensorValueType { get => sensorValueType; }
        public SensorModeType SensorMode { get => sensorMode; set => sensorMode = value; }
        public object SensorValue { get => sensorValue; set => sensorValue = value; }

        public BaseSensor(string name, string description, int id, string room, Type valueType) : base(name, description, room, id)
        {
            sensorValueType = valueType;
            SensorMode = SensorModeType.Enabled;

        }
    }
}
