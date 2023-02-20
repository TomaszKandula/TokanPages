export const HasWarning = (result: any, property: string): boolean => 
{
    return result && result[property];
}
