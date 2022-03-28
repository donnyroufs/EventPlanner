using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Infrastructure;
using EventPlanner.WebAPI.Presenters;
using EventPlanner.WebAPI.ProblemDetails;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(opts => opts.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails(opts =>
{
    opts.IncludeExceptionDetails = (ctx, ex) => false;

    opts.Map<ValidationException>(exception => new ValidationProblemDetails(exception.Message));
    opts.Map<CannotCastTheSameVoteException>(exception =>
        new DomainProblemDetails(exception.Message));
    opts.Map<OccasionRequiresAtleastOneDayException>(exception =>
        new DomainProblemDetails(exception.Message));
    opts.Map<InvitationDoesNotExistException>(exception =>
        new UnknownEntityProblemDetails(exception.Message));
    opts.Map<InvitationDoesNotExistException>(exception =>
        new UnknownEntityProblemDetails(exception.Message));
    opts.Map<OccasionDoesNotExistException>(exception =>
        new UnknownEntityProblemDetails(exception.Message));
});

builder.Services.AddInfrastructure();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IGetOccasionsPresenter, GetOccasionsPresenter>();
builder.Services.AddScoped<GetOccasionsUseCase>();

builder.Services.AddScoped<ICreateOccasionPresenter, CreateOccasionPresenter>();
builder.Services.AddScoped<CreateOccasionUseCase>();

builder.Services.AddScoped<IInviteUserPresenter, InviteUserPresenter>();
builder.Services.AddScoped<InviteUserUseCase>();

builder.Services.AddScoped<IReplyToInvitationPresenter, ReplyToInvitationPresenter>();
builder.Services.AddScoped<ReplyToInvitationUseCase>();

builder.Services.AddScoped<IGetOccasionPresenter, GetOccasionPresenter>();
builder.Services.AddScoped<GetOccasionUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseProblemDetails();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}