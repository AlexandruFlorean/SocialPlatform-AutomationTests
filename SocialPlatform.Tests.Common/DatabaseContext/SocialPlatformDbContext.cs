using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialPlatform.Tests.Common.DatabaseContext;

public partial class SocialPlatformDbContext : DbContext
{
    public SocialPlatformDbContext(DbContextOptions<SocialPlatformDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => new { e.Name, e.UserId }).HasName("PK__album__997ACC6A7FC71CEF");

            entity.ToTable("album");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Albums)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKf8jilwyp6s7bb1ruw11u94808");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detail__3213E83F2388A16D");

            entity.ToTable("detail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Hobbies)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hobbies");
            entity.Property(e => e.ProfilePictureExtension)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_picture_extension");
            entity.Property(e => e.PublicDetails).HasColumnName("public_details");
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__friend_r__3213E83F8634CBB9");

            entity.ToTable("friend_request");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Approved).HasColumnName("approved");
            entity.Property(e => e.FromUser).HasColumnName("from_user");
            entity.Property(e => e.ToUser).HasColumnName("to_user");

            entity.HasOne(d => d.FromUserNavigation).WithMany(p => p.FriendRequestFromUserNavigations)
                .HasForeignKey(d => d.FromUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKn24g8gne2qwl6fqm5ipr7c9yn");

            entity.HasOne(d => d.ToUserNavigation).WithMany(p => p.FriendRequestToUserNavigations)
                .HasForeignKey(d => d.ToUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKl2re0vi4yfd63xmhs8btluupq");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__image__3213E83F889E44EC");

            entity.ToTable("image");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AlbumName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("album_name");
            entity.Property(e => e.AlbumUserId).HasColumnName("album_user_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.HasBlockRequest).HasColumnName("has_block_request");
            entity.Property(e => e.PublicImage).HasColumnName("public_image");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.UploadDate).HasColumnName("upload_date");

            entity.HasOne(d => d.Album).WithMany(p => p.Images)
                .HasForeignKey(d => new { d.AlbumName, d.AlbumUserId })
                .HasConstraintName("FKdllr75s0d7ik04idfplpw32v1");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message__3213E83FC5B94BBB");

            entity.ToTable("message");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Receiver).HasColumnName("receiver");
            entity.Property(e => e.Sender).HasColumnName("sender");

            entity.HasOne(d => d.ReceiverNavigation).WithMany(p => p.MessageReceiverNavigations)
                .HasForeignKey(d => d.Receiver)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKs6lhguxvl0ds1dys9mjdjx3rk");

            entity.HasOne(d => d.SenderNavigation).WithMany(p => p.MessageSenderNavigations)
                .HasForeignKey(d => d.Sender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKasb3qf0ctsk6rbupw10j9nm6c");
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__password__3213E83FA3464F1B");

            entity.ToTable("password_reset_token");

            entity.HasIndex(e => e.UserToken, "UK_nhqx43h037roeng4na5ee0ai2")
                .IsUnique()
                .HasFilter("([user_token] IS NOT NULL)");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ExpireDateTime)
                .HasPrecision(6)
                .HasColumnName("expire_date_time");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.UserToken).HasColumnName("user_token");

            entity.HasOne(d => d.UserTokenNavigation).WithOne(p => p.PasswordResetToken)
                .HasForeignKey<PasswordResetToken>(d => d.UserToken)
                .HasConstraintName("FKa3dbwa5opsc13qhkke9qu3h1q");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F4AADDB6F");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UK6dotkott2kjsp8vw4d0m25fb7").IsUnique();

            entity.HasIndex(e => e.Detail, "UK_q1peynr6vw2chvy2pc0ympyjb")
                .IsUnique()
                .HasFilter("([detail] IS NOT NULL)");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Detail).HasColumnName("detail");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PublicContent).HasColumnName("public_content");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.DetailNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.Detail)
                .HasConstraintName("FKgn86kgvpeldq0mre7u739y83l");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
