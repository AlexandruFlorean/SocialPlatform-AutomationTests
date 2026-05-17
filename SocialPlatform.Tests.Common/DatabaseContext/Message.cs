using System;
using System.Collections.Generic;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class Message
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public long Date { get; set; }

    public Guid Receiver { get; set; }

    public Guid Sender { get; set; }

    public virtual User ReceiverNavigation { get; set; } = null!;

    public virtual User SenderNavigation { get; set; } = null!;
}
