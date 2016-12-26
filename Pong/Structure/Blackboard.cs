using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pong.Structure
{
    public class Blackboard
    {
        private static List<Entity> _entities;

        public static List<Entity> Entities => _entities ?? (_entities = new List<Entity>());

        public static int P1Score { get; set; }
        public static int P2Score { get; set; }

        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }
        public static Vector2 Center => new Vector2(WindowWidth / 2f, WindowHeight / 2f);

        public static Entity GetEntity(string name)
        {
            try
            {
                return Entities.First(e => e.Name == name);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            
        }
        public static Entity[] GetEntities(string searchTerm)
        {
            try
            {
                return Entities.Where(e => e.Name.Contains(searchTerm)).ToArray();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
        
    }
}
