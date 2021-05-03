using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Configuration;
using rdev_tests.Model;

namespace rdev_tests.AppManager
{
    public class DbHelper
    {
        private ApplicationManager applicationManager;
        public string ConnectionString { get; private set; }

        public DbHelper(ApplicationManager manager, string connectionString)
        {
            this.applicationManager = manager;
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
        /// Получение информации о состоянии колонки sysboolean из БД
        /// </summary>
        /// <param name="recid"></param>
        /// <returns></returns>
        public bool? GetInfoTypesSysboolean(string recid)
        {
            RdevDB db = new RdevDB(ConnectionString);
            Guid id = Guid.Parse(recid);
            bool? sysboolean = db.Types.Where(x => x.Recid == id).Select(x => x.Sysboolean).FirstOrDefault();
            return sysboolean;
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
        /// возвращает количество записей в БД
        /// </summary>
        /// <returns></returns>
        public int CheckingRowInDb()
        {
            RdevDB db = new RdevDB(ConnectionString);
            int records = db.Types.Where(x => x.Recstate != 0).Count();
            return records;
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
            if (type == "sysboolean")
            {
                textTypes = db.Types.Where(x => x.Recid == id).Select(x => x.Sysboolean).FirstOrDefault().ToString();
                return textTypes;
            }
            if (type == "sysdate")
            {
                textTypes = db.Types.Where(x => x.Recid == id).Select(x => x.Sysdate).FirstOrDefault().ToString();
                return textTypes;
            }
            if (type == "sysint")
            {
                textTypes = db.Types.Where(x => x.Recid == id).Select(x => x.Sysint).FirstOrDefault().ToString();
                return textTypes;
            }
            if (type == "sysenum")
            {
                textTypes = db.Types.Where(x => x.Recid == id).Select(x => x.Sysenum).FirstOrDefault().ToString();
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
            if (type == "sysboolean")
            {
                id = db.Types.Where(x => x.Sysboolean != null).Select(x => x.Recid).FirstOrDefault();
            }
            if (type == "sysdate")
            {
                id = db.Types.Where(x => x.Sysdate != null).Select(x => x.Recid).FirstOrDefault();
            }
            if (type == "sysint")
            {
                id = db.Types.Where(x => x.Sysint != null).Select(x => x.Recid).FirstOrDefault();
            }
            if (type == "sysenum")
            {
                id = db.Types.Where(x => x.Sysenum != null).Select(x => x.Recid).FirstOrDefault();
            }
            return id.ToString();
        }
    }
}
