namespace Models
{
    public class Box
    {
        /// <summary>
        /// The quantity of the box in the storage.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The updated date of the box.
        /// </summary>
        public DateTime UpdatedDate { get; private set; }

        /// <summary>
        /// The length and width of the box.
        /// </summary>
        public double Length { get; private set; }
        /// <summary>
        /// The height if the box.
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Represents the amount of boxes for the gift. After the purchasing, it'd be decreased from the box's quantity.
        /// </summary>
        public int AmountToGive { get; set; } = 0;

        public Box(double length, double height, int quantity, DateTime date = default)
        {
            Length = length;
            Height = height;
            Quantity = quantity;
            UpdatedDate = date == default ? DateTime.Now : date;
        }

        public void AddBoxCount(int q) => Quantity += q;

        public bool IsEmpty() => Quantity is 0;

        /// <summary>
        /// Updating the box's date to the current date.
        /// </summary>
        public void UpdateTheDate() => UpdatedDate = DateTime.Now;

        public override string ToString()
        {
            return $"Box's length and width: {Length}, height: {Height}, quantity in storage: {Quantity}" +
                $", updated date: {UpdatedDate:d}";
        }
    }
}
