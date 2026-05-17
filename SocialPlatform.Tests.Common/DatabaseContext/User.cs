using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class User
{
    public Guid Id { get; set; }

    public bool Active { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool PublicContent { get; set; }

    public short Role { get; set; }

    public Guid? Detail { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual Detail? DetailNavigation { get; set; }

    public virtual ICollection<FriendRequest> FriendRequestFromUserNavigations { get; set; } = new List<FriendRequest>();

    public virtual ICollection<FriendRequest> FriendRequestToUserNavigations { get; set; } = new List<FriendRequest>();

    public virtual ICollection<Message> MessageReceiverNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenderNavigations { get; set; } = new List<Message>();

    public virtual PasswordResetToken? PasswordResetToken { get; set; }
}
