using System;

namespace BehaviorTreeCS {
    public abstract class Decorator:BehaviorComponent {

        protected BehaviorComponent _Behavior;

        public Decorator(BehaviorComponent behavior) {
            this._Behavior = behavior;
        }

        protected override BehaviorReturnCode Update(object agent, Blackboard blackboard) {
            return _Behavior.Behave(agent, blackboard);
        }

        public override void Stop(object agent, Blackboard blackboard) {
            base.Stop(agent, blackboard);
            _Behavior.Stop(agent, blackboard);
        }
    }
}
