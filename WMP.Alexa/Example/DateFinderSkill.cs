using System;
using WMP.Alexa;
using System.Web.Http;

namespace AlexaSkills.Controllers
{
    [Route("dateskill")]
    public class DateController : DefaultAlexaController<DateFinderSkill> { }

    public class DateFinderSkill : AlexaSkill
    {
        public override void StartSession(AlexaSession session, AlexaRequest request, AlexaResponse response)
        {
            response.SayText("If you tell me a date, I can tell you what day it was. What date would you like to know?");
        }

        public override void EndSession(AlexaSession session, AlexaRequest request, AlexaResponse response)
        {
            // No need to do anything
        }

        [Intent("GetDate")]
        public void GetDate(AlexaSession session, AlexaRequest request, AlexaResponse response)
        {
            string dateString = request.Body.Intent.Slots["Date"].Value;

            if (dateString == null || dateString == String.Empty || dateString.Split('-').Length != 3)
            {
                response.SayText("I didn't get that. Tell me a date and I'll tell you what day of the week it represents.");
                return;
            }

            string[] rawDate = dateString.Split('-');

            DateTime date = new DateTime(Convert.ToInt32(rawDate[0]), Convert.ToInt32(rawDate[1]), Convert.ToInt32(rawDate[2]));

            string interText = " is a ";
            if (date < DateTime.Today)
                interText = " was on a ";
            else if (date > DateTime.Today)
                interText = " will be on a ";


            response.SayText(dateString + interText + date.DayOfWeek);
            response.EndSession();
        }
    }
}