# Bank application in Blazer

<h3>Description</h3>
This is my first project where I have built a bank applictation using blazer in visual studio where you can; create accounts, view account history with filtering and sorting possibilities and also the ability to make deposits/withdraws and transfers. Everything is saved in the browser via local storage.

<h5>This project demonstrates the use of:</h5>

- Dependency injection of services
- Data persistence using JSON serialization and 'IJSRuntime' for local storage access
- Bankinglogic in the form of simple transactions and account management
- Error handling and simple logging for actions in the console

<h3>How to use the application</h3>


<h5>Home</h5>

- Welcome page

<h5>Create Account</h5>

- Allows creation of an account
- Choosing account type, initial balance and currency type
- Listing the accounts for a quick overview

<h5>Accounts</h5>

Displaying a list of all accounts that have been created with:
  
  - Name
  - Type
  - Balance
  - Currency
  - Interest rate
  - Accumulated interest
  - Last updated

<h5>New Transaction</h5>

Allows the use of transactions for:

- Deposits
- Withdraws
- Transfers between accounts
- Also showing a simple list of current accounts

<h5>History</h5>

Allows you to select an account to view different transactions and filter them by date and type.
Also allows you to sort them by date, amount, type and balance after transaction.

<h3>Credits</h3>
<h5>Developed by: Christopher Petti</h5>

<h5>Technologies used:</h5>

- Blazer WebAssembly(.NET 8)
- C#
- JavaScript Interop (IJSRuntime)
- Local Storage  API
- HTML & CSS


