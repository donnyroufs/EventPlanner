using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Infrastructure;
using EventPlanner.WebAPI.Presenters;
using EventPlanner.WebAPI.ProblemDetails;
using EventPlanner.WebAPI.Responses;
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

builder.Services.AddScoped<IGetOccasionsPresenter<OccasionsResponse>, GetOccasionsPresenter>();
builder.Services.AddScoped<GetOccasionsUseCase<OccasionsResponse>>();

builder.Services.AddScoped<ICreateOccasionPresenter<OccasionResponse>, CreateOccasionPresenter>();
builder.Services.AddScoped<CreateOccasionUseCase<OccasionResponse>>();

builder.Services.AddScoped<IInviteUserPresenter<InvitationResponse>, InviteUserPresenter>();
builder.Services.AddScoped<InviteUserUseCase<InvitationResponse>>();

builder.Services.AddScoped<IReplyToInvitationPresenter<InvitationResponse>, ReplyToInvitationPresenter>();
builder.Services.AddScoped<ReplyToInvitationUseCase<InvitationResponse>>();

builder.Services.AddScoped<IGetOccasionPresenter<OccasionWithInvitationsResponse>, GetOccasionPresenter>();
builder.Services.AddScoped<GetOccasionUseCase<OccasionWithInvitationsResponse>>();

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