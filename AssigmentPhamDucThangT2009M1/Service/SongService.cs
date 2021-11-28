using AssigmentPhamDucThangT2009M1.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AssigmentPhamDucThangT2009M1.Service
{
    public class SongService
    {
        private const string ApiBaseUrl = "https://music-i-like.herokuapp.com";
        private const string ApiSongPath = "/api/v1/songs";

        public async Task<List<Song>> GetLatestSongAsync()
        {
            List<Song> result = new List<Song>();
            try
            {
                HttpClient httpClient = new HttpClient();
                var requestConnection =
                    await httpClient.GetAsync(ApiBaseUrl + ApiSongPath);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject <List<Song>> (content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public async Task<Song> CreateSongAsync(Song song)
        {
            AccountService accountService = new AccountService();
            var credential = await accountService.LoadAccessTokenFromFile();
            try
            {
                var songJson = Newtonsoft.Json.JsonConvert.SerializeObject(song);
                HttpClient httpClient = new HttpClient();

                //Tạo dữ liệu thô để gửi đi.
                Debug.WriteLine(songJson);
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {credential.access_token}");
                //Đóng gói dữ liệu, dán nhãn UTF8, dán định dạng json.
                var httpContent = new StringContent(songJson, Encoding.UTF8, "application/json");

                //Thực hiện gửi dữ liệu sử dụng async, await
                var requestConnection =
                    await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/songs/mine", httpContent);
                Debug.WriteLine(requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return song;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
