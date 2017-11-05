using System;


namespace BehaviorTreeCS {
    public class Succeeder : Decorator {

        /// <summary>
        /// Returns Suceess no matter what
        /// </summary>
        /// <param name="behavior"></param>
        public Succeeder(BehaviorComponent behavior):base(behavior) { }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) { 
            _Behavior.Behave(agent, blackboard); 
            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }

    }
}
