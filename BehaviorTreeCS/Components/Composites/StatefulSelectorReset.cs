using System;


namespace BehaviorTreeCS {
    public class StatefulSelectorReset : StatefulSelector {

        public StatefulSelectorReset(params BehaviorComponent[] behaviors):base(behaviors) { }

        public override void Stop(object agent, Blackboard blackboard) {
            _LastBehavior = 0;
            base.Stop(agent, blackboard);
        }

    }
}

