# WTelegramChatUserBot-re-factored

---

- Simple Project - https://github.com/russianowner/WChatUserBot-simple

---
- Бот авторизуется под аккаунтом телеги и отвечает пользователям в заданном стиле, используя LLM-модель от Together .(Можно использовать не только Together, но и другие Апи)
- Ссылка на Together - https://www.together.ai/
- Для получения API_ID и API_HASH нужно создать приложение в телеге - https://my.telegram.org/apps

---

- The bot logs in under the cart account and responds to users in a preset style using the LLM model from Together.(You can use not only Together, but also other APIs)
- Link to Together - https://www.together.ai/
- To get the API_ID and API_HASH, you need to create an application in the cart - https://my.telegram.org/apps

---

# Что делает бот:

- Авторизуется через WTelegramClient
- Получает последние сообщения от списка пользователей
- Отправляет их в Together API
- Генерирует ответ и пишет в чат с пользователем
- Сохраняет историю диалога для контекста
- Работает в бесконечном цикле с таймером

---

# What does the bot do:

- Logged in via WTelegramClient
- Receives the latest messages from a list of users
- Sends them to the Together API
- Generates a response and writes a chat with the user
- Saves the dialog history for context
- Works in an endless loop with a timer

---

# Что используется/What is used:
- dotnet add package WTelegramClient
- dotnet add package Microsoft.Extensions.Configuration
- dotnet add package Microsoft.Extensions.Configuration.Json
- dotnet add package System.Text.Json
- dotnet add package Microsoft.Extensions.Configuration.Binder
- Конфигурация через appsettings.json / Configuration via appsettings.json

---

# Как запустить:

- Скопируй репозиторий
- Заходи в appsetting.json (зайди в свойства и сделай чтобы он постоянно копировался в выходной каталог)
- Меняй API_ID, API_HASH, PHONE, API_KEY на свои и юзернеймы без @ которым бот будет отвечать
- В TogetherService.cs - меняй модель, температуру, макс. длину ответа в методе GetReplyAsync
- В HistoryService.cs - методе BuildPrompt меняй шаблон запроса и стиль общения
- Запускай проект, логинься и бот начнет работать

---

# How to launch:

- Copy the repository
- Go to appsetting.json (go to the properties and make it constantly copied to the output directory)
- Change API_ID, API_HASH, PHONE, API_KEY to your own and usernames without @ to which the bot will respond
- In TogetherService.cs - change the model, temperature, max. response length in the GetReplyAsync method
- Change the request template and communication style in the HistoryService.cs BuildPrompt method
- Launch the project, log in and the bot will start working

---
