using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class Image
{
    public Guid Id { get; set; }

    public byte[] Content { get; set; } = null!;

    public bool HasBlockRequest { get; set; }

    public bool PublicImage { get; set; }

    public string Type { get; set; } = null!;

    public long UploadDate { get; set; }

    public string? AlbumName { get; set; }

    public Guid? AlbumUserId { get; set; }

    public virtual Album? Album { get; set; }
}
