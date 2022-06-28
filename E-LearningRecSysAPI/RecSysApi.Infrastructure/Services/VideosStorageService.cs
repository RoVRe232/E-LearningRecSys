using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Services
{
    public class VideosStorageService : IVideosStorageService
    {
        public async Task<string> StoreVideoToPermanentStorage(VideoSourceUploadDTO source)
        {
            var clientString = "mongodb://mongodb:27017";
            var envDockerDatabase = Environment.GetEnvironmentVariable("DOCKER_DATABASE");

            if (Environment.GetEnvironmentVariable("DOCKER_DATABASE") != null && Environment.GetEnvironmentVariable("DOCKER_DATABASE") == "true")
                clientString = "mongodb://mongodb:27017";
            var client = new MongoClient(clientString);
            var database = client.GetDatabase("RecSysDb");

            var bucket = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = "videos",
                ChunkSizeBytes = 1048576 //1MB
            });

            var uploadOptions = new GridFSUploadOptions
            {
                ChunkSizeBytes = 64512,
                Metadata = new MongoDB.Bson.BsonDocument
                {
                    { "internalId", source.InternalID },
                    { "location", source.Location },
                    { "resolution", "1080P" },
                    { "copyrighted", true }
                }
            };

            var sr = new StreamReader(source.VideoContent.OpenReadStream());
            using (var memstream = new MemoryStream())
            {
                sr.BaseStream.CopyTo(memstream);
                byte[] bytes = memstream.ToArray();
                var id = await bucket.UploadFromBytesAsync(source.Location, bytes, uploadOptions);
                return id.ToString();
            }
        }

        public async Task<byte[]> GetVideoContentFromPermanentStorage(string id)
        {
            var clientString = "mongodb://mongodb:27017";
            var envDockerDatabase = Environment.GetEnvironmentVariable("DOCKER_DATABASE");

            if (Environment.GetEnvironmentVariable("DOCKER_DATABASE") != null && Environment.GetEnvironmentVariable("DOCKER_DATABASE") == "true")
                clientString = "mongodb://mongodb:27017";
            var client = new MongoClient(clientString);
            var database = client.GetDatabase("RecSysDb");
            
            var bucket = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = "videos",
                ChunkSizeBytes = 1048576 //1MB
            });

            var filter = Builders<GridFSFileInfo>.Filter.And(
                Builders<GridFSFileInfo>.Filter.Eq(x => x.Metadata["internalId"], id));

            var cursor = await bucket.FindAsync(filter);
            var fileInfos = await cursor.ToListAsync();
            foreach (GridFSFileInfo fileInfo in fileInfos)
            {
                var result = await bucket.DownloadAsBytesAsync(fileInfo.Id);
                return result;
            }
            return null;
        }
    }
}
