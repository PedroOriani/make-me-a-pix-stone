import http from "k6/http";
import { SharedArray } from 'k6/data';

export const options = {
    vus: 20, // virtual users
    duration: "60s",
    thresholds: {
        http_req_failed: ['rate<0.01'], // http errors should be less than 1%
        http_req_duration: ['p(95)<200'], // 95% of requests should be below 200ms
      },
}

const dataUser = new SharedArray("users", () => JSON.parse(open("../seed/existing_users.json")))
const dataKey = new SharedArray("keys", () => JSON.parse(open("../seed/existing_keys.json")))
const dataBank = new SharedArray("bank", () => JSON.parse(open("../seed/existing_banks.json")))

export default function () {

    const randomUserOrigin = dataUser[Math.floor(Math.random() * dataUser.length)];
    const randomUserDestiny = dataUser[Math.floor(Math.random() * dataUser.length)];
    const randomBank = dataBank[Math.floor(Math.random() * dataBank.length)];
    const randomKey = dataKey[Math.floor(Math.random() * dataKey.length)];

    const inputData = {
        origin: {
            user: {
                cpf: randomUserOrigin.Cpf,
            },
            Account: {
                Number: `${Date.now() + randomUserOrigin.Name}`,
                Agency: `${Date.now() + randomUserOrigin.Cpf}`
            }
        },
        destiny: {
            key: {
                type: randomKey.Type,
                value: randomKey.Value
            }
        },
        amount: Math.floor(Math.random() * 10000) + 1,
        description: `Payment from ${randomUserOrigin.Name} to ${randomUserDestiny.Name}`
    }

    const body = JSON.stringify(inputData);
    const headers = { "Content-Type": "application/json",
     'Authorization': 'Bearer ' + randomBank.Token};
    const response = http.post("http://localhost:5045/Payment", body, { headers })
    if (response.status !== 201) console.log(response);
}