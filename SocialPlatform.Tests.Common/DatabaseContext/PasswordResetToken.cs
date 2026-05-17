using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class PasswordResetToken
{
    public Guid Id { get; set; }

    public DateTime? ExpireDateTime { get; set; }

    public string? Token { get; set; }

    public Guid? UserToken { get; set; }

    public virtual User? UserTokenNavigation { get; set; }
}
