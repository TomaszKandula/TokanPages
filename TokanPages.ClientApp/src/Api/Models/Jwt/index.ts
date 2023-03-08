export interface JWT
{
    unique_name: string;
    nameid: string;
    given_name: string;
    family_name: string;
    email: string;
    role: [ ],
    nbf: number;
    exp: number;
    iat: number;
    iss: string;
    aud: string;
}
