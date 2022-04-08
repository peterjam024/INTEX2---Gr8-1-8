using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrashySmashy.Models.ViewModels;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrashySmashy.Controllers
{
    public class AccountController : Controller
    {


        //this is same thing we set up in the Seed data for the login
        //attributes A.K.A. Instances
        private UserManager<IdentityUser> userManager;

        private SignInManager<IdentityUser> signInManager;



        //Constructor
        //gets instances of each of the things and passes in as parameters
        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            userManager = um;
            signInManager = sim;
        }

        //Get for login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {

            //if there is a return URL, we will use it, but if not, then we don't need to.
            if (returnUrl is null)
            {
                return View(new LoginModel());
            }

            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        //POST for login
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //get the user associated with the name
            if (ModelState.IsValid)
            {

                //trying to find the username that has been entered
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Username);

                //if we find something... 
                if (user != null)
                {
                    await signInManager.SignOutAsync();


                    //try logging in with information!!
                    //this is if it works!!

                   


                    //try logging in with information
                    //this is if it works

                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {

                        if (user.UserName != "admin")
                        {
                            //return Redirect(loginModel?.ReturnUrl ?? "/Admin");
                            //return RedirectToAction(loginModel?.ReturnUrl ?? "Admin");
                            return RedirectToAction("SeeTable", "Home", null);
                        }
                        else
                        {
                            //Session["tempid"] = fc["username"];

                            return RedirectToAction("VerifyAuth");
                        }
                        
                    }
                }
            }

            //if we don't find something...
            ModelState.AddModelError("", "Invalid username or password");
            return View(loginModel);
        }


        //LOGOUT
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();

            return Redirect(returnUrl);
        }


        string key = "test123!@@)(*";


        [HttpGet]
        //2FAuthentication
        public IActionResult VerifyAuth()

        {

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

            string UserUniqueKey = key; //as  demo, I have done this way. But you should use any encrypted value here which will be unique value per user.

            var user = User.ToString();

            var setupInfo = tfa.GenerateSetupCode("GoogleAuthenticationTest", user, UserUniqueKey, false, 10);


            //ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;

            return View();




            //if we don't find something...
            //ModelState.AddModelError("", "Invalid username or password");
                //return View("Login", loginModel);

        }



        

        [HttpPost]

        public IActionResult VerifyAuth(string passcode)

        {

            //if (Session["tempid"] == null)

            //{

            //    return RedirectToAction("Admin_Login");

            //}

            var token = passcode;

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();

            string UserUniqueKey = key; 

            bool isValid = tfa.ValidateTwoFactorPIN(UserUniqueKey, token);

            if (isValid)

            {

                return RedirectToAction("SeeTable", "Home", null);
            }

            return RedirectToAction("Login");

        }

    }

}
