using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Aspire.Services;

public class DigitalOceanSpacesOptions
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
}

public class SpacesService
{
    private readonly IAmazonS3 _s3;
    private readonly DigitalOceanSpacesOptions _options;

    public SpacesService(IConfiguration configuration)
    {
        _options = configuration
            .GetSection("DigitalOceanSpaces")
            .Get<DigitalOceanSpacesOptions>()
            ?? throw new InvalidOperationException(
                "DigitalOcean Spaces configuration is missing.");

        var credentials = new BasicAWSCredentials(_options.AccessKey, _options.SecretKey);
        var s3Config = new AmazonS3Config
        {
            ServiceURL = !string.IsNullOrWhiteSpace(_options.Region)
                ? $"https://{_options.Region}.digitaloceanspaces.com"
                : _options.Endpoint,
            AuthenticationRegion = !string.IsNullOrWhiteSpace(_options.Region) ? _options.Region : "sfo3",
            ForcePathStyle = false
        };

        _s3 = new AmazonS3Client(credentials, s3Config);
    }

    public SpacesService(IAmazonS3 s3, IConfiguration configuration)
    {
        _s3 = s3;

        _options = configuration
            .GetSection("DigitalOceanSpaces")
            .Get<DigitalOceanSpacesOptions>()
            ?? throw new InvalidOperationException(
                "DigitalOcean Spaces configuration is missing.");
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, string contentType, bool isPublic = false)
    {
        var request = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = contentType,
            CannedACL = isPublic ? S3CannedACL.PublicRead : S3CannedACL.Private
        };

        await _s3.PutObjectAsync(request);

        return fileName;
    }

    public string GetPresignedUrl(string key, double expiryMinutes = 60, string? downloadFileName = null)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _options.BucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Verb = HttpVerb.GET
        };

        if (!string.IsNullOrWhiteSpace(downloadFileName))
        {
            request.ResponseHeaderOverrides.ContentDisposition = $"attachment; filename=\"{downloadFileName}\"";
        }

        return _s3.GetPreSignedURL(request);
    }

    public async Task<(Stream Stream, string ContentType, string FileName)> DownloadAsync(string key)
    {
        var request = new GetObjectRequest
        {
            BucketName = _options.BucketName,
            Key = key
        };

        var response = await _s3.GetObjectAsync(request);
        return (response.ResponseStream, response.Headers.ContentType ?? "application/octet-stream", Path.GetFileName(key));
    }

    public async Task<bool> DeleteAsync(string key)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _options.BucketName,
            Key = key
        };

        var response = await _s3.DeleteObjectAsync(request);
        return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent || response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public string GetFileUrl(string key)
    {
        return $"https://{_options.BucketName}.{_options.Region}.digitaloceanspaces.com/{key}";
    }
}
