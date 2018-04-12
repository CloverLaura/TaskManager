using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ViewModels;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class ProjectController : Controller
    {
        public IEnumerable<SelectListItem> GetSelectListItems { get; set; }

        // GET: /<controller>/
        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(AddViewModel addViewModel)
        {
            ProjectData projectData = new ProjectData();
            UserData userData = new UserData();
            TeamData teamData = new TeamData();

            Project project = new Project
            {
                Name = addViewModel.Name,
                Description = addViewModel.Description,

            };
            projectData.Add(project);
            Response.Cookies.Append("projectCookie", project.ProjectID.ToString());
            string cookie=HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            userData.AddProject(user, project);

            if(user.UserTeams.Count() != 0)
            {
                foreach(Team team in user.UserTeams)
                {
                    if (user.UserID == team.CreatedBy)
                    {
                        teamData.AddProjectToTeam(team, project);
                    }
                }
            }
            return RedirectToAction("AddTasks", new { projectID = project.ProjectID });


        }

        [HttpGet]
        public IActionResult JoinTeam()
        {
            UserData userData = new UserData();
            TeamData teamData = new TeamData();
            List<Team> teams = teamData.TeamsToList();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            JoinTeamViewModel joinTeamViewModel = new JoinTeamViewModel();
            joinTeamViewModel.User = user;

            List<SelectListItem> dropTeams = new List<SelectListItem>();
            dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "0" });

            int value = 1;
            foreach (Team team in teams)
            {
                dropTeams.Add(new SelectListItem { Text = team.Name, Value = team.Name });
                value += 1;
            }

            joinTeamViewModel.Teams = dropTeams;
            


            return View(joinTeamViewModel);
        }

        [HttpPost] 
        public IActionResult JoinTeam(JoinTeamViewModel joinTeamViewModel)
        {
            TeamData teamData = new TeamData();
            UserData userData = new UserData();
            //Team selectedTeam = joinTeamViewModel.Team;
            //User user = joinTeamViewModel.User;
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            Team team = teamData.FindByName(joinTeamViewModel.Team);
            User user = userData.GetById(userID);
            team.UsersInTeam.Add(user);
            userData.AddTeam(user, joinTeamViewModel.Team);

            return RedirectToAction("Home", "Login", new { email = user.Email });
        }

        

        public IActionResult AddTasks(int projectID)
        {
            ProjectData projectData = new ProjectData();
            Project project = projectData.GetByID(projectID);
            AddTasksViewModel addTasksViewModel = new AddTasksViewModel();
            return View(addTasksViewModel);
        }

        [HttpPost]
        public ActionResult AddTasks(AddTasksViewModel obj, string addNewTask, string finish)
        {
            ProjectData projectData = new ProjectData();
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            int projectID = Convert.ToInt32(HttpContext.Request.Cookies["projectCookie"]);
            Project project = projectData.GetByID(projectID);
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(addNewTask))
                {

                    
                    Models.Task task = new Models.Task();
                    task.Name = obj.Name;
                    task.Description = obj.Description;
                    task.ProjectID = project.ProjectID;
                    taskData.AddTask(project, task);
                    taskData.Add(task);
                    return RedirectToAction("AddTasks", "Project", new { projectID = projectID });
                    

                }
                if (!string.IsNullOrEmpty(finish))
                {
                    Models.Task task = new Models.Task
                    {
                        Name = obj.Name,
                        Description = obj.Description,
                        ProjectID = projectID
                    };
                    
                    taskData.AddTask(project, task);
                    taskData.Add(task);
                    
                }
            }

            else
            {
                return View(obj);
            }

            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            return RedirectToAction("Home", "Login", new { email = user.Email });
        }

        public IActionResult ViewProjects()
        {
            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);

            return View(user);
        }

        public IActionResult FindTasks()
        {
            FindTasksViewModel findTasksViewModel = new FindTasksViewModel();
            ProjectData projectData = new ProjectData();
            List<Project> allProjects = projectData.ViewAllProjects();
            foreach (Project project in allProjects)
            {
                findTasksViewModel.AllProjects.Add(project);
            }

            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);

            findTasksViewModel.User = user;
            

            List<SelectListItem> dropTeams = new List<SelectListItem>();
            dropTeams.Add(new SelectListItem { Text = "Please select Team", Value = "0" });

            int value = 1;
            foreach (Team team in user.UserTeams)
            {
                dropTeams.Add(new SelectListItem { Text = team.Name, Value = team.Name });
                value += 1;
            }

            findTasksViewModel.Teams = dropTeams;

            return View(findTasksViewModel);
        }

        public IActionResult TeamProjects(FindTasksViewModel findTasksViewModel)
        {
            TeamProjectsViewModel teamProjectsViewModel = new TeamProjectsViewModel();
            TeamData teamData = new TeamData();
            Team selectedTeam = teamData.FindByName(findTasksViewModel.TeamName);
            foreach(Project project in selectedTeam.TeamProjects)
            {
                teamProjectsViewModel.AllProjects.Add(project);
            }
            return View(teamProjectsViewModel);
        }

        [HttpGet]
        public IActionResult FindTasks_ViewTasks(int id)
        {
            ProjectData projectData = new ProjectData();
            Project project = projectData.GetByID(id);

            return View(project);
        }

        [HttpPost]
        public IActionResult FindTasks_ViewTasks(string id)
        {
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            Models.Task task = taskData.GetByName(id);
            task.IsTaken = true;

            userData.AddTask(user, task);

            return RedirectToAction("ViewTasks");
        }

        [HttpGet]
        public IActionResult ViewTasks()
        {
            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);
            ViewTasksViewModel viewTasksViewModel = new ViewTasksViewModel();
            viewTasksViewModel.User = user;
            return View(viewTasksViewModel);
        }

        [HttpPost]
        public IActionResult ViewTasks(ViewTasksViewModel viewTasksViewModel)
        {
            TaskData taskData = new TaskData();
            Models.Task task = taskData.GetByName(viewTasksViewModel.Name);
            task.Completed = true;

            UserData userData = new UserData();
            string cookie = HttpContext.Request.Cookies["userCookie"];
            int userID = Convert.ToInt32(cookie);
            User user = userData.GetById(userID);

            user.UserTasks.Remove(task);

            return RedirectToAction("Home", "Login", new { email = user.Email });
        }

        

    }
}
