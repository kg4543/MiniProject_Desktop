using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSMSApp.Model;

namespace WpfSMSApp.Logic
{
    public class DataAcess
    {
        public static List<User> GetUsers()
        {
            List<User> users;

            using(var ctx = new SMSEntities())
            {
                users = ctx.User.ToList(); // = select * from user
            }

            return users;
        }

        // 입력 수정 동시에 (0 or 1)
        internal static int SetUser(User user)
        {
            using (var ctx = new SMSEntities())
            {
                ctx.User.AddOrUpdate(user);
                return ctx.SaveChanges(); // commit
            }
        }

        public static List<Store> GetStores()
        {
            List<Store> stores;
            using (var ctx = new SMSEntities())
            {
                stores = ctx.Store.ToList();
            }

            return stores;
        }

        internal static int SetStore(Store store)
        {
            using (var ctx = new SMSEntities())
            {
                ctx.Store.AddOrUpdate(store);
                return ctx.SaveChanges(); // commit
            }
        }

        public static List<Stock> GetStocks()
        {
            List<Stock> stocks;
            using (var ctx = new SMSEntities())
            {
                stocks = ctx.Stock.ToList();
            }
            return stocks;
        }

        /*internal static int SetStock(Stock stock)
        {
            using (var ctx = new SMSEntities())
            {
                ctx.Store.AddOrUpdate(stock);
                return ctx.SaveChanges(); // commit
            }
        }*/
    }
}
