using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace LoginReg.Controllers
{


    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context){
            _context=context;
            _context.SaveChanges();
        }



        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        
       [HttpPost("Home/create")]
       public IActionResult Create(User user ){
           if(ModelState.IsValid){
               Console.WriteLine("Valid Registery");
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password= Hasher.HashPassword(user,user.Password);
            user.Confirm_Password= Hasher.HashPassword(user,user.Confirm_Password);
            
                _context.Add(user);
                _context.SaveChanges();
                

               return View("Registerd");
           }
           else{
               Console.WriteLine("Invalid Register");
               return View("Index");

           }
    
        }


        public IActionResult displayView(){

            List<User> allUsers = _context.users.ToList();
            ViewBag.user=allUsers;
            ViewBag.users=allUsers;
            return View("Details");
        }

        [HttpPost("Home/login")]
        public IActionResult LogingMethod(string Email, string password){
            User logUser= _context.users.SingleOrDefault(user =>user.Email==Email);
            Console.WriteLine(logUser.Email);
            Console.WriteLine(logUser.Password);
            ViewBag.user=logUser;
            return View("Details");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
