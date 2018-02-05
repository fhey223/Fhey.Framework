using System;

namespace Fhey.Framework.Conversion
{
    public class ConvertDataSize 
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string GetSize(long b)
        {
            if (b.ToString().Length <= 10)
                return ConvertMB(b);
            if (b.ToString().Length >= 11 && b.ToString().Length <= 12)
                return ConvertGB(b);
            if (b.ToString().Length >= 13)
                return ConvertTB(b);
            return String.Empty;
        }

        /// <summary>
        /// 将B转换为TB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string ConvertTB(long b)
        {
            for (int i = 0; i < 4; i++)
            {
                b /= 1024;
            }
            return b + "TB";
        }

        /// <summary>
        /// 将B转换为GB
        /// </summary>
        /// <param name="b"></param>
        /// <returns drive.Name>盘符</returns>
        /// <returns drive.DriveFormat>磁盘格式</returns>
        /// <returns drive.DriveType>磁盘品牌</returns>
        /// <returns drive.VolumeLabel>磁盘卷标</returns>
        /// <returns drive.TotalSize>磁盘总容量</returns>
        /// <returns drive.TotalFreeSpace>磁盘空余容量</returns>
        public string ConvertGB(long b)
        {
            for (int i = 0; i < 3; i++)
            {
                b /= 1024;
            }
            return b + "GB";
        }

        /// <summary>
        /// 将B转换为MB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string ConvertMB(long b)
        {
            for (int i = 0; i < 2; i++)
            {
                b /= 1024;
            }
            return b + "MB";
        }
    }
}
