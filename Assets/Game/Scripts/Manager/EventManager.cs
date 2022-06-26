using System;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicArgTest
{
    public class EventArg
    {
        public List<object> Args;

        public EventArg()
        {
            this.Args = new List<object>(0);
        }
        public EventArg(object rArg0)
        {
            this.Args = new List<object>(1);
            this.Args.Add(rArg0);
        }
        public EventArg(object rArg0, object rArg1)
        {
            this.Args = new List<object>(2);
            this.Args.Add(rArg0);
            this.Args.Add(rArg1);
        }
        public EventArg(object rArg0, object rArg1, object rArg2)
        {
            this.Args = new List<object>(3);
            this.Args.Add(rArg0);
            this.Args.Add(rArg1);
            this.Args.Add(rArg2);
        }
        public EventArg(object rArg0, object rArg1, object rArg2, object rArg3)
        {
            this.Args = new List<object>(4);
            this.Args.Add(rArg0);
            this.Args.Add(rArg1);
            this.Args.Add(rArg2);
            this.Args.Add(rArg3);
        }
        public EventArg(object rArg0, object rArg1, object rArg2, object rArg3, object rArg4)
        {
            this.Args = new List<object>(5);
            this.Args.Add(rArg0);
            this.Args.Add(rArg1);
            this.Args.Add(rArg2);
            this.Args.Add(rArg3);
            this.Args.Add(rArg4);
        }
        public EventArg(object rArg0, object rArg1, object rArg2, object rArg3, object rArg4, object rArg5)
        {
            this.Args = new List<object>(6);
            this.Args.Add(rArg0);
            this.Args.Add(rArg1);
            this.Args.Add(rArg2);
            this.Args.Add(rArg3);
            this.Args.Add(rArg4);
            this.Args.Add(rArg5);
        }

        public T Get<T>(int nIndex)
        {
            if (this.Args == null) return default(T);
            if (nIndex < 0 || nIndex >= this.Args.Count) return default(T);
            return (T)this.Args[nIndex];
        }
    }

    public class EventManager : TSingleton<EventManager>
    {
        public class Event
        {
            public GameEvent GameEvent;
            public List<Action<EventArg>> Callbacks;
        }

        public Dictionary<GameEvent, Event> mEvents;

        private EventManager()
        {
        }

        public void Initialize()
        {
            this.mEvents = new Dictionary<GameEvent, Event>();
        }
        public void Binding(GameEvent rGameEvent, Action<EventArg> rEventCallback)
        {
            if (this.mEvents == null) return;

            if (!this.mEvents.TryGetValue(rGameEvent, out var rEvent))
            {
                rEvent = new Event() { GameEvent = rGameEvent, Callbacks = new List<Action<EventArg>>() { rEventCallback } };
                this.mEvents.Add(rGameEvent, rEvent);
                return;
            }

            if (rEvent.Callbacks == null)
            {
                rEvent.Callbacks = new List<Action<EventArg>>();
                rEvent.Callbacks.Add(rEventCallback);
                return;
            }

            if (!rEvent.Callbacks.Contains(rEventCallback))
            {
                rEvent.Callbacks.Add(rEventCallback);
            }

        }

        public void Unbinding(GameEvent rGameEvent, Action<EventArg> rEventCallback)
        {
            if (this.mEvents == null) return;
            if (this.mEvents.TryGetValue(rGameEvent, out var rEvent))
            {
                if (rEvent.Callbacks != null)
                    rEvent.Callbacks.Remove(rEventCallback);
            }
        }

        public void Unbinding(GameEvent rGameEvent)
        {
            if (this.mEvents == null) return;

            if (this.mEvents.TryGetValue(rGameEvent, out var rEvent))
            {
                if (rEvent.Callbacks != null)
                    rEvent.Callbacks.Clear();
            }
        }

        public void Distribute(GameEvent rGameEvent)
        {
            var rEventArg = new EventArg();
            this.DistributeArg(rGameEvent, rEventArg);
        }
        public void Distribute(GameEvent rGameEvent, object rEventArg0)
        {
            var rEventArg = new EventArg(rEventArg0);
            this.DistributeArg(rGameEvent, rEventArg);
        }
        public void Distribute(GameEvent rGameEvent, object rEventArg0, object rEventArg1)
        {
            var rEventArg = new EventArg(rEventArg0, rEventArg1);
            this.DistributeArg(rGameEvent, rEventArg);
        }
        public void Distribute(GameEvent rGameEvent, object rEventArg0, object rEventArg1, object rEventArg2)
        {
            var rEventArg = new EventArg(rEventArg0, rEventArg1, rEventArg2);
            this.DistributeArg(rGameEvent, rEventArg);
        }
        public void Distribute(GameEvent rGameEvent, object rEventArg0, object rEventArg1, object rEventArg2, object rEventArg3)
        {
            var rEventArg = new EventArg(rEventArg0, rEventArg1, rEventArg2, rEventArg3);
            this.DistributeArg(rGameEvent, rEventArg);
        }
        public void Distribute(GameEvent rGameEvent, object rEventArg0, object rEventArg1, object rEventArg2, object rEventArg3, object rEventArg4)
        {
            var rEventArg = new EventArg(rEventArg0, rEventArg1, rEventArg2, rEventArg3, rEventArg4);
            this.DistributeArg(rGameEvent, rEventArg);
        }
        public void Distribute(GameEvent rGameEvent, object rEventArg0, object rEventArg1, object rEventArg2, object rEventArg3, object rEventArg4, object rEventArg5)
        {
            var rEventArg = new EventArg(rEventArg0, rEventArg1, rEventArg2, rEventArg3, rEventArg4, rEventArg5);
            this.DistributeArg(rGameEvent, rEventArg);
        }

        private void DistributeArg(GameEvent rGameEvent, EventArg rEventArg)
        {
            if (this.mEvents == null) return;

            if (this.mEvents.TryGetValue(rGameEvent, out var rEvent))
            {
                var rCallbacks = rEvent.Callbacks;
                if (rCallbacks != null)
                {
                    for (int i = 0; i < rCallbacks.Count; i++)
                    {
                        rCallbacks[i].Invoke(rEventArg);
                    }
                }
            }
        }
    }

}
