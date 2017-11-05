using System;


namespace BehaviorTreeCS {
    public class Counter : Decorator {
        private int _MaxCount;
        private int _Counter = 0;

        /// <summary>
        /// executes the behavior based on a counter
        /// -each time Counter is called the counter increments by 1
        /// -Counter executes the behavior when it reaches the supplied maxCount
        /// </summary>
        /// <param name="maxCount">max number to count to</param>
        /// <param name="behavior">behavior to run</param>
        public Counter(int maxCount, BehaviorComponent behavior):base(behavior) {
            _MaxCount = maxCount;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            if (_Counter < _MaxCount) {
                _Counter++;
                ReturnCode = BehaviorReturnCode.Running;
                return BehaviorReturnCode.Running;
            }
            else {
                _Counter = 0;
                ReturnCode = _Behavior.Behave(agent, blackboard);
                return ReturnCode;
            }
        }
    }
}
