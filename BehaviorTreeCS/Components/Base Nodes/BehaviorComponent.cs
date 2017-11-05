using System;

namespace BehaviorTreeCS {

    public enum BehaviorReturnCode {
        NotStarted,
        Failure,
        Success,
        Running
    }

    public abstract class  BehaviorComponent {

        protected bool started = false;

        public BehaviorReturnCode ReturnCode;

        public BehaviorComponent() { }

        public BehaviorReturnCode Behave(object agent, Blackboard blackboard) {
            try {
                if (!started) {
                    Enter(agent, blackboard);
                    started = true;
                }

                BehaviorReturnCode returnCode = Update(agent, blackboard);

                if (returnCode != BehaviorReturnCode.Running) {
                    started = false;
                    Exit(agent, blackboard);
                }

                return returnCode;
            }
            catch (Exception e) {

                //Output error

                HandleException(e);

                //This means that all nodes will return FAILURE if they throw an unhandled exception
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }

        protected virtual void Enter(object agent, Blackboard blackboard) {  }

        protected virtual BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            return BehaviorReturnCode.Success;
        }

        protected virtual void Exit(object agent, Blackboard blackboard) { }

        public virtual void Stop(object agent, Blackboard blackboard) {
            ReturnCode = BehaviorReturnCode.NotStarted;
            started = false;
            Exit(agent, blackboard);
        }

        protected virtual void HandleException(Exception e) { }
    }
}
