global using WebUI;
global using WebUI.Infrastructure.ActionResults;
global using WebUI.Infrastructure.Filters;

global using Application;
global using Application.Common.Exceptions;
global using Application.Features.MeetingFeatures.Commands.CreateMeeting;
global using Application.Features.MeetingFeatures.Queries.GetMeetingById;
global using Application.Features.MeetingFeatures.Queries.GetMeetings;
global using Application.Features.SectionFeatures.Queries.GetSectionById;
global using Application.Features.SectionFeatures.Queries.GetSections;
global using Application.Features.StudentFeatures.Commands.CreateStudent;
global using Application.Features.StudentFeatures.Commands.RateStudent;
global using Application.Features.StudentFeatures.Commands.UpdateStudent;
global using Application.Features.StudentFeatures.Queries.GetStudentById;
global using Application.Features.StudentFeatures.Queries.GetStudents;

global using Domain.Exceptions;

global using FluentValidation.AspNetCore;

global using Infrastructure;

global using MediatR;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;