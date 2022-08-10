using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Box
    {
        public int Quantity { get; set; }
        public DateTime ReleaseDate { get; set; }

        public double Length { get; set; }
        public double Height { get; set; }

        public Box(double length, double height, int quantity, DateTime releaseDate = default)
        {
            Length = length;
            Height = height;
            Quantity = quantity;
            ReleaseDate = default ? DateTime.Now : releaseDate;//======================check!!!
        }

        public void AddBoxCount(int q) => Quantity += q;

        public void BuyABox()
        {
            ReleaseDate = DateTime.Now;
            Quantity--;
        }

        public override string ToString()
        {
            return $"Box's length and width: {Length}, height: {Height}, quantity in storage: {Quantity}" +
                $", released date: {ReleaseDate:d}";
        }
    }
}
