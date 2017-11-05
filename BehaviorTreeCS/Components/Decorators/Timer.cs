using System;

using System.Diagnostics;

namespace BehaviorTreeCS {
    public class Timer : Decorator {

		private TimerDelegate _ElapsedTimeFunction;

        private float _TimeElapsed = 0;

        private float _WaitTime;

        Stopwatch stopwatch;

        /// <summary>
        /// executes the behavior after a given amount of time in miliseconds has passed
        /// </summary>
        /// <param name="elapsedTimeFunction">function that returns elapsed time</param>
        /// <param name="timeToWait">maximum time to wait before executing behavior</param>
        /// <param name="behavior">behavior to run</param>
		/*public Timer(TimerDelegate elapsedTimeFunction, long timeToWait, BehaviorComponent behavior)
        {
            _ElapsedTimeFunction = elapsedTimeFunction;
            _Behavior = behavior;
            _WaitTime = timeToWait;
        }*/

        public Timer(float timeToWait, BehaviorComponent behavior):base(behavior) {
            _WaitTime = timeToWait;
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            _TimeElapsed += GetElapsedTime();

            if (_TimeElapsed >= _WaitTime)
            {
                Exit(agent, blackboard);
                ReturnCode = _Behavior.Behave(agent, blackboard);
                return ReturnCode;
            }
            else
            {
                ReturnCode = BehaviorReturnCode.Running;
                return BehaviorReturnCode.Running;
            }
        }

        long GetElapsedTime() {
            if (!stopwatch.IsRunning) {
                stopwatch.Start();
                return 0;
            }

            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            return elapsed;
        }

        protected override void Exit(object agent, Blackboard blackboard) {
            _TimeElapsed = 0;
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }

    /// <summary>
    /// Should return an int which represents num milliseconds 
    /// </summary>
    public delegate int TimerDelegate();
}
