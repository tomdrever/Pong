using System.Collections.Generic;
using System.Linq;

namespace Pong.Structure
{
    public class DelayedActionboard
    {
        public delegate void DelayedAction();

        private static Dictionary<DelayedAction, float> _actions;

        public static void AddDelayedAction(DelayedAction action, float time)
        {
            if (_actions == null) _actions = new Dictionary<DelayedAction, float>();

            if (_actions.ContainsKey(action))
            {
                _actions.Remove(action);
            }
            _actions.Add(action, time);
        }

        public static void Tick(float deltaTime)
        {
            if (_actions == null) _actions = new Dictionary<DelayedAction, float>();

            foreach (var action in _actions.ToList())
            {
                _actions[action.Key] -= deltaTime;

                if (_actions[action.Key] <= 0)
                {
                    // Get action and perform it
                    _actions.Remove(action.Key);

                    action.Key();
                }
            }
        }
    }
}