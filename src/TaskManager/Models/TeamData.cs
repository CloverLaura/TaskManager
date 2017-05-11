using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TeamData
    {
        static private readonly IList<Team> Teams;

        static int nextId = 1;



        public void Add(Team team)
        {
            team.TeamID = nextId++;
            Teams.Add(team);
        }
    }
}
