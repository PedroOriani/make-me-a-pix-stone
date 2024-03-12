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
    users.push({
      Name:  faker.person.firstName(),
      Cpf: (VALID_CPF + i).toString()
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
    banks.push({
      Name: faker.company.name(),
      Token: faker.number.bigInt().toString()
    });
  }

  return banks;
}

async function populateBanks(banks) {
  console.log("Storing on DB...");

  const tableName = "Banks";
  await knex.batchInsert(tableName, banks);
}

function generateJson(filepath, data) {
  if (fs.existsSync(filepath)) {
    fs.unlinkSync(filepath);
  }
  fs.writeFileSync(filepath, JSON.stringify(data));
}
