using System;


namespace BehaviorTreeCS {
    public class BehaviorAction : Action {
		protected BehaviourActionDelegate _Action;

        /// <summary>
        /// Generic Action. Use this when you don't need to use the Enter() and Exit() methods
        /// </summary>
        /// <param name="action">The Action it is going to invoke when Behave() is called</param>
		public BehaviorAction(BehaviourActionDelegate action) {
            _Action = action;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            return _Action(agent, blackboard);
        }

    }

	public delegate BehaviorReturnCode BehaviourActionDelegate(object agent, Blackboard blackboard);
}


