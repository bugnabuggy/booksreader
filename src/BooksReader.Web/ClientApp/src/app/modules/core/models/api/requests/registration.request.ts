export interface RegistrationRequest {
    username: string;
    fullname: string;
    password: string;
    antiforgeryKey?: string;
}
