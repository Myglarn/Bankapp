# Bank application in Blazer

## Description
This is my first project where I have built a bank applictation using Blazor in Visual Studio. The application allows you to create accounts, perform deposits, withdrawals, and transfers, and view transaction history with filtering and sorting options. All data is stored locally in the browser using Local Storage.

## Key Features Demonstrated in This Project

### Dependency Injection of Services
Example: Accountservices and Storageservices 

These services contain the core logic of the application, while the Blazor components focus only on displaying data and handling user interactions.
Dependency Injection helps to:

- Improve code structure and organization

- Separate business logic from the UI layer

- Make the application easier to test and maintain


### Data persistence using JSON serialization and 'IJSRuntime'

The application stores accounts and transaction data in the browser’s Local Storage.
Data is serialized to and from JSON so that:

- Accounts and their histories remain intact between sessions

- No external database is required 

### Bankinglogic 

Includes:

- Account management

- Deposit and withdrawal operations (with optional expense categories)

- Transfers between accounts

- Savings accounts with a fixed interest rate

- Automatic updating of balances and accumulated interest

### Error handling 

Basic console logging is used to track actions and identify where errors occur during runtime.

### Simple UI locking 

A basic username and PIN system is used to lock and unlock the UI, adding a realistic bank application feel.

## How to use the application

### Log in credentials:
Username : User
Pin: 1234


### Home

- Welcome page
- Login to unlock the UI

### Create Account

- Allows account creation
- Choosing account type, initial balance and currency
- Displays an overview list of existing accounts

### Accounts

Shows all created accounts, including:
  
  - Name
  - Type
  - Balance
  - Currency
  - Interest rate
  - Accumulated interest
  - Last updated date

### New Transaction

Allows you to perform:

- Deposits
- Withdraws (With expense category selection)
- Transfers between accounts

A simple list of existing accounts is also shown.

### History

- Select an account to view its transaction history
- Filter by date, transaction type, or expense category
- Sort by date, amount, type, or balance after the transaction

### Log out

Logs the user out and returns to the home page.

## Credits
### Developed by: Christopher Petti

### Technologies Used:

- Blazer WebAssembly(.NET 8)
- C#
- JavaScript Interop (IJSRuntime)
- Local Storage API
- HTML & CSS


