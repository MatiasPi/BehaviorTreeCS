using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Just a silly test for you to start getting used to the library
namespace BehaviorTreeCS.test {

    class Test {
        static void Main() {

            BehaviorComponent tree;
            Blackboard blackboard;

            blackboard = new Blackboard();
            blackboard.SetVariable("num", 0);

            tree =
                new StatefulSequence(
                    new BehaviorAction(delegate (object ag, Blackboard bb) {
                        int num = (int)bb.GetVariable("num");
                        if(num < 9) {
                            bb.SetVariable("num", num + 1);
                            return BehaviorReturnCode.Running;
                        }
                        return BehaviorReturnCode.Success;
                    }),
                    new BehaviorAction(delegate (object ag, Blackboard bb) {
                        bb.SetVariable("successCode", 45);
                        return BehaviorReturnCode.Success;
                    })
                );

            BehaviorReturnCode treeState;
            do {
                treeState = tree.Behave(null, blackboard);
            }
            while (treeState == BehaviorReturnCode.Running);

            int res = (int)blackboard.GetVariable("num");
            int suc = (int)blackboard.GetVariable("successCode");

            // If res isn't 9 and suc isn't 45, then the tree didn't work as expected
            if (res != 9 || suc != 45 || treeState != BehaviorReturnCode.Success)
                throw new Exception();

            System.Console.WriteLine("All tests passed! Press ESC to exit");
            Console.ReadKey();
        }
    }
}