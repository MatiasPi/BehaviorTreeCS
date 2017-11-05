using System;

namespace BehaviorTreeCS {
    public class RandomSelector : Composite {
        //use current milliseconds to set random seed
		private System.Random _Random = new System.Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Randomly selects and performs one of the passed behaviors
        /// -Returns Success if selected behavior returns Success
        /// -Returns Failure if selected behavior returns Failure
        /// -Returns Running if selected behavior returns Running
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public RandomSelector(params BehaviorComponent[] behaviors) :base(behaviors) {}

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) { 
            _Random = new System.Random(DateTime.Now.Millisecond);

            switch (_Behaviors[_Random.Next(0, _Behaviors.Length - 1)].Behave(agent, blackboard)) {
                case BehaviorReturnCode.Failure:
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                case BehaviorReturnCode.Success:
                    ReturnCode = BehaviorReturnCode.Success;
                    return ReturnCode;
                case BehaviorReturnCode.Running:
                    ReturnCode = BehaviorReturnCode.Running;
                    return ReturnCode;
                default:
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
            }
        }
    }
}
