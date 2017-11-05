using System;
using System.Linq;

namespace BehaviorTreeCS {
    public class PartialSelector : Composite {

        private short _selections = 0;

        private short _selLength = 0;

        /// <summary>
		/// Selects among the given behavior components (one evaluation per Behave call)
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Failure or Running
        /// -Returns Failure if all behavior components returned Failure or an error has occured
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public PartialSelector(params BehaviorComponent[] behaviors):base(behaviors) {
            _selLength = (short)_Behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            while (_selections < _selLength) {
                switch (_Behaviors[_selections].Behave(agent, blackboard)) {
                    case BehaviorReturnCode.Failure:
                        _selections++;
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        _selections = 0;
                        StopChildren(agent, blackboard);
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        //This stops all running behaviors that are lower in priority than the behavior in question
                        //that are running because of a previous Behave() call.
                        //This is done for the purpose of not letting any behavior run loose
                        var runningNodes = _Behaviors.Skip(_selections + 1).Where(behavior => behavior.ReturnCode == BehaviorReturnCode.Running).ToArray();
                        foreach (BehaviorComponent beh in runningNodes) {
                            beh.Stop(agent, blackboard);
                        }
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        _selections++;
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                }
            }

            _selections = 0;
            ReturnCode = BehaviorReturnCode.Failure;
            return ReturnCode;
        }

        protected override void HandleException(Exception e) {
            _selections++;
        }


    }
}
