using System.Linq;

namespace BehaviorTreeCS {
    public abstract class Composite:BehaviorComponent {

        protected BehaviorComponent[] _Behaviors;

        public Composite(params BehaviorComponent[] behaviors) {
            _Behaviors = behaviors;
        }

        public override void Stop(object agent, Blackboard blackboard) {
            base.Stop(agent, blackboard);
            StopChildren(agent, blackboard);
        }

        protected void StopChildren(object agent, Blackboard blackboard) {
            var runningNodes = _Behaviors.Where(behavior => behavior.ReturnCode == BehaviorReturnCode.Running).ToArray();
            foreach (BehaviorComponent beh in runningNodes) {
                beh.Stop(agent, blackboard);
            }
        }
    }
}
