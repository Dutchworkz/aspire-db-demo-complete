using AspireWorkshopDb.Data.Models;

namespace AspireWorkshopDb.Web.Clients
{
    public class SongsClient(HttpClient client) : ISongsClient
    {
        public async Task<List<Song>> GetSongsAsync() =>
           await client.GetFromJsonAsync<List<Song>>("/api/songs");

        public async Task AddSongAsync(Song song) =>
            await client.PostAsJsonAsync("/api/song", song);
    }

    public interface ISongsClient
    {
        Task<List<Song>> GetSongsAsync();

        Task AddSongAsync(Song song);
    }
}
