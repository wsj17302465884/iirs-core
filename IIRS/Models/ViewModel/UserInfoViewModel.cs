using System;
namespace IIRS.Models.ViewModel
{
    public class UserInfoViewModel
    {
        public UserInfoViewModel()
        {
        }

        public string LoginName { get; set; }

        public string RealName { get; set; }

        public int Status { get; set; }

        public string Remark { get; set; }

        public DateTime LastLoginTime { get; set; }
    }
}
