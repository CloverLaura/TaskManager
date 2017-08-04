using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TeamData
    {
        static public List<Team> Teams = new List<Team>();

        static int nextId = 1;


        public void Add(Team team)
        {
            team.TeamID = nextId++;
            Teams.Add(team);
        }

        public List<Team> TeamsToList()
        {
            return TeamData.Teams;
        }

        public Team FindByName(string name)
        {
            var team = Teams.Find(t => t.Name == name);
            return team;
        }


        static TeamData()
        {
            Teams.Add(new Team
            {
                Name = "Night Stalkers",
                Description = "Stalks the neighborhood for innocent voters.",
            });

            Teams.Add(new Team
            {
                Name = "Money Stealers",
                Description = "Collects money from unsuspecting do-gooders.",
            });
        }
    }
}
