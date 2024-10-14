using IOBBank.Domain.Entidades.Base;
using IOBBank.Domain.Enum;

namespace IOBBank.Domain.Entidades
{
    public class BankLaunch : Entidade
    {
        public BankLaunch()
        {
                
        }
        public BankLaunch(double value, OperationType
            operationType, Guid originBankAccountId, Guid destinationBankAccount)
        {
            Value = value;
            OperationType = operationType;
            OriginBankAccountId = originBankAccountId;
            DestinationBankAccountId = destinationBankAccount;

        }

        public double Value { get; set; }
        public OperationType OperationType { get; set; }
        public BankAccount BankAccount { get; set; }
        public Guid OriginBankAccountId { get; set; }
        public Guid DestinationBankAccountId { get; set; }
    }
}
