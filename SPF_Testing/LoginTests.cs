using SPF_ClassLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF_ClassLib.Tests
{
    [TestClass()]
    public class LoginTests
    {
        [TestMethod()]
        public void GetUsersTest()
        {
            Login loginF = new Login();
            Users UL = loginF.GetUsers();
        }
    }
}

