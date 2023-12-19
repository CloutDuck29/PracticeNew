using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaProj.Functions
{
    public class UserSession
    {
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public string UserName { get; set; }    

        public void OpenUserSession(string username)
        {
            Authentication.session.LoginTime = DateTime.Now;
            Authentication.session.UserName = username;

            var json = JsonConvert.SerializeObject(Authentication.session);
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UserSession.json"), json);
        }

        public void CloseUserSession()
        {
            var json = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UserSession.json"));
            var userSession = JsonConvert.DeserializeObject<UserSession>(json);

            userSession.LogoutTime = DateTime.Now;

            json = JsonConvert.SerializeObject(userSession);
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UserSession.json"), json);
        }

    }
}
