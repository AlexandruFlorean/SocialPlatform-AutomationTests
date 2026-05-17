using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class Detail
{
    public Guid Id { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public string? Hobbies { get; set; }

    public byte[]? Content { get; set; }

    public string? ProfilePictureExtension { get; set; }

    public bool? PublicDetails { get; set; }

    public virtual User? User { get; set; }
}
