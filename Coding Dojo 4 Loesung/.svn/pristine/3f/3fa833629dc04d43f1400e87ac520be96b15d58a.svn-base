using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class DoorContact : ItemBase, ISensor, IActuator
    {
        private Type sensorValueType, actuatorValueType;
        private ModeType actuatorMode;
        private SensorModeType sensorMode;
        private object sensorValue, actorValue;

        public Type SensorValueType { get => sensorValueType; }
        public SensorModeType SensorMode { get => sensorMode; set => sensorMode = value; }
        public object SensorValue { get => sensorValue; set => sensorValue = value; }

        public Type ActuatorValueType => actuatorValueType;
        public ModeType ActuatorMode { get => actuatorMode; set => actuatorMode = value; }
        public object ActuatorValue { get => actorValue; set => actorValue = value; }


        public DoorContact(string name, string description, string room, int id) : base(name, description, room, id)
        {
            actuatorValueType = typeof(byte);
            sensorValueType = typeof(byte); //locked, unlocked, open,closed
        }


    }
}
