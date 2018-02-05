using log4net;

namespace Fhey.Framework.Log4
{
    public class Provider 
    {
        private ILog _Loger;
        public Provider()
        {
            log4net.Config.XmlConfigurator.Configure();
            _Loger = LogManager.GetLogger(GetType());
        }

        public Provider(string name)
        {
            log4net.Config.XmlConfigurator.Configure();
            _Loger = LogManager.GetLogger(name);
        }

        public void Info(string message)=> _Loger.Info(message);

        public void Error(string message)=> _Loger.Error(message);


        public void Debug(string message)=> _Loger.Debug(message);


        public void Fatal(string message)=> _Loger.Fatal(message);


        public void Warn(string message)=> _Loger.Warn(message);
    }
}
