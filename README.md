Клонировать репозиторий:
```
git clone https://github.com/artem777764/DefectsManagmentProject
```

Для запуска бэкенда:
```
cd Backend
dotnet run
```

Для запуска фронтенда:
```
cd Frontend
npm install
npm run dev
```

Также в папке Backend нужно создать .env файл со следующей структурой
```
ApplicationDatabaseConnection=postgresql_connection_string
JwtSettings__SecretKey=secret_key_for_jwt
JwtSettings__Issuer=issuer
JwtSettings__Audience=audience
```

Проект работает на 2 портах:
```
Порт 5234: Бэкенд
Порт 5173: Фронтенд
```
