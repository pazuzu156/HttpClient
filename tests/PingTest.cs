using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazuzu156.HttpClient;
using System.Diagnostics;

namespace HttpClientTests
{
    [TestClass]
    public class PingTest
    {
        [TestMethod]
        public void PingTest_TestPingBase()
        {
            Ping p = new Ping();
            p.Send("kalebklein.com");

            this._log(p, "Ping with default itteration count");

            Assert.AreEqual("Success", p.PingStatus.ToString());
        }

        [TestMethod]
        public void PingTest_TestPingWithCustomItterCount()
        {
            int count = 10;
            Ping p = new Ping();
            p.Send("kalebklein.com", count);

            this._log(p, "Ping with " + count + " itterations");

            Assert.AreEqual("Success", p.PingStatus.ToString());
        }

        private void _log(Ping p, string intro)
        {
            Debug.WriteLine("Ping testing: " + intro + "\n");
            Debug.WriteLine("PingStatus: " + p.PingStatus.ToString() + "\n");

            long small = p.AllPingTimes[0]["LastPingTime"];
            long large = p.AllPingTimes[0]["LastPingTime"];

            Debug.WriteLine("==== Start All Ping Times ====");

            for (int i = 0; i < p.AllPingTimes.Count; i++) {
                var pingItem = p.AllPingTimes[i];

                var s = string.Format("Ping itteration: {0}\nLast ping time in ms: {1}\n"
                    + "Total ping time in ms: {2}\n"
                    + "Ping status: {3}\n",
                    i + 1, pingItem["LastPingTime"], pingItem["TotalPingTime"], p.AllStatuses[i]);

                Debug.WriteLine(s);

                if (pingItem["LastPingTime"] > large) {
                    large = pingItem["LastPingTime"];
                } else if (pingItem["LastPingTime"] < small) {
                    small = pingItem["LastPingTime"];
                }
            }
            Debug.WriteLine("==== End All Ping Times ====");

            Debug.WriteLine("Ping time in ms: " + p.TotalPingTime);
            Debug.WriteLine("Last ping time in ms: " + p.LastPingTime + "\n");

            Debug.WriteLine("Shortest ping time in ms: " + small);
            Debug.WriteLine("Longest ping time in ms: " + large);
        }
    }
}
