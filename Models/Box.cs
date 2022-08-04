using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Box
    {
        public int Count { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Box(int count, DateTime releaseDate)
        {
            Count = count;
            ReleaseDate = releaseDate;
        }

        public void AddBoxCount() => Count++;

        public void BuyABox()
        {
            ReleaseDate = DateTime.Now;
            Count--;
        }
    }
}
