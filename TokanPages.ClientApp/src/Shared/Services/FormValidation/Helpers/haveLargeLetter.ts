export const HaveLargeLetter = (value: string): boolean => 
{
    for (let index: number = 0; index < value.length; index++) 
    {
        let charCode = value.charCodeAt(index);
        if (charCode >= 65 && charCode <= 90)
        {
            return true;
        }
    }

    return false;
}
