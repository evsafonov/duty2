using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Duty2.Helpers;
using Duty2.Models;

namespace Duty2.Controllers
{
    public class MailController : ApiController
    {
        [Authorize]
        [ClausAuth]
        public async Task<IHttpActionResult> Post(string group, int month, int year)
        {
            var emailSender = new EmailSender();

            var db = new DataContext();
            var users = await
                (db.Users.AsQueryable().Where(u => u.Group.Description == group)).ToListAsync();

            var duties = await
                db.Duties.AsQueryable()
                    .Include(d => d.PartOfDay.Group)
                    .Include(d => d.User.Group)
                    .Where(d => d.User.Group.Description == group && d.Date.Month == month && d.Date.Year == year)
                    .ToListAsync();

            foreach (var user in users)
            {
                var currentUserDuties = duties.Where(d => d.User.Id == user.Id).ToList();

                var dutyDates = currentUserDuties.Select(d => d.Date).Distinct().OrderByDescending(d => d.Date);
                var dutyDayParts = db.DayParts.Where(p => p.Group.Description == group).OrderBy(g => g.Sortpos);

                string htmlToEmail =
                    "<html><head><meta http - equiv = 'Content-Type' content = 'text/html; charset=utf-8'>" +
                    "<style type = 'text/css'>TABLE {background: #F7F6F3; border: 1px solid; border-collapse: collapse;}" +
                    "TD, TH {padding: 3px;}TD{text-align: center;border-bottom: 1px solid #EECBAD; }" +
                    ".weekend {background: #ffd09e; }.clock {background: #FFAB51;}</style></head>" +
                    "<body><table bordercolor='#FFAB51' id='dutyTable' class='dutyTable' borderthickness='1' border='1' align='center' padding='4px' width='90%'>";

                htmlToEmail += "<tr  align='center' class='clock'><td></td>";
                foreach (var dutyDayPart in dutyDayParts)
                {
                    htmlToEmail += "<td>";
                    htmlToEmail += dutyDayPart.Description;
                    htmlToEmail += "</td> ";
                }
                htmlToEmail += "</tr>";


                foreach (var dutyDate in dutyDates)
                {
                    htmlToEmail += "<tr  align='center'";
                    if (TransformDayOfWeek(dutyDate) == "Сб" || TransformDayOfWeek(dutyDate) == "Вс")
                    {
                        htmlToEmail += "class='weekend'";
                    }
                    htmlToEmail += ">";

                    htmlToEmail += "<td>" + ConvertDatetimeToDate(dutyDate) + "</td>";

                    foreach (var dutyDayPart in dutyDayParts)
                    {
                        htmlToEmail += "<td>";
                        var a = currentUserDuties.Where(d => d.PartOfDay.Id == dutyDayPart.Id && d.Date == dutyDate);
                        if (currentUserDuties.Any(d => d.PartOfDay.Id == dutyDayPart.Id && d.Date == dutyDate))
                        {
                            htmlToEmail += "✓";
                        }
                        htmlToEmail += "</td> ";
                    }

                    htmlToEmail += "</tr>";
                }
                htmlToEmail += "</table></body></html>";
                emailSender.SendMail("График дежурств для " + user.Name, htmlToEmail, user.Email);
            }
            
            return Ok();
        }

        private string ConvertDatetimeToDate(DateTime dateTime)
        {
            return AddZeroToNuberLessThen10(dateTime.Day) + "." + AddZeroToNuberLessThen10(dateTime.Month) + "." +
                   AddZeroToNuberLessThen10(dateTime.Year) + " " + TransformDayOfWeek(dateTime);
        }

        private string TransformDayOfWeek(DateTime dayOfWeek)
        {
            switch (dayOfWeek.DayOfWeek.ToString())
            {
                case "Sunday":
                    return "Вс";
                case "Monday":
                    return "Пн";
                case "Tuesday":
                    return "Вт";
                case "Wednesday":
                    return "Ср";
                case "Thursday":
                    return "Чт";
                case "Friday":
                    return "Пт";
                case "Saturday":
                    return "Сб";
            }

            return "Неизвестноый день";
        }

        private string AddZeroToNuberLessThen10(int number)
        {
            string result;
            if (number < 10)
            {
                result = "0" + number.ToString();
            }
            else
            {
                result = number.ToString();
            }
            return result;
        }
    }
}