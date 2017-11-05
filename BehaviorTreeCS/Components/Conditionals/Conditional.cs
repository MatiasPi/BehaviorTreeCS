using System;


namespace BehaviorTreeCS {
    public class Conditional : BehaviorComponent {

		private ConditionalDelegate _Bool;

        /// <summary>
        /// Returns a return code equivalent to the test 
        /// -Returns Success if true
        /// -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
		public Conditional(ConditionalDelegate test) {
            _Bool = test;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            switch (_Bool(agent, blackboard)) {
                case true:
                    ReturnCode = BehaviorReturnCode.Success;
                    return ReturnCode;
                case false:
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                default:
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
            }
        }
    }

	/// <summary>
	/// Should return an index that represents which of the behavior branches to perform
	/// </summary>
	public delegate bool ConditionalDelegate(object agent, Blackboard blackboard);
}
