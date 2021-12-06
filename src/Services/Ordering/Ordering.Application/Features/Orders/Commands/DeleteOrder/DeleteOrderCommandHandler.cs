using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler:IRequestHandler<DeleteOrderCommand>
    {
        readonly IOrderRepository _orderRepository;
        readonly ILogger<DeleteOrderCommandHandler> _logger;
        readonly IMapper _mapper;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                _logger.LogError($"Order with id {request.Id} doesn't exists to delete");
                throw new NotFoundException(nameof(Order), request.Id);
            }
            await _orderRepository.DeleteAsync(order);
            _logger.LogInformation($"Order with id {request.Id} deleted successfully");

            return Unit.Value;
        }
    }
}
