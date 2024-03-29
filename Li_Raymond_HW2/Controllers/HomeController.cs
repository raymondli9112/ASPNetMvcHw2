﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
namespace Li_Raymond_HW2.Controllers
{
    public class HomeController : Controller
    {
        const int tacoPrice = 3;
        const int sandwichPrice = 5;
        const decimal tax = 0.085M;

        public ViewResult Index()
        {          
            return View();
        }
        public ViewResult SubmitOrder(string customerCode, string numberOfTacos, string numberOfSandwiches)
        {
            Boolean bolCheckCustomerCode = ValidateCustomerCode(customerCode);
            if (bolCheckCustomerCode == false)
            {
                ViewBag.ErrorMessage = "Customer code must be 2-4 characters.";
                return View("Index");
            }

            //Validate(customerCode, numberOfTacos, numberOfSandwiches);

            decimal decTemp = 0.00M;
            ViewBag.CustomerCode = customerCode.ToUpper();
            int numOfSandwiches;
            int.TryParse(numberOfSandwiches, out numOfSandwiches);
            int numOfTacos;
            int.TryParse(numberOfTacos, out numOfTacos);
            ViewBag.TotalItems = numOfSandwiches + numOfTacos;               
            decimal decTempTaco = numOfTacos * tacoPrice;
            ViewBag.TacoSubtotal = decTempTaco.ToString("C2");
            decimal decTempSandwich = numOfSandwiches * sandwichPrice;
            ViewBag.SandwichSubtotal = decTempSandwich.ToString("C2");
            ViewBag.Subtotal = decTempTaco + decTempSandwich;                
            decTemp = (decTempTaco + decTempSandwich) * tax;
            ViewBag.TaxSubtotal = decTemp.ToString("C2");
            ViewBag.GrandTotal = (decTemp + ViewBag.Subtotal).ToString("C2");
            ViewBag.Subtotal = (decTempTaco + decTempSandwich).ToString("C2");
            return View();
        }

        private Boolean ValidateCustomerCode(string customerCode)
        {
            if (String.IsNullOrEmpty(customerCode))
            {
                return false;
            }
            if (customerCode.Length < 2 || customerCode.Length > 4) //length is less than 2 - there's a problem
            {
                return false;
            }
            if (!(customerCode.All(char.IsLetter)))
            {
                return false;
            }
            else  //everything is okay, return true
            {
                return true;
            }
        }

        private void Validate(string numberOfTacos, string numberOfSandwiches)
        {
            int tempNumberOfTacos = 0;
            int tempNumberOfSandwiches = 0;
            if (numberOfTacos == null)
                numberOfTacos = "0";
            if (numberOfSandwiches == null)
                numberOfSandwiches = "0";
            try
            {
                tempNumberOfTacos = int.Parse(numberOfTacos);
            }
            catch
            {
                ModelState.AddModelError(nameof(numberOfTacos), numberOfTacos + " is not a valid integer!");
                return;
            }

            try
            {
                tempNumberOfSandwiches = int.Parse(numberOfSandwiches);
            }
            catch
            {
                ModelState.AddModelError(nameof(numberOfSandwiches), numberOfSandwiches + " is not a valid integer!");
                return;
            }
        }
    
    }
}