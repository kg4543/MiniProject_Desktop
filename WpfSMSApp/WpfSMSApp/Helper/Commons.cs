using NLog;
using WpfSMSApp.Model;

namespace WpfSMSApp
{
    public class Commons
    {
        //NLog 정적 인스턴스 생성
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        //Login한 정보 인스턴스 
        public static User LOGINED_USER; 
    }
}
