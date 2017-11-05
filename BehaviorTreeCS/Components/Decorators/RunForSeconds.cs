using System;

using System.Diagnostics;

namespace BehaviorTreeCS {
    /// <summary>
    /// It runs the child action for X seconds no matter what the result of the action is.
    /// It will always return RUNNING, until the timer runs out, which will be SUCCESS
    /// </summary>
    public class RunForSeconds : Decorator {

        private float _TimeElapsed = 0;

        private float _WaitTime;

        Stopwatch stopwatch;


        public RunForSeconds(float timeToRun, BehaviorComponent behavior):base(behavior) {
            _WaitTime = timeToRun;
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) { 
            _TimeElapsed += GetElapsedTime();

            if (_TimeElapsed > _WaitTime) {
                Exit(agent, blackboard);
                ReturnCode = BehaviorReturnCode.Success;
                return ReturnCode;
            }
            else {
                _Behavior.Behave(agent, blackboard);
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
}
