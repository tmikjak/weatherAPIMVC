using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherAPIMVC.Models;

namespace WeatherAPIMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult About(weatherModel model) 
        {
            //need to re-add the api key to end of built url/ keep key private 
            var url = "http://api.openweathermap.org/data/2.5/forecast?q=" + model.city + "&units=imperial&appid=7496b4fb8b4ec08eaa5e1ca1f08ec264";
            SqlConnection con = new SqlConnection("");

            var client = new WebClient();
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();

            // data request and serialization
            var jsonContent = JsonConvert.DeserializeObject<dynamic>(content);
            string temp = jsonContent.list[1].main.temp;
            string feelsLikeTemp = jsonContent.list[1].main.feels_like;
            string tempMin = jsonContent.list[1].main.temp_min;
            string tempMax = jsonContent.list[1].main.temp_max;
            string humidity = jsonContent.list[1].main.humidity;
            string time = jsonContent.list[1].dt_txt;

            model.temperature = temp;
            model.maxTemp = tempMax;
            model.minTemp = tempMin;
            model.humidity = humidity;
            model.feelsLikeTemp = feelsLikeTemp;
            model.time = time;
            

            // result string email
            //string _body = tempLabel.Text + feelsLikeLabel.Text + tempMinLabel.Text + tempMaxLabel.Text + humidityLabel.Text;
            //SendMail(toEmail.Text,"weather forecast",_body);

            //insert into database
            //con.Open();
            ////SqlCommand insert = new SqlCommand("insert into weatherReports values (@tempVar)", con)
            //var tempDB = model.temperature;
            //SqlCommand insert = new SqlCommand();
            //insert.CommandText = "INSERT INTO weatherReports (temperature) VALUES (@tempVar)";
            //insert.Parameters.Add(new SqlParameter("@tempVar", tempDB));
            //insert.Connection = con;
            //insert.ExecuteNonQuery();

            return View("About",model);
        }



        public string SendMail(string _toEmail,
        string _subject, string _body)
        {
            //Sendgrid
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("");
            var subject = _subject;
            var toEmail = _toEmail;
            var to = new EmailAddress(toEmail);
            var plainTextContent = _body;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);

            return "";
        }



    }
}
