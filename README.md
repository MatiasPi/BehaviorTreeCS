BehaviorTreeCS
================

It is a fork of https://github.com/NetGnome/BehaviorTreeCS

Added features:

- I added a Blackboard to the implementation, so that the tree can share data across nodes
- I added a reference to the agent that calls the tree. Any node in the tree can access it.
- I made it easier to make custom Nodes
- I added extra Composite, Action and Decorator nodes.
- I added the methods Start(), Exit(), and Stop() to the nodes.
    - The Start() method gets called only when the node is being accessed for the first time, and every time it is accessed after it returned Success or Failure. This method is useful for initialization code.

    - The Exit() method gets called when only when the node has returned either Success or Failure, or when the node is Running, but the Stop() method is called. This method is useful for stopping any functionality that is running in the node, and preparing the node for future use.

    - The Stop() method invokes the Exit() method in the node that it is being called, and in ALL sub-nodes. Perfect to stop any running behavior that the tree is executing. You could call this method from the agent to stop the entire tree.

- Fixed some bugs


 Check out the test/ folder for an example.