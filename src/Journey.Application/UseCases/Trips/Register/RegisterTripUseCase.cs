using Journey.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public void Execute(RequestRegisterTripJson request)
        {
            Validate(request);
        }

        private void Validate(RequestRegisterTripJson request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Trip cannot be registered for a past date");
            }

            if (request.EndDate.Date < request.StartDate.Date)
            {
                throw new ArgumentException("Trip end date must be equal to or later than the start date");
            }
        }
    }
}
