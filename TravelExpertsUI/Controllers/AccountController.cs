using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExpertsData.Models;
using TravelExpertsData.Managers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelExpertsUI.Controllers
{
    public class AccountController : Controller
    {
        private TravelExpertsContext _context { get; set; }

        public AccountController(TravelExpertsContext context)
        {
            _context = context;
        }

        public IActionResult Login(string returnUrl = "")
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        //Author : Rabab Kazim Dated feb- 2023
        public IActionResult Register()
        {
            return View(new Customer());
        }

        //Author : Rabab Kazim Dated feb- 2023

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AccountManager.RegisterCustomer(_context, customer);
                    return Redirect("~/Home/Index");
                }
                else
                {
                    return View(customer);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer customer)
        {
            Customer thisCustomer = AccountManager.Authenticate(_context, customer.Username, customer.Password);
            if (thisCustomer == null)
            {
                return View(); //Authentication failed
            }

            HttpContext.Session.SetInt32("CurrentCustomer", (int)thisCustomer.CustomerId);

            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, thisCustomer.CustFirstName + " " + thisCustomer.CustLastName),
            new Claim("FullName", thisCustomer.CustFirstName + " " + thisCustomer.CustLastName),
            new Claim(ClaimTypes.Role, "RegisteredUser")
        };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            //Get Authentication ticket
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (string.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(TempData["ReturnUrl"].ToString()); //Go to the return URL
        }

        public async Task<IActionResult> LogoutAsync()
        {
            //Return the authentication ticket
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("CurrentCustomer");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        //Author : Rabab Kazim Dated feb- 2023
        public IActionResult ProfileView()
        {
            TempData["CustomerId"] = HttpContext.Session.GetInt32("CurrentCustomer");

            int CustomerID = (int)TempData["CustomerId"];

            Customer customer = AccountManager.GetCustomer(_context, CustomerID);
           
            return View(customer);
        }

        //Author : Rabab Kazim Dated feb- 2023
        public ActionResult Edit(int id)
        {
           Customer customer = AccountManager.GetCustomer(_context, id);
            
           return View(customer);
        
        }
        //Author : Rabab Kazim Dated feb- 2023
        // POST: Account Controller/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
                if (ModelState.IsValid)
                {
                    AccountManager.UpdateCustomer(_context, id, customer);
                }
            return View(customer);
        }
    }
}
