﻿using System.Text.Json.Serialization;

namespace Library_WebServer.Models.Author.Request;

public class AuthorRequestUpdateModel : AuthorRequestBaseModel
{
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }

    public AuthorRequestUpdateModel() { }

    public AuthorRequestUpdateModel(
        Guid id,
        string firstName,
        string lastName)
            : base(firstName, lastName)
    {
        Id = id;
    }
}
