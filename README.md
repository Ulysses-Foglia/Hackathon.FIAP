Este repositório tem como objetivo entregar uma aplicação para conclusão do Hackathon, da Pos Tech na FIAP, turma 2NETT, curso Arquitetura de Sistemas .NET com Azure, grupo 11.

Segue abaixo a documentação do projeto:

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Hackaton - Health&Med

## Introdução

Este documento detalha extensivamente os requisitos e funcionalidades do sistema de agendamento de consultas médicas, que está atualmente em operação. O sistema foi desenvolvido para proporcionar uma maneira organizada e eficiente de gerenciar os compromissos médicos, tanto para os profissionais de saúde quanto para os pacientes.

Por meio deste sistema, os médicos têm a capacidade de gerenciar suas agendas de maneira prática e dinâmica, permitindo ajustes rápidos e controle total sobre os horários disponíveis para atendimento. Isso inclui a possibilidade de bloquear horários para emergências, férias ou quaisquer outras eventualidades, além de permitir a visualização de todas as consultas marcadas, canceladas ou remarcadas.

Para os pacientes, o sistema oferece uma interface intuitiva e segura para o agendamento de consultas. Eles podem acessar a plataforma de qualquer dispositivo conectado à internet, visualizar os horários disponíveis dos médicos de sua preferência e marcar consultas conforme sua conveniência. Além disso, o sistema envia lembretes automáticos para os pacientes via e-mail ou SMS, ajudando a reduzir as faltas e garantindo que os pacientes não percam seus compromissos.

O sistema também conta com diversas funcionalidades adicionais, como a possibilidade de integrar o histórico médico do paciente, facilitando o acesso às informações durante as consultas e promovendo um atendimento mais personalizado e eficiente. Há ainda opções de relatórios e estatísticas que ajudam na gestão administrativa do consultório ou clínica, permitindo uma análise detalhada das atividades e identificação de áreas para melhoria.

Em resumo, o sistema de agendamento de consultas médicas foi concebido para simplificar e otimizar o processo de marcação de consultas, beneficiando tanto médicos quanto pacientes, ao proporcionar uma experiência mais fluida, organizada e segura para todos os envolvidos.

## **Arquitetura do Sistema**

![image](https://github.com/user-attachments/assets/8fc95e72-57ff-4bde-ad22-30f6df76e4ea)

---

## **Requisitos Funcionais**

## Cadastro do Usuário (Médico)

### Descrição

O sistema deve permitir que médicos se cadastrem preenchendo os campos obrigatórios com suas informações pessoais e profissionais. Este processo inclui validações de unicidade para evitar duplicidade de registros e o envio de um e-mail de confirmação para validar o endereço de e-mail fornecido.

### Fluxo do Cadastro

1. **Formulário de Cadastro**:
    - O médico acessa a página de cadastro do sistema.
    - O sistema apresenta um formulário com os seguintes campos obrigatórios:
        - Nome
        - CPF
        - Número do CRM (Conselho Regional de Medicina)
        - E-mail
        - Senha
2. **Preenchimento dos Campos**:
    - O médico preenche todos os campos obrigatórios.
    - O sistema realiza validação em tempo real para garantir que todos os campos obrigatórios estão preenchidos e que seguem o formato esperado (por exemplo, o CPF deve ter 11 dígitos, o e-mail deve ter um formato válido, etc.).
3. **Validação de Unicidade**:
    - Após o preenchimento do formulário, o sistema verifica a unicidade dos campos CPF e E-mail.
    - Se um CPF ou E-mail já estiver cadastrado no sistema, uma mensagem de erro é exibida informando que o dado já existe e o médico não pode prosseguir com o cadastro até corrigir a informação.
4. **Criação da Conta**:
    - Se todas as validações forem bem-sucedidas, o sistema cria uma nova conta para o médico com as informações fornecidas.
    - O sistema gera um token de confirmação e associa ao cadastro do médico.
5. **Envio de E-mail de Confirmação**:
    - O sistema envia um e-mail de confirmação para o endereço de e-mail fornecido pelo médico.
    - O e-mail contém um link para confirmação de cadastro. Este link inclui o token de confirmação gerado anteriormente.
6. **Confirmação do E-mail**:
    - O médico deve acessar sua conta de e-mail e clicar no link de confirmação.
    - O sistema verifica o token e, se válido, ativa a conta do médico.

### Regras de Negócio

- **Obrigatoriedade dos Campos**: Todos os campos Nome, CPF, Número do CRM, E-mail e Senha são obrigatórios.
- **Formato dos Campos**:
    - CPF: Deve conter 11 dígitos e ser válido conforme o algoritmo de validação de CPF.
    - Número do CRM: Deve ser um número válido reconhecido pelo Conselho Regional de Medicina.
    - E-mail: Deve estar em um formato válido (exemplo@dominio.com).
    - Senha: Deve atender aos critérios de complexidade definidos (mínimo de 8 caracteres, contendo letras maiúsculas, minúsculas, números e caracteres especiais).
- **Unicidade**: CPF e E-mail devem ser únicos no sistema.
- **Envio de E-mail de Confirmação**: Deve ser enviado imediatamente após o cadastro com um link para ativação.
- **Token de Confirmação**: Deve ser único, seguro e ter validade limitada (por exemplo, 24 horas).

### Mensagens de Erro

- **Campo Obrigatório Não Preenchido**: "O campo [Nome do Campo] é obrigatório."
- **Formato Inválido**: "O campo [Nome do Campo] está em um formato inválido."
- **CPF ou E-mail Já Cadastrado**: "O CPF ou E-mail informado já está cadastrado no sistema."

### Considerações de Segurança

- **Criptografia de Senhas**: As senhas devem ser armazenadas de forma criptografada utilizando algoritmos de criptografia seguros (por exemplo, bcrypt).
- **Proteção Contra Ataques de Força Bruta**: Implementação de limites de tentativas de login e mecanismos de bloqueio temporário após múltiplas tentativas falhas.
- **Validação do E-mail**: O link de confirmação deve expirar após um período específico para evitar uso indevido.

## Conclusão

Esta documentação detalha os requisitos funcionais essenciais para o cadastro de médicos no sistema, assegurando um processo seguro, eficiente e conforme as regras de negócio estabelecidas. Implementar essas funcionalidades garantirá a integridade dos dados cadastrados e uma boa experiência para os usuários do sistema.

## Autenticação do Usuário (Médico)

### Descrição

O sistema deve permitir que médicos façam login utilizando seu e-mail e senha. O processo de autenticação deve ser seguro, incluindo verificações adequadas dos dados fornecidos. Além disso, o sistema deve oferecer uma funcionalidade de recuperação de senha via e-mail para casos de esquecimento.

### Fluxo de Autenticação

1. **Tela de Login**:
    - O médico acessa a página de login do sistema.
    - O sistema apresenta um formulário com os seguintes campos:
        - E-mail
        - Senha
2. **Preenchimento dos Campos**:
    - O médico preenche os campos de e-mail e senha.
    - O sistema valida em tempo real se os campos obrigatórios estão preenchidos.
3. **Verificação de Credenciais**:
    - O médico submete o formulário de login.
    - O sistema verifica as credenciais fornecidas (e-mail e senha):
        - **E-mail**: Verifica se o e-mail está registrado no sistema.
        - **Senha**: Verifica se a senha fornecida corresponde à senha armazenada (criptografada) associada ao e-mail.
4. **Autenticação Bem-sucedida**:
    - Se as credenciais forem válidas, o sistema autentica o médico e redireciona para a página principal do sistema.
5. **Falha na Autenticação**:
    - Se as credenciais forem inválidas, o sistema exibe uma mensagem de erro e permite que o médico tente novamente.
    - Após múltiplas tentativas falhas, o sistema pode implementar um bloqueio temporário da conta ou apresentar um desafio de CAPTCHA para aumentar a segurança.

### Recuperação de Senha

1. **Solicitação de Recuperação de Senha**:
    - Na tela de login, o médico pode clicar no link "Esqueci minha senha".
    - O sistema apresenta um formulário para que o médico informe o e-mail associado à sua conta.
2. **Envio do E-mail de Recuperação**:
    - O médico preenche o campo de e-mail e submete o formulário.
    - O sistema verifica se o e-mail está registrado.
    - Se o e-mail estiver registrado, o sistema gera um token de recuperação de senha e envia um e-mail com um link para redefinição de senha.
3. **Redefinição de Senha**:
    - O médico acessa o e-mail e clica no link de recuperação.
    - O sistema apresenta um formulário para que o médico digite a nova senha.
    - O médico preenche o campo de nova senha e confirma.
    - O sistema valida a nova senha (complexidade, tamanho, etc.) e, se válida, atualiza a senha no banco de dados.

### Regras de Negócio

- **Obrigatoriedade dos Campos**: E-mail e Senha são obrigatórios para o login.
- **Validação de E-mail**: E-mail deve estar em formato válido e ser registrado no sistema.
- **Complexidade da Senha**: Senha deve atender aos critérios de complexidade definidos (mínimo de 8 caracteres, contendo letras maiúsculas, minúsculas, números e caracteres especiais).
- **Recuperação de Senha**:
    - O link de recuperação de senha deve expirar após um período específico (por exemplo, 24 horas).
    - A nova senha não pode ser igual à anterior.

### Mensagens de Erro

- **Campo Obrigatório Não Preenchido**: "O campo [Nome do Campo] é obrigatório."
- **E-mail Não Registrado**: "O e-mail informado não está registrado no sistema."
- **Credenciais Inválidas**: "E-mail ou senha inválidos."
- **Link de Recuperação Expirado**: "O link de recuperação de senha expirou. Por favor, solicite um novo link."

### Considerações de Segurança

- **Criptografia de Senhas**: Senhas devem ser armazenadas de forma criptografada utilizando algoritmos de criptografia seguros (por exemplo, bcrypt).
- **Proteção Contra Ataques de Força Bruta**: Implementação de limites de tentativas de login e mecanismos de bloqueio temporário após múltiplas tentativas falhas.
- **Validação de E-mail**: E-mails para recuperação de senha devem conter um link seguro com um token de tempo limitado.
- **HTTPS**: Todo o tráfego de autenticação deve ser feito sobre HTTPS para garantir a segurança dos dados transmitidos.

## Conclusão

Esta documentação detalha os requisitos funcionais essenciais para a autenticação de médicos no sistema, assegurando um processo seguro e eficiente de login e recuperação de senha. Implementar essas funcionalidades garantirá a segurança dos dados de acesso e uma boa experiência para os usuários do sistema.

## Cadastro/Edição de Horários Disponíveis (Médico)

### Descrição

O sistema deve permitir que médicos cadastrem e editem seus horários disponíveis e indisponíveis. Além disso, deve permitir a alteração de horários já cadastrados, refletindo a mudança de status para "Disponível" e removendo qualquer relação com pacientes agendados, se houver. O sistema também deve verificar a duplicidade de horários ao inserir novos horários na agenda.

### Fluxo de Cadastro/Edição de Horários

1. **Tela de Gerenciamento de Horários**:
    - O médico acessa a página de gerenciamento de horários.
    - O sistema apresenta uma interface com opções para adicionar, editar e remover horários disponíveis e indisponíveis.
2. **Cadastro de Horários Disponíveis**:
    - O médico seleciona a opção para cadastrar um novo horário disponível.
    - O sistema apresenta um formulário onde o médico pode definir:
        - Data
        - Horário de Início
        - Horário de Término
    - O médico submete o formulário.
3. **Verificação de Duplicidade**:
    - O sistema verifica se o horário já está cadastrado para o mesmo dia e intervalo de tempo.
    - Se o horário já existir, o sistema exibe uma mensagem de erro informando sobre a duplicidade e solicita que o médico ajuste o horário.
4. **Edição de Horários Disponíveis**:
    - O médico seleciona um horário já cadastrado para edição.
    - O sistema apresenta os detalhes atuais do horário (data, início e término).
    - O médico altera os detalhes conforme necessário e submete as alterações.
5. **Alteração de Horário**:
    - Se um horário disponível for alterado, o sistema deve:
        - Atualizar o status do horário para "Disponível".
        - Remover a relação com qualquer paciente que estava agendado para o horário original.
    - Se um horário for alterado para um intervalo diferente, o sistema deve garantir que o novo horário não entre em conflito com outros horários já cadastrados.
6. **Cadastro de Horários Indisponíveis**:
    - O médico seleciona a opção para cadastrar um horário indisponível.
    - O sistema apresenta um formulário onde o médico pode definir:
        - Data
        - Horário de Início
        - Horário de Término
    - O médico submete o formulário.
7. **Remoção de Horários**:
    - O médico pode remover horários previamente cadastrados.
    - O sistema exibe uma confirmação antes de remover o horário.
    - A remoção de um horário também deve atualizar o status para "Indisponível" e remover qualquer relação com pacientes agendados.

### Regras de Negócio

- **Cadastro e Edição de Horários**:
    - Horários devem ser definidos com data, horário de início e horário de término.
    - Não pode haver sobreposição de horários disponíveis para o mesmo intervalo de tempo.
- **Verificação de Duplicidade**:
    - O sistema deve garantir que não haja horários duplicados para o mesmo intervalo e data.
- **Alteração de Horários**:
    - Alterações em horários devem refletir imediatamente na agenda e alterar o status para "Disponível".
    - A relação com pacientes deve ser removida se o horário for alterado.
- **Cadastro de Horários Indisponíveis**:
    - Horários indisponíveis não devem ser exibidos para agendamento de pacientes.

### Mensagens de Erro

- **Duplicidade de Horário**: "O horário informado já está cadastrado para a data selecionada. Por favor, escolha um horário diferente."
- **Campo Obrigatório Não Preenchido**: "Todos os campos (data, horário de início e horário de término) devem ser preenchidos."
- **Sobreposição de Horários**: "O horário informado entra em conflito com um horário já cadastrado. Por favor, ajuste o horário."

### Considerações de Segurança

- **Validação de Dados**: Garantir que todos os dados inseridos sejam validados para evitar inconsistências e sobreposições na agenda.
- **Proteção Contra Conflitos**: Implementar lógica para evitar conflitos de horários e garantir que as alterações sejam refletidas corretamente.
- **Integração com Agenda de Pacientes**: Atualizar a relação com pacientes agendados de forma automática para refletir alterações em horários disponíveis.

## Conclusão

Esta documentação detalha os requisitos funcionais para o cadastro e edição de horários disponíveis e indisponíveis pelos médicos. Implementar essas funcionalidades garantirá uma gestão eficiente da agenda, evitando conflitos e mantendo a integridade das informações de disponibilidade e agendamento.

## Cadastro do Usuário (Paciente)

### Descrição

O sistema deve permitir que pacientes se cadastrem preenchendo os campos obrigatórios com suas informações pessoais. O processo deve incluir validação de unicidade para evitar duplicidade de registros e envio de um e-mail de confirmação para verificar o endereço de e-mail fornecido.

### Fluxo de Cadastro

1. **Formulário de Cadastro**:
    - O paciente acessa a página de cadastro do sistema.
    - O sistema apresenta um formulário com os seguintes campos obrigatórios:
        - Nome
        - CPF
        - E-mail
        - Senha
2. **Preenchimento dos Campos**:
    - O paciente preenche todos os campos obrigatórios.
    - O sistema realiza validação em tempo real para garantir que todos os campos obrigatórios estão preenchidos e seguem o formato esperado (por exemplo, o CPF deve ter 11 dígitos, o e-mail deve estar no formato correto, etc.).
3. **Validação de Unicidade**:
    - Após o preenchimento do formulário, o sistema verifica a unicidade dos campos CPF e E-mail.
    - Se um CPF ou E-mail já estiver cadastrado no sistema, o sistema exibe uma mensagem de erro informando que o dado já existe e o paciente deve corrigir a informação antes de prosseguir com o cadastro.
4. **Criação da Conta**:
    - Se todas as validações forem bem-sucedidas, o sistema cria uma nova conta para o paciente com as informações fornecidas.
    - O sistema gera um token de confirmação e o associa ao cadastro do paciente.
5. **Envio de E-mail de Confirmação**:
    - O sistema envia um e-mail de confirmação para o endereço fornecido pelo paciente.
    - O e-mail contém um link para confirmação de cadastro. Este link inclui o token de confirmação gerado anteriormente.
6. **Confirmação do E-mail**:
    - O paciente deve acessar sua conta de e-mail e clicar no link de confirmação.
    - O sistema verifica o token e, se válido, ativa a conta do paciente.

### Regras de Negócio

- **Obrigatoriedade dos Campos**: Todos os campos Nome, CPF, E-mail e Senha são obrigatórios.
- **Formato dos Campos**:
    - CPF: Deve conter 11 dígitos e ser válido conforme o algoritmo de validação de CPF.
    - E-mail: Deve estar em um formato válido (exemplo@dominio.com).
    - Senha: Deve atender aos critérios de complexidade definidos (mínimo de 8 caracteres, contendo letras maiúsculas, minúsculas, números e caracteres especiais).
- **Unicidade**: CPF e E-mail devem ser únicos no sistema.
- **Envio de E-mail de Confirmação**: Deve ser enviado imediatamente após o cadastro com um link para ativação.
- **Token de Confirmação**: Deve ser único, seguro e ter validade limitada (por exemplo, 24 horas).

### Mensagens de Erro

- **Campo Obrigatório Não Preenchido**: "O campo [Nome do Campo] é obrigatório."
- **Formato Inválido**: "O campo [Nome do Campo] está em um formato inválido."
- **CPF ou E-mail Já Cadastrado**: "O CPF ou E-mail informado já está cadastrado no sistema."

### Considerações de Segurança

- **Criptografia de Senhas**: As senhas devem ser armazenadas de forma criptografada utilizando algoritmos de criptografia seguros (por exemplo, bcrypt).
- **Proteção Contra Ataques de Força Bruta**: Implementação de limites de tentativas de login e mecanismos de bloqueio temporário após múltiplas tentativas falhas.
- **Validação do E-mail**: O link de confirmação deve expirar após um período específico para evitar uso indevido.

## Conclusão

Esta documentação detalha os requisitos funcionais essenciais para o cadastro de pacientes no sistema, assegurando um processo seguro e eficiente de criação de contas. Implementar essas funcionalidades garantirá a integridade dos dados dos pacientes e uma experiência positiva durante o registro.

## Autenticação do Usuário (Paciente)

### Descrição

O sistema deve permitir que pacientes façam login utilizando seu e-mail e senha. O processo de autenticação deve ser seguro, incluindo a verificação das credenciais fornecidas. Além disso, o sistema deve oferecer uma funcionalidade de recuperação de senha via e-mail para casos de esquecimento.

### Fluxo de Autenticação

1. **Tela de Login**:
    - O paciente acessa a página de login do sistema.
    - O sistema apresenta um formulário com os seguintes campos:
        - E-mail
        - Senha
2. **Preenchimento dos Campos**:
    - O paciente preenche os campos de e-mail e senha.
    - O sistema realiza validação em tempo real para garantir que os campos estejam preenchidos corretamente.
3. **Verificação de Credenciais**:
    - O paciente submete o formulário de login.
    - O sistema verifica as credenciais fornecidas (e-mail e senha):
        - **E-mail**: Verifica se o e-mail está registrado no sistema.
        - **Senha**: Verifica se a senha fornecida corresponde à senha armazenada (criptografada) associada ao e-mail.
4. **Autenticação Bem-sucedida**:
    - Se as credenciais forem válidas, o sistema autentica o paciente e o redireciona para a página principal do sistema.
5. **Falha na Autenticação**:
    - Se as credenciais forem inválidas, o sistema exibe uma mensagem de erro e permite que o paciente tente novamente.
    - Após múltiplas tentativas falhas, o sistema pode implementar um bloqueio temporário da conta ou apresentar um desafio de CAPTCHA para aumentar a segurança.

### Recuperação de Senha

1. **Solicitação de Recuperação de Senha**:
    - Na tela de login, o paciente pode clicar no link "Esqueci minha senha".
    - O sistema apresenta um formulário para que o paciente informe o e-mail associado à sua conta.
2. **Envio do E-mail de Recuperação**:
    - O paciente preenche o campo de e-mail e submete o formulário.
    - O sistema verifica se o e-mail está registrado.
    - Se o e-mail estiver registrado, o sistema gera um token de recuperação de senha e envia um e-mail com um link para redefinição de senha.
3. **Redefinição de Senha**:
    - O paciente acessa o e-mail e clica no link de recuperação.
    - O sistema apresenta um formulário para que o paciente digite a nova senha.
    - O paciente preenche o campo de nova senha e confirma.
    - O sistema valida a nova senha (complexidade, tamanho, etc.) e, se válida, atualiza a senha no banco de dados.

### Regras de Negócio

- **Obrigatoriedade dos Campos**: E-mail e Senha são obrigatórios para o login.
- **Validação de E-mail**: E-mail deve estar em formato válido e ser registrado no sistema.
- **Complexidade da Senha**: Senha deve atender aos critérios de complexidade definidos (mínimo de 8 caracteres, contendo letras maiúsculas, minúsculas, números e caracteres especiais).
- **Recuperação de Senha**:
    - O link de recuperação de senha deve expirar após um período específico (por exemplo, 24 horas).
    - A nova senha não pode ser igual à anterior.

### Mensagens de Erro

- **Campo Obrigatório Não Preenchido**: "O campo [Nome do Campo] é obrigatório."
- **E-mail Não Registrado**: "O e-mail informado não está registrado no sistema."
- **Credenciais Inválidas**: "E-mail ou senha inválidos."
- **Link de Recuperação Expirado**: "O link de recuperação de senha expirou. Por favor, solicite um novo link."

### Considerações de Segurança

- **Criptografia de Senhas**: Senhas devem ser armazenadas de forma criptografada utilizando algoritmos de criptografia seguros (por exemplo, bcrypt).
- **Proteção Contra Ataques de Força Bruta**: Implementação de limites de tentativas de login e mecanismos de bloqueio temporário após múltiplas tentativas falhas.
- **Validação de E-mail**: E-mails para recuperação de senha devem conter um link seguro com um token de tempo limitado.
- **HTTPS**: Todo o tráfego de autenticação deve ser feito sobre HTTPS para garantir a segurança dos dados transmitidos.

## Conclusão

Esta documentação detalha os requisitos funcionais essenciais para a autenticação de pacientes no sistema, assegurando um processo seguro e eficiente de login e recuperação de senha. Implementar essas funcionalidades garantirá a segurança dos dados de acesso e uma boa experiência para os usuários do sistema.

## Busca por Médicos (Paciente)

### Descrição

O sistema deve permitir que pacientes visualizem uma lista de médicos disponíveis e apliquem filtros para encontrar médicos que atendam suas necessidades específicas. Os filtros disponíveis devem incluir especialidade, localização e disponibilidade dos médicos.

### Fluxo de Busca

1. **Tela de Busca por Médicos**:
    - O paciente acessa a página de busca por médicos.
    - O sistema apresenta uma interface com uma lista de médicos e opções para aplicar filtros.
2. **Exibição de Lista de Médicos**:
    - A lista de médicos deve exibir informações básicas, como:
        - Nome do médico
        - Especialidade
        - Localização (endereço ou cidade)
        - Disponibilidade (horários disponíveis para consulta)
3. **Aplicação de Filtros**:
    - O paciente pode aplicar os seguintes filtros para refinar a busca:
        - **Especialidade**: Seleção de uma ou mais especialidades médicas (por exemplo, cardiologia, ortopedia, dermatologia).
        - **Localização**: Filtro por cidade, estado ou endereço específico.
        - **Disponibilidade**: Seleção de horários disponíveis para consulta ou dias da semana em que o médico está disponível.
4. **Processamento da Busca**:
    - Após aplicar os filtros, o sistema atualiza a lista de médicos para exibir apenas aqueles que correspondem aos critérios selecionados.
    - O sistema deve garantir que a busca seja eficiente e retorne resultados relevantes com base nos filtros aplicados.
5. **Visualização de Detalhes do Médico**:
    - O paciente pode clicar em um médico da lista para visualizar detalhes adicionais, como:
        - Biografia e qualificações
        - Contato e informações de localização
        - Horários disponíveis
        - Avaliações e comentários de outros pacientes (se disponível)

### Regras de Negócio

- **Filtros**:
    - **Especialidade**: Deve permitir a seleção de múltiplas especialidades para a busca.
    - **Localização**: Deve suportar filtros por cidade, estado e endereço específico.
    - **Disponibilidade**: Deve permitir a seleção de horários ou dias da semana específicos para encontrar médicos disponíveis nesses períodos.
- **Atualização da Lista**: A lista de médicos deve ser atualizada dinamicamente com base nos filtros aplicados.
- **Exibição de Resultados**: Deve exibir informações relevantes e atualizadas sobre os médicos.

### Mensagens de Erro

- **Nenhum Médico Encontrado**: "Nenhum médico foi encontrado com os critérios de busca selecionados. Por favor, ajuste seus filtros e tente novamente."
- **Filtro Inválido**: "Um ou mais filtros selecionados são inválidos. Por favor, revise suas seleções e tente novamente."

### Considerações de Usabilidade

- **Interface de Filtros**: A interface de filtros deve ser intuitiva e fácil de usar, permitindo ao paciente selecionar e aplicar filtros rapidamente.
- **Feedback Visual**: Fornecer feedback visual claro ao paciente sobre a aplicação dos filtros e a atualização dos resultados da busca.
- **Desempenho**: Garantir que a busca e a atualização dos resultados sejam rápidas e eficientes, mesmo com um grande número de médicos cadastrados.

## Conclusão

Esta documentação detalha os requisitos funcionais para o recurso de busca por médicos, garantindo que pacientes possam localizar e visualizar médicos de forma eficaz com base em especialidade, localização e disponibilidade. Implementar essas funcionalidades proporcionará uma experiência de busca eficiente e satisfatória para os usuários do sistema.

## Agendamento de Consultas (Paciente)

### Descrição

O sistema deve permitir que pacientes agendem consultas com médicos, visualizando as agendas e horários disponíveis dos médicos. O processo de agendamento deve incluir a validação da disponibilidade do horário antes da confirmação e a prevenção de agendamentos duplos para o mesmo horário.

### Fluxo de Agendamento

1. **Acesso à Agenda do Médico**:
    - O paciente acessa a página de agendamento de consultas.
    - O sistema permite que o paciente selecione um médico da lista de médicos disponíveis (conforme descrito no requisito de busca por médicos).
    - Após selecionar um médico, o sistema exibe a agenda do médico com os horários disponíveis para consulta.
2. **Visualização da Agenda**:
    - A agenda do médico deve mostrar:
        - Datas disponíveis
        - Horários disponíveis para cada data
        - Horários já ocupados ou indisponíveis
3. **Seleção do Horário**:
    - O paciente seleciona uma data e um horário disponível na agenda do médico.
    - O sistema valida a disponibilidade do horário selecionado:
        - **Validação de Disponibilidade**: Verifica se o horário está realmente disponível e não foi reservado por outro paciente.
4. **Confirmação do Agendamento**:
    - Após a validação do horário, o paciente confirma o agendamento.
    - O sistema reserva o horário para o paciente e atualiza a agenda do médico para refletir o novo agendamento.
5. **Prevenção de Agendamento Duplo**:
    - O sistema deve impedir que o mesmo horário seja reservado por múltiplos pacientes.
    - Se o horário selecionado já estiver ocupado (por outro agendamento ou por outra alteração), o sistema deve informar o paciente e solicitar a seleção de um novo horário.
6. **Notificação de Confirmação**:
    - Após a confirmação do agendamento, o paciente recebe uma notificação (e-mail ou mensagem no sistema) confirmando a consulta.
    - O médico também deve ser notificado sobre o novo agendamento.

### Regras de Negócio

- **Exibição da Agenda**:
    - A agenda deve refletir com precisão os horários disponíveis e ocupados.
- **Validação de Disponibilidade**:
    - O sistema deve verificar a disponibilidade do horário em tempo real antes da confirmação do agendamento.
- **Prevenção de Agendamento Duplo**:
    - O sistema deve garantir que um horário não possa ser reservado simultaneamente por dois pacientes.
- **Notificações**:
    - Enviar confirmações de agendamento para o paciente e para o médico.

### Mensagens de Erro

- **Horário Já Ocupado**: "O horário selecionado não está mais disponível. Por favor, escolha outro horário."
- **Data ou Hora Inválida**: "A data ou horário selecionado é inválido. Por favor, selecione uma data e horário válidos."
- **Falha na Confirmação do Agendamento**: "Houve um problema ao confirmar o agendamento. Por favor, tente novamente."

### Considerações de Usabilidade

- **Interface de Agendamento**: A interface deve ser clara e intuitiva, permitindo que o paciente selecione datas e horários de forma fácil.
- **Feedback Imediato**: Fornecer feedback imediato ao paciente sobre a disponibilidade do horário e a confirmação do agendamento.
- **Atualização da Agenda**: A agenda deve ser atualizada em tempo real para refletir as alterações e prevenir conflitos.

## Conclusão

Esta documentação detalha os requisitos funcionais para o agendamento de consultas, garantindo que pacientes possam agendar consultas de forma eficiente, com visualização clara da agenda dos médicos e validação adequada da disponibilidade dos horários. Implementar essas funcionalidades garantirá um processo de agendamento sem problemas e uma experiência positiva para os usuários do sistema.

## Notificação de Consulta Marcada (Médico)

### Descrição

O sistema deve enviar uma notificação por e-mail para os médicos sempre que uma nova consulta for agendada. O e-mail deve conter informações relevantes sobre a consulta, incluindo o nome do paciente, a data e o horário agendado.

### Fluxo de Notificação

1. **Geração da Notificação**:
    - Quando uma consulta é confirmada e agendada, o sistema deve gerar uma notificação por e-mail para o médico responsável.
2. **Envio do E-mail de Notificação**:
    - O sistema deve enviar um e-mail para o endereço de e-mail cadastrado do médico.
    - O e-mail deve conter as seguintes informações:
        - **Título do E-mail**: "Health&Med - Nova consulta agendada"
        - **Corpo do E-mail**:
            
            ```css
            Olá, Dr. {nome_do_médico}!
            
            Você tem uma nova consulta marcada!
            
            Paciente: {nome_do_paciente}
            Data e horário: {data} às {horário_agendado}
            ```
            
3. **Informações no Corpo do E-mail**:
    - **Nome do Médico**: Substituído pelo nome do médico responsável pela consulta.
    - **Nome do Paciente**: Nome do paciente que agendou a consulta.
    - **Data e Horário**: Data e horário agendado para a consulta.
4. **Verificação de Envio**:
    - O sistema deve verificar se o e-mail foi enviado com sucesso.
    - Em caso de falha no envio, o sistema deve registrar um erro e tentar re-enviar o e-mail, se possível.

### Regras de Negócio

- **Título do E-mail**: Deve ser sempre "Health&Med - Nova consulta agendada".
- **Formato do Corpo do E-mail**: O corpo do e-mail deve seguir o formato especificado, substituindo os campos de marcador pelas informações reais.
- **Verificação de E-mail**: O e-mail deve ser enviado para o endereço cadastrado do médico.

### Mensagens de Erro

- **Falha no Envio de E-mail**: "Houve um problema ao enviar a notificação para o médico. Por favor, verifique o endereço de e-mail e tente novamente."

### Considerações de Usabilidade

- **Clareza das Informações**: O e-mail deve ser claro e conter todas as informações necessárias sobre a nova consulta.
- **Personalização**: As mensagens devem ser personalizadas com os nomes corretos do médico e do paciente para facilitar o entendimento.
- **Log de Envio**: O sistema deve manter um log das notificações enviadas para auditoria e resolução de problemas.

## Conclusão

Esta documentação detalha os requisitos funcionais para o envio de notificações por e-mail aos médicos quando uma nova consulta é agendada. Implementar essas funcionalidades garantirá que os médicos sejam informados de forma clara e eficiente sobre novos agendamentos, facilitando o gerenciamento de suas consultas.

## Requisitos Não Funcionais

## Concorrência de Agendamentos

### Descrição

O sistema deve ser capaz de gerenciar múltiplos acessos simultâneos e assegurar que apenas um agendamento possa ser feito para um determinado horário. Para garantir a integridade dos dados e evitar conflitos, o sistema utilizará mecanismos de controle de concorrência e validação de versões de linha.

### Controle de Concorrência

1. **Controle de Versão de Linha**:
    - O sistema implementará um mecanismo de controle de concorrência utilizando versões de linha para gerenciar atualizações de agendamentos.
    - Cada registro de horário disponível terá um número de versão associado que será atualizado sempre que o registro for modificado.
    - Quando um paciente tenta agendar uma consulta, o sistema verifica a versão atual do registro para garantir que o horário ainda esteja disponível.
2. **Verificação de Disponibilidade**:
    - Antes de confirmar um agendamento, o sistema realiza uma dupla verificação para garantir que o horário solicitado ainda está disponível.
    - O sistema comparará a versão do registro atual com a versão armazenada na tentativa de agendamento.
3. **Tratamento de Conflitos**:
    - Se a versão do registro do horário estiver desatualizada (indicando que o horário foi reservado por outro paciente), o sistema retornará uma exceção.
    - A exceção informará ao paciente que o horário já está ocupado e pedirá que selecione um novo horário.

### Regras de Negócio

- **Versão de Linha**: Cada horário disponível terá um número de versão que é incrementado em cada atualização.
- **Verificação de Versão**: O sistema deve comparar a versão do horário no momento da tentativa de agendamento com a versão armazenada para garantir que o horário ainda esteja livre.
- **Tratamento de Exceção**: Se a versão do horário estiver incorreta (ou seja, se o horário tiver sido reservado por outro paciente), o sistema deve informar ao usuário que o horário já está ocupado e permitir que ele tente um novo horário.

### Mensagens de Erro

- **Horário Ocupado**: "O horário selecionado não está mais disponível. Por favor, escolha outro horário."

### Considerações de Usabilidade

- **Feedback Imediato**: Fornecer feedback claro e imediato para o paciente se o horário desejado já tiver sido reservado.
- **Interface de Seleção de Horário**: Garantir que a interface de seleção de horário seja intuitiva e permita ao paciente rapidamente escolher um novo horário quando necessário.
- **Desempenho**: O sistema deve garantir que o controle de concorrência não afete negativamente o desempenho ou a responsividade do sistema.

## Conclusão

Esta documentação detalha os requisitos não funcionais relacionados à concorrência de agendamentos, garantindo que o sistema suporte múltiplos acessos simultâneos de forma eficiente e assegure que apenas um agendamento seja permitido para um horário específico. Implementar essas funcionalidades garantirá a integridade dos dados e uma experiência de agendamento sem conflitos para os usuários do sistema.

## Validação de Conflito de Horários

### Descrição

O sistema deve validar a disponibilidade dos horários em tempo real para evitar conflitos e sobreposições de consultas agendadas. Além disso, médicos devem ter a capacidade de alterar o status de sua agenda de disponível para indisponível conforme necessário.

### Validação em Tempo Real

1. **Verificação Imediata de Disponibilidade**:
    - Quando um paciente tenta agendar uma consulta, o sistema deve validar a disponibilidade do horário em tempo real.
    - O sistema consulta a base de dados para verificar se o horário solicitado está livre antes de confirmar o agendamento.
2. **Prevenção de Sobreposição**:
    - O sistema deve garantir que não haja sobreposição de horários para consultas agendadas.
    - Se um conflito de horário for detectado, o sistema deve impedir o agendamento e informar ao paciente sobre a indisponibilidade do horário.
3. **Resposta em Tempo Real**:
    - O sistema deve fornecer uma resposta imediata sobre a disponibilidade do horário, permitindo ao paciente tentar outro horário se o solicitado estiver ocupado.

### Alteração de Status da Agenda

1. **Alteração de Disponibilidade pelo Médico**:
    - Médicos devem poder alterar o status de sua agenda, marcando horários como disponíveis ou indisponíveis.
    - O sistema deve refletir imediatamente essas alterações para evitar conflitos com agendamentos futuros.
2. **Atualização em Tempo Real**:
    - Quando um médico altera o status de um horário, o sistema deve atualizar imediatamente a disponibilidade na base de dados.
    - Pacientes que tentarem agendar consultas durante esses horários indisponíveis devem ser informados da mudança de status.

### Regras de Negócio

- **Validação de Horário**:
    - A validação de disponibilidade deve ocorrer em tempo real, consultando a base de dados antes de confirmar um agendamento.
- **Alteração de Status**:
    - Médicos podem marcar horários como indisponíveis, e essas mudanças devem ser refletidas imediatamente para evitar novos agendamentos nesses horários.
- **Prevenção de Conflitos**:
    - O sistema deve garantir que um horário não possa ser reservado por mais de um paciente e que as mudanças na agenda do médico sejam respeitadas.

### Mensagens de Erro

- **Horário Ocupado**: "O horário selecionado não está mais disponível. Por favor, escolha outro horário."
- **Horário Indisponível**: "O horário selecionado foi marcado como indisponível pelo médico. Por favor, escolha outro horário."

### Considerações de Usabilidade

- **Feedback Imediato**: Fornecer feedback em tempo real ao paciente sobre a disponibilidade do horário.
- **Interface Intuitiva**: A interface deve permitir que médicos alterem o status da sua agenda de forma fácil e intuitiva.
- **Desempenho**: A validação em tempo real deve ser rápida e eficiente para não impactar negativamente a experiência do usuário.

## Conclusão

Esta documentação detalha os requisitos não funcionais relacionados à validação de conflito de horários, garantindo que o sistema valide a disponibilidade dos horários em tempo real e permita que médicos alterem o status de sua agenda de maneira eficiente. Implementar essas funcionalidades assegurará que não haja sobreposição de consultas e que a disponibilidade dos horários seja sempre precisa e atualizada.

## Exclusão de Médicos e Horários

### Descrição

O sistema deve gerenciar corretamente as exclusões de médicos e horários, assegurando que médicos com horários agendados não possam ser excluídos, permitindo a exclusão de horários e realizando exclusão em cascata dos horários relacionados quando uma agenda é excluída. Além disso, médicos devem poder alterar o status de um horário para disponível e limpar a relação com o paciente, se houver.

### Prevenção de Exclusão de Médicos

1. **Validação de Relação com Agenda**:
    - O sistema deve prevenir a exclusão de médicos que possuem horários agendados em sua agenda.
    - Antes de permitir a exclusão de um médico, o sistema verifica se há horários futuros agendados.
2. **Mensagem de Bloqueio**:
    - Se o médico possuir horários agendados, o sistema deve bloquear a exclusão e informar ao usuário.
    - Mensagem: "Não é possível excluir o médico, pois há consultas agendadas em sua agenda."

### Exclusão de Horários

1. **Permissão para Exclusão de Horários**:
    - O sistema deve permitir a exclusão de horários específicos na agenda de um médico.
    - Médicos podem selecionar horários específicos para excluir.
2. **Exclusão de Agenda e Horários em Cascata**:
    - Quando uma agenda é excluída, todos os horários relacionados devem ser excluídos em cascata.
    - Esta exclusão deve ser automática e garantir que não haja horários órfãos na base de dados.
3. **Limpeza de Relação com Paciente**:
    - Médicos devem poder alterar o status de um horário para disponível, removendo qualquer relação existente com um paciente.
    - Esta ação deve liberar o horário para novos agendamentos e informar ao paciente sobre a alteração, se necessário.

### Regras de Negócio

- **Prevenção de Exclusão de Médicos**:
    - Um médico não pode ser excluído se houver horários futuros agendados em sua agenda.
- **Permissão de Exclusão de Horários**:
    - Horários podem ser excluídos individualmente.
- **Exclusão em Cascata**:
    - A exclusão de uma agenda deve resultar na exclusão automática de todos os horários relacionados.
- **Alteração de Status de Horários**:
    - Médicos podem alterar o status de um horário para disponível, limpando a relação com o paciente.

### Mensagens de Erro

- **Médico com Horários Agendados**: "Não é possível excluir o médico, pois há consultas agendadas em sua agenda."
- **Falha na Exclusão de Horário**: "Houve um problema ao excluir o horário. Por favor, tente novamente."

### Considerações de Usabilidade

- **Feedback Claro**: Fornecer mensagens claras ao usuário sobre o motivo pelo qual a exclusão não pôde ser realizada.
- **Interface Intuitiva**: A interface deve facilitar a seleção e exclusão de horários específicos e a alteração de status para disponível.
- **Desempenho**: A exclusão em cascata deve ser eficiente para não impactar negativamente a performance do sistema.

## Conclusão

Esta documentação detalha os requisitos não funcionais relacionados à exclusão de médicos e horários, garantindo que o sistema gerencie corretamente as exclusões e mantenha a integridade dos dados. Implementar essas funcionalidades assegurará que médicos com horários agendados não possam ser excluídos inadvertidamente, que horários possam ser excluídos individualmente e que a exclusão de uma agenda remova todos os horários relacionados de forma eficiente.

---

Esperamos que este documento tenha sido útil para entender as funcionalidades e requisitos do sistema. Se tiver alguma dúvida ou sugestão, entre em contato conosco.

Grupo 11 - FIAP PÓS TECH

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Exemplificamos abaixo também o Diagrama do Banco de Dados criado para esta solução:

![image](https://github.com/user-attachments/assets/1d0f9db5-6625-493c-81ba-5f5821613484)


Agradecemos mais uma vez o conteúdo transmitido através deste curso de pós gradução.
