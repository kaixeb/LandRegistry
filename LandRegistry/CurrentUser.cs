using System;
using System.Collections.Generic;
using System.Text;

namespace LandRegistry
{
    public static class CurrentUser
    {
        public enum User
        {
            Admin,
            Guest
        }

        private static User curUser;

        public static User CurUser { get => curUser; set => curUser = value; }
    }
}
