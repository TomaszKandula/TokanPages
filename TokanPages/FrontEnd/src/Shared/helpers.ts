function IsEmpty(value: any): boolean 
{

    if (value === null 
        || value === undefined 
        || (value.length !== undefined && value.length === 0) 
        || Object.keys(value).length === 0) 
    {
        return true;
    }

    return false;

}

export 
{
    IsEmpty
}
