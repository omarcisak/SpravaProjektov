using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPF_Wpf
{
    /// <summary>
    /// Interaction logic for ZoznamProjektov.xaml
    /// </summary>
    public partial class ZoznamProjektov : Page
    {
        public List<ProjectService.Project> Projects { get; set; }

        public ZoznamProjektov()
        {
            InitializeComponent();

            ServiceAgent sa = new ServiceAgent();
            Projects = sa.soapClient.GetProjects().ToList();

            ShowData();
            if (LVProjects.Items.Count == 0)
                AddRow();
            else
                LVProjects.SelectedIndex = 0;
            setDataChanged(false);
            textBox1.Focus();
        }


        private ICommand _commandSaved;
        public ICommand commandSave
        {
            get
            {
                if (_commandSaved == null)
                {
                    _commandSaved = new RelayCommand(
                        param => this.SaveObject()//,
                        //param => this.CanSave()
                    );
                }
                return _commandSaved;
            }
        }

        //private bool CanSave()
        //{
        //    // Verify command can be executed here
        //}

        private void SaveObject()
        {
            ServiceAgent sa = new ServiceAgent();
            //sa.soapClient.SaveProjects()
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

        }

        public class RelayCommand : ICommand
        {
            #region Fields

            readonly Action<object> _execute;
            readonly Predicate<object> _canExecute;

            #endregion // Fields

            #region Constructors

            /// <summary>
            /// Creates a new command that can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            public RelayCommand(Action<object> execute)
                : this(execute, null)
            {
            }

            /// <summary>
            /// Creates a new command.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");

                _execute = execute;
                _canExecute = canExecute;
            }

            #endregion // Constructors

            #region ICommand Members


            public bool CanExecute(object parameters)
            {
                return _canExecute == null ? true : _canExecute(parameters);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameters)
            {
                _execute(parameters);
            }

            #endregion // ICommand Members
        }


        #region ListView

        private bool stopRefreshControls { get; set; }
        private bool dataChanged { get; set; }

        private void ShowData()
        {

            LVProjects.Items.Clear();

            foreach (var row in this.Projects)
            {
                LVProjects.Items.Add(row);
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //RefreshListView(textBox1.Text, textBox2.Text, textBox3.Text);
            setDataChanged(true);
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //RefreshListView(textBox1.Text, textBox2.Text, textBox3.Text);
            setDataChanged(true);
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            //RefreshListView(textBox1.Text, textBox2.Text, textBox3.Text);
            setDataChanged(true);
        }

        private void RefreshListView(string name, string abbreviation, string customer)
        {
            ProjectService.Project prj = (ProjectService.Project)LVProjects.SelectedItem;
            if (prj != null && !stopRefreshControls)
            {
                setDataChanged(true);

                prj.name = name;
                prj.abbreviation = abbreviation;
                prj.customer = customer;

                LVProjects.Items.Refresh();
            }
        }
        private void setDataChanged(bool value)
        {
            dataChanged = value;
            saveButton.IsEnabled = value;
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProjectService.Project prj = (ProjectService.Project)LVProjects.SelectedItem;
            if (prj != null)
            {
                stopRefreshControls = true;
                textBox1.Text = prj.name;
                textBox2.Text = prj.abbreviation;
                textBox3.Text = prj.customer;

                stopRefreshControls = false;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            setDataChanged(true);
            AddRow();
        }

        private void AddRow()
        {
            LVProjects.Items.Add(new ProjectService.Project() { id = getNewId() });
            LVProjects.SelectedIndex = LVProjects.Items.Count - 1;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Focus();
        }

        private string getNewId()
        {
            string newID = "prj1";
            ProjectService.Project lastProj = this.Projects.OrderByDescending(a => a.id).FirstOrDefault();
            if (lastProj != null)
            {
                int lastNum = int.Parse(lastProj.id.Remove(0, 3));
                newID = string.Format("prj{0}", lastNum + 1);
            }
            return newID;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectService.Project prj = (ProjectService.Project)LVProjects.SelectedItem;

            if (prj != null)
            {                
                string msg = string.Format("Naozaj vymazať projekt {0}?", prj.name);
                if (MessageBox.Show(msg, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    ServiceAgent sa = new ServiceAgent();
                    sa.soapClient.DeleteProject(prj);
                    Projects = sa.soapClient.GetProjects().ToList();
                    ShowData();
                    setDataChanged(false);
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView(textBox1.Text, textBox2.Text, textBox3.Text);
            ProjectService.Project prj = (ProjectService.Project)LVProjects.SelectedItem;
            int selIndex = LVProjects.SelectedIndex;

            ServiceAgent sa = new ServiceAgent();
            sa.soapClient.SaveProject(prj);

            Projects = sa.soapClient.GetProjects().ToList();
            ShowData();
            setDataChanged(false);
            LVProjects.SelectedIndex = selIndex;
        }

       
        #endregion

    }
}
