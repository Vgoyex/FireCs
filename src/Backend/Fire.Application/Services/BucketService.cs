using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Fire.Infra.Configuration;
using Microsoft.Extensions.Options;

// para exibir no front
//string result = $"https://pub-61e84a980e7d4fc08c0f3f103445561e.r2.dev/{key}";

namespace Fire.Application.Services
{
    public class BucketService
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucket;
        private readonly string _accountId;

        public BucketService(IOptions<R2Settings> options)
        {
            var settings = options.Value;

            _bucket = settings.Bucket;
            _accountId = settings.AccountId;

            var creds = new BasicAWSCredentials(
                settings.AccessKey,
                settings.SecretKey
            );

            _s3 = new AmazonS3Client(
                creds,
                new AmazonS3Config
                {
                    ServiceURL = $"https://{settings.AccountId}.r2.cloudflarestorage.com",
                    ForcePathStyle = true,
                    AuthenticationRegion = "auto",
                    UseHttp = false,
                    //DisableChunkedEncoding = true
                }
            );
        }

        public async Task<string> UploadBase64(string base64)
        {
            string key = $"images/{Guid.NewGuid()}.png";

            if (base64.Contains(','))
                base64 = base64.Split(",")[1];

            byte[] bytes = Convert.FromBase64String(base64);

            using var stream = new MemoryStream(bytes);
            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = key,
                ContentType = "image/png",
                InputStream = stream,
                UseChunkEncoding = false
            };

            await _s3.PutObjectAsync(request);

            return key; // ou a URL pública
        }

        public async Task<string> UploadVideo(byte[] file)
        {
            string key = $"videos/{Guid.NewGuid()}.mp4";

            using var stream = new MemoryStream(file);
            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = key,
                ContentType = "video/mp4",
                InputStream = stream,
                UseChunkEncoding = false
            };

            await _s3.PutObjectAsync(request);
            
            return key;
        }

        public async Task<string> UploadImage(/*IFormFile ff,*/byte[] file)
        {
            string key = $"images/{Guid.NewGuid()}.png";
            var stream = new MemoryStream(file);
            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = key,
                ContentType = "image/png",
                InputStream = stream,
                UseChunkEncoding = false
            };
            //request.Headers.ContentLength = stream.Length;
            
            await _s3.PutObjectAsync(request);

            return key;
        }

        public string GetPublicUrl(string key)
        {
            return $"https://{_accountId}.r2.cloudflarestorage.com/{_bucket}/{key}";
        }

    }
}
