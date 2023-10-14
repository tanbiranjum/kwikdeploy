# KwikDeploy Frontend

## How to run locally

Create a copy of .env.example as .env.local

Then start the application by running:

    npm install
    npm run dev

Login with default credentials - User: admin, Password: Password123!

The default config points to the REST API at api.kwikdeploy.com so that frontend developers need not setup
the backend locally.

However, if you want to run the backend locally, follow the README in the backend folder. Then update
your .env.local to point to the local backend API.
