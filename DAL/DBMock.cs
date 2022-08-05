using Models;
namespace DAL
{
    public class DBMock
    {
        private static DBMock _instance;
        public static DBMock Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBMock();
                return _instance;
            }
        }
        private DBMock()
        {
            _instance = new DBMock();
            Initialize();
        }

        private void Initialize()
        {
            Box b1 = new(4, 5, 2);
            Box b2 = new(6.5, 2.5, 6);
            Box b3 = new(8, 9, 5);
        }
    }
}
