namespace Models
{
    public class Box
    {
        public int Quantity { get; set; }
        public DateTime UpdatedDate { get; set; }

        public double Length { get; set; }
        public double Height { get; set; }

        /// <summary>
        /// Represents the amount of boxes for the gift. After the purchasing, it'd be decreased from the box's quantity.
        /// </summary>
        public int AmountToGive { get; set; } = 0;

        /// <summary>
        /// Represents the reference for the DateTime Queue.
        /// </summary>
        public QNode<DateTime> DateReference { get; set; }

        public Box(double length, double height, int quantity, DateTime releaseDate = default)
        {
            Length = length;
            Height = height;
            Quantity = quantity;
            UpdatedDate = releaseDate == default ? DateTime.Now : releaseDate;
            DateReference = new QNode<DateTime>(UpdatedDate);
        }

        public void AddBoxCount(int q) => Quantity += q;

        public void BuyBoxes(int q)
        {
            UpdatedDate = DateTime.Now;
            Quantity -= q;
        }

        public bool IsEmpty() => Quantity is 0;

        public override string ToString()
        {
            return $"Box's length and width: {Length}, height: {Height}, quantity in storage: {Quantity}" +
                $", updated date: {UpdatedDate:d}";
        }
    }
}
