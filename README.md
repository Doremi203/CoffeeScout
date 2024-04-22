# CoffeeScout

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## Покрытие тестами
![Coverage Report](https://ibb.co/jv5frpz)

## Описание

CoffeeScout - это мобильное приложение, которое позволяет пользователям находить кофейни в Москве, заказывать любимый кофе, а также оставлять отзывы и оценки о них.

## Содержание

- [Развертывание](#развертывание)
- [Руководство по использованию](#руководство-по-использованию)
- [Contributing](#contributing)
- [License](#license)

## Развертывание

Для развертывания проекта необходимо выполнить следующие шаги:

1. Скачать docker-compose.yml и nginx.conf из репозитория.
2. Создать рядом с docker-compose.yml файл .env и заполнить его следующим образом своими данными:

```
DATABASESETTINGS__USERID=postgres
DATABASESETTINGS__PASSWORD=cffout
DATABASESETTINGS__DATABASE=coffee-scout
DATABASESETTINGS__HOST=database
DATABASESETTINGS__OPTIONS=Port=5432;Pooling=true;
ADMINSETTINGS__EMAIL=admin@gmail.com
ADMINSETTINGS__PASSWORD=AdminSuper!2
MAILERSENDSETTINGS__APITOKEN=mlsn.676ce4c3ba1dc336d6c2bf0d4f1f834a05a8011683c124d18cefb1d05fea43e8
```
3. Запустить docker-compose:

```bash
docker-compose up -d
```
Готово! Backend и Web часть приложения доступны по адресу http://localhost на локальной машине.

## Руководство по использованию

Инструкция по использованию Backend'а.

Обновление backend'а:

```bash
docker-compose pull backend
```

Обновление Web'а:

```bash
docker-compose pull frontend
```

Посмотреть логи:

```bash
docker-compose logs backend
```

## Contributing

If you want to contribute to this project and make it better, your help is very welcome. Create a pull request with your recommended changes.
We will review your changes and apply them to the master branch if they are approved.

## License

This project is licensed under the [MIT License](LICENSE).
