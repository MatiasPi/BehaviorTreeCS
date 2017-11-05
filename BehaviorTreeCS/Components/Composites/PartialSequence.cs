using System;
using System.Linq;

namespace BehaviorTreeCS {
    public class PartialSequence : Composite {

        private short _sequence = 0;

        private short _seqLength = 0;
        
        /// <summary>
        /// Performs the given behavior components sequentially (one evaluation per Behave call)
        /// Performs an AND-Like behavior and will perform each successive component
        /// -Returns Success if all behavior components return Success
        /// -Returns Running if an individual behavior component returns Success or Running
        /// -Returns Failure if a behavior components returns Failure or an error is encountered
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public PartialSequence(params BehaviorComponent[] behaviors):base(behaviors) {
            _seqLength = (short) _Behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard)
        {
            //while you can go through them, do so
            while (_sequence < _seqLength) {
                switch (_Behaviors[_sequence].Behave(agent, blackboard)) {
                    case BehaviorReturnCode.Failure:
                        StopChildren(agent, blackboard);
                        _sequence = 0;
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        _sequence++;
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        //This stops all running behaviors that are lower in priority than the behavior in question
                        //that are running because of a previous Behave() call.
                        //This is done for the purpose of not letting any behavior run loose
                        var runningNodes = _Behaviors.Skip(_sequence + 1).Where(behavior => behavior.ReturnCode == BehaviorReturnCode.Running).ToArray();
                        foreach (BehaviorComponent beh in runningNodes) {
                            beh.Stop(agent, blackboard);
                        }
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                }
            }

            _sequence = 0;
            ReturnCode = BehaviorReturnCode.Success;
            return ReturnCode;

        }

        protected override void HandleException(Exception e) {
            _sequence = 0;
        }

    }
}
