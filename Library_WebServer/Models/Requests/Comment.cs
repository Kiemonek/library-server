﻿using Library_WebServer.Models.Database;
using System.Text.Json.Serialization;

namespace Library_WebServer.Models.Requests;

public class Comment
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }

    [JsonPropertyName("Grade")]
    public ushort Grade { get; set; }

    [JsonPropertyName("Contents")]
    public string Contents { get; set; } = string.Empty;

    [JsonPropertyName("PublicationId")]
    public Guid PublicationId { get; set; }

    [JsonPropertyName("UserId")]
    public Guid UserId { get; set; }

    public Comment() { }

    public Comment(LibraryComment comment)
    {
        Id = comment.Id;
        Grade = comment.Grade;
        Contents = comment.Contents;
        PublicationId = comment.LibraryPublication.Id;
        UserId = comment.LibraryUser.Id;
    }

    public Comment(Guid id, ushort grade, string contents, Guid publicationId, Guid userId)
    {
        Id = id;
        Grade = grade;
        Contents = contents;
        PublicationId = publicationId;
        UserId = userId;
    }
}