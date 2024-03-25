const dotenv = require("dotenv");
const fs = require("fs");
const { faker } = require ("@faker-js/faker");

dotenv.config();

const knex = require("knex")({
  client: "pg",
  connection: process.env.DATABASE_URL,
});

const VALUE = 1000000;
const VALID_CPF = 10000000000;
const ERASE_DATA = true;

async function run() {
  if (ERASE_DATA) {
    await knex("Users").del();
    await knex("Banks").del();
    await knex("Keys").del();
    await knex("Accounts").del();
    await knex("Payments").del();
  }

  const start = new Date();

  // users
  const users = generateUsers();
  await populateUsers(users);
  generateJson("./seed/existing_users.json", users);

  console.log("Closing DB connection...");

  //banks
  const banks = generateBanks();
  await populateBanks(banks);
  generateJson("./seed/existing_banks.json", banks);

  console.log("Closing DB connection...");

  //accounts
  const accounts = await generateAccounts();
  await populateAccounts(accounts);
  generateJson("./seed/existing_accounts.json", accounts);

  console.log("Closing DB connection...");

  //keys
  const keys = await generateKeys();
  await populateKeys(keys);
  generateJson("./seed/existing_keys.json", keys);

  //payments
  const payments = await generatePayments();
  await populatePayments(payments);
  const paymentsDB = await knex.select('Id', 'Status').table('Payments');
  generateJson("./seed/existing_payments.json", payments);
  generateNDJSON("./seed/existing_payments.ndjson", paymentsDB)


  console.log("Closing DB connection...");
  await knex.destroy();

  const end = new Date();

  console.log("Done!");
  console.log(`Finished in ${(end - start) / 1000} seconds`);
}

run();

// User
function generateUsers() {
  console.log(`Generating ${VALUE} users...`);
  const users = [];
  for (let i = 0; i < VALUE; i++) {
    const createdAt = new Date();
    const updatedAt = new Date();
    users.push({
      Name:  faker.person.firstName(),
      Cpf: (VALID_CPF + i).toString(),
      CreatedAt: createdAt,
      UpdatedAt: updatedAt
    });
  }

  return users;
}

async function populateUsers(users) {
  console.log("Storing on DB...");

  const tableName = "Users";
  await knex.batchInsert(tableName, users);
}

// Banks
function generateBanks() {
  console.log(`Generating ${VALUE} banks...`);
  const banks = [];
  for (let i = 0; i < VALUE; i++) {
    const createdAt = new Date();
    const updatedAt = new Date();
    banks.push({
      Name: faker.company.name(),
      Token: faker.number.bigInt().toString(),
      CreatedAt: createdAt,
      UpdatedAt: updatedAt
    });
  }

  return banks;
}

async function populateBanks(banks) {
  console.log("Storing on DB...");

  const tableName = "Banks";
  await knex.batchInsert(tableName, banks);
}

// Accounts
async function generateAccounts() {
  console.log(`Generating ${VALUE} accounts...`);
  const accounts = [];

  const dataUser = await knex.select('Id').table('Users');
  const dataBank = await knex.select('Id').table('Banks');

  for (let i = 0; i < VALUE; i++) {
    const randomUser = dataUser[Math.floor(Math.random() * dataUser.length)];
    const randomBank = dataBank[Math.floor(Math.random() * dataBank.length)];

    const createdAt = new Date();
    const updatedAt = new Date();

    accounts.push({
      Number: faker.finance.accountNumber().toString(),
      Agency: faker.finance.accountNumber().toString(),
      UserId: randomUser.Id,
      BankId: randomBank.Id,
      CreatedAt: createdAt,
      UpdatedAt: updatedAt
    });
  }

  return accounts;
}

async function populateAccounts(accounts) {
  console.log("Storing on DB...");

  const tableName = "Accounts";
  await knex.batchInsert(tableName, accounts);
}

// Keys
async function generateKeys() {
  console.log(`Generating ${VALUE} keys...`);
  const keys = [];

  const dataAccount = await knex.select('Id').table('Accounts');
  const dataAccountUser = await knex.select('UserId').table('Accounts');
  

  for (let i = 0; i < VALUE; i++) {

    const randomAccount = dataAccount[Math.floor(Math.random() * dataAccount.length)];
    const randomUser = dataAccountUser[Math.floor(Math.random() * dataAccountUser.length)];
    
    const createdAt = new Date();
    const updatedAt = new Date();

    keys.push(generateRandomKey(randomAccount, randomUser, createdAt, updatedAt));
  }

  return keys;
}

async function populateKeys(keys) {
  console.log("Storing on DB...");

  const tableName = "Keys";
  await knex.batchInsert(tableName, keys);
}

// Payments
async function generatePayments() {
  console.log(`Generating ${VALUE} payments...`);
  const payments = [];

  const dataKey = await knex.select('Id').table('Keys');
  const dataAccount = await knex.select('Id').table('Accounts');

  for (let i = 0; i < VALUE; i++) {

    const status = ["SUCCESS", "FAILED"];

    const randomAccount = dataAccount[Math.floor(Math.random() * dataAccount.length)];
    const randomKey = dataKey[Math.floor(Math.random() * dataKey.length)];

    const createdAt = new Date();
    const updatedAt = new Date();

    payments.push({
      Status: status[Math.floor(Math.random() * status.length)],
      Amount: faker.number.int({max: 100000}),
      Description: faker.lorem.sentence(),
      PixKeyId: randomKey.Id,
      PaymentProviderAccountId: randomAccount.Id,
      CreatedAt: createdAt,
      UpdatedAt: updatedAt
    });
  }

  return payments;
}

async function populatePayments(payments) {
  console.log("Storing on DB...");

  const tableName = "Payments";
  await knex.batchInsert(tableName, payments);
}

//Json
function generateJson(filepath, data) {
  if (fs.existsSync(filepath)) {
    fs.unlinkSync(filepath);
  }
  fs.writeFileSync(filepath, JSON.stringify(data));
}

//NDJSON
function generateNDJSON(filepath, data) {
  if (fs.existsSync(filepath)) {
      fs.unlinkSync(filepath);
  }

  const stream = fs.createWriteStream(filepath, { flags: 'a' });

  data.forEach(obj => {
      const newObj = {id: obj.Id, status: obj.Status}
      stream.write(JSON.stringify(newObj) + '\n');
  });

  stream.end();
}

function generateRandomKey (account, user, createdAt, updatedAt) {
  const types = ['Email', "Phone", "Random"];
  const type = types[Math.floor(Math.random()*types.length)];

  let value = ''

  switch (type) {
      case 'Email':
          value = `${faker.lorem.word() + faker.number.bigInt()}@gmail.com`;
          break;
      case 'Phone':
          value = generatePhone();
          break;
      case 'Random':
          value = `${faker.lorem.word() + faker.number.bigInt()}`;
          break;
  }

  return { Type: type, Value: value, UserId: user.UserId, AccountId: account.Id, CreatedAt: createdAt, UpdatedAt: updatedAt }
}

function generatePhone() {
  let phone = '';
  for (let i = 0; i < 11; i++) {
      phone += Math.floor(Math.random() * 9) + 1;
  }

  return phone;
}