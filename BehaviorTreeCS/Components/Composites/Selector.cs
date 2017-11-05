using System;
using System.Linq;


namespace BehaviorTreeCS { 
    public class Selector : Composite {

        /// <summary>
        /// Selects among the given behavior components
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public Selector(params BehaviorComponent[] behaviors):base(behaviors) {  }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            
            for (int i = 0; i < _Behaviors.Length; i++) {
                switch (_Behaviors[i].Behave(agent, blackboard))
                {
                    case BehaviorReturnCode.Failure:
                        continue;
                    case BehaviorReturnCode.Success:
                        StopChildren(agent, blackboard);
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        //This stops all running behaviors that are lower in priority than the behavior in question
                        //that are running because of a previous Behave() call.
                        //This is done for the purpose of not letting any behavior run loose
                        var runningNodes = _Behaviors.Skip(i + 1).Where(behavior => behavior.ReturnCode == BehaviorReturnCode.Running).ToArray();
                        foreach (BehaviorComponent beh in runningNodes) {
                            beh.Stop(agent, blackboard);
                        }
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        continue;
                }
            }

            ReturnCode = BehaviorReturnCode.Failure;
            return ReturnCode;
        }
    }
}
