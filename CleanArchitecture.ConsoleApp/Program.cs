using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.IO;

StreamerDbContext dbContext = new();
await MultipleEntitiesQuery();
//await TrackingAndNotTracking();
//await QueryLinq();
//await QueryMethods();
//await QueryFilter();
//QueryStreaming();
//await AddNewRecords();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();

async Task MultipleEntitiesQuery()
{
    //var videoWithActores = await dbContext!.Videos!.Include(q => q.Actores).FirstOrDefaultAsync(q => q.Id == 1);

    //var actor = await dbContext!.Actores!.Select(q => q.Name).ToListAsync();


    var videoWithDirector = await dbContext!.Videos!
                            .Where(q => q.Director != null)
                            .Include(q => q.Director)
                            .Select(q =>
                               new
                               {
                                   Director_Nombre_Completo = $"{q.Director.Name} {q.Director.LastName}",
                                   Movie = q.Name
                               }
                             )
                            .ToListAsync();


    foreach (var pelicula in videoWithDirector)
    {
        Console.WriteLine($"{pelicula.Movie} - {pelicula.Director_Nombre_Completo}");
    }


}


async Task AddNewDirectorWithVideo()
{
    var director = new Director
    {
        Name = "Lorenzo",
        LastName = "Basteri",
        VideoId = 1
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();
}


async Task AddNewActorWithVideo()
{
    var actor = new Actor
    {
        Name = "Brad",
        LastName = "Pitt"
    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();

}



async Task AddNewStreamerWithVideoId()
{
    var batmanForever = new Video
    {
        Name = "batman forever",
        StreamerId = 2
    };

    await dbContext.AddAsync(batmanForever);
    await dbContext.SaveChangesAsync();

}



async Task AddNewStreamerWithVideo()
{
    var pantaya = new Streamer
    {
        Name = "Pantaya"
    };

    var hungerGames = new Video
    {
        Name = "Hunger Games",
        Streamer = pantaya
    };

    await dbContext.AddAsync(hungerGames);
    await dbContext.SaveChangesAsync();

}



//async Task TrackingAndNotTracking()
//{

//    var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
//    var streamerWithNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 3);


//    streamerWithTracking.Nombre = "Netflix Super";
//    streamerWithNoTracking.Nombre = "Amazon Plus";


//    await dbContext!.SaveChangesAsync();

//}



async Task QueryLinq()
{
    Console.WriteLine($"Ingrese el servicio de streaming");
    var streamerNombre = Console.ReadLine();

    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Name, $"%{streamerNombre}%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }


}


async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;

    var firstAsync = await streamer.Where(y => y.Name.Contains("a")).FirstAsync();

    var firstOrDefaultAsync = await streamer.Where(y => y.Name.Contains("a")).FirstOrDefaultAsync();

    var firstOrDefault_v2 = await streamer.FirstOrDefaultAsync(y => y.Name.Contains("a"));



    var singleAsync = await streamer.Where(y => y.Id == 1).SingleAsync();
    var singleOrDefaultAsync = await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();


    var resultado = await streamer.FindAsync(1);

    var count = await streamer.CountAsync();
    var longAccount = await streamer.LongCountAsync();
    var min = await streamer.MinAsync();
    var max = await streamer.MaxAsync();

}

async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compania de streaming:");
    var streamingNombre = Console.ReadLine();
    var streamers = await dbContext!.Streamers!.Where(x => x.Name.Equals(streamingNombre)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} -  {streamer.Name}");
    }

    //var streamerPartialResults = await dbContext!.Streamers!.Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync();
    var streamerPartialResults = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Name, $"%{streamingNombre}%")).ToListAsync();
    foreach (var streamer in streamerPartialResults)
    {
        Console.WriteLine($"{streamer.Id} -  {streamer.Name}");
    }



}

void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }

}

async Task AddNewRecords()
{

    Streamer streamer = new()
    {
        Name = "disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();


    var movies = new List<Video>
{
    new Video{
       Name = "La Cenicienta",
       StreamerId = streamer.Id
    },
    new Video{
       Name = "1001 dalmatas",
       StreamerId = streamer.Id
    },
    new Video{
       Name = "El Jorobado de Notredame",
       StreamerId = streamer.Id
    },
    new Video{
       Name = "Star Wars",
       StreamerId = streamer.Id
    },
};


    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();

}