using SQLite;
using ZhooCars.Model.DTOs;
using ZhooSoft.LocalData.DataStore;

namespace ZCarsDriver.Core.DBModel
{
    public class VehicleModel : VehicleModelDto, IBaseDataObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }

    public class UserModel : UserDetailDto , IBaseDataObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
