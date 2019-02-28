using ModalViewProduct.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModalViewProduct.Controllers
{
    public class CustomersController : Controller
    {

        private ProductSalesEntities db = new ProductSalesEntities();


        // GET: Customers
        public ActionResult Index()
        {
            db.Customers.ToList();
            return View();
        }

        public JsonResult GetCustomerList()
        {
            List<CustomerViewModel> CustList = db.Customers.Select(x => new CustomerViewModel
            {
                ID = x.ID,
                Name = x.Name,
                Address = x.Address,

            }).ToList();

            return Json(CustList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult SaveDataInDatabase(CustomerViewModel model)
        {
            bool result = false;
            try
            {
                if (model.ID > 0)
                {
                    Customer cust = db.Customers.SingleOrDefault(x=> x.ID == model.ID);
                    cust.Name = model.Name;
                    cust.Address = model.Address;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Customer cust = new Customer();
                    cust.Name = model.Name;
                    cust.Address = model.Address;
                    db.Customers.Add(cust);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerById(int Id)
        {
            Customer model = db.Customers.Where(x => x.ID == Id).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCustomer(int id)
        {
            bool result = false;
            
            Customer cust = db.Customers.SingleOrDefault(x => x.ID ==id );
            if (cust != null)
            {
               
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }


}