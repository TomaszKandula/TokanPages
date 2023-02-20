export const HaveSmallLetter = (value: string): boolean => 
{
    for (let index: number = 0; index < value.length; index++) 
    {
        let charCode = value.charCodeAt(index);
        if (charCode >= 97 && charCode <= 122)
        {
            return true;
        }
    }

    return false;
}
