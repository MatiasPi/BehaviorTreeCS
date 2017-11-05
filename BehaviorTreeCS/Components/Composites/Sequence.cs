using System;

using System.Linq;

namespace BehaviorTreeCS { 
    public class Sequence : Composite {

        /// <summary>
        /// attempts to run the behaviors all in one cycle
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Returns Running if any are running
        /// </summary>
        /// <param name="behaviors"></param>
        public Sequence(params BehaviorComponent[] behaviors) : base(behaviors) { }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {

            for(int i = 0; i < _Behaviors.Length;i++) {
                switch (_Behaviors[i].Behave(agent, blackboard))
                {
                    case BehaviorReturnCode.Failure:
                        StopChildren(agent,blackboard);
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        continue;
                    case BehaviorReturnCode.Running:
                        //This stops all running behaviors that are lower in priority than the behavior in question
                        //that are running because of a previous Behave() call.
                        //This is done for the purpose of not letting any behavior run loose
                        var runningNodes = _Behaviors.Skip(i+1).Where(behavior => behavior.ReturnCode == BehaviorReturnCode.Running).ToArray();
                        foreach (BehaviorComponent beh in runningNodes) {
                            beh.Stop(agent, blackboard);
                        }
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        StopChildren(agent, blackboard);
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                }
            }

            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;
        }

    }
}
