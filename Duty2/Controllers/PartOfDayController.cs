﻿using System.Linq;
using System.Web.Http;
using Duty2.DTOs;
using Duty2.Models;

namespace Duty2.Controllers
{
    public class PartOfDayController : ApiController
    {
        public Resp Get(string group)
        {
            var db = new DataContext();

            return new Resp()
            {
                Id = "PartOfDays",
                Responce = db.DayParts.AsQueryable().Where(d => d.Group.Description == group)
            };
        }
    }
}