import { Login } from "./login.dto";

export interface Register extends Login
{
    email: string
}