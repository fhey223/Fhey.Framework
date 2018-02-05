using System;

namespace Common
{
    public class TimeHelper 
    {
        /// <summary>
        ///     将选中的分钟或者小时转换秒
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="time"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int TimeConversion(string cbo, int time, out string message)
        {
            message = string.Empty;
            if (time == 0)
                return 0;
            var protectionTime = 0;
            if (cbo.Equals("ss")) //秒
            {
                if (time < 0 || time > 157680000)
                {
                    message = "时间小于１秒,时间大于5年";
                    return -1;
                }
                protectionTime = time;
            }
            if (cbo.Equals("mm")) //分钟
            {
                if (time > 2628000 || time < 0)
                {
                    message = "时间大于5年";
                    return -1;
                }
                protectionTime = time*60;
            }
            if (cbo.Equals("hh")) //小时
            {
                if (time > 43800 || time < 0)
                {
                    message = "时间大于5年";
                    return -1;
                }
                protectionTime = time*60*60;
            }

            if (cbo.Equals("tt")) //天
            {
                if (time > 1825 || time < 0)
                {
                    message = "时间大于5年";
                    return -1;
                }
                protectionTime = time*60*60*24;
            }

            if (cbo.Equals("dd")) //月
            {
                if (time > 60 || time < 0)
                {
                    message = "时间大于5年";
                    return -1;
                }
                protectionTime = time*60*60*24*30;
            }

            if (cbo.Equals("yy")) //年
            {
                if (time > 5 || time < 0)
                {
                    message = "时间大于5年";
                    return -1;
                }
                protectionTime = time*60*60*24*365;
            }

            return protectionTime;
        }

        /// <summary>
        ///     时间转换成功分钟或者小时
        /// </summary>
        /// <param name="protectionTypetime">秒</param>
        /// <returns></returns>
        private string TimeConversion(string protectionTypetime)
        {
            if (protectionTypetime != null)
            {
                var time = int.Parse(protectionTypetime)/60/60/24/365; //年
                if (time > 0)
                    return time + "年";
                time = int.Parse(protectionTypetime)/60/60/24/30; //月
                if (time > 0)
                    return time + "月";
                time = int.Parse(protectionTypetime)/60/60/24; //天
                if (time > 0)
                    return time + "天";
                time = Convert.ToInt32(protectionTypetime)/60/60; //小时
                if (time > 0)
                    return time + "小时";
                time = Convert.ToInt32(protectionTypetime)/60; //分钟
                if (time > 0)
                    return time + "分钟";
                time = Convert.ToInt32(protectionTypetime); //秒
                return time + "秒";
            }
            return null;
        }
    }
}