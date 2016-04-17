using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apteka.Models;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;

namespace Apteka.Helpers
{
    public class WeatherHelper {
        private static CityWeather cityWeather;
        private static DateTime lastUpdated = DateTime.UtcNow.AddMinutes(21);

        public CityWeather CityWeather {
            get {
                if(lastUpdated.AddMinutes(20) > DateTime.UtcNow) {
                    try {
                        cityWeather = readWeather();
                        lastUpdated = DateTime.UtcNow;
                    } catch {
                        System.Diagnostics.Debug.WriteLine("Failed to get weather data.");
                        //Let's hope next time will succeed :)
                    }
                }
                return cityWeather;
            }
            private set { cityWeather = value; }
        }

        private static CityWeather readWeather() {
            using (WebResponse wr = WebRequest.Create("http://api.openweathermap.org/data/2.5/forecast/daily?id=3081368&mode=json&units=metric&cnt=14&appid=e1dde2b10610d5e45a5da1bbab616dff").GetResponse()) {
                using (StreamReader sr = new StreamReader(wr.GetResponseStream())) {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Deserialize<CityWeather>(sr.ReadToEnd()); 
                }
            }
        }

        public IEnumerable<double> Temperatures {
            get {
                return (
                    from day in cityWeather.list
                    select day.temp.day
                );
            }
        }

        public IEnumerable<double> Rain {
            get {
                return (
                    from day in cityWeather.list
                    select (day.rain ?? 0)
                );
            }
        }

        public IEnumerable<double> Pressure {
            get {
                return (
                    from day in cityWeather.list
                    select day.pressure
                );
            }
        }
    }
}