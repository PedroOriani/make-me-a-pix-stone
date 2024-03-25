import http from "k6/http";
import { SharedArray } from 'k6/data';

export const options = {
    scenarios: {
        contacts: {
            executor: 'constant-arrival-rate',
            duration: '1m',
            preAllocatedVUs: 200,
            rate: 100000,
            timeUnit: '1m'
        },
    },
    thresholds: {
        http_req_failed: ['rate<0.01'], // http errors should be less than 1%
        http_req_duration: ['p(95)<200'], // 95% of requests should be below 200ms
      },
}

const dataKey = new SharedArray("key", () => JSON.parse(open("../seed/existing_keys.json")))

const dataBank = new SharedArray("bank", () => JSON.parse(open("../seed/existing_banks.json")))

export default function () {

    const randomKey = dataKey[Math.floor(Math.random() * dataKey.length)];
    const randomBank = dataBank[Math.floor(Math.random() * dataBank.length)];
    
    const headers = { "Content-Type": "application/json", 'Authorization': 'Bearer ' + randomBank.Token };
    const response = http.get(`http://localhost:5045/keys/${randomKey.Type}/${randomKey.Value}`, { headers })

    if (response.status !== 200) console.log(response.body);
}

