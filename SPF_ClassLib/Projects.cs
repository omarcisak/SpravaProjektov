using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SPF_ClassLib
{
    public class Proj
    {
        public List<Project> GetProjects()
        {
            //string path = string.Empty;
            //path = Directory.GetCurrentDirectory() + Properties.Settings.Default.ProjectsXML_path;// @"\CustOrders.xml";
            //Projects result = Helpers.DeserializeXMLFileToObject<Projects>(Properties.Settings.Default.ProjectsXML_path);// ("c:\\users\\ondrej\\source\\repos\\SpravaProjektov\\SPF_ClassLib\\ZoznamProjektov.xml");
            Projects result = Helpers.ReadFromXmlFile<Projects>(Properties.Settings.Default.ProjectsXML_path);
            return result.ProjectList;
        }

        public void WriteProject(Project project)
        {
            List<Project> existsProjects = GetProjects();
            Project existsProject = existsProjects.FirstOrDefault(a => a.Id == project.Id);
            if (existsProject != null)
                existsProjects.Remove(existsProject);

            existsProjects.Add(project);

            Projects projects = new Projects() { ProjectList = existsProjects.OrderBy(a=>a.Id).ToList() };
            Helpers.WriteToXmlFile<Projects>(Properties.Settings.Default.ProjectsXML_path, projects);

        }

        public void DeleteProject(Project project)
        {
            List<Project> existsProjects = GetProjects();
            Project existsProject = existsProjects.FirstOrDefault(a => a.Id == project.Id);
            if (existsProject != null)
                existsProjects.Remove(existsProject);

            Projects projects = new Projects() { ProjectList = existsProjects };
            Helpers.WriteToXmlFile<Projects>(Properties.Settings.Default.ProjectsXML_path, projects);

        }
    }

    [XmlRoot("projects")]
    public class Projects
    {
        [XmlElement("project")]
        public List<Project> ProjectList { get; set; }
    }

    public class Project
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("abbreviation")]
        public string Abbreviation { get; set; }
        [XmlElement("customer")]
        public string Customer { get; set; }
    }
}
