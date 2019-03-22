using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SPF_ClassLib;

/// <summary>
/// Summary description for SPF_WS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SPF_WS : System.Web.Services.WebService
{
    
    private Logs LogFunc;

    public SPF_WS()
    {
        LogFunc = new Logs();
        //InitializeComponent(); 
    }

    public SPF_WS(string SignedUser)
    {
        LogFunc = new Logs(SignedUser);
    } 

    [WebMethod]
    public List<User> GetUsers(string email, string password)
    {
        Login loginF = new Login();
        List<User> UL = loginF.GetUsers(email, password);
        return UL;
    }

    [WebMethod]
    public List<Project> GetProjects()
    {
        Proj projF = new Proj();
        List<Project> result = projF.GetProjects();
        return result;
    }

    [WebMethod]
    public void SaveProject(Project project)
    {
        Proj projF = new Proj();
        projF.WriteProject(project);
        LogFunc.WriteLog(new Log("Save project " + project.Name));
    }

    [WebMethod]
    public void DeleteProject(Project project)
    {
        Proj projF = new Proj();
        projF.DeleteProject(project);
    }

}
