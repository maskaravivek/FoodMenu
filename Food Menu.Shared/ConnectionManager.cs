using Food_Menu.Models;
using Food_Menu.Models.Response;
using Food_Menu.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu
{
    public class ConnectionManager
    {
        public async static Task<ResponseData> SendRequestPacket<T>(string method, object payload)
        {
            Uri theUri = new Uri($"{Constants.EndPoint}{method}");

            //Create an Http client and set the headers we want 
            HttpClient aClient = new HttpClient();
            aClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            aClient.DefaultRequestHeaders.Host = theUri.Host;

            //Create a Json Serializer for our type 
            DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(T));

            // use the serializer to write the object to a MemoryStream 
            MemoryStream ms = new MemoryStream();
            jsonSer.WriteObject(ms, payload);
            ms.Position = 0;

            //use a Stream reader to construct the StringContent (Json) 
            StreamReader sr = new StreamReader(ms);
            // Note if the JSON is simple enough you could ignore the 5 lines above that do the serialization and construct it yourself 
            // then pass it as the first argument to the StringContent constructor 
            StringContent theContent = new StringContent(sr.ReadToEnd(), Encoding.UTF8, "application/json");

            //Post the data 
            HttpResponseMessage aResponse = await aClient.PostAsync(theUri, theContent);

            if (aResponse.IsSuccessStatusCode)
            {
                string content = await aResponse.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(content);
                return JsonConvert.DeserializeObject<ResponseData>(content);
            }
            else
            {
                // show the response status code 
                String failureMsg = "HTTP Status: " + aResponse.StatusCode.ToString() + " - Reason: " + aResponse.ReasonPhrase;
                //return new ResponseData(Constants.ErrorString);
                return null;
            }
        }
    }
}
