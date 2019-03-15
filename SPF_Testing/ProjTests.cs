using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPF_ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPF_ClassLib.Tests
{
    [TestClass()]
    public class ProjTests
    {
        [TestMethod()]
        public void GetProjectsTest()
        {
            Proj projF = new Proj();
            List<Project> UL = projF.GetProjects();
            Project proN = new Project() { Id = "prj6", Name = "Added Project", Abbreviation = "Add abbrev", Customer = "add cust" };
            UL.Add(proN);
            projF.WriteProject(proN);
        }
    }
}