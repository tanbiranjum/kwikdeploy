import UserLoginForm from "./user-login-form";
import React from "react";

type Props = {};

const LoginPage = (props: Props) => {
  return (
    <main className="min-h-screen p-24">
      <div className="max-w-lg mx-auto space-y-6">
        <div className="flex flex-col space-y-2 text-center">
          <h1 className="text-2xl font-semibold tracking-tight">
            KwikDeploy Login
          </h1>
        </div>
        <UserLoginForm />
      </div>
    </main>
  );
};

export default LoginPage;
