using System;


namespace BehaviorTreeCS {
    public class StatefulSequenceReset : StatefulSequence {

        public StatefulSequenceReset(params BehaviorComponent[] behaviors):base(behaviors) { }

        public override void Stop(object agent, Blackboard blackboard) {
            _LastBehavior = 0;
            base.Stop(agent, blackboard);
        }

    }
}

