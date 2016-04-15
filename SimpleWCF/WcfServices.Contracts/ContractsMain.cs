using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WcfServices.Contracts
{
    public class ContractsMain
    {
    }
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double x, double y);
    }
}
