using MiniToken.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using System.Configuration;

namespace MiniToken.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult SubmitToken(bool showToolTip = false)
        {
            var model = new SubmitTokenModel
            {
                ShowTooltip = showToolTip
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitToken(SubmitTokenModel model) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var connectionString = ConfigurationManager.ConnectionStrings["SXSAdmin"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new SqlCommand("[dbo].[MiniTokenCorrection]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@TokenSN", model.Token);
                    command.Parameters.AddWithValue("@ClockTime", model.ClockTime);

                    command.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(SubmitToken), new { showToolTip = true });
            }
            catch (System.Exception)
            {

                return View(model);
            }

        }
    }
}