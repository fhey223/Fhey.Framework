using Fhey.Framework.Log4;
using System;
using System.Diagnostics;
using System.Threading;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var loger = new Provider();
            for (int count = 0; count < 10; count++)
            {
                Thread writeThread = new Thread(new ParameterizedThreadStart((para) =>
                {
                    Console.WriteLine(string.Format("开启线程{0}", para));
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    for (int i = 0; i < 100000; i++)
                    {
                        loger.Info(string.Format("日志测试数据,序号：{0}", i.ToString()));
                    }
                    sw.Stop();
                    Console.WriteLine(string.Format("线程{0}写入日志结束，共用时{1}毫秒", para, sw.ElapsedMilliseconds));
                }));
                writeThread.IsBackground = true;
                writeThread.Start(count);
            }
            Console.ReadKey();
        }
    }
}
