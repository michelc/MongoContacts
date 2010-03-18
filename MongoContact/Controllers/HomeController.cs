using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MongoContact.Models;

namespace MongoContact.Controllers
{
    public class HomeController : Controller
    {

        private MongoSession _session = new MongoSession();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            // Ajout de quelques données d'exemple
            var count = _session.Contacts.Count();
            if (count == 0)
            {
                _session.Add(new Contact { FirstName = "Scott",
                                            LastName = "Guthrie", 
                                            Phone = "555-444-5556", 
                                            Email = "scott@somewhere.com" });
                _session.Add(new Contact { FirstName = "Phil",
                                            LastName = "Haack", 
                                            Phone = "206-777-5555", 
                                            Email = "phil@somewhere.com" });
                _session.Add(new Contact { FirstName = "Stephen", 
                                            LastName = "Walther", 
                                            Phone = "206-555-8988", 
                                            Email = "steve@somewhere.com" });
                _session.Add(new Contact { FirstName = "Eilon", 
                                            LastName = "Lipton", 
                                            Phone = "415-777-5555", 
                                            Email = "eilon@somewhere.com" });
                _session.Add(new Contact { FirstName = "Scott", 
                                            LastName = "Hanselman", 
                                            Phone = "555-444-5555", 
                                            Email = "scottha@somewhere.com" });
                _session.Add(new Contact { FirstName = "Rob", 
                                            LastName = "Conery", 
                                            Phone = "333-899-9999", 
                                            Email = "rob@somewhere.com" });
            }

            // Renvoie la vue pour afficher la liste des contacts
            return View(_session.Contacts.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Contact contactToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _session.Add<Contact>(contactToCreate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
