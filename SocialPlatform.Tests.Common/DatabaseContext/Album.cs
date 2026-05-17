using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class Album
{
    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual User User { get; set; } = null!;
}
