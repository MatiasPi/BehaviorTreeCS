using System;
using System.Collections.Generic;

namespace BehaviorTreeCS {
    public class PopFromStack : Action {
        string stackKey;
        string stackValKey;

        public PopFromStack(string stackKey, string stackValKey) {
            this.stackKey = stackKey;
            this.stackValKey = stackValKey;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            try {
                Stack<object> stack = blackboard.GetVariable(stackKey) as Stack<object>;
                blackboard.SetVariable(stackValKey, stack.Pop());
                ReturnCode = BehaviorReturnCode.Success;
                return BehaviorReturnCode.Success;
            }
            catch(InvalidOperationException e) {
                ReturnCode = BehaviorReturnCode.Failure;
                return BehaviorReturnCode.Failure;
            }
        }

    }
}


