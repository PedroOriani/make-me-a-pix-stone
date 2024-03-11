const dotenv = require("dotenv");
const fs = require("fs");
// const { faker } = require("@faker-js/faker");

dotenv.config();

const knex = require("knex")({
  client: "pg",
  connection: process.env.DATABASE_URL,
});

const VALUE = 1000000;
const VALID_CPF = "12345678910"; // driven
const ERASE_DATA = true;

async function run() {
  if (ERASE_DATA) {
    await knex("Users").del();
  }

  const start = new Date();

  // users
  const users = generateUsers();
  await populateUsers(users);
  generateJson("./seed/existing_users.json", users);

  console.log("Closing DB connection...");
  await knex.destroy();

  const end = new Date();

  console.log("Done!");
  console.log(`Finished in ${(end - start) / 1000} seconds`);

  const accounts = generateUsers();
  await populateUsers(users);
  generateJson("./seed/existing_users.json", users);

  console.log("Closing DB connection...");
  await knex.destroy();

  console.log("Done!");
  console.log(`Finished in ${(end - start) / 1000} seconds`);

  const banks = generateUsers();
  await populateUsers(users);
  generateJson("./seed/existing_users.json", users);

  console.log("Closing DB connection...");
  await knex.destroy();

  console.log("Done!");
  console.log(`Finished in ${(end - start) / 1000} seconds`);
}

run();

function generateUsers() {
  console.log(`Generating ${VALUE} users...`);
  const users = [];
  for (let i = 0; i < VALUE; i++) {
    users.push({
      Name: `${'Pedro' + i}`,
      Cpf: VALID_CPF
    });
  }

  return users;
}

async function populateUsers(users) {
  console.log("Storing on DB...");

  const tableName = "Users";
  await knex.batchInsert(tableName, users);
}

function generateJson(filepath, data) {
  if (fs.existsSync(filepath)) {
    fs.unlinkSync(filepath);
  }
  fs.writeFileSync(filepath, JSON.stringify(data));
}


