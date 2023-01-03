# Digital Wallet API

- [Digital Wallet API](#digital-wallet-api)
  - [Auth](#auth)
    - [Sign Up](#sign-up)
      - [Sign Up Request](#sign-up-request)
      - [Sign Up Response](#sign-up-response)
    - [Sign In](#sign-in)
      - [Sign In Request](#sign-in-request)
      - [Sign In Response](#sign-in-response)

## Authentication

### Sign-up

#### Sign-up Request

```js
POST {{host}}/authentication/signUp
```

```json
{
  "FirstName": "Dawid",
  "LastName": "Kowalski",
  "UserName": "DKowalski",
  "Email": "dawidkowalski@domail.com",
  "PhoneNumber": "726836917",
  "Password": "SomeHardP@$w0rd",
  "ConfirmPassword": "SomeHardP@$w0rd"
}
```

#### Sign-up Response

```js
201 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "firstName": "Dawid",
  "lastName": "Kowalski",
  "userName": "DKowalski",
  "email": "dawidkowalski@domail.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```

### Sign-in

#### Sign-In Request

```js
POST {{host}}/authentication/signIn
```

```json
{
  "UserName": "dawidkowalski@domain.com",
  "Password": "SomeHardP@$w0rd"
}
```

#### Sign-in Response

```js
200 Ok
```

```json
{
  "Id": "00000000-0000-0000-0000-000000000000",
  "FirstName": "Dawid",
  "LastName": "Kowalski",
  "UserName": "DKowalski",
  "Email": "dawidkowalski@domail.com",
  "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```
