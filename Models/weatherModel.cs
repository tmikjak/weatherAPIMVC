using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAPIMVC.Models
{
    public class weatherModel
    {
        public string temperature { set; get; }
        public string feelsLikeTemp { set; get; }
        public string city { set; get; }
        public string humidity { set; get; }
       
        public string maxTemp { set; get;}

        public string minTemp { set; get; }

        public string time { set; get; }

    }
}