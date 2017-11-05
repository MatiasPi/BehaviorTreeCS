using System;


namespace BehaviorTreeCS {

    public class Repeat : Decorator {

        uint times;
        uint counter = 0;

        public Repeat(uint times, BehaviorComponent behavior):base(behavior) {
            this.times = times;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {

            BehaviorReturnCode returnedCode = _Behavior.Behave(agent, blackboard);

            switch (returnedCode) {
                case BehaviorReturnCode.Success:
                    counter++;
                    if (counter == times)
                        ReturnCode = BehaviorReturnCode.Success;
                    else
                        ReturnCode = BehaviorReturnCode.Running;
                    break;
                case BehaviorReturnCode.Running:
                    ReturnCode = BehaviorReturnCode.Running;
                    break;
                case BehaviorReturnCode.Failure:
                    ReturnCode = BehaviorReturnCode.Failure;
                    break;
            }

            return ReturnCode;
        }

        protected override void Exit(object agent, Blackboard blackboard) {
            counter = 0;
        }

    }
}


