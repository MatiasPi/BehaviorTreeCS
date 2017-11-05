using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorTreeCS {

    public class VariableNotFoundException : Exception {
        public VariableNotFoundException(string key) : base(string.Format("The key {0} is not present in the blackboard", key)) { }
    }
    
    public class Blackboard {

        Dictionary<string, object> blackboard;
        
        public Blackboard(params KeyValuePair<string, object>[] variables) {

            blackboard = new Dictionary<string, object>();

            foreach(var var in variables)
                blackboard.Add(var.Key, var.Value);            
        }

        public object GetVariable(string key) {

            if(Exists(key))
                return blackboard[key];

            throw new VariableNotFoundException(key);
        }

        public void SetVariable(string key, object value) {
            blackboard[key] = value;
        }

        public bool Exists(string key) {
            return blackboard.ContainsKey(key);
        }

    }
}
