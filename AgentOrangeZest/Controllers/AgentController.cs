using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgentOrange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentOrangeZest.Controllers
{
    public class AgentController : Controller
    {
        public AgentOrange.Models.Data.JsonDataContext AgentContext = new AgentOrange.Models.Data.JsonDataContext();
        public IList<Agent> gobjContext;
        // GET: Agent
        public ActionResult Index()
        {
            gobjContext = AgentContext.GetAgentData();

            //ViewData.Model = agent.gobjAgents;
            ViewData.Model = gobjContext;
            return View();
        }

        // GET: Agent/Details/5
        public ActionResult Details(int id)
        {
           ViewData.Model = gobjContext;
            return View();
        }

        // GET: Agent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Agent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Agent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agent/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}