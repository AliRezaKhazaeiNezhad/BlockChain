
using KHN.Cons.Entities;


Contract contract = new Contract(0);



Account aliAccount = new Account(Guid.NewGuid().ToString());
Account minaAccount = new Account(Guid.NewGuid().ToString());
Account rezaAccount = new Account(Guid.NewGuid().ToString());



Transaction transaction1 = new Transaction(500 , aliAccount.Address, minaAccount.Address);
contract.AddTransactionAndMineBlock(transaction1);

Transaction transaction2 = new Transaction(500, aliAccount.Address, rezaAccount.Address);
contract.AddTransactionAndMineBlock(transaction2);

Transaction transaction3 = new Transaction(150, minaAccount.Address, rezaAccount.Address);
contract.AddTransactionAndMineBlock(transaction3);



int aliBalance = contract.GetAccountBalance(aliAccount.Address);
int minaBalance = contract.GetAccountBalance(minaAccount.Address);
int rezaBalance = contract.GetAccountBalance(rezaAccount.Address);



Console.WriteLine(contract);
Console.WriteLine($"Ali : {aliBalance}");
Console.WriteLine($"Mina : {minaBalance}");
Console.WriteLine($"Reza : {rezaBalance}");