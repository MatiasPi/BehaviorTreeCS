using System;


namespace BehaviorTreeCS {
	public class StatefulSequence : Composite { 
		protected int _LastBehavior = 0;

        /// <summary>
        /// attempts to run the behaviors all in one cycle (stateful on running)
        /// -Returns Success when all are successful
        /// -Returns Failure if one behavior fails or an error occurs
        /// -Does not Return Running
        /// </summary>
        /// <param name="behaviors"></param>
        public StatefulSequence(params BehaviorComponent[] behaviors):base(behaviors) { }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) { 
			//start from last remembered position
			for(; _LastBehavior < _Behaviors.Length;_LastBehavior++){
				switch (_Behaviors[_LastBehavior].Behave(agent, blackboard)){
				case BehaviorReturnCode.Failure:
					_LastBehavior = 0;
					ReturnCode = BehaviorReturnCode.Failure;
					return ReturnCode;
				case BehaviorReturnCode.Success:
					continue;
				case BehaviorReturnCode.Running:
					ReturnCode = BehaviorReturnCode.Running;
					return ReturnCode;
				default:
					_LastBehavior = 0;
					ReturnCode = BehaviorReturnCode.Success;
					return ReturnCode;
				}
			}

			_LastBehavior = 0;
			ReturnCode = BehaviorReturnCode.Success;
			return ReturnCode;
		}

        protected override void HandleException(Exception e) {
            _LastBehavior = 0;
        }

    }
}

