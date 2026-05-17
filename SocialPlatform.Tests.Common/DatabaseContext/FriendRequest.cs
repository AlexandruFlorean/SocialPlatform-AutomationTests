using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class FriendRequest
{
    public Guid Id { get; set; }

    public bool Approved { get; set; }

    public Guid FromUser { get; set; }

    public Guid ToUser { get; set; }

    public virtual User FromUserNavigation { get; set; } = null!;

    public virtual User ToUserNavigation { get; set; } = null!;
}
