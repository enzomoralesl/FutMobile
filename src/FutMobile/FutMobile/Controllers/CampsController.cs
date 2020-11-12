using FutMobile.Context;
using FutMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FutMobile.Controllers
{
    public class CampsController : ApiController
    {
        private ContextoCampeonato db = new ContextoCampeonato();

        // GET: Camps
        public List<Campeonato> Get()
        {
            var campeonatos = db.Campeonato.ToList();
            return campeonatos;
        }
    }
}