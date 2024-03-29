﻿using System.Text.Json.Serialization;

namespace Library_WebServer.Models.Comment.Request;

public class CommentRequestUpdateModel : CommentRequestBaseModel
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }

    public CommentRequestUpdateModel() { }

    public CommentRequestUpdateModel(
        Guid id,
        ushort grade,
        string contents,
        Guid publicationId,
        Guid userId)
            : base(grade, contents, publicationId, userId)
    {
        Id = id;
    }
}
