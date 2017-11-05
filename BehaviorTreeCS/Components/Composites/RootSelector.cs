using System;

namespace BehaviorTreeCS {
    [System.Obsolete("I dunno what's this for. Just use Selector")]
    public class RootSelector : PartialSelector {

        private Func<int> _Index;

        /// <summary>
        /// The selector for the root node of the behavior tree
        /// </summary>
        /// <param name="index">an index representing which of the behavior branches to perform</param>
        /// <param name="behaviors">the behavior branches to be selected from</param>
        [System.Obsolete("I dunno what's this for. Just use Selector")]
        public RootSelector(Func<int> index, params BehaviorComponent[] behaviors):base(behaviors) {
            _Index = index;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            switch (_Behaviors[_Index.Invoke()].Behave(agent, blackboard)) {
                case BehaviorReturnCode.Failure:
                    ReturnCode = BehaviorReturnCode.Failure;
                    return ReturnCode;
                case BehaviorReturnCode.Success:
                    ReturnCode = BehaviorReturnCode.Success;
                    return ReturnCode;
                case BehaviorReturnCode.Running:
                    ReturnCode = BehaviorReturnCode.Running;
                    return ReturnCode;
                default:
                    ReturnCode = BehaviorReturnCode.Running;
                    return ReturnCode;
            }
        }
    }

	/// <summary>
	/// Should return an index that represents which of the behavior branches to perform
	/// </summary>
	public delegate int SelectorIndexDelegate();

}
