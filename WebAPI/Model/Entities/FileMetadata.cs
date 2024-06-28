using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Model.Entities;

[Table("FilesMetadata")]
public class FileMetadata : Entity
{   
    /// <summary>
    /// File name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// File Id in content storage service. May be a path in a storage volume or another kind of identifier.
    /// </summary>
    [Required]
    public string FileId { get; set; }

    /// <summary>
    /// Type of file. It is the content type.
    /// </summary>
    [Required]
    public string ContentType { get; set; }

    /// <summary>
    /// Size in bytes
    /// </summary>
    [Required]
    public long Size { get; set; }

}