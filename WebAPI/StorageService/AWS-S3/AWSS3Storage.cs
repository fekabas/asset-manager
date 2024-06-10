
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SecurityToken;

namespace WebAPI.StorageService.AWSS3;
public class AWSS3Storage : IAWSS3Storage
{
    private IAmazonS3 client;
    private string BucketName;
    private string VolumeStoragePath;

    public AWSS3Storage()
    {
        this.BucketName = "fkabas-asset-manager";
        this.VolumeStoragePath = Path.Combine("./", "VolumeStorage", "Temp");
        string clientId = "";
        string clientSecret = "";
        this.client = new AmazonS3Client(clientId, clientSecret);
    }

    public async Task<byte[]?> Download(FileDTO id)
    {
        var request = new GetObjectRequest(){
            BucketName = this.BucketName,
            Key = id.FilePath
        };
        using GetObjectResponse result = await client.GetObjectAsync(request);
        var tempFile = Path.Combine(this.VolumeStoragePath, request.Key);
        await result.WriteResponseStreamToFileAsync(tempFile, true, CancellationToken.None);
        byte[] fileBytes = await File.ReadAllBytesAsync(tempFile);
        File.Delete(tempFile);
        return fileBytes;

    }

    public async Task<FileDTO> Upload(IFormFile file)
    {
        string filePath = Path.Combine(this.VolumeStoragePath, file.FileName);
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        
        PutObjectRequest request = new PutObjectRequest(){
            BucketName = this.BucketName,
            Key = file.FileName,
            FilePath = filePath
        };

        var response = await client.PutObjectAsync(request);
        File.Delete(filePath);

        return new FileDTO(file.FileName);
    }
}