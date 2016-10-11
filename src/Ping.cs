using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazuzu156.HttpClient
{
	/// <summary>
	/// Pings a server
	/// </summary>
	public class Ping
	{
		private System.Net.NetworkInformation.Ping _p;
		private System.Net.NetworkInformation.PingReply _pr;

		private System.Net.NetworkInformation.IPStatus _pingStatus;
		private long _lastPingTime = 0;
		private long _totalPingTime = 0;
		private List<Dictionary<string, long>> _allPingTimes;
		private List<System.Net.NetworkInformation.IPStatus> _allStatuses;

		#region Properties
		/// <summary>
		/// Gets the total ping time
		/// </summary>
		public long TotalPingTime
		{
			get { return this._totalPingTime; }
			private set { this._totalPingTime = value; }
		}

		/// <summary>
		/// Gets the last ping time
		/// </summary>
		public long LastPingTime
		{
			get { return this._lastPingTime; }
			private set { this._lastPingTime = value; }
		}

		/// <summary>
		/// Gets all ping times in a list
		/// </summary>
		public List<Dictionary<string, long>> AllPingTimes
		{
			get { return this._allPingTimes; }
			private set { this._allPingTimes = value; }
		}

		/// <summary>
		/// Displays last ping status
		/// </summary>
		public System.Net.NetworkInformation.IPStatus PingStatus
		{
			get { return this._pingStatus; }
			private set { this._pingStatus = value; }
		}

		/// <summary>
		/// Gets all ping statuses in a list
		/// </summary>
		public List<System.Net.NetworkInformation.IPStatus> AllStatuses
		{
			get { return this._allStatuses; }
			private set { this._allStatuses = value; }
		}
		#endregion

		/// <summary>
		/// Pings a server
		/// </summary>
		public Ping()
		{
			this._p = new System.Net.NetworkInformation.Ping();
			this._allPingTimes = new List<Dictionary<string, long>>();
			this._allStatuses = new List<System.Net.NetworkInformation.IPStatus>();
		}

		/// <summary>
		/// Sends packets to host with itteration count
		/// </summary>
		/// <param name="host"></param>
		/// <param name="itterations"></param>
		/// <returns></returns>
		public void Send(string host, int itterations=4)
		{
			for(int i = 0; i < itterations; i++)
			{
				this._pr = this._p.Send(host);
				this._pingStatus = this._pr.Status;
				if(this._pingStatus == System.Net.NetworkInformation.IPStatus.Success)
				{
					this._lastPingTime = this._pr.RoundtripTime;
					this._totalPingTime += this._lastPingTime;

					var d = new Dictionary<string, long>();
					d.Add("LastPingTime", this._lastPingTime);
					d.Add("TotalPingTime", this._totalPingTime);

					this._allPingTimes.Add(d);
					this._allStatuses.Add(this._pingStatus);
				}
				else
				{
					throw new System.Net.NetworkInformation.PingException(host + " could not be reached!");
				}
			}
		}
	}
}
