using System.Text.RegularExpressions;
using Fire.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace Fire.Application.Services
{
    public class ContentService
    {
        private readonly BucketService _bucketService;

        public ContentService(BucketService bucketService)
        {
            _bucketService = bucketService;
        }

        public static int IsVideo(ContentsEntity file)
        {
            int result = 0;
            if (file is not null)
            {
                if (file.file_name is not null && file.file is not null)
                {
                    string regex = ".*\\.(mp4|mov)$";
                    bool isVideo = Regex.IsMatch(file.file_name, regex, RegexOptions.IgnoreCase);
                    if(isVideo)
                        result = 1;
                    else
                        result = 2;
                }
            }
            return result;
        }

    
        public async Task<List<string>> UploadBucket(List<IFormFile> files)
        {
            List<string> result = new List<string>();
            if (files is not null)
            {
                //Consertar o adapt
                List<ContentsEntity> listFiles = files.Adapt<List<ContentsEntity>>();
                foreach (var file in files)
                {
                    //var file = x.Adapt<ContentsEntity>();
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    byte[] dados = ms.ToArray();
                    var entity = new ContentsEntity
                    {
                        id = Guid.NewGuid(),
                        file_name = file.FileName,
                        type = file.ContentType,
                        size = (int)file.Length,
                        file = dados,        // bytes do arquivo
                        base64 = Convert.ToBase64String(ms.ToArray())
                    };
                    var isVideo = IsVideo(entity);
                    //ver isso dps
                    //if (!string.IsNullOrEmpty(x.base64))
                    //{
                    //    var rr = await _bucketService.UploadBase64(x.base64);
                    //    result.Add(rr);
                    //    continue;
                    //}
                    if (isVideo == 0)
                    {
                        continue;
                    }
                    else if (isVideo == 1)
                    {
                        var rr = await _bucketService.UploadVideo(entity.file); // É vídeo
                        result.Add(rr);
                    }
                    else
                    {
                        var rr = await _bucketService.UploadImage(entity.file);
                        result.Add(rr);
                    }
                }
            }
            return result;
        }

        
    }



}

