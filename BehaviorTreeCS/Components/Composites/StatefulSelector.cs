using System;


namespace BehaviorTreeCS {
	public class StatefulSelector : Composite {
		protected int _LastBehavior = 0;

        /// <summary>
        /// Selects among the given behavior components (stateful on running) 
        /// Performs an OR-Like behavior and will "fail-over" to each successive component until Success is reached or Failure is certain
        /// -Returns Success if a behavior component returns Success
        /// -Returns Running if a behavior component returns Running
        /// -Returns Failure if all behavior components returned Failure
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public StatefulSelector(params BehaviorComponent[] behaviors):base(behaviors) { }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) { 
			for(; _LastBehavior < _Behaviors.Length; _LastBehavior++) {
				switch (_Behaviors[_LastBehavior].Behave(agent, blackboard)) {
				case BehaviorReturnCode.Failure:
					continue;
				case BehaviorReturnCode.Success:
					_LastBehavior = 0;
					ReturnCode = BehaviorReturnCode.Success;
					return ReturnCode;
				case BehaviorReturnCode.Running:
					ReturnCode = BehaviorReturnCode.Running;
					return ReturnCode;
				default:
					continue;
				}
			}

			_LastBehavior = 0;
			ReturnCode = BehaviorReturnCode.Failure;
			return ReturnCode;
		}
    }
}
