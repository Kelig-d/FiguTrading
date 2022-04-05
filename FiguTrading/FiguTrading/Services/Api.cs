using FiguTrading.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FiguTrading.Services
{
    class Api
    {
        /// <summary>
        /// Cette methode est générique
        /// Cette méthode permet de recuperer la liste de toutes les occurences de la table.
        /// 
        /// </summary>
        /// <typeparam name="T">la classe concernée</typeparam>
        /// <param name="paramUrl">l'adresse de l'API</param>
        /// <param name="param">la collection de classe concernee</param>
        /// public async void GetListe()
        ///{
        ///MaListeClients = await _apiServices.GetAllAsync<Client>("api/clients", Client.CollClasse);
        ///}
        /// <returns>la liste des occurences</returns>
        public async Task<ObservableCollection<T>> GetAsync<T>(string paramUrl, Dictionary<string, object> param = null)
        {

            try
            {
                string jsonString = @"{}";
                if (param != null)
                {
                    jsonString = @"{";
                    foreach (KeyValuePair<string, object> entry in param)
                    {
                        jsonString += "'" + entry.Key + "':'" + entry.Value + "',";
                    }
                    jsonString = jsonString.Remove(jsonString.Length - 1);
                    jsonString += "}";
                }
                var getResult = JObject.Parse(jsonString);
                var clientHttp = new HttpClient();
                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8, "application/json");
                var response = await clientHttp.PostAsync(Constantes.ApiAddress + paramUrl, jsonContent);
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public async Task<T> GetOneAsync<T>(string paramUrl, Dictionary<string,object> param = null)
        {
            try
            {
                string jsonString = @"{}";
                if (param != null)
                {
                    jsonString = @"{";
                    foreach (KeyValuePair<string, object> entry in param)
                    {
                        jsonString += "'" + entry.Key + "':'" + entry.Value + "',";
                    }
                    jsonString = jsonString.Remove(jsonString.Length - 1);
                    jsonString += "}";
                }
                var getResult = JObject.Parse(jsonString);
                var clientHttp = new HttpClient();
                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8, "application/json");

                var response = await clientHttp.PostAsync(Constantes.ApiAddress + paramUrl, jsonContent);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return default;
            }
        }

        public async Task<int> PostAsync<T>(string paramUrl, T param)
        {

            var jsonstring = JsonConvert.SerializeObject(param);
            try
            {
                var client = new HttpClient();
                var jsonContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Constantes.ApiAddress + paramUrl, jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = int.TryParse(content, out int nID) ? nID : 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return -1;
            }
        }

        public async Task<int> PostAsyncEncherir(string paramUrl, Encherir encherir)
        {
            Dictionary<string, object> param = new Dictionary<string, object>() { { "IdEnchere", encherir.LaEnchere.Id },{ "IdUser", encherir.LeUser.Id },{ "PrixEnchere", encherir.PrixEnchere } };
            string jsonString = @"{";
            foreach (KeyValuePair<string, object> entry in param)
            {
                jsonString += "'" + entry.Key + "':'" + entry.Value + "',";
            }
            jsonString = jsonString.Remove(jsonString.Length - 1);
            jsonString += "}";
            var getResult = JObject.Parse(jsonString);
            try
            {
                var client = new HttpClient();
                var jsonContent = new StringContent(getResult.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Constantes.ApiAddress + paramUrl, jsonContent);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = int.TryParse(content, out int nID) ? nID : 0;
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return -1;
            }
        }
    }
}