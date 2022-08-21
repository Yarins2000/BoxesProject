using DAL;
using Models;
namespace BoxesProject
{
    internal class BoxManagerUI
    {
        readonly BoxManager _bm;
        readonly DBMock _db;

        public BoxManagerUI()
        {
            _bm = new();
            _db = DBMock.Instance;
        }
        public void Start()
        {
            _bm.DeleteExpiredBoxes(Console.WriteLine);
            while (true)
            {
                Console.WriteLine("Hello and welcome to the box's storage management." +
                    "Are you a worker or a purchaser? Please type 1 for worker or 2 for purchaser");
                bool isValid = IntValidation(Console.ReadLine(), out int choose);
                while(!isValid || (choose is not 1 && choose is not 2))
                {
                    Console.WriteLine("ivalid input. Only integers and only 1/2 is acceptable. Type again");
                    isValid = IntValidation(Console.ReadLine(), out choose);
                }
                if (choose is 1)
                {
                    Console.WriteLine("Would you like to: ");
                    Console.WriteLine("(1) Add a new box to the storage");
                    Console.WriteLine("(2) Display all the boxes");
                    Console.WriteLine("(3) Display a certain box");
                    Console.WriteLine("(4) Display boxes that haven't been sold for a certain period of time");

                    isValid = IntValidation(Console.ReadLine(), out int actionChoose);
                    while (!isValid || (actionChoose is not 1 && actionChoose is not 2 && actionChoose is not 3
                        && actionChoose is not 4))
                    {
                        Console.WriteLine("ivalid input. Only integers and only 1/2/3/4 is acceptable. Type again");
                        isValid = IntValidation(Console.ReadLine(), out actionChoose);
                    }

                    switch(actionChoose)
                    {
                        case 1:
                            AddNewBoxToStorage();
                            break;
                        case 2:
                            DisplayAllBoxes();
                            break;
                        case 3:
                            DisplayCertainBox();
                            break;
                        case 4:
                            DisplayBoxesByPeriodOfTime();
                            break;
                    }
                }

                if(choose is 2)
                {
                    ChooseBoxesForGift();
                }

                Console.WriteLine("Would you want to do somthing else? yes/no (or any other key)");
                string ans = Console.ReadLine();
                if (ans is not "yes")
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Have a nice day ♥");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void GetBoxInputFromUser(out double length, out double height)
        {
            Console.Write("Length: ");
            bool isValid = SizeValidation(Console.ReadLine(), out length);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only decimals. Type again");
                isValid = SizeValidation(Console.ReadLine(), out length);
            }
            Console.WriteLine();
            Console.Write("height: ");
            isValid = SizeValidation(Console.ReadLine(), out height);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only decimals. Type again");
                isValid = SizeValidation(Console.ReadLine(), out height);
            }
            Console.WriteLine();
        }

        public void AddNewBoxToStorage()
        {
            Console.WriteLine("Please type the box's details: ");
            GetBoxInputFromUser(out double length, out double height);
            Console.Write("Quantity: ");
            bool isValid = IntValidation(Console.ReadLine(), out int quantity);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only integers. Type again");
                isValid = IntValidation(Console.ReadLine(), out quantity);
            }

            Box newBox = new(length, height, quantity);
            _bm.AddNewBox(newBox);
        }

        public void DisplayAllBoxes()
        {
            _bm.ShowAllBoxes(Console.WriteLine);
        }

        public void DisplayCertainBox()
        {
            GetBoxInputFromUser(out double length, out double height);
            var suitsBoxes = _bm.GetSuitableBoxes(length, length, height, height);
            if(suitsBoxes.IsEmpty())
                Console.WriteLine("There is no box that suits the given size");
            else
                Console.WriteLine(suitsBoxes);
        }

        public void DisplayBoxesByPeriodOfTime()
        {
            Console.WriteLine("Please type the amount of days that have been passed since the boxes were bought: ");
            bool isValid = IntValidation(Console.ReadLine(), out int days);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only integeers. Type again");
                isValid = IntValidation(Console.ReadLine(), out days);
            }
            foreach (var box in _db.BoxesDates)
            {
                var currentBoxDate = box.UpdatedDate;
                if (currentBoxDate.AddDays(days) < DateTime.Now)
                    Console.WriteLine(box);
                else
                    break;
            }
        }


        /// <summary>
        /// choose the best box choices for the gift.
        /// </summary>
        public void ChooseBoxesForGift()
        {
            Console.WriteLine("Please type the gift's details: ");
            GetBoxInputFromUser(out double length, out double height);
            Console.Write("Amount: ");
            bool isValid = IntValidation(Console.ReadLine(), out int amount);
            while (!isValid)
            {
                Console.WriteLine("ivalid input. Only integers. Type again");
                isValid = IntValidation(Console.ReadLine(), out amount);
            }

            var suitableBoxesListByAmount = _bm.SuitableBoxListByAmount(length, height, amount, out bool AreThereEnoughBoxes);
            if(suitableBoxesListByAmount.IsEmpty())
            {
                Console.WriteLine("Unfortunately we don't have any boxes that suits the data you asked for.");
                return;
            }    

            string result = "";
            foreach (var box in suitableBoxesListByAmount)
                result += $"{box.AmountToGive} boxes of {box.Length}X{box.Height}\n";

            if (!AreThereEnoughBoxes)
                result += "Unfortunately we don't have the amount you asked for, would you want to complete the purchasing anyway?\n";
            else
                result += "Would you want to make the purchasing?\n";
            result += "type yes / no";

            Console.WriteLine(result);
            string answer = Console.ReadLine().ToLower();
            if (answer is "yes")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Thank you ☺");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                _bm.UpdateStorageAfterPurchase(suitableBoxesListByAmount);
                foreach (var box in suitableBoxesListByAmount)
                {
                    if(_bm.IsBoxQuantityAlmostEmpty(box))
                        Console.WriteLine($"Pay attention that box {box.Length}X{box.Height} is almost out of stock, only {box.Quantity} is/are left.");
                    if(box.IsEmpty())
                        Console.WriteLine($"Pay attention that box {box.Length}X{box.Height} is out of stock.");
                }
            }
            else
                Console.WriteLine("Alright then, Bye");
        }

        public bool SizeValidation(string inputSize, out double size)
        {
            return double.TryParse(inputSize, out size);
        }
        public bool IntValidation(string inputInt, out int amount)
        {
            return int.TryParse(inputInt, out amount);
        }
    }
}
