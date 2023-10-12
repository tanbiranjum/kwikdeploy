import UserLoginForm from "./user-login-form";
import React from "react";

type Props = {};

const LoginPage = (props: Props) => {
  return (
    <main className="min-h-screen p-24">
      <div className="max-w-lg mx-auto space-y-6">
        <div className="flex flex-col space-y-2 text-center">
          <h1 className="text-2xl font-semibold tracking-tight">
            Login to app
          </h1>
          <p className="text-sm text-muted-foreground">
            Enter your email and password to login
          </p>
        </div>
        <UserLoginForm />
      </div>
    </main>
  );
};

export default LoginPage;
