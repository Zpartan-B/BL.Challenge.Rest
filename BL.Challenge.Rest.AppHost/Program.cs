var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BL_Challenge_Rest>("bl-challenge-rest");

builder.Build().Run();
