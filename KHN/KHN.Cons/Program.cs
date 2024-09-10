
using KHN.Cons.Entities;


Contract contract = new Contract(0);



Account aliAccount = new Account(Guid.NewGuid().ToString());
Account minaAccount = new Account(Guid.NewGuid().ToString());
Account rezaAccount = new Account(Guid.NewGuid().ToString());



Transaction transaction1 = new Transaction(TransactionType.Charging, 500, null , aliAccount.Address);
contract.AddTransactionAndMineBlock(transaction1);

Transaction transaction2 = new Transaction(TransactionType.Charging, 100, null, minaAccount.Address);
contract.AddTransactionAndMineBlock(transaction2);

Transaction transaction3 = new Transaction(TransactionType.Transfering, 150, aliAccount.Address, rezaAccount.Address);
contract.AddTransactionAndMineBlock(transaction3);

contract.Mine();

int aliBalance = contract.GetAccountBalance(aliAccount.Address);
int minaBalance = contract.GetAccountBalance(minaAccount.Address);
int rezaBalance = contract.GetAccountBalance(rezaAccount.Address);



Console.WriteLine(contract);
Console.WriteLine($"Ali : {aliBalance}");
Console.WriteLine($"Mina : {minaBalance}");
Console.WriteLine($"Reza : {rezaBalance}");