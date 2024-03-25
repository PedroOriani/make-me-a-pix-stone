import http from "k6/http";
import { SharedArray } from 'k6/data';

export const options = {
    vus: 10, // virtual users
    duration: "60s",
    thresholds: {
        http_req_failed: ['rate<0.01'], // http errors should be less than 1%
        http_req_duration: ['p(95)<200'], // 95% of requests should be below 200ms
      },
}

const dataUser = new SharedArray("users", () => JSON.parse(open("../seed/existing_users.json")))

const dataBank = new SharedArray("bank", () => JSON.parse(open("../seed/existing_banks.json")))

export default function () {

    const randomUser = dataUser[Math.floor(Math.random() * dataUser.length)];
    const randomBank = dataBank[Math.floor(Math.random() * dataBank.length)];

    const inputData = {
        Key: generateRandomKey(randomUser),
        User: {
            Cpf: randomUser.Cpf,
        },
        Account: {
            Number: `${Date.now() + randomUser.Name}`,
            Agency: `${Date.now() + randomUser.Cpf}`
        }
    }

    const body = JSON.stringify(inputData);
    const headers = { "Content-Type": "application/json", 'Authorization': 'Bearer ' + randomBank.Token };
    const response = http.post("http://localhost:8080/keys", body, { headers })
    if (response.status !== 201) console.log(`Error creating key: ${response.body}`)
}

function generateRandomKey (user) {
    const types = ['CPF', 'Email', "Phone", "Random"];
    const type = types[Math.floor(Math.random()*types.length)];

    let value = ''

    switch (type) {
        case 'CPF':
            value = user.Cpf;
            break;
        case 'Email':
            value = `${user.Cpf}@gmail.com`;
            break;
        case 'Phone':
            value = generatePhone();
            break;
        case 'Random':
            value = (Number(user.Cpf) + Math.floor(Math.random() * 10000) + 1).toString();
            break;
    }

    return { type, value }
}

function generatePhone() {
    let phone = '';
    for (let i = 0; i < 11; i++) {
        phone += Math.floor(Math.random() * 9) + 1;
    }

    return phone;
}
