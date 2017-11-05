using System;

namespace BehaviorTreeCS {
    public class IsNull : Action{
        string key;

        public IsNull(string key) {
            this.key = key;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            if (blackboard.Exists(key))
                ReturnCode = BehaviorReturnCode.Failure;
            else
                ReturnCode =  BehaviorReturnCode.Success;
            return ReturnCode;
        }

    }
}


