using System;


namespace BehaviorTreeCS {
    public class RandomDecorator : Decorator {
        private float _Probability;
        private Func<float> _RandomFunction;

        /// <summary>
        /// randomly executes the behavior
        /// </summary>
        /// <param name="probability">probability of execution</param>
        /// <param name="randomFunction">function that determines probability to execute</param>
        /// <param name="behavior">behavior to execute</param>
        public RandomDecorator(float probability, Func<float> randomFunction, BehaviorComponent behavior):base(behavior) {
            _Probability = probability;
            _RandomFunction = randomFunction;
        }


        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            if (_RandomFunction() <= _Probability)
            {
                ReturnCode = _Behavior.Behave(agent, blackboard);
                return ReturnCode;
            }
            else
            {
                ReturnCode = BehaviorReturnCode.Running;
                return BehaviorReturnCode.Running;
            }
        }
    }

	/// <summary>
	/// Should return a float that represents a probability
	/// </summary>
	public delegate float RandomDelegate();
}
