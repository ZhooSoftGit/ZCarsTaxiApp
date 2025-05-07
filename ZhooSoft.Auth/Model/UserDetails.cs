using ZhooCars.Common;

namespace ZhooSoft.Auth.Model
{
    public sealed class UserDetails
    {
        #region Fields

        private static readonly Lazy<UserDetails> lazy = new Lazy<UserDetails>(() => new UserDetails());

        #endregion

        #region Constructors

        private UserDetails()
        {
        }

        #endregion

        #region Properties

        public static UserDetails Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public string Badge { get; set; }

        public string Country { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Photo { get; set; }

        public string Title { get; set; }

        public string UserNTID { get; set; }

        public List<UserRoles> UserRoles { get; set; }

        #endregion

        #region Methods

        public static UserDetails getInstance()
        {
            return Instance;
        }

        public void reset()
        {
            Name = "";
            UserNTID = "";
            Badge = "";
            Title = "";
            Department = "";
            Phone1 = "";
            Phone2 = "";
            Email = "";
            Location = "";
            Country = "";
            Photo = "";
        }

        #endregion
    }
}
