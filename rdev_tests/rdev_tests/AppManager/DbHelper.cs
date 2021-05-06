using System;
using System.Linq;
using rdev_tests.Model;

namespace rdev_tests.AppManager
{
    public class DbHelper
    {
        private ApplicationManager applicationManager;
        public string ConnectionString { get; private set; }

        public DbHelper(ApplicationManager manager, string connectionString)
        {
            applicationManager = manager;
            ConnectionString = connectionString;
        }
        /// <summary>
        /// Получение информации о recstate записи
        /// </summary>
        /// <param name="recid"></param>
        public int CheckRecstateInDb(string recid)
        {
            RdevDB db = new RdevDB(ConnectionString);
            Guid id = Guid.Parse(recid);
            int recstate = db.Types.Where(x => x.Recid == id).Select(x => x.Recstate).FirstOrDefault();
            return recstate;
        }
       
        /// <summary>
        /// проверка наличия записи в БД
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public bool CheckingRecord(string recid)
        {
            RdevDB db = new RdevDB(ConnectionString);
            Guid id = Guid.Parse(recid);
            int records = db.Types.Where(x => x.Recid == id).Count();
            if (records > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        
        /// <summary>
        /// получение текста колонки тестируемого типа
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public string GetInfoTypesForTestingType(string recid, string type)
        {
            RdevDB db = new RdevDB(ConnectionString);
            Guid id = Guid.Parse(recid);
            string textTypes = "";            
            if (type == "sysstring")
            {
                textTypes = db.Types.Where(x => x.Recid == id).Select(x => x.Sysstring).FirstOrDefault().ToString();
                return textTypes;
            }            
            return textTypes;
        }
        /// <summary>
        /// получаем первый найденный в БД id тестируемого типа
        /// </summary>
        /// <param name="type"></param>
        public string GetRecidForTestingType(string type)
        {
            RdevDB db = new RdevDB(ConnectionString);
            Guid id = new Guid();            
            if (type == "sysstring")
            {
                id = db.Types.Where(x => x.Sysstring != null).Select(x => x.Recid).FirstOrDefault();
            }            
            return id.ToString();
        }
    }
}
