using System;
using System.Collections.Generic;
using System.Linq;
using Pong.Structure.Components;

namespace Pong.Structure
{
    public class Entity
    {
        public List<Component> Components { get; }

        public string Name { get; }
        public bool Active { get; set; }

        public Entity(string name)
        {
            Name = name;
            Components = new List<Component>();

            Active = true;
        }

        public void AddComponent(Component component)
        {
            component.Entity = this;

            Components.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            try
            {
                return (T)Components.First(c => c is T);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public bool HasComponent<T>() where T : Component
        {
            return GetComponent<T>() != null;
        }
    }
}