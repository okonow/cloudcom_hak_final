using Domain.Common;
using Domain.Common.Exceptions;

namespace Domain.Entities
{
    public class Wallet : BaseEntity
    {

        public decimal MoneyBalance { get; private set; }

        public void AddMoney(decimal amount)
        {
            if (amount < 0)
                throw new InvalidMoneyOperationException("the amount is less than zero");

            MoneyBalance += amount;
        }

        public decimal WithdrawMoney(decimal amount)
        {
            if (amount < 0)
                throw new InvalidMoneyOperationException("the amount is less than zero");

            if (MoneyBalance < amount)
                throw new InvalidMoneyOperationException("the withdrawal amount is greater than the balance");

            MoneyBalance -= amount;
            return amount;
        }
    }
}
