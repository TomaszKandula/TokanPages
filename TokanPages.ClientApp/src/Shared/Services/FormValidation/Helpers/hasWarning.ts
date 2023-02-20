export const HasWarning = (result: any): boolean => 
{
    return result !== undefined && result["password"] !== undefined;
}
