namespace Pong.Structure.Components
{
    public abstract class Component
    {
        public Component()
        {
            ComponentType = Type.Update;
        }

        public Entity Entity;

        public abstract void Update();

        public Type ComponentType { get; protected set; }

        public enum Type { Draw, Update}
    }
}
