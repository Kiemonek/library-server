﻿namespace Library_WebServer.Models.Login.Request;

public class LoginRequestBaseModel
{
    public Guid Id { get; set; }
    public string Password { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public LoginRequestBaseModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public LoginRequestBaseModel(
        Guid loginId,
        string loginPassword)
    {
        Id = loginId;
        Password = loginPassword;
    }
}
