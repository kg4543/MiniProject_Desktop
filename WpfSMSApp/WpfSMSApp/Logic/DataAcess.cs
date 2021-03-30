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

        /// <summary>
        /// 입력 수정 동시에
        /// </summary>
        /// <param name="user"></param>
        /// <returns> 0 또는 1 이상 </returns>
        internal static int SetUser(User user)
        {
            using (var ctx = new SMSEntities())
            {
                ctx.User.AddOrUpdate(user);
                return ctx.SaveChanges(); // commit
            }
        }
    }
}
