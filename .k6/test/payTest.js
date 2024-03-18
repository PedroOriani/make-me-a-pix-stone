import { http } from "k6";
import { SharedArray } from 'k6/data';

export const options = {
    vus: 2, // virtual users
    duration: "20s"
}

const dataUser = new SharedArray("users", () => JSON.parse(open("../seed/existing_users.json")))

const dataBank = new SharedArray("bank", () => JSON.parse(open("../seed/existing_banks.json")))

export default function () {

    const randomUser = dataUser[Math.floor(Math.random() * dataUser.length)];
    const randomBank = dataBank[Math.floor(Math.random() * dataBank.length)];

    const inputData = {
        origin: {
            user: {
                cpf: randomUser.cpf
            },
            account: {
                number: "001",
                agency: "0000003"
            }
        },
        destiny: {
            key: generateKey(randomUser)
        },
        amount: Math.floor(Math.random()*10000) + 1,
        description: ""
    }

    const body = JSON.stringify(inputData);
    const headers = { "Content-Type": "application/json",
     'Authorization': 'Bearer' + randomBank.Token};
    http.post("http://localhost:5045/payment", body, { headers })
}

function generateKey (user) {
    const types = ['CPF', 'Email', "Phone", "Random"];
    const type = types[Math.floor(Math.random()*types.length)];

    let value = ''

    switch (type) {
        case 'CPF':
            value = user.Cpf;
        case 'Email':
            value = `${user.Cpf}@gmail.com`;
        case 'Phone':
            value = generatePhone();;
        case 'Random':
            value = (Number(user.Cpf) + 10000000000).toString();
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
