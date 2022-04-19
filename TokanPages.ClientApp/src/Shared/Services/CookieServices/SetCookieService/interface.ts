export interface ISetCookie 
{
    cookieName: string, 
    value: string, 
    days: number, 
    sameSite: string, 
    secure: boolean,
    exact?: string    
}
