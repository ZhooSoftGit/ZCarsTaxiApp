using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTaxiApp.Core.DBModel;
using ZhooSoft.LocalData.DataStore;
using ZhooSoft.LocalData.Util;

namespace ZTaxiApp.Core.TableStore
{
    public class LiveRideDetailsHandler : BaseStore<LiveRideDetails>, ILiveRideDetailsStore
    {
        public LiveRideDetailsHandler(IDBUtil dBUtil) : base(dBUtil)
        {

        }
    }
}
