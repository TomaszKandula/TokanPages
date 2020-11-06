function IsEmpty(AValue: string) 
{
    return typeof !AValue.trim() || typeof AValue === undefined || AValue === null;        
}

export 
{
    IsEmpty
}
