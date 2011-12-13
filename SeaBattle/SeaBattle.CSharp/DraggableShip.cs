namespace SeatBattle.CSharp
{
    public class DraggableShip : Ship
    {
        private DraggableShip(int length)
            : base(length)
        {
        }

        public Ship Source { get; private set; }

        public static DraggableShip From(Ship ship)
        {
            var draggableShip = new DraggableShip(ship.Length)
                                    {
                                        X = ship.X,
                                        Y = ship.Y,
                                        Orientation = ship.Orientation,
                                        Source = ship
                                    };

            return draggableShip;
        }

        public bool IsOrientationModified { get; set; }
    }
}