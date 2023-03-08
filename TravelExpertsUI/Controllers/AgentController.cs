using Microsoft.AspNetCore.Mvc;
using TravelExpertsData.Managers;
using TravelExpertsData.Models;

namespace TravelExpertsUI.Controllers
{
    /// <summary>
    /// Author: Aaron Li
    /// Februaray 18, 2023
    /// </summary>
    public class AgentController : Controller
    {
        /// <summary>
        /// travel experts context constructor
        /// </summary>
        private TravelExpertsContext _context { get; set; }

        public AgentController(TravelExpertsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of agencies and sends it to the view
        /// </summary>
        /// <returns>Contact view</returns>
        public IActionResult Contact()
        {
            List<Agency> agencies = AgentManager.GetAgencies(_context);
            return View(agencies);
        }
    }
}
