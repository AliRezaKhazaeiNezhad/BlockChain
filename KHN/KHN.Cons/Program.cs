
using KHN.Cons.Entities;


var saraAhmadi = new Account("1");
var alirezaAlavi = new Account("2");
var KhazaeiNezhad = new Account("3");


var contract = new Contract(2, 6.25, 0.1);


Transaction transaction;

transaction = new Transaction(TransactionType.Charging, 0.11, 100, null, saraAhmadi.Address);
contract.AddTransaction(transaction);


transaction = new Transaction(TransactionType.Charging, 0.12, 200, null, alirezaAlavi.Address);
contract.AddTransaction(transaction);

transaction = new Transaction(TransactionType.Charging, 0.13, 300, null, KhazaeiNezhad.Address);
contract.AddTransaction(transaction);




//Console.WriteLine(contract);

//double khazaeiAccountBalance = contract.GetAccountBalance(KhazaeiNezhad.Address);

//Console.WriteLine($"Khazaei Nezhad Account Balance : {khazaeiAccountBalance}");





contract.Mine(KhazaeiNezhad.Address);

Console.WriteLine(contract);

double khazaeiAccountBalance = contract.GetAccountBalance(KhazaeiNezhad.Address);

Console.WriteLine($"Khazaei Nezhad Account Balance : {khazaeiAccountBalance}");
