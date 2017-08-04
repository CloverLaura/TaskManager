using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class UserData
    {
        static private readonly List<User> Users = new List<User>();
        static private readonly List<Team> Teams = new List<Team>();

        static int nextId = 1;

        public void Add(User user)
        {
            user.UserID = nextId++;
            Users.Add(user);
        }

        public void AddTeam(User user, string team)
        {
            TeamData teamData = new TeamData();
            Team newTeam = teamData.FindByName(team);
            user.Teams.Add(newTeam);
        }

        public User GetById(int id)
        {
            var user = Users.Find(u => u.UserID == id);
            return user;
        }

        public User GetByEmail(string email)
        {
            var user = Users.Find(u => u.Email == email);
            return user;
        }

        public List<Team> GetTeamToList(User user)
        {
            List<Team> teams = new List<Team>();
            foreach(var team in TeamData.Teams)
            {
                teams.Add(team);
            }

            return teams;
        }

        static UserData()
        {
            Users.Add(new User()
            {
                FirstName = "Tyler",
                LastName = "Schlichenmeyer",
                Email = "tyler.schlichenmeyer@gmail.com",
                Password = "monkey",
                UserID = nextId++,

            });
        }
    }
}
