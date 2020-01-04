namespace MyGame
{
    /// <summary>
    ///     All the item that have coordinate and can be draw on screen is a game object
    /// </summary>
    public abstract class GameObject
    {
        public abstract void Draw ();

        public float X
        {
            get; set;
        }

        public float Y
        {
            get; set;
        }
    }
}
