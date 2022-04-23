global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;

global using Application.Common.Behaviours;
global using Application.Common.Exceptions;
global using Application.Common.Handler;
global using Application.Common.Mappings;
global using Application.Features.MeetingFeatures.Queries.GetMeetingsByStudentId;
global using Application.Features.SectionFeatures.Queries.GetSectionById;

global using AutoMapper;

global using Domain.AggregatesModel.MeetingAggregate;
global using Domain.AggregatesModel.SectionAggregate;
global using Domain.AggregatesModel.StudentAggregate;

global using FluentValidation;
global using FluentValidation.Results;

global using MediatR;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using Polly;
global using Polly.Extensions.Http;