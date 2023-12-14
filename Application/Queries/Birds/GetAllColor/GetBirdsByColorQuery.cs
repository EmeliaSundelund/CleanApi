using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Birds.GetAllColor
{
    public class GetBirdsByColorQuery : IRequest<List<Bird>>
    {
        public GetBirdsByColorQuery(string color)
        {
            Color = color;
        }

        public string Color { get; }
    }
}
