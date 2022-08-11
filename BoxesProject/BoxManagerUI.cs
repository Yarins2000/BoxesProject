using DAL;
namespace BoxesProject
{
    internal class BoxManagerUI
    {
        readonly BoxManager _bm;

        public BoxManagerUI()
        {
            _bm = new();
        }
        public void Start()
        {
            while (true) //change later
            {

            }
        }
        //************* if there aren't any suitable boxes ***************

        /// <summary>
        /// choose the best box choices for the gift.
        /// </summary>
        /// <param name="x">the required gift's length</param>
        /// <param name="y">the required gift's height</param>
        public void ChooseBoxesForGift(/*double length, double height, int desiredBoxAmount*/)
        {
            Console.WriteLine("Please type the length you want for the gift: ");
            bool isValid = SizeValidation(Console.ReadLine(), out double length);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only decimals. Type again");
                isValid = SizeValidation(Console.ReadLine(), out length);
            }

            Console.WriteLine("Please type the height you want for the gift: ");
            isValid = SizeValidation(Console.ReadLine(), out double height);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only decimals. Type again");
                isValid = SizeValidation(Console.ReadLine(), out height);
            }

            Console.WriteLine("Please choose the amount you want: ");
            isValid = AmountValidation(Console.ReadLine(), out int amount);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only integers. Type again");
                isValid = AmountValidation(Console.ReadLine(), out amount);
            }

            var suitableBoxesListByAmount = _bm.SuitableBoxListByAmount(length, height, amount, out bool AreThereEnoughBoxes);

            string result = "";
            foreach (var box in suitableBoxesListByAmount)
                result += $"{box.AmountToGive} boxes of {box.Length}X{box.Height}\n";

            if (!AreThereEnoughBoxes)
                result += "Unfortunately we don't have the amount you asked for, would you still want to complete the purchasing?\n";
            else
                result += "Would you want to make the purchasing?\n";

            Console.WriteLine(result);
            string answer = Console.ReadLine();
            if (answer is "yes")
                _bm.UpdateTreeAfterPurchase(suitableBoxesListByAmount);
            else
                Console.WriteLine("Alright then, Bye");
        }

        public bool SizeValidation(string inputSize, out double size)
        {
            return double.TryParse(inputSize, out size);
        }
        public bool AmountValidation(string inputAmount, out int amount)
        {
            return int.TryParse(inputAmount, out amount);
        }
    }
}
