using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataAccessLibrary
{
    public class DataAccessLayer
    {
        private ApplicationSettings ApplicationSettings;

        public static DataAccessLayer Instance { get; set; } = new DataAccessLayer();

        private DataAccessLayer() {

            SqliteDataBase.InitializeDatabase();
        }

        public void InitializeApplicationSettings(ApplicationSettings settings)
        {
            var dataList = SqliteDataBase.GetData(settings.Id);
            if (dataList.Count != 0) return;

            ApplicationSettings = settings;
            string output = JsonConvert.SerializeObject(settings);
            SqliteDataBase.AddData(output, settings.Id);
        }

        
        public void UpdateSettings(ApplicationSettings settings)
        {
            string output = JsonConvert.SerializeObject(settings);
            SqliteDataBase.UpdateData(output, ApplicationSettings.Id);
        }

        public ApplicationSettings GetAppSettings(ApplicationSettings settings)
        {
            var dataList = SqliteDataBase.GetData(settings.Id);
            var data = dataList.FirstOrDefault();
            var ApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(data);

            return ApplicationSettings;
        }
    }
}
