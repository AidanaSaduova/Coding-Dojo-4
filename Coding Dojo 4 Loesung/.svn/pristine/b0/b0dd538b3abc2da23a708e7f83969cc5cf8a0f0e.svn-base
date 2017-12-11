using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
   public abstract class BaseActuator : ItemBase, IActuator
    {
        private object actuatorValue;
        private ModeType actuatorMode;
        private Type actuatorValueType;

        public Type ActuatorValueType => actuatorValueType;
        public ModeType ActuatorMode { get => actuatorMode; set => actuatorMode = value; }
        public object ActuatorValue { get => actuatorValue; set => actuatorValue = value; }


        public BaseActuator(string name, string description, int id, string room, Type valueType) : base(name, description, room, id)
        {
            actuatorValueType = valueType;

        }
    }
}
