using AspireWorkshopDb.Data;
using AspireWorkshopDb.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AspireWorkshopDb.Api.Apis
{
    public static class SongApis
    {
        public static void MapGroupSongEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api");

            group.WithTags("Songs");

            group.MapGet("/songs", GetSongs);
            group.MapPost("/song", AddSong);
        }

        private static async Task<List<Song>> GetSongs(
           CollectionContext context) => await context.Songs.ToListAsync();

        private static async Task AddSong(
           CollectionContext context, HttpRequest request)
        {
            var song = await request.ReadFromJsonAsync<Song>();
            context.Songs.Add(song!);
            await context.SaveChangesAsync();
        }
    }
}
