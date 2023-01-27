using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User6Lib
{
    public class Calculations
    {
        public string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime,int consultationTime)
        {
            int i = 0;
            TimeSpan workdur = endWorkingTime - beginWorkingTime;
            int a = Convert.ToInt32(workdur.Ticks / new TimeSpan(0, consultationTime, 0).Ticks);
            TimeSpan[] endtime = new TimeSpan[startTimes.Length];
            List<string> retst = new List<string>();
            foreach(TimeSpan start in startTimes)
            {
                endtime[i]=start + new TimeSpan(0, durations[i], 0);
                retst.Add(string.Format("{0:00}:{1:00}", endtime[i].Hours, endtime[i].Minutes));
                i++;
            }
            return retst.ToArray();
        }
    }
}
