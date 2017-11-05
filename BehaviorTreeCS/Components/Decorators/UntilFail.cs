using System;


namespace BehaviorTreeCS {

    public class UntilFail:Decorator {

        public UntilFail(BehaviorComponent behavior):base(behavior) {  }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            while (_Behavior.Behave(agent, blackboard) != BehaviorReturnCode.Failure) ;
            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }
    }
}
