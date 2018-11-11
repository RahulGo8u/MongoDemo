using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDemo.App_Start;
using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoDemo.Controllers
{
    public class PersonInfoController : Controller
    {
        MongoContext _dbContext;
        public PersonInfoController()
        {
            _dbContext = new MongoContext();
        }
        // GET: PersonInfo
        public ActionResult Index()
        {
            var personDetails = _dbContext._database.GetCollection<PersonModel>("PersonModel").FindAll().ToList();

            return View(personDetails);
        }

        // GET: PersonInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonInfo/Create
        [HttpPost]
        public ActionResult Create(PersonModel personmodel)
        {

            //Gets a MongoCollection instance representing a collection on this database
            var document = _dbContext._database.GetCollection<BsonDocument>("PersonModel");                       
            if (ModelState.IsValid)
            {
                // Added Filter to Check Duplicate records on Bases of Personname and Color (PersonColor)   
                var query = Query.And(Query.EQ("FullName", personmodel.FullName), Query.EQ("MobPhone", personmodel.MobPhone), Query.EQ("HomeEmail", personmodel.HomeEmail));

                // Will Return Count if same document exists else will Return 0 
                var count = document.FindAs<PersonModel>(query).Count();

                //if it is 0 then only we are going to insert document
                if (count == 0)
                {
                    var result = document.Insert(personmodel);
                }
                else
                {
                    TempData["Message"] = "Duplicate Person Details with Phone and Email ID";
                    return View("Create", personmodel);
                }
                return RedirectToAction("Index");
            }            
            return View();
        }

        // GET: PersonInfo/Details/5
        public ActionResult Details(string id)
        {
            var personId = Query<PersonModel>.EQ(p => p.Id, new ObjectId(id));
            var personDetail = _dbContext._database.GetCollection<PersonModel>("PersonModel").FindOne(personId);
            return View(personDetail);
        }

        // GET: PersonInfo/Edit/5
        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<PersonModel>("PersonModel");
           
            var personDetailscount = document.FindAs<PersonModel>(Query.EQ("_id", new ObjectId(id))).Count();

            if (personDetailscount > 0)
            {
                var personObjectid = Query<PersonModel>.EQ(p => p.Id, new ObjectId(id));

                var personDetail = _dbContext._database.GetCollection<PersonModel>("PersonModel").FindOne(personObjectid);

                return View(personDetail);
            }
            return RedirectToAction("Index");                        
        }
        // POST: PersonInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, [Bind(Exclude = "Id")] PersonModel personmodel)
        {
            try
            {

                personmodel.Id = ObjectId.Parse(id);                
                //Mongo Query
                var PersonObjectId = Query<PersonModel>.EQ(p => p.Id, new ObjectId(id));
                if (ModelState.IsValid)
                {
                    // Document Collections
                    var collection = _dbContext._database.GetCollection<PersonModel>("PersonModel");
                    // Document Update which need Id and Data to Update
                    var result = collection.Update(PersonObjectId, Update.Replace(personmodel), UpdateFlags.None);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonInfo/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            //Mongo Query
            var personObjectid = Query<PersonModel>.EQ(p => p.Id, new ObjectId(id));
            // Getting Detials of person by passing ObjectId
            var personDetails = _dbContext._database.GetCollection<PersonModel>("PersonModel").FindOne(personObjectid);

            return View(personDetails);
        }

        // POST: PersonInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, PersonModel PersonModel)
        {
            try
            {   
                //Mongo Query
                var personObjectid = Query<PersonModel>.EQ(p => p.Id, new ObjectId(id));
                // Document Collections
                var collection = _dbContext._database.GetCollection<PersonModel>("PersonModel");
                // Document Delete which need ObjectId to Delete Data 
                var result = collection.Remove(personObjectid, RemoveFlags.Single);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
