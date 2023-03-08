using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

namespace TravelExpertsData.Managers
{
    /// <summary>
    /// Author: Aaron Li
    /// February 18, 2023
    /// </summary>
    public static class AgentManager
    {
        /// <summary>
        /// Retrieves a list of agencies from the database
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <returns>List of travel agencies</returns>
        public static List<Agency> GetAgencies(TravelExpertsContext db)
        {
            List<Agency> agencyList = db.Agencies.Include(a => a.Agents).ToList();
            return agencyList;
        }

        /// <summary>
        /// Retrieves a list of agents sorted by agency
        /// </summary>
        /// <param name="db">Travel Experts Database context</param>
        /// <param name="id">Agency ID</param>
        /// <returns>List of agents sorted by agency</returns>
        public static List<Agent> GetAgentsByAgency(TravelExpertsContext db, int id)
        {
            List <Agent> agents = db.Agents.Where(a => a.AgencyId == id).ToList();
            return agents;
        }
    }
}
