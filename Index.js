import http from 'k6/http';
import { sleep } from 'k6';
import faker from 'https://cdnjs.cloudflare.com/ajax/libs/Faker/3.1.0/faker.min.js';


//Obs para rodar o K6 batendo na sua api você precisa
//desabilitar o SSL
//Incluir a porta no firewall do windows
//incluir o aplicativo no firewall do windows

export const option = {
    vus: 300,
    duration: '30s'
}

function autenticar() {


    var objeto = {
        "nome": "UsuTeste1",
        "email": "usuTeste1@email.com.br",
        "senha": "123456",
        "papel": "Admin"
    }

    var corpoDaSolicitacao = JSON.stringify(objeto);

    const response = http.post('https://localhost:7003/Usuario/autenticar', corpoDaSolicitacao, {
        headers: { 'Content-Type': 'application/json' }, 
        insecureSkipTLSVerify: true
    });

    return response.json().token;
}

export default function () {
    const token = autenticar();

    var objeto = {
        "nome": faker.name.findName(),
        "email": faker.internet.email(),
        "senha": faker.internet.password(),
        "papel": "Admin"
    }
       
    var corpoDaSolicitacao = JSON.stringify(objeto);
        
    var url = 'https://localhost:7003/Usuario/criar';


    const response = http.post('https://localhost:7003/Usuario/criar', corpoDaSolicitacao,
        {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            insecureSkipTLSVerify: true
        });

   
    sleep(1);

}