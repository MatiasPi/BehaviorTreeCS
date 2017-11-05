using System;

namespace BehaviorTreeCS {
    public class SetVariable : Action {
        string variableKey;
        object varValue;

        public SetVariable(string variableKey, object varValue) {
            this.variableKey = variableKey;
            this.varValue = varValue;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            blackboard.SetVariable(variableKey, varValue);
            ReturnCode = BehaviorReturnCode.Success;
            return BehaviorReturnCode.Success;
        }

    }
}


